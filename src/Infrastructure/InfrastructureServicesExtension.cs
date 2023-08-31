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
    public static void RegisterInfrastructureServices(this IServiceCollection services)
    {
        // Read configurations and add it as a strongly typed object to dependency injection.
        var databaseConfiguration = new DatabaseConfiguration();
        IConfigurationSection databaseConfigurationSection = BuildConfiguration().GetSection(nameof(databaseConfiguration));
        databaseConfigurationSection.Bind(databaseConfiguration);

        var smsGatewayConfiguration = new SmsGatewayConfiguration();
        IConfigurationSection smsGatewayConfigurationSection = BuildConfiguration().GetSection(nameof(smsGatewayConfiguration));
        smsGatewayConfigurationSection.Bind(smsGatewayConfiguration);

        services.AddTransient<IRepository, Repository>();
        services.AddTransient<ISmsGateway, MessageBirdGateway>();
        services.AddDbContext<DatabaseContext>(options => options.UseSqlite(databaseConfiguration.ExpandedConnectionString));
    }

    private static IConfiguration BuildConfiguration()
    {
        var builder = new ConfigurationBuilder();
        builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: false);

        return builder.Build();
    }
}