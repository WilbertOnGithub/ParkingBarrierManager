using Arentheym.ParkingBarrier.UI.ViewModels;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Arentheym.ParkingBarrier.UI.Messages;

/// <summary>
/// Message that will be sent when we want to refresh the display for the
/// remaining credits remaining for the SMS service.
/// </summary>
public class RefreshRemainingCreditsMessage : ValueChangedMessage<MainViewModel>
{
    public RefreshRemainingCreditsMessage(MainViewModel vm) : base(vm)
    {
    }
}