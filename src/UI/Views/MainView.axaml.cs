using Arentheym.ParkingBarrier.UI.ViewModels;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.DependencyInjection;
using SukiUI.Controls;

namespace Arentheym.ParkingBarrier.UI.Views;

public partial class MainView : SukiWindow
{
    public MainView()
    {
        InitializeComponent();
        DataContext = Ioc.Default.GetRequiredService<MainViewModel>();
    }
}
