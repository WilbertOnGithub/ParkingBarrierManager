using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

using Arentheym.ParkingBarrier.Application;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using FluentResults;

namespace Arentheym.ParkingBarrier.UI.ViewModels;

public partial class StatusBarViewModel : ObservableObject
{
    private readonly CultureInfo dutchCulture = new("nl-NL");

    private readonly SmsGatewayService smsGatewayService;
    private readonly MainViewModel mainViewModel;

    [ObservableProperty]
    private string remainingCredits = string.Empty;

    [ObservableProperty]
    private string pricePerMessage = string.Empty;

    [ObservableProperty]
    private string numberOfMessagesLeft = string.Empty;

    /// <summary>
    /// Is the Save Configuration button enabled or not?
    /// </summary>
    public bool SaveConfigurationButtonEnabled => mainViewModel.IsDirty;

    [ObservableProperty]
    private bool sendingSMS;

    /// <summary>
    /// How many SMS messages do we need to send if we want to update this
    /// configuration?
    /// </summary>
    public int NrSMSNeeded => mainViewModel.Configurations.Count(x => x.IsDirty) * 2;

    [SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "Dependency injection")]
    public StatusBarViewModel(MainViewModel mainViewModel, SmsGatewayService smsGatewayService)
    {
        this.mainViewModel = mainViewModel;
        this.smsGatewayService = smsGatewayService;

        mainViewModel.PropertyChanged += MainViewModelOnPropertyChanged;
        UpdateValues();
    }

    private void MainViewModelOnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(MainViewModel.IsDirty))
        {
            OnPropertyChanged(nameof(SaveConfigurationButtonEnabled));
            OnPropertyChanged(nameof(NrSMSNeeded));
        }
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

    [RelayCommand]
    public async Task SaveConfigurationsAsync()
    {
        if (SendingSMS)
        {
            await mainViewModel.UpdateConfigurationBySMS();
        }
        await mainViewModel.SaveConfigurationsAsync();
    }
}
