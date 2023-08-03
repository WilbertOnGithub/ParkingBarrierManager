using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;

namespace Arentheym.ParkingBarrier.UI.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public ObservableCollection<ApartmentConfigurationViewModel> Configurations { get; } = new ObservableCollection<ApartmentConfigurationViewModel>();

    // TODO: Add code to retrieve configurations from application layer

    public MainWindowViewModel()
    {
        Configurations.Add(new ApartmentConfigurationViewModel()
        {
            ApartmentNumber = 131,
            DisplayName = "Wilbert",
            AccessCode = "1234",
            DialToOpen = true,
            PrimaryPhoneNumber = "0613739851",
            Intercoms = { new IntercomViewModel()
                {
                    Id = 1,
                    Name = "Voor",
                    IsUsed = true
                },
                new IntercomViewModel()
                {
                    Id =2,
                    Name = "Achter",
                    IsUsed = false
                }
            }

        });
    }
}