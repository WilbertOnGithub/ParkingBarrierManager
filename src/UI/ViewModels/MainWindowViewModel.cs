using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Arentheym.ParkingBarrier.Application;
using Arentheym.ParkingBarrier.Domain;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Arentheym.ParkingBarrier.UI.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private readonly DataService dataService;
    private readonly Task initialisationTask;
    private IEnumerable<Intercom> availableIntercoms = Enumerable.Empty<Intercom>();

    [ObservableProperty]
    private bool buttonEnabled;

    [ObservableProperty]
    private int numberOfDirtyConfigurations;

    public ObservableCollection<ApartmentConfigurationViewModel> Configurations { get; } = new ObservableCollection<ApartmentConfigurationViewModel>();

    [SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "Dependency injection")]
    public MainWindowViewModel(DataService dataService)
    {
        this.dataService = dataService;
        initialisationTask = InitializeAsync();

        foreach (var configurationViewModel in Configurations)
        {
            foreach (var intercomViewModel in configurationViewModel.Intercoms)
            {
                intercomViewModel.PropertyChanged += OnConfigurationChanged;
            }
            configurationViewModel.PropertyChanged += OnConfigurationChanged;
        }
    }

    private void OnConfigurationChanged(object? sender, PropertyChangedEventArgs args)
    {
        ButtonEnabled = Configurations.Any(x => x.IsDirty);
        NumberOfDirtyConfigurations = Configurations.Count(x => x.IsDirty);
    }

    private async Task InitializeAsync()
    {
        availableIntercoms = await dataService.GetIntercoms();
        foreach (var domainEntity in await dataService.GetApartmentConfigurations())
        {
            Configurations.Add(ManualMapper.EntityToViewModel(domainEntity, availableIntercoms.ToList()));
            Configurations.Last().SetOriginal();
        }
    }

    [RelayCommand]
    public async Task SaveConfigurationsAsync()
    {
        var dirtyConfigurations = Configurations.Where(x => x.IsDirty).ToList();
        await dataService.SaveApartmentConfigurations(
            dirtyConfigurations.Select(x => ManualMapper.ViewModelToEntity(x, availableIntercoms.ToList())).ToList());

        // Set original for all dirty configurations so that they are no longer dirty.
        dirtyConfigurations.ForEach(x => x.SetOriginal());
    }
}