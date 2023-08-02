using System.ComponentModel.DataAnnotations;
using Arentheym.ParkingBarrier.UI.Validators;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Arentheym.ParkingBarrier.UI.ViewModels;

public partial class ApartmentConfigurationViewModel : ObservableValidator
{
    [ObservableProperty]
    private string primaryPhoneNumber;

    [ObservableProperty]
    private string secondaryPhoneNumber;

    [ObservableProperty]
    private string tertiaryPhoneNumber;

    [ObservableProperty]
    private string quaternaryPhoneNumber;

    [AccessCode]
    private string accessCode;

    [ObservableProperty]
    private bool dialToOpen;

    [Required]
    [MaxLength(16)]
    private string displayName;

    public int ApartmentNumber { get; init; }

    public ApartmentConfigurationViewModel()
    {
        primaryPhoneNumber = string.Empty;
        secondaryPhoneNumber = string.Empty;
        tertiaryPhoneNumber = string.Empty;
        quaternaryPhoneNumber = string.Empty;
        accessCode = string.Empty;
        displayName = string.Empty;
        dialToOpen = false;
    }
}