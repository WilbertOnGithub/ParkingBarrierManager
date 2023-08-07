using Arentheym.ParkingBarrier.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Arentheym.ParkingBarrier.Infrastructure.Database.Configurations;

/// <summary>
/// Database configuration for <see cref="ApartmentConfiguration"/>.
/// </summary>
internal sealed class ApartmentConfigurationConfiguration : IEntityTypeConfiguration<ApartmentConfiguration>
{
    /// <summary>
    /// Configure table for <see cref="IntercomConfiguration"/>.
    /// </summary>
    /// <param name="builder">Type builder for <see cref="IntercomConfiguration"/>.</param>
    public void Configure(EntityTypeBuilder<ApartmentConfiguration> builder)
    {
        builder.ToTable("ApartmentConfigurations");
        builder.HasKey(k => k.Id);

        builder.Property(p => p.Id)
               .HasColumnName(nameof(ApartmentConfiguration.Id))
               .HasConversion(x => x.Number, x => new ApartmentId(x))
               .ValueGeneratedNever();
        builder.Property(p => p.MemoryLocation)
               .HasColumnName(nameof(MemoryLocation))
               .HasConversion(x => x.Location, x => new MemoryLocation(x))
               .ValueGeneratedNever();
        builder.Property(p => p.AccessCode)
               .HasColumnName(nameof(AccessCode))
               .HasConversion(x => x.Code, x => new AccessCode(x))
               .ValueGeneratedNever();

        builder.Property(p => p.DialToOpen).HasColumnName("DialToOpen");

        // We need to map it this because we want a one-way relationship in code from
        // apartment configurations to intercoms but not the other way around.

        builder.HasMany(x => x.Intercoms)
               .WithMany()
               .UsingEntity(x => x.ToTable("ApartmentConfigurationIntercoms"));

        builder.OwnsMany(x => x.PhoneNumbers, owned =>
            {
                const string foreignKeyColumnName = "ApartmentConfigurationId";
                const string orderColumnName = "Order";
                const string numberColumnName = "Number";

                owned.ToTable("ApartmentConfigurationPhoneNumbers");
                owned.HasOne<ApartmentConfiguration>().WithMany(x => x.PhoneNumbers).HasForeignKey(foreignKeyColumnName);
                owned.Property(p => p.Order).HasColumnName(orderColumnName);
                owned.Property(p => p.Number).HasColumnName(numberColumnName);

                owned.HasKey(foreignKeyColumnName, orderColumnName, numberColumnName);
            });
    }
}