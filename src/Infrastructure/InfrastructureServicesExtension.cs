using System.Reflection;
using Arentheym.ParkingBarrier.Application;
using Arentheym.ParkingBarrier.Infrastructure.Database;
using Arentheym.ParkingBarrier.Infrastructure.SmsGateway;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Arentheym.ParkingBarrier.Infrastructure;

using Microsoft.Extensions.DependencyInjection;

public static class InfrastructureServicesExtension
{
    public static IServiceCollection RegisterInfrastructureServices(this IServiceCollection services)
    {
        // Read configuration and add it as a strongly typed object to dependency injection.
        var databaseConfiguration = new DatabaseConfiguration();
        IConfigurationSection section = BuildConfiguration().GetSection(nameof(databaseConfiguration));
        section.Bind(databaseConfiguration);

        services.AddTransient<IRepository, Repository>();
        services.AddTransient<ISmsGateway, MessageBirdGateway>();
        services.AddDbContext<DatabaseContext>(options => options.UseSqlite(databaseConfiguration.ExpandedConnectionString));

        return services;
    }

    private static IConfiguration BuildConfiguration()
    {
        var builder = new ConfigurationBuilder();
        builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: false);

        return builder.Build();
    }
}