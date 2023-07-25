using Microsoft.Extensions.Logging;

namespace Arentheym.ParkingBarrier.UI.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private readonly ILogger<MainWindowViewModel> logger;

    public MainWindowViewModel(ILogger<MainWindowViewModel> logger)
    {
        this.logger = logger;
    }

    public string Greeting => "Welcome to Avalonia!";
}