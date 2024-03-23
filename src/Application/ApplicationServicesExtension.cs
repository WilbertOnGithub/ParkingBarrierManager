using Microsoft.Extensions.Configuration;

namespace Arentheym.ParkingBarrier.Application;

using Microsoft.Extensions.DependencyInjection;

public static class ApplicationServicesExtension
{
    public static void RegisterApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration);

        services.AddSingleton<DataService>();
        services.AddTransient<SmsGatewayService>();
    }
}
