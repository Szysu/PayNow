using System;
using System.Collections.Generic;

namespace PayNow.Infrastructure.Entities;

public class Scooter
{
    public int? Id { get; set; }
    public string Manufacturer { get; set; }
    public string Model { get; set; }
    public DateOnly ProductionDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    public virtual ICollection<Maintenance> Maintenances { get; set; }
    public virtual ICollection<Rental> Rentals { get; set; }

    internal DateTimeOffset? ProductionDateAsDateTime
    {
        get => ProductionDate.ToDateTime(TimeOnly.MinValue);
        set => ProductionDate = DateOnly.FromDateTime(value?.LocalDateTime ?? DateTime.Now);
    }
}