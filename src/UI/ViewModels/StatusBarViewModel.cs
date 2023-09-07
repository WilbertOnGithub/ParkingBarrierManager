using Arentheym.ParkingBarrier.Application;
using CommunityToolkit.Mvvm.ComponentModel;
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

        RetrieveCreditsFromGateway();
    }

    private void RetrieveCreditsFromGateway()
    {
        Result<string> result = smsGatewayService.GetBalanceDetails();
        RemainingCredits = result.IsSuccess ?
            result.Value :
            "Error occurred while retrieving SMS balance.";
    }
}