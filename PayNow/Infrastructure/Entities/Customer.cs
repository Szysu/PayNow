using System.Collections.Generic;

namespace PayNow.Infrastructure.Entities;

public class Customer
{
    public int? Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhotoUrl { get; set; }
    public virtual ICollection<Rental> Rentals { get; set; }
}