using System;

namespace PayNow.Infrastructure.Entities;

public class Maintenance
{
    public int? Id { get; set; }
    public int ScooterId { get; set; }
    public string Description { get; set; }
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset? EndDate { get; set; }
    public virtual Scooter Scooter { get; set; }
}