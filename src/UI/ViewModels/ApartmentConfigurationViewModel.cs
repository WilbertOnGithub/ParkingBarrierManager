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
    private string primaryPhoneNumber = string.Empty;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [PhoneNumber]
    private string secondaryPhoneNumber = string.Empty;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [PhoneNumber]
    private string tertiaryPhoneNumber = string.Empty;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [PhoneNumber]
    private string quaternaryPhoneNumber = string.Empty;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [AccessCode]
    private string accessCode = string.Empty;

    [ObservableProperty]
    private bool dialToOpen;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required]
    [MaxLength(16)]
    private string displayName = string.Empty;

    private int apartmentNumber;
    public int ApartmentNumber
    {
        get => apartmentNumber;
        init => SetProperty(ref apartmentNumber, value);
    }

    public ObservableCollection<IntercomViewModel> Intercoms { get; init; } = new ObservableCollection<IntercomViewModel>();
}