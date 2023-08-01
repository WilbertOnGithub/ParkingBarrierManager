using System.Globalization;
using Arentheym.ParkingBarrier.Application;
using Arentheym.ParkingBarrier.Infrastructure;
using Arentheym.ParkingBarrier.UI.ViewModels;
using Arentheym.ParkingBarrier.UI.Views;
using Serilog;
using Serilog.Debugging;

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
        services.AddLogging(builder =>
        {
            var logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console(formatProvider: CultureInfo.InvariantCulture)
                .WriteTo.File("C:\\temp\\logs\\log.txt", rollingInterval: RollingInterval.Day, formatProvider: CultureInfo.InvariantCulture)
                .CreateLogger();

            builder.AddSerilog(logger);
        });

        return services;
    }
}