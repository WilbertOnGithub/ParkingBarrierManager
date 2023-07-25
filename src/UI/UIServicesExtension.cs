﻿using Arentheym.ParkingBarrier.Application;
using Arentheym.ParkingBarrier.Infrastructure;
using Arentheym.ParkingBarrier.UI.ViewModels;
using Arentheym.ParkingBarrier.UI.Views;
using Avalonia.Logging;
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
        services.AddLogging(builder =>
        {
            var logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();

            builder.AddSerilog(logger);
        });

        return services;
    }
}