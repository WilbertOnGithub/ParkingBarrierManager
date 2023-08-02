using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;

namespace Arentheym.ParkingBarrier.UI.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public ObservableCollection<ApartmentConfigurationViewModel> Configurations { get; } = new ObservableCollection<ApartmentConfigurationViewModel>();

    public MainWindowViewModel()
    {
        Configurations.Add(new ApartmentConfigurationViewModel()
        {
            ApartmentNumber = 131,
            DisplayName = "Wilbert",
            AccessCode = "1234",
            DialToOpen = true,
            PrimaryPhoneNumber = "0613739851"
        });
    }

    [SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Needed for binding")]
    public string Greeting => "Welcome to Avalonia!";
}