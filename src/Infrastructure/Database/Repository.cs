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
                                              .ToListAsync()
                                              .ConfigureAwait(false);
    }

    public async Task<IEnumerable<ApartmentConfiguration>> GetApartmentConfigurationsAsync()
    {
        return await databaseContext.ApartmentConfigurations
                                    .Include(x => x.PhoneNumbers)
                                    .Include(x => x.Intercoms)
                                    .OrderBy(x => x.Id)
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

        // Update existing object
        databaseContext.Entry(existingEntry).CurrentValues.SetValues(first);

        // Update linked phone numbers
        foreach (var phoneNumber in first.PhoneNumbers)
        {
            var existingPhoneNumber = existingEntry.PhoneNumbers.First(p => p.Order == phoneNumber.Order);
            databaseContext.Entry(existingPhoneNumber).CurrentValues.SetValues(phoneNumber);
        }

        await databaseContext.SaveChangesAsync().ConfigureAwait(false);
    }
}