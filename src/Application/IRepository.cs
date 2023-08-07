using Arentheym.ParkingBarrier.Domain;

namespace Arentheym.ParkingBarrier.Application;

/// <summary>
///
/// </summary>
public interface IRepository
{
    Task<IEnumerable<Intercom>> GetIntercomsAsync();

    Task<IEnumerable<ApartmentConfiguration>> GetApartmentConfigurationsAsync();

    Task SaveApartmentConfigurations(IEnumerable<ApartmentConfiguration> modifiedApartmentConfigurations);
}