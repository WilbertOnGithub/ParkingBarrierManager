using Arentheym.ParkingBarrier.Application;
using Arentheym.ParkingBarrier.UI.Messages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using FluentResults;

namespace Arentheym.ParkingBarrier.UI.ViewModels;

public partial class StatusBarViewModel : ObservableObject
{
    private readonly SmsGatewayService smsGatewayService;

    [ObservableProperty]
    private string remainingCredits = string.Empty;

    public StatusBarViewModel(SmsGatewayService smsGatewayService)
    {
        this.smsGatewayService = smsGatewayService;

        // Listen for RefreshRemainingCreditsMessage messages
        WeakReferenceMessenger.Default.Register<RefreshRemainingCreditsMessage>(this, (_, _) =>
        {
            OnMessageReceived();
        });

        RetrieveCreditsFromGateway();
    }

    private void RetrieveCreditsFromGateway()
    {
        Result<string> result = smsGatewayService.GetBalanceDetails();
        RemainingCredits = result.IsSuccess ?
            result.Value :
            "Error occurred while retrieving SMS balance.";
    }

    private void OnMessageReceived()
    {
        RetrieveCreditsFromGateway();
    }
}