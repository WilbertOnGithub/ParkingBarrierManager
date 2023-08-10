using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using Arentheym.ParkingBarrier.UI.Validators;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Arentheym.ParkingBarrier.UI.ViewModels;

public partial class ApartmentConfigurationViewModel : ObservableValidator
{
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [PhoneNumber]
    private string primaryPhoneNumber;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [PhoneNumber]
    private string secondaryPhoneNumber;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [PhoneNumber]
    private string tertiaryPhoneNumber;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [PhoneNumber]
    private string quaternaryPhoneNumber;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [AccessCode]
    private string accessCode;

    [ObservableProperty]
    private bool dialToOpen;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required]
    [MaxLength(16)]
    private string displayName;

    private string apartmentNumber;
    public string ApartmentNumber
    {
        get => apartmentNumber;
        init => SetProperty(ref apartmentNumber, value);
    }

    public ObservableCollection<IntercomViewModel> Intercoms { get; init; } = new ObservableCollection<IntercomViewModel>();

    public ApartmentConfigurationViewModel()
    {
        primaryPhoneNumber = string.Empty;
        secondaryPhoneNumber = string.Empty;
        tertiaryPhoneNumber = string.Empty;
        quaternaryPhoneNumber = string.Empty;
        accessCode = string.Empty;
        displayName = string.Empty;
        dialToOpen = false;

        apartmentNumber = "1";
    }
}