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

using CommunityToolkit.Mvvm.Messaging;

namespace Arentheym.ParkingBarrier.UI.ViewModels;

public partial class MainViewModel : ViewModelBase, IAsyncInitialization
{
    private readonly SmsGatewayService smsGatewayService;
    private readonly DataService dataService;
    private IEnumerable<Intercom> availableIntercoms = [];

    public bool IsDirty => Configurations.Any(x => x.IsDirty);

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
    public async Task UpdateConfigurationsAsync()
    {
        List<ApartmentConfigurationViewModel> toBeUpdatedViewModels = Configurations.Where(x => x.IsDirty).ToList();
        List<ApartmentConfiguration> toBeUpdatedApartmentConfigurations = toBeUpdatedViewModels.Select(x => ManualMapper.ViewModelToEntity(x, availableIntercoms.ToList()))
            .ToList();

        foreach (var toBeUpdatedApartmentConfiguration in toBeUpdatedApartmentConfigurations)
         {
            List<string> toBeLogged = await smsGatewayService.UpdateApartmentConfiguration(toBeUpdatedApartmentConfiguration);
            foreach (var message in toBeLogged)
            {
                WeakReferenceMessenger.Default.Send(new LogEntryAdded(new LogMessage { Message = message, Timestamp = DateTime.Now }));
            }
        }
        await dataService.SaveApartmentConfigurations(toBeUpdatedApartmentConfigurations);

        // Set original for all dirty configurations so that they are no longer considered dirty.
        toBeUpdatedViewModels.ForEach(x => x.SetOriginal());
        OnPropertyChanged(nameof(IsDirty));
    }
}
