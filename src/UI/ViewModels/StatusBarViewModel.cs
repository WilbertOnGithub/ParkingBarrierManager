using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Arentheym.ParkingBarrier.Application;
using Arentheym.ParkingBarrier.UI.Messages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using FluentResults;

namespace Arentheym.ParkingBarrier.UI.ViewModels;

public partial class StatusBarViewModel : ObservableObject
{
    private readonly CultureInfo dutchCulture = new("nl-NL");
    private readonly SmsGatewayService smsGatewayService;

    [ObservableProperty]
    private string remainingCredits = string.Empty;

    [ObservableProperty]
    private string pricePerMessage = string.Empty;

    [ObservableProperty]
    private string numberOfMessagesLeft = string.Empty;

    public StatusBarViewModel([NotNull] SmsGatewayService smsGatewayService)
    {
        this.smsGatewayService = smsGatewayService;

        // Listen for RefreshRemainingCreditsMessage messages
        WeakReferenceMessenger.Default.Register<RefreshRemainingCreditsMessage>(
            this,
            (_, _) =>
            {
                OnMessageReceived();
            }
        );

        UpdateValues();
    }

    private void UpdateValues()
    {
        Result<float> balanceDetails = smsGatewayService.GetBalanceDetails();
        Result<int> messagesLeft = smsGatewayService.NumberOfMessagesLeft();

        RemainingCredits = balanceDetails.IsSuccess
            ? $"{balanceDetails.Value.ToString(dutchCulture)} euro."
            : "Error occurred while retrieving SMS balance.";

        PricePerMessage = smsGatewayService.GetPricePerMessage().ToString(dutchCulture);
        NumberOfMessagesLeft = messagesLeft.IsSuccess
            ? messagesLeft.Value.ToString(dutchCulture)
            : "Could not determine number of messages left.";
    }

    private void OnMessageReceived()
    {
        UpdateValues();
    }
}
