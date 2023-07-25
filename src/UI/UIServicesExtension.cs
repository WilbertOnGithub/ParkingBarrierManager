namespace Arentheym.ParkingBarrier.UI;

using Microsoft.Extensions.DependencyInjection;

public static class UIServicesExtension
{
    public static IServiceCollection RegisterUIServices(this IServiceCollection services)
    {
        // TODO: Extend list here
        // services.AddTransient<IUpdateReportingUseCase, UpdateReportingUseCase>();
        return services;
    }
}