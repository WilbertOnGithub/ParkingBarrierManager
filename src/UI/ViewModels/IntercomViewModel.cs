using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Arentheym.ParkingBarrier.UI.ViewModels;

/// <summary>
/// Represents the intercom to which a <see cref="ApartmentConfigurationViewModel"/>
/// is linked.
/// </summary>
public partial class IntercomViewModel : ObservableObject, IEquatable<IntercomViewModel>
{
    public Guid Id { get; init; }

    [ObservableProperty]
    private bool isUsed;

    private string name = String.Empty;

    public string Name
    {
        get => name;
        init => SetProperty(ref name, value);
    }

    public bool Equals(IntercomViewModel? other)
    {
        if (ReferenceEquals(null, other))
            return false;
        return IsUsed == other.IsUsed
            && string.Equals(Name, other.Name, StringComparison.OrdinalIgnoreCase)
            && Id.Equals(other.Id);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj))
            return false;
        if (obj.GetType() != GetType())
            return false;
        return Equals((IntercomViewModel)obj);
    }

    public override int GetHashCode()
    {
        var hashCode = new HashCode();
        hashCode.Add(IsUsed);
        hashCode.Add(Name, StringComparer.InvariantCultureIgnoreCase);
        hashCode.Add(Id);
        return hashCode.ToHashCode();
    }
}
