using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Arentheym.ParkingBarrier.UI.Extensions;
using Arentheym.ParkingBarrier.UI.Validators;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Arentheym.ParkingBarrier.UI.ViewModels;

public partial class ApartmentConfigurationViewModel : ObservableValidator, IEquatable<ApartmentConfigurationViewModel>
{
    private ApartmentConfigurationViewModel? original;

    public bool IsDirty
    {
        get
        {
            if (original is null)
            {
                throw new InvalidOperationException(
                    $"Original has not been set. Call method {nameof(SetOriginal)} first."
                );
            }
            return !Equals(original);
        }
    }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsDirty))]
    [NotifyDataErrorInfo]
    [PhoneNumber]
    private string primaryPhoneNumber = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsDirty))]
    [NotifyDataErrorInfo]
    [PhoneNumber]
    private string secondaryPhoneNumber = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsDirty))]
    [NotifyDataErrorInfo]
    [PhoneNumber]
    private string tertiaryPhoneNumber = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsDirty))]
    [NotifyDataErrorInfo]
    [PhoneNumber]
    private string quaternaryPhoneNumber = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsDirty))]
    [NotifyDataErrorInfo]
    [AccessCode]
    private string accessCode = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsDirty))]
    private bool dialToOpen;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsDirty))]
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

    public ObservableCollection<IntercomViewModel> Intercoms { get; init; } = new();

    /// <summary>
    /// Store original values to compare if values have changed. <seealso cref="IsDirty"/>
    /// </summary>
    public void SetOriginal()
    {
        original = this.Copy();
    }

    public bool Equals(ApartmentConfigurationViewModel? other)
    {
        if (ReferenceEquals(null, other))
            return false;
        if (ReferenceEquals(this, other))
            return true;
        return string.Equals(PrimaryPhoneNumber, other.PrimaryPhoneNumber, StringComparison.OrdinalIgnoreCase)
            && string.Equals(SecondaryPhoneNumber, other.SecondaryPhoneNumber, StringComparison.OrdinalIgnoreCase)
            && string.Equals(TertiaryPhoneNumber, other.TertiaryPhoneNumber, StringComparison.OrdinalIgnoreCase)
            && string.Equals(QuaternaryPhoneNumber, other.QuaternaryPhoneNumber, StringComparison.OrdinalIgnoreCase)
            && string.Equals(AccessCode, other.AccessCode, StringComparison.OrdinalIgnoreCase)
            && DialToOpen == other.DialToOpen
            && string.Equals(DisplayName, other.DisplayName, StringComparison.OrdinalIgnoreCase)
            && ApartmentNumber == other.ApartmentNumber
            && Intercoms.SequenceEqual(other.Intercoms);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj))
            return false;
        if (ReferenceEquals(this, obj))
            return true;
        if (obj.GetType() != GetType())
            return false;
        return Equals((ApartmentConfigurationViewModel)obj);
    }

    public override int GetHashCode()
    {
        var hashCode = new HashCode();
        hashCode.Add(PrimaryPhoneNumber, StringComparer.OrdinalIgnoreCase);
        hashCode.Add(SecondaryPhoneNumber, StringComparer.OrdinalIgnoreCase);
        hashCode.Add(TertiaryPhoneNumber, StringComparer.OrdinalIgnoreCase);
        hashCode.Add(QuaternaryPhoneNumber, StringComparer.OrdinalIgnoreCase);
        hashCode.Add(AccessCode, StringComparer.OrdinalIgnoreCase);
        hashCode.Add(DialToOpen);
        hashCode.Add(DisplayName, StringComparer.OrdinalIgnoreCase);
        hashCode.Add(ApartmentNumber);
        hashCode.Add(Intercoms);
        return hashCode.ToHashCode();
    }
}
