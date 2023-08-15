using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Arentheym.ParkingBarrier.Application;
using Arentheym.ParkingBarrier.Domain;
using CommunityToolkit.Mvvm.Input;

namespace Arentheym.ParkingBarrier.UI.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private readonly DataService dataService;
    private readonly Task initialisationTask;
    private IEnumerable<Intercom> availableIntercoms = Enumerable.Empty<Intercom>();

    public ObservableCollection<ApartmentConfigurationViewModel> Configurations { get; } = new ObservableCollection<ApartmentConfigurationViewModel>();

    [SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "Dependency injection")]
    public MainWindowViewModel(DataService dataService)
    {
        this.dataService = dataService;
        initialisationTask = InitializeAsync();
    }

    private async Task InitializeAsync()
    {
        availableIntercoms = await dataService.GetIntercoms().ConfigureAwait(false);
        foreach (var domainEntity in await dataService.GetApartmentConfigurations().ConfigureAwait(false))
        {
            Configurations.Add(ManualMapper.EntityToViewModel(domainEntity, availableIntercoms));
        }
    }

    [RelayCommand]
    public async Task SaveConfigurationsAsync()
    {
        await dataService.SaveApartmentConfigurations(Configurations.Select(x => ManualMapper.ViewModelToEntity(x, availableIntercoms)).ToList()).ConfigureAwait(false);
    }
}