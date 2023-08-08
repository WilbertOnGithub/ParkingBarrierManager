namespace Arentheym.ParkingBarrier.Application;

using Microsoft.Extensions.DependencyInjection;

public static class ApplicationServicesExtension
{
    public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
    {
        services.AddTransient<DataService>();

        return services;
    }
}