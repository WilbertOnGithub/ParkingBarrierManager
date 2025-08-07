using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Arentheym.ParkingBarrier.Application;
using Arentheym.ParkingBarrier.Domain;
using Arentheym.ParkingBarrier.UI.Messages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Drawing.Geometries;

namespace Arentheym.ParkingBarrier.UI.ViewModels;

public partial class MainViewModel : ViewModelBase, IAsyncInitialization
{
    private readonly SmsGatewayService smsGatewayService;
    private readonly DataService dataService;
    private IEnumerable<Intercom> availableIntercoms = [];

    public bool IsDirty => Configurations.Any(x => x.IsDirty);

#pragma warning disable CA1819
    public ISeries[] Series { get; set; } =
    [
#pragma warning restore CA1819
        new ColumnSeries<int>(3, 4, 2),
        new ColumnSeries<int>(4, 2, 6),
        new ColumnSeries<double, DiamondGeometry>(4, 3, 4),
    ];

    public ObservableCollection<ApartmentConfigurationViewModel> Configurations { get; } = new();

    public Task Initialization { get; }

    [SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "Dependency injection")]
    public MainViewModel(DataService dataService, SmsGatewayService smsGatewayService)
    {
        this.dataService = dataService;
        this.smsGatewayService = smsGatewayService;

        Initialization = InitializeAsync();
    }

    private void OnConfigurationChanged(object? sender, PropertyChangedEventArgs args)
    {
        OnPropertyChanged(nameof(IsDirty));
    }

    private async Task InitializeAsync()
    {
        availableIntercoms = await dataService.GetIntercoms();
        foreach (var domainEntity in await dataService.GetApartmentConfigurations())
        {
            Configurations.Add(ManualMapper.EntityToViewModel(domainEntity, availableIntercoms.ToList()));
            Configurations.Last().SetOriginal();
        }

        foreach (var configurationViewModel in Configurations)
        {
            foreach (var intercomViewModel in configurationViewModel.Intercoms)
            {
                intercomViewModel.PropertyChanged += OnConfigurationChanged;
            }
            configurationViewModel.PropertyChanged += OnConfigurationChanged;
        }

        OnPropertyChanged(nameof(IsDirty));
    }

    /// <summary>
    /// Save all dirty configurations to the database
    /// </summary>
    public async Task SaveConfigurationsAsync()
    {
        List<ApartmentConfiguration> toBeUpdatedApartmentConfigurations = GetDirtyConfigurations();
        await dataService.SaveApartmentConfigurations(toBeUpdatedApartmentConfigurations);

        ResetDirtyConfigurations();
        OnPropertyChanged(nameof(IsDirty));
    }

    public async Task UpdateConfigurationBySMS()
    {
        List<ApartmentConfiguration> toBeUpdatedApartmentConfigurations = GetDirtyConfigurations();
        foreach (var toBeUpdatedApartmentConfiguration in toBeUpdatedApartmentConfigurations)
        {
            List<string> toBeLogged = await smsGatewayService.UpdateApartmentConfiguration(
                toBeUpdatedApartmentConfiguration
            );
            foreach (var message in toBeLogged)
            {
                WeakReferenceMessenger.Default.Send(
                    new LogEntryAdded(new LogMessage { Message = message, Timestamp = DateTime.Now })
                );
            }
        }
    }

    private List<ApartmentConfiguration> GetDirtyConfigurations()
    {
        List<ApartmentConfigurationViewModel> toBeUpdatedViewModels = Configurations.Where(x => x.IsDirty).ToList();
        List<ApartmentConfiguration> toBeUpdatedApartmentConfigurations = toBeUpdatedViewModels
            .Select(x => ManualMapper.ViewModelToEntity(x, availableIntercoms.ToList()))
            .ToList();

        return toBeUpdatedApartmentConfigurations;
    }

    private void ResetDirtyConfigurations()
    {
        foreach (var dirty in Configurations.Where(x => x.IsDirty))
        {
            dirty.SetOriginal();
        }
    }
}
