namespace Arentheym.ParkingBarrier.Infrastructure;

using Microsoft.Extensions.DependencyInjection;

public static class InfrastructureServicesExtension
{
    public static IServiceCollection RegisterInfrastructureServices(this IServiceCollection services)
    {
        // TODO: Extend list here
        // services.AddTransient<IUpdateReportingUseCase, UpdateReportingUseCase>();
        return services;
    }
}