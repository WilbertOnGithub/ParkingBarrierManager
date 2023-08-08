using Arentheym.ParkingBarrier.Domain;

namespace Arentheym.ParkingBarrier.Application;

public class DataService
{
    private readonly IRepository repository;
    public DataService(IRepository repository)
    {
        this.repository = repository;
    }

    public async Task<IEnumerable<ApartmentConfiguration>> GetApartmentConfigurations()
    {
        return await repository.GetApartmentConfigurationsAsync().ConfigureAwait(false);
    }

    public async Task<IEnumerable<Intercom>> GetIntercoms()
    {
        return await repository.GetIntercomsAsync().ConfigureAwait(false);
    }
}