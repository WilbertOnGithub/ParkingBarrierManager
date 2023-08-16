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

    public async Task<IList<Intercom>> GetIntercoms()
    {
        return await repository.GetIntercomsAsync().ConfigureAwait(false);
    }

    public async Task SaveApartmentConfigurations(IList<ApartmentConfiguration> modifiedApartmentConfigurations)
    {
        await repository.UpdateApartmentConfigurationsAsync(modifiedApartmentConfigurations).ConfigureAwait(false);
    }
}