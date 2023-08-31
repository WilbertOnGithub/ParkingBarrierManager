using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;

namespace Arentheym.ParkingBarrier.Application;

using Microsoft.Extensions.DependencyInjection;

public static class ApplicationServicesExtension
{
    public static void RegisterApplicationServices(this IServiceCollection services, [NotNull]IConfiguration configuration)
    {
        services.AddTransient<DataService>();
    }
}