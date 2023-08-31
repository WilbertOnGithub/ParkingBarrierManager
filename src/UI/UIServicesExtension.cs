using System.Globalization;
using Arentheym.ParkingBarrier.Application;
using Arentheym.ParkingBarrier.Infrastructure;
using Arentheym.ParkingBarrier.UI.ViewModels;
using Arentheym.ParkingBarrier.UI.Views;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Debugging;

namespace Arentheym.ParkingBarrier.UI;

using Microsoft.Extensions.DependencyInjection;

public static class UIServicesExtension
{
    public static void RegisterUIServices(this IServiceCollection services)
    {
        services.AddSingleton<MainWindow>();
        services.AddSingleton<MainWindowViewModel>();
        services.AddSingleton<ApartmentConfigurationViewModel>();

        services.RegisterApplicationServices();
        services.RegisterInfrastructureServices();

        // Add Serilog configuration from appsettings.json
        services.AddLogging(builder =>
        {
            var logger = new LoggerConfiguration().ReadFrom.Configuration(ReadConfiguration()).CreateLogger();
            builder.AddSerilog(logger);
        });
    }

    private static IConfiguration ReadConfiguration()
    {
        return new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
            .Build();
    }
}