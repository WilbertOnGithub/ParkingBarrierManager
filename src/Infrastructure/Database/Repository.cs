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

    public async Task<IList<Intercom>> GetIntercomsAsync()
    {
        return await databaseContext.Intercoms.OrderBy(x => x.Name)
                                              .AsNoTracking()
                                              .ToListAsync()
                                              .ConfigureAwait(false);
    }

    public async Task<IList<ApartmentConfiguration>> GetApartmentConfigurationsAsync()
    {
        return await databaseContext.ApartmentConfigurations
                                    .Include(x => x.PhoneNumbers)
                                    .Include(x => x.Intercoms)
                                    .OrderBy(x => x.Id)
                                    .AsNoTracking()
                                    .ToListAsync()
                                    .ConfigureAwait(false);
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="modifiedApartmentConfigurations"></param>
    public async Task UpdateApartmentConfigurationsAsync(IList<ApartmentConfiguration> modifiedApartmentConfigurations)
    {
        ArgumentNullException.ThrowIfNull(modifiedApartmentConfigurations);

        var existingIntercoms = await databaseContext.Intercoms.ToListAsync().ConfigureAwait(false);
        foreach (var apartmentConfiguration in modifiedApartmentConfigurations)
        {
            UpdateApartmentConfiguration(apartmentConfiguration, existingIntercoms);
        }

        await databaseContext.SaveChangesAsync().ConfigureAwait(false);
    }

    private void UpdateApartmentConfiguration(
        ApartmentConfiguration apartmentConfiguration,
        List<Intercom> existingIntercoms)
    {
        var existingEntry = databaseContext.ApartmentConfigurations
            .Include(x => x.PhoneNumbers)
            .Include(x => x.Intercoms)
            .First(x => x.Id == apartmentConfiguration.Id);

        // Update existing ApartmentConfiguration object.
        databaseContext.Entry(existingEntry).CurrentValues.SetValues(apartmentConfiguration);

        // Update all owned phone numbers.
        foreach (var phoneNumber in apartmentConfiguration.PhoneNumbers)
        {
            existingEntry.UpsertPhoneNumber(phoneNumber);
        }

        // Add any new referenced intercoms (if any).
        foreach (var intercom in apartmentConfiguration.Intercoms)
        {
            if (existingEntry.Intercoms.FirstOrDefault(x => x.Id == intercom.Id) == null)
            {
                existingEntry.LinkIntercom(existingIntercoms.First(x => x.Id == intercom.Id));
            }
        }

        // Delete any no longer referenced intercoms.
        foreach (var intercom in existingEntry.Intercoms)
        {
            if (apartmentConfiguration.Intercoms.All(x => x.Id != intercom.Id))
            {
                existingEntry.UnlinkIntercom(intercom);
            }
        }
    }
}