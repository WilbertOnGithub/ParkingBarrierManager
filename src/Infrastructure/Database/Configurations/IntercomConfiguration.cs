using Arentheym.ParkingBarrier.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Arentheym.ParkingBarrier.Infrastructure.Database.Configurations;

/// <summary>
/// Database configuration for <see cref="Intercom"/>.
/// </summary>
internal sealed class IntercomConfiguration : IEntityTypeConfiguration<Intercom>
{
    /// <summary>
    /// Configure table for <see cref="Intercom"/>.
    /// </summary>
    /// <param name="builder">Type builder for <see cref="Intercom"/>.</param>
    public void Configure(EntityTypeBuilder<Intercom> builder)
    {
        builder.ToTable("Intercoms");
        builder.HasKey(k => k.Id);

        builder.Property(p => p.Id)
            .HasColumnName(nameof(Intercom.Id))
            .HasConversion(x => x.Id, x => new IntercomId(x))
            .ValueGeneratedNever();
        builder.Property(p => p.MasterCode)
            .HasColumnName(nameof(MasterCode))
            .HasMaxLength(4)
            .HasConversion(x => x.Code, x => new MasterCode(x))
            .ValueGeneratedNever();
        builder.Property(p => p.Name).HasColumnName("Name");

        builder.OwnsOne(p => p.PhoneNumber, owned =>
        {
            owned.Property(p => p.Number).HasColumnName(nameof(PhoneNumber));
        });
    }
}