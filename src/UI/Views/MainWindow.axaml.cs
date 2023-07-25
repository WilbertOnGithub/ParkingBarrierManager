using Arentheym.ParkingBarrier.UI.ViewModels;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.DependencyInjection;

namespace Arentheym.ParkingBarrier.UI.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = Ioc.Default.GetRequiredService<MainWindowViewModel>();
    }
}