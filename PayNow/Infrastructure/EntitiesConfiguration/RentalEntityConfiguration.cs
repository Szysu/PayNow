using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PayNow.Infrastructure.Entities;

namespace PayNow.Infrastructure.EntitiesConfiguration;

public class RentalEntityConfiguration : IEntityTypeConfiguration<Rental>
{
    public void Configure(EntityTypeBuilder<Rental> builder)
    {
        builder.ToTable("Rentals");

        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id)
            .ValueGeneratedOnAdd();

        builder.Property(r => r.CustomerId)
            .IsRequired();

        builder.Property(r => r.ScooterId)
            .IsRequired();

        builder.Property(r => r.FromLocation)
            .HasSrid(4326)
            .IsRequired();

        builder.Property(r => r.ToLocation)
            .HasSrid(4326)
            .IsRequired();

        builder.Property(r => r.StartDate)
            .IsRequired();

        builder.Property(r => r.EndDate)
            .IsRequired();

        builder.HasOne(r => r.Customer)
            .WithMany(c => c.Rentals)
            .HasForeignKey(r => r.CustomerId);

        builder.HasOne(r => r.Scooter)
            .WithMany(s => s.Rentals)
            .HasForeignKey(r => r.ScooterId);
    }
}