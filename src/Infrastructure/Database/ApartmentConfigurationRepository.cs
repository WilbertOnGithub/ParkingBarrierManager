using Arentheym.ParkingBarrier.Application;
using Arentheym.ParkingBarrier.Domain;

namespace Arentheym.ParkingBarrier.Infrastructure.Database;

public class ApartmentConfigurationRepository : IApartmentConfigurationRepository
{
    public IEnumerable<ApartmentConfiguration> GetApartmentConfigurations()
    {
        throw new NotImplementedException();
    }

    public void SaveUpdates(IEnumerable<ApartmentConfiguration> apartmentConfigurations)
    {
        throw new NotImplementedException();
    }
}