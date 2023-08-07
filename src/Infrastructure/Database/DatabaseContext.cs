using System.Globalization;
using Arentheym.ParkingBarrier.Domain;
using Arentheym.ParkingBarrier.Infrastructure.Database.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Arentheym.ParkingBarrier.Infrastructure.Database;

/// <summary>
/// Database context.
/// </summary>
public class DatabaseContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DatabaseContext"/> class.
    /// </summary>
    /// <param name="options">Options for the database context.</param>
    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Gets the <see cref="DbSet{ApartmentConfiguration}"/>.
    /// </summary>
    public virtual DbSet<ApartmentConfiguration> ApartmentConfigurations => Set<ApartmentConfiguration>();

    /// <summary>
    /// Gets the <see cref="DbSet{Intercom}"/>.
    /// </summary>
    public virtual DbSet<Intercom> Intercoms => Set<Intercom>();

    /// <summary>
    /// Apply configuration for this database context.
    /// </summary>
    /// <param name="modelBuilder">The model builder to use for the configuration.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);
        modelBuilder.ApplyConfiguration(new IntercomConfiguration());
        modelBuilder.ApplyConfiguration(new ApartmentConfigurationConfiguration());

        const string masterCode = "8601";

        var intercomFront = new Intercom("Slagboom voor", new PhoneNumber("0657093298"), new MasterCode(masterCode));
        var intercomBack = new Intercom("Slagboom achter", new PhoneNumber("0657181402"), new MasterCode(masterCode));

        modelBuilder.Entity<Intercom>().HasData(intercomFront);
        modelBuilder.Entity<Intercom>().HasData(intercomBack);

        // Seeding data apartment configurations
        for (int apartmentNumber = 51; apartmentNumber <= 189; apartmentNumber += 2)
        {
            var apartmentConfiguration = new ApartmentConfiguration(
                new ApartmentId(apartmentNumber.ToString(CultureInfo.InvariantCulture)),
                new MemoryLocation(Convert.ToInt16(apartmentNumber)),
                apartmentNumber.ToString("D3", CultureInfo.InvariantCulture),
                true,
                new AccessCode(string.Empty));

            modelBuilder.Entity<ApartmentConfiguration>().HasData(apartmentConfiguration);
        }
    }
}