namespace Arentheym.ParkingBarrier.Application;

using Microsoft.Extensions.DependencyInjection;

public static class ApplicationServicesExtension
{
    public static void RegisterApplicationServices(this IServiceCollection services)
    {
        services.AddTransient<DataService>();
    }
}