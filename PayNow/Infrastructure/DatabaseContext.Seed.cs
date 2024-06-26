using System.Collections.Generic;
using System.Linq;
using Bogus;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using PayNow.Infrastructure.Entities;

namespace PayNow.Infrastructure;

public partial class DatabaseContext
{
    private readonly Point[] _locations =
    [
        new(22.01665018354217, 50.02675112790157), // Millenium Hall
        new(21.9820326776338, 50.0488640161686), // University of Information Technology and Management
        new(22.004222957113736, 50.037442844208364), // Market Square
        new(22.02017422563502, 50.01786933002482), // Pope Park
        new(21.99483655351874, 50.0214809432979), // Football Stadium
        new(22.006907537409102, 50.04264640188262), // Railway Station
        new(21.989932709533154, 50.02000831832533), // Rzeszow University of Technology
        new(22.015111646430658, 50.030203904376634), // University of Rzeszow
        new(22.00478536563984, 50.031630150326876) // Olszynki Park
    ];

    private readonly string[] _manufacturers =
    [
        "Xiaomi", "Ninebot", "Segway"
    ];

    private IEnumerable<Point> GetLocationsExcept(Point? except = null) =>
        _locations.Where(l => except == null || l != except);

    public void Seed()
    {
        Randomizer.Seed = new(4202137);

        var idsCounter = 0;
        var customers = new Faker<Customer>()
            // ReSharper disable once AccessToModifiedClosure
            .RuleFor(c => c.Id, _ => ++idsCounter)
            .RuleFor(c => c.FirstName, f => f.Person.FirstName)
            .RuleFor(c => c.LastName, f => f.Person.LastName)
            .RuleFor(c => c.PhotoUrl, f => f.Person.Avatar)
            .Generate(10);
        Customers.ExecuteDelete();
        Customers.AddRange(customers);

        idsCounter = 0;
        var scooters = new Faker<Scooter>()
            .RuleFor(s => s.Id, _ => ++idsCounter)
            .RuleFor(s => s.Manufacturer, s => s.PickRandom(_manufacturers))
            .RuleFor(s => s.Model, f => f.Vehicle.Model())
            .RuleFor(s => s.ProductionDate, f => f.Date.PastDateOnly())
            .Generate(10);
        Scooters.ExecuteDelete();
        Scooters.AddRange(scooters);

        idsCounter = 0;
        var rentals = new Faker<Rental>()
            .RuleFor(r => r.Id, _ => ++idsCounter)
            .RuleFor(r => r.CustomerId, f => f.PickRandom(customers).Id)
            .RuleFor(r => r.ScooterId, f => f.PickRandom(scooters).Id)
            .RuleFor(r => r.FromLocation, f => f.PickRandom(GetLocationsExcept()))
            .RuleFor(r => r.ToLocation, (f, r) => f.PickRandom(GetLocationsExcept(r.FromLocation)))
            .RuleFor(r => r.StartDate, f => f.Date.PastOffset())
            .RuleFor(r => r.EndDate, (f, r) => f.Date.Between(r.StartDate.LocalDateTime, r.StartDate.AddHours(2).LocalDateTime))
            .Generate(10);
        Rentals.ExecuteDelete();
        Rentals.AddRange(rentals);

        idsCounter = 0;
        var maintenances = new Faker<Maintenance>()
            .RuleFor(m => m.Id, _ => ++idsCounter)
            .RuleFor(m => m.ScooterId, f => f.PickRandom(scooters).Id)
            .RuleFor(m => m.Description, f => f.Lorem.Sentence())
            .RuleFor(m => m.StartDate, f => f.Date.PastOffset())
            .RuleFor(m => m.EndDate, (f, m) => f.Date.Between(m.StartDate.LocalDateTime, m.StartDate.AddDays(7).LocalDateTime))
            .Generate(10);
        Rentals.ExecuteDelete();
        Maintenances.AddRange(maintenances);

        SaveChanges();
    }
}