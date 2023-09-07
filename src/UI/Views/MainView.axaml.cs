using Arentheym.ParkingBarrier.UI.ViewModels;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.DependencyInjection;

namespace Arentheym.ParkingBarrier.UI.Views;

public partial class MainView : Window
{
    public MainView()
    {
        InitializeComponent();
        DataContext = Ioc.Default.GetRequiredService<MainViewModel>();
    }
}