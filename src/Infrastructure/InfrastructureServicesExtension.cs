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

        var smsGatewayConfiguration = new SmsGatewayConfiguration();
        IConfigurationSection smsGatewayConfigurationSection = configuration.GetSection(nameof(smsGatewayConfiguration));
        smsGatewayConfigurationSection.Bind(smsGatewayConfiguration);

        using var encryptor = new Encryptor();
        string decryptedApiKey = encryptor.Decrypt(smsGatewayConfiguration.ApiKey);

        services.AddSingleton<IRepository, Repository>();
        services.AddSingleton<ISmsGateway, MessageBirdGateway>(_ => new MessageBirdGateway(decryptedApiKey));
        services.AddDbContext<DatabaseContext>(options =>
            options.UseSqlite(databaseConfiguration.ExpandedConnectionString));
    }
}