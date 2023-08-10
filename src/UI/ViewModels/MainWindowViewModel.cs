using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Arentheym.ParkingBarrier.Application;

namespace Arentheym.ParkingBarrier.UI.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private readonly DataService dataService;
    private readonly Task initialisationTask;

    public ObservableCollection<ApartmentConfigurationViewModel> Configurations { get; } = new ObservableCollection<ApartmentConfigurationViewModel>();


    [SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "Dependency injection")]
    public MainWindowViewModel(DataService dataService)
    {
        this.dataService = dataService;
        this.initialisationTask = InitializeAsync();
    }

    private async Task InitializeAsync()
    {
        var intercoms = await dataService.GetIntercoms().ConfigureAwait(false);
        foreach (var domainEntity in await dataService.GetApartmentConfigurations().ConfigureAwait(false))
        {
            Configurations.Add(ManualMapper.EntityToViewModel(domainEntity, intercoms));
        }
    }
}