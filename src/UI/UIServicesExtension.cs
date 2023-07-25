using Arentheym.ParkingBarrier.Application;
using Arentheym.ParkingBarrier.Infrastructure;
using Arentheym.ParkingBarrier.UI.ViewModels;
using Arentheym.ParkingBarrier.UI.Views;
using Serilog;

namespace Arentheym.ParkingBarrier.UI;

using Microsoft.Extensions.DependencyInjection;

public static class UIServicesExtension
{
    public static IServiceCollection RegisterUIServices(this IServiceCollection services)
    {
        services.AddSingleton<MainWindow>();
        services.AddSingleton<MainWindowViewModel>();

        services.RegisterApplicationServices();
        services.RegisterInfrastructureServices();

        // Add Serilog configuration
        using var log = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();
        services.AddSingleton<ILogger>(log);

        return services;
    }
}