using Arentheym.ParkingBarrier.Application;
using Arentheym.ParkingBarrier.Domain;
using Microsoft.EntityFrameworkCore;

namespace Arentheym.ParkingBarrier.Infrastructure.Database;

public class Repository : IRepository
{
    private readonly DatabaseContext databaseContext;
    public Repository(DatabaseContext databaseContext)
    {
        this.databaseContext = databaseContext;
    }

    public async Task<IEnumerable<Intercom>> GetIntercomsAsync()
    {
        return await databaseContext.Intercoms.OrderBy(x => x.Name)
                                              .AsNoTracking()
                                              .ToListAsync()
                                              .ConfigureAwait(false);
    }

    public async Task<IEnumerable<ApartmentConfiguration>> GetApartmentConfigurationsAsync()
    {
        return await databaseContext.ApartmentConfigurations
                                    .Include(x => x.PhoneNumbers)
                                    .Include(x => x.Intercoms)
                                    .OrderBy(x => x.Id)
                                    .AsNoTracking()
                                    .ToListAsync()
                                    .ConfigureAwait(false);
    }

    public async Task SaveApartmentConfigurations(IEnumerable<ApartmentConfiguration> modifiedApartmentConfigurations)
    {
        databaseContext.UpdateRange(modifiedApartmentConfigurations);

        await databaseContext.SaveChangesAsync().ConfigureAwait(false);
    }
}