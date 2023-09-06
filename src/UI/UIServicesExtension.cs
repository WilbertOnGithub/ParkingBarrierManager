using Arentheym.ParkingBarrier.Application;
using Arentheym.ParkingBarrier.Infrastructure;
using Arentheym.ParkingBarrier.UI.ViewModels;
using Arentheym.ParkingBarrier.UI.Views;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace Arentheym.ParkingBarrier.UI;

using Microsoft.Extensions.DependencyInjection;

public static class UIServicesExtension
{
    public static void RegisterUIServices(this IServiceCollection services)
    {
        IConfiguration configuration = ReadConfiguration();

        services.AddSingleton<MainWindow>();
        services.AddSingleton<MainWindowViewModel>();
        services.AddSingleton<ApartmentConfigurationViewModel>();

        services.RegisterApplicationServices(configuration);
        services.RegisterInfrastructureServices(configuration);

        // Add Serilog configuration from appsettings.json
        services.AddLogging(builder =>
        {
            var logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();
            builder.AddSerilog(logger);
        });
    }

    private static IConfiguration ReadConfiguration()
    {
        return new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
            .AddEnvironmentVariables()
            .Build();
    }
}