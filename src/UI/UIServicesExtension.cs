using Arentheym.ParkingBarrier.UI.ViewModels;
using Arentheym.ParkingBarrier.UI.Views;

namespace Arentheym.ParkingBarrier.UI;

using Microsoft.Extensions.DependencyInjection;

public static class UIServicesExtension
{
    public static IServiceCollection RegisterUIServices(this IServiceCollection services)
    {
        services.AddSingleton<MainWindow>();
        services.AddSingleton<MainWindowViewModel>();

        return services;
    }
}