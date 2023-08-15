using Arentheym.ParkingBarrier.Domain;

namespace Arentheym.ParkingBarrier.Application;

/// <summary>
/// Defines data access methods.
/// </summary>
public interface IRepository
{
    Task<IEnumerable<Intercom>> GetIntercomsAsync();

    Task<IEnumerable<ApartmentConfiguration>> GetApartmentConfigurationsAsync();

    Task SaveApartmentConfigurations(IList<ApartmentConfiguration> modifiedApartmentConfigurations);
}