using Arentheym.ParkingBarrier.Domain;

namespace Arentheym.ParkingBarrier.Application;

/// <summary>
/// Defines data access methods.
/// </summary>
public interface IRepository
{
    Task<IList<Intercom>> GetIntercomsAsync();

    Task<IList<ApartmentConfiguration>> GetApartmentConfigurationsAsync();

    Task UpdateApartmentConfigurationsAsync(IList<ApartmentConfiguration> modifiedApartmentConfigurations);
}
