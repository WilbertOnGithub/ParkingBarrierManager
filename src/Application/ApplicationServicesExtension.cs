namespace Arentheym.ParkingBarrier.Application;

using Microsoft.Extensions.DependencyInjection;

public static class ApplicationServicesExtension
{
    public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
    {
        // TODO: Extend list here
        // services.AddTransient<IUpdateReportingUseCase, UpdateReportingUseCase>();
        return services;
    }
}