using System.Diagnostics.CodeAnalysis;
using Arentheym.ParkingBarrier.Domain;
using Arentheym.ParkingBarrier.Infrastructure.Database;
using AutoFixture;
using AutoFixture.AutoNSubstitute;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Arentheym.ParkingBarrier.Infrastructure.Tests;

[SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "Unit testing naming")]
public class RepositoryTests
{
    private readonly IFixture fixture = new Fixture().Customize(new AutoNSubstituteCustomization());

    [Fact]
    public async Task Updating_a_phonenumber_for_an_apartment_saves_it_correctly()
    {
        // Arrange
        using DatabaseContext databaseContext = await CreateTemporaryDatabaseContext().ConfigureAwait(false);
        fixture.Inject(databaseContext);
        var repository = fixture.Create<Repository>();

        IList<ApartmentConfiguration> apartmentConfigurations =
            await repository.GetApartmentConfigurationsAsync().ConfigureAwait(false);
        var apartment131 = apartmentConfigurations.First(x => x.Id.Number == 131);
        apartment131.UpsertPhoneNumber(new DivertPhoneNumber(DivertOrder.Primary, "1234567890"));

        // Act
        await repository.UpdateApartmentConfigurationsAsync(apartmentConfigurations).ConfigureAwait(false);

        IList<ApartmentConfiguration> updatedList =
            await repository.GetApartmentConfigurationsAsync().ConfigureAwait(false);

        // Assert
        updatedList.First(x => x.Id.Number == 131).PhoneNumbers[0].Number.Should().Be("1234567890");
        await databaseContext.Database.EnsureDeletedAsync().ConfigureAwait(false);
    }

    /// <summary>
    /// Creates a <see cref="DatabaseContext"/> for a uniquely named SQLITE database in the TEMP folder
    /// for unit testing purposes.
    /// </summary>
    private static async Task<DatabaseContext> CreateTemporaryDatabaseContext()
    {
        string tempDatabasePath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid().ToString()}-PBM.db");
        string connectionString = $"Filename={tempDatabasePath};Mode=ReadWriteCreate;Foreign Keys=True;Default Timeout=30;Cache=Default";

        var databaseContext = new DatabaseContext(new DbContextOptionsBuilder<DatabaseContext>().UseSqlite(connectionString).Options);
        await databaseContext.Database.EnsureDeletedAsync().ConfigureAwait(false);
        await databaseContext.Database.MigrateAsync().ConfigureAwait(false);

        return databaseContext;
    }
}