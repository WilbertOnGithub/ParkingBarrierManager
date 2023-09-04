using System.Diagnostics.CodeAnalysis;
using Arentheym.ParkingBarrier.Application;
using Arentheym.ParkingBarrier.Infrastructure.Database;
using Arentheym.ParkingBarrier.Infrastructure.SmsGateway;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Arentheym.ParkingBarrier.Infrastructure;

using Microsoft.Extensions.DependencyInjection;

public static class InfrastructureServicesExtension
{
    public static void RegisterInfrastructureServices(this IServiceCollection services, [NotNull]IConfiguration configuration)
    {
        // Read configurations and add it as a strongly typed object to dependency injection.
        var databaseConfiguration = new DatabaseConfiguration();
        IConfigurationSection databaseConfigurationSection = configuration.GetSection(nameof(databaseConfiguration));
        databaseConfigurationSection.Bind(databaseConfiguration);

        services.AddTransient<IRepository, Repository>();
        services.AddTransient<ISmsGateway, MessageBirdGateway>();

        //ConfigureWarnings(w => w.Throw(RelationalEventId.MultipleCollectionIncludeWarning))
        services.AddDbContext<DatabaseContext>(options =>
            options.UseSqlite(databaseConfiguration.ExpandedConnectionString));
    }
}