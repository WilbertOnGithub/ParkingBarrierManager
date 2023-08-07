using Arentheym.ParkingBarrier.UI.Views;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;

using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Arentheym.ParkingBarrier.UI;

public partial class App : Avalonia.Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        var services = new ServiceCollection();
        services.RegisterUIServices();

        var provider = services.BuildServiceProvider();
        Ioc.Default.ConfigureServices(provider);

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // Line below is needed to remove Avalonia data validation.
            // Without this line you will get duplicate validations from both Avalonia and CT
            BindingPlugins.DataValidators.RemoveAt(0);

            // Start the first view. Viewmodel is bound to datacontext in constructor of view.
            var mainView = provider.GetRequiredService<MainWindow>();
            desktop.MainWindow = mainView;
        }

        base.OnFrameworkInitializationCompleted();
    }
}