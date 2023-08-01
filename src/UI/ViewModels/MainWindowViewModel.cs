using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;

namespace Arentheym.ParkingBarrier.UI.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public MainWindowViewModel()
    {
    }

    [SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Needed for binding")]
    public string Greeting => "Welcome to Avalonia!";
}