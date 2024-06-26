using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PayNow.Infrastructure.Entities;

namespace PayNow.Infrastructure.EntitiesConfiguration;

public class ScooterEntityConfiguration : IEntityTypeConfiguration<Scooter>
{
    public void Configure(EntityTypeBuilder<Scooter> builder)
    {
        builder.ToTable("Scooters");

        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id)
            .ValueGeneratedOnAdd();

        builder.Property(s => s.Manufacturer)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(s => s.Model)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(s => s.ProductionDate)
            .IsRequired();

        builder.HasMany(s => s.Maintenances)
            .WithOne(m => m.Scooter)
            .HasForeignKey(m => m.ScooterId);

        builder.HasMany(s => s.Rentals)
            .WithOne(r => r.Scooter)
            .HasForeignKey(r => r.ScooterId);
    }
}