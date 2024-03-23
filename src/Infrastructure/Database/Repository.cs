using Arentheym.ParkingBarrier.Application;
using Arentheym.ParkingBarrier.Domain;
using Microsoft.EntityFrameworkCore;

namespace Arentheym.ParkingBarrier.Infrastructure.Database;

public class Repository(DatabaseContext context) : IRepository
{
    public async Task<IList<Intercom>> GetIntercomsAsync()
    {
        return await context.Intercoms.OrderBy(x => x.Name).AsNoTracking().ToListAsync().ConfigureAwait(false);
    }

    public async Task<IList<ApartmentConfiguration>> GetApartmentConfigurationsAsync()
    {
        return await context
            .ApartmentConfigurations.Include(x => x.PhoneNumbers)
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

        var existingIntercoms = await context.Intercoms.ToListAsync().ConfigureAwait(false);
        foreach (var apartmentConfiguration in modifiedApartmentConfigurations)
        {
            UpdateApartmentConfiguration(apartmentConfiguration, existingIntercoms);
        }

        await context.SaveChangesAsync().ConfigureAwait(false);
    }

    private void UpdateApartmentConfiguration(
        ApartmentConfiguration apartmentConfiguration,
        List<Intercom> existingIntercoms
    )
    {
        var existingEntry = context
            .ApartmentConfigurations.Include(x => x.PhoneNumbers)
            .Include(x => x.Intercoms)
            .First(x => x.Id == apartmentConfiguration.Id);

        // Update existing ApartmentConfiguration object.
        context.Entry(existingEntry).CurrentValues.SetValues(apartmentConfiguration);

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
