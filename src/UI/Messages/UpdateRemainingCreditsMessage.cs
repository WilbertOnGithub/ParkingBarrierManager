using Arentheym.ParkingBarrier.UI.ViewModels;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Arentheym.ParkingBarrier.UI.Messages;

/// <summary>
/// Message that will be sent when we want to update the display for the
/// remaining credits.
/// </summary>
public class UpdateRemainingCreditsMessage : ValueChangedMessage<MainViewModel>
{
    public UpdateRemainingCreditsMessage(MainViewModel vm) : base(vm)
    {
    }
}