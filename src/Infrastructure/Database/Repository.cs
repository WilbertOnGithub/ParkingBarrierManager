﻿using Arentheym.ParkingBarrier.Application;
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

        var existingIntercoms = databaseContext.Intercoms.ToList();

        // Update existing ApartmentConfiguration object.
        databaseContext.Entry(existingEntry).CurrentValues.SetValues(first);

        // Update all owned phone numbers.
        foreach (var phoneNumber in first.PhoneNumbers)
        {
            existingEntry.UpsertPhoneNumber(phoneNumber);
        }

        // Add any new referenced intercoms (if any).
        foreach (var intercom in first.Intercoms)
        {
            if (existingEntry.Intercoms.FirstOrDefault(x => x.Id == intercom.Id) == null)
            {
                existingEntry.LinkIntercom(existingIntercoms.First(x => x.Id == intercom.Id));
            }
        }

        // Delete any no longer referenced intercoms.
        foreach (var intercom in existingEntry.Intercoms)
        {
            if (!first.Intercoms.Any(x => x.Id == intercom.Id))
            {
                existingEntry.UnlinkIntercom(intercom);
            }
        }

        foreach (var entry in databaseContext.ChangeTracker.Entries())
        {
            Console.WriteLine($"Entity: {entry.Entity.GetType().Name}, State: {entry.State.ToString()} ");
        }


        // Finally, save the new state of the graph.
        await databaseContext.SaveChangesAsync().ConfigureAwait(false);
    }
}