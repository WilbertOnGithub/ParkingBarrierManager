﻿using Arentheym.ParkingBarrier.Application;
using Arentheym.ParkingBarrier.Infrastructure.Database;
using Arentheym.ParkingBarrier.Infrastructure.SmsGateway;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Arentheym.ParkingBarrier.Infrastructure;

using Microsoft.Extensions.DependencyInjection;

public static class InfrastructureServicesExtension
{
    public static void RegisterInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration);

        // Read configurations and add it as a strongly typed object for dependency injection.
        var databaseConfiguration = new DatabaseConfiguration();
        IConfigurationSection databaseConfigurationSection = configuration.GetSection(nameof(databaseConfiguration));
        databaseConfigurationSection.Bind(databaseConfiguration);

        var smsGatewayConfiguration = new SmsGatewayConfiguration();
        IConfigurationSection smsGatewayConfigurationSection = configuration.GetSection(nameof(smsGatewayConfiguration));
        smsGatewayConfigurationSection.Bind(smsGatewayConfiguration);

        // Decrypt the MessageBird API key before using it to correctly register the SMS gateway.
        using var encryptor = new Encryptor();
        string decryptedApiKey = encryptor.Decrypt(smsGatewayConfiguration.ApiKey);
        services.AddSingleton<ISmsGateway, MessageBirdGateway>(_ => new MessageBirdGateway(decryptedApiKey));

        services.AddSingleton<IRepository, Repository>();
        services.AddDbContext<DatabaseContext>(options =>
            options.UseSqlite(databaseConfiguration.ExpandedConnectionString));
    }
}