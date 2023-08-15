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

    public async Task SaveApartmentConfigurations(IList<ApartmentConfiguration> modifiedApartmentConfigurations)
    {
        ArgumentNullException.ThrowIfNull(modifiedApartmentConfigurations);

        var first = modifiedApartmentConfigurations[0];

        var existingEntry = databaseContext.ApartmentConfigurations
            .Include(x => x.PhoneNumbers)
            .Include(x => x.Intercoms)
            .First(x => x.Id == first.Id);

        // Update existing ApartmentConfiguration object.
        databaseContext.Entry(existingEntry).CurrentValues.SetValues(first);

        // Also update all owned phone numbers.
        foreach (var foo in first.PhoneNumbers)
        {
            existingEntry.UpsertPhoneNumber(foo);
        }

        foreach (var entry in databaseContext.ChangeTracker.Entries())
        {
            Console.WriteLine($"Entity: {entry.Entity.GetType().Name}, State: {entry.State.ToString()} ");
        }

        // Finally, save the graph
        await databaseContext.SaveChangesAsync().ConfigureAwait(false);
    }
}