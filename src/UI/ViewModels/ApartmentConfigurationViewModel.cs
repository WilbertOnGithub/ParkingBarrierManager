using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using Arentheym.ParkingBarrier.UI.Validators;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Arentheym.ParkingBarrier.UI.ViewModels;

public partial class ApartmentConfigurationViewModel : ObservableValidator
{
    [ObservableProperty]
    [PhoneNumber]
    private string primaryPhoneNumber;

    [ObservableProperty]
    [PhoneNumber]
    private string secondaryPhoneNumber;

    [ObservableProperty]
    [PhoneNumber]
    private string tertiaryPhoneNumber;

    [ObservableProperty]
    [PhoneNumber]
    private string quaternaryPhoneNumber;

    [ObservableProperty]
    [AccessCode]
    private string accessCode;

    [ObservableProperty]
    private bool dialToOpen;

    [ObservableProperty]
    [Required]
    [MaxLength(16)]
    private string displayName;

    private int apartmentNumber;
    public int ApartmentNumber
    {
        get => apartmentNumber;
        init => SetProperty(ref apartmentNumber, value);
    }

    public ObservableCollection<IntercomViewModel> Intercoms { get; } = new ObservableCollection<IntercomViewModel>();

    public ApartmentConfigurationViewModel()
    {
        primaryPhoneNumber = string.Empty;
        secondaryPhoneNumber = string.Empty;
        tertiaryPhoneNumber = string.Empty;
        quaternaryPhoneNumber = string.Empty;
        accessCode = string.Empty;
        displayName = string.Empty;
        dialToOpen = false;

        ApartmentNumber = 1;
    }
}