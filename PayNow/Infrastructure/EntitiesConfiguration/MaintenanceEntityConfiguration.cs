using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PayNow.Infrastructure.Entities;

namespace PayNow.Infrastructure.EntitiesConfiguration;

public class MaintenanceEntityConfiguration : IEntityTypeConfiguration<Maintenance>
{
    public void Configure(EntityTypeBuilder<Maintenance> builder)
    {
        builder.ToTable("Maintenances");

        builder.HasKey(m => m.Id);
        builder.Property(m => m.Id)
            .ValueGeneratedOnAdd();

        builder.Property(m => m.ScooterId)
            .IsRequired();

        builder.Property(m => m.Description)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(m => m.StartDate)
            .IsRequired();

        builder.Property(m => m.EndDate)
            .IsRequired(false);

        builder.HasOne(m => m.Scooter)
            .WithMany(s => s.Maintenances)
            .HasForeignKey(m => m.ScooterId);
    }
}