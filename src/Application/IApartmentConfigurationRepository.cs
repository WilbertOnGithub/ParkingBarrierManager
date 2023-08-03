using Arentheym.ParkingBarrier.Domain;

namespace Arentheym.ParkingBarrier.Application;

/// <summary>
///
/// </summary>
public interface IApartmentConfigurationRepository
{
    IEnumerable<ApartmentConfiguration> GetApartmentConfigurations();

    void SaveUpdates(IEnumerable<ApartmentConfiguration> apartmentConfigurations);
}