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
    }
}