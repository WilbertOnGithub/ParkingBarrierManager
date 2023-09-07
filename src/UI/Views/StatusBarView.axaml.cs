using Arentheym.ParkingBarrier.UI.ViewModels;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using CommunityToolkit.Mvvm.DependencyInjection;

namespace Arentheym.Views;

public partial class StatusBarView : UserControl
{
    public StatusBarView()
    {
        InitializeComponent();
        DataContext = Ioc.Default.GetRequiredService<StatusBarViewModel>();
    }
}