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

        // Retrieve existing intercoms to prevent the EF Core tracker from adding them as new entries in the
        var existingIntercoms = databaseContext.Intercoms.ToList();

        foreach (var modifiedApartmentConfiguration in modifiedApartmentConfigurations)
        {
            UpdateApartmentConfiguration(modifiedApartmentConfiguration, existingIntercoms);
        }

        // Finally, save the new state of the entire ApartmentConfiguration graph.
        await databaseContext.SaveChangesAsync().ConfigureAwait(false);
    }

    private void UpdateApartmentConfiguration(
        ApartmentConfiguration modifiedApartmentConfiguration,
        IReadOnlyCollection<Intercom> existingIntercoms)
    {
        var entityOnDisk = databaseContext.ApartmentConfigurations
            .Include(x => x.PhoneNumbers)
            .Include(x => x.Intercoms)
            .First(x => x.Id == modifiedApartmentConfiguration.Id);

        // Update all owned phone numbers with new values.
        foreach (var phoneNumber in modifiedApartmentConfiguration.PhoneNumbers)
        {
            entityOnDisk.UpsertPhoneNumber(phoneNumber);
        }

        // Update existing ApartmentConfiguration object with new values.
        databaseContext.Entry(entityOnDisk).CurrentValues.SetValues(modifiedApartmentConfiguration);

        // Add any new referenced intercoms to the join table ApartmentConfigurationIntercom.
        foreach (var intercom in entityOnDisk.Intercoms)
        {
            if (entityOnDisk.Intercoms.FirstOrDefault(x => x.Id == intercom.Id) == null)
            {
                entityOnDisk.LinkIntercom(existingIntercoms.First(x => x.Id == intercom.Id));
            }
        }

        // Delete no longer referenced intercoms from the join table ApartmentConfigurationIntercom.
        foreach (var intercom in entityOnDisk.Intercoms)
        {
            if (modifiedApartmentConfiguration.Intercoms.All(x => x.Id != intercom.Id))
            {
                entityOnDisk.UnlinkIntercom(intercom);
            }
        }
    }
}