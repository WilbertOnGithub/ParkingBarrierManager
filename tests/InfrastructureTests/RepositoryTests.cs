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
    public async Task Updating_phoneNumber_saves_it_in_database()
    {
        // Arrange
        await using DatabaseContext databaseContext = await CreateTemporaryDatabaseContext();
        fixture.Inject(databaseContext);
        var repository = fixture.Create<Repository>();

        IList<ApartmentConfiguration> apartmentConfigurations =
            await repository.GetApartmentConfigurationsAsync();
        var apartment131 = apartmentConfigurations.First(x => x.Id.Number == 131);
        apartment131.UpsertPhoneNumber(new DivertPhoneNumber(DivertOrder.Primary, "31 1234567890"));

        // Act
        await repository.UpdateApartmentConfigurationsAsync(apartmentConfigurations);

        IList<ApartmentConfiguration> updatedList =
            await repository.GetApartmentConfigurationsAsync();

        // Assert
        updatedList.First(x => x.Id.Number == 131).PhoneNumbers[0].Number.Should().Be("31 1234567890");
        await databaseContext.Database.EnsureDeletedAsync();
    }

    [Fact]
    public async Task Removing_linked_intercom_saves_it_in_database()
    {
        // Arrange
        await using DatabaseContext databaseContext = await CreateTemporaryDatabaseContext();
        fixture.Inject(databaseContext);
        var repository = fixture.Create<Repository>();

        IList<Intercom> intercoms = await repository.GetIntercomsAsync();
        IList<ApartmentConfiguration> apartmentConfigurations =
            await repository.GetApartmentConfigurationsAsync();
        var apartment131 = apartmentConfigurations.First(x => x.Id.Number == 131);
        apartment131.UnlinkIntercom(intercoms[0]);
        apartment131.UnlinkIntercom(intercoms[1]);

        // Act
        await repository.UpdateApartmentConfigurationsAsync(apartmentConfigurations);

        IList<ApartmentConfiguration> updatedList =
            await repository.GetApartmentConfigurationsAsync();

        // Assert
        updatedList.First(x => x.Id.Number == 131).Intercoms.Count.Should().Be(0);
        await databaseContext.Database.EnsureDeletedAsync();
    }

    [Fact]
    public async Task Adding_intercom_saves_it_in_database()
    {
        // Arrange
        await using DatabaseContext databaseContext = await CreateTemporaryDatabaseContext();
        fixture.Inject(databaseContext);
        var repository = fixture.Create<Repository>();

        IList<Intercom> intercoms = await repository.GetIntercomsAsync();
        IList<ApartmentConfiguration> apartmentConfigurations =
            await repository.GetApartmentConfigurationsAsync();
        var apartment131 = apartmentConfigurations.First(x => x.Id.Number == 131);
        apartment131.UnlinkIntercom(intercoms[0]);
        apartment131.UnlinkIntercom(intercoms[1]);
        await repository.UpdateApartmentConfigurationsAsync(apartmentConfigurations);

        // Act
        apartment131.LinkIntercom(intercoms[0]);
        await repository.UpdateApartmentConfigurationsAsync(apartmentConfigurations);

        IList<ApartmentConfiguration> updatedList =
            await repository.GetApartmentConfigurationsAsync();

        // Assert
        updatedList.First(x => x.Id.Number == 131).Intercoms.Count.Should().Be(1);
        await databaseContext.Database.EnsureDeletedAsync();
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
        await databaseContext.Database.EnsureDeletedAsync();
        await databaseContext.Database.MigrateAsync();

        return databaseContext;
    }
}