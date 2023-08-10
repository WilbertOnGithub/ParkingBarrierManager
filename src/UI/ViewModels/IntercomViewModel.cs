using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Arentheym.ParkingBarrier.UI.ViewModels;

/// <summary>
/// Represents the intercom to which a <see cref="ApartmentConfigurationViewModel"/>
/// is linked.
/// </summary>
public partial class IntercomViewModel : ObservableObject
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
}