using System;
using Flurl;
using Flurl.Http;
using NetTopologySuite.Geometries;
using Serilog;

namespace PayNow.Infrastructure.Entities;

public class Rental
{
    public int? Id { get; set; }
    public int CustomerId { get; set; }
    public int ScooterId { get; set; }
    public Point FromLocation { get; set; }
    public Point ToLocation { get; set; }
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }
    public virtual Customer Customer { get; set; }
    public virtual Scooter Scooter { get; set; }

    private string? _routeAsGeoJson;
    
    /// <summary>
    ///     Get the route as GeoJson from OpenRouteService.
    /// </summary>
    /// <returns>GeoJSON response or null, if the response was not successful</returns>
    public string? GetRouteAsGeoJson()
    {
        var baseUrl = Program.Configuration["OpenRouteService:BaseUri"];
        var apiKey = Program.Configuration["OpenRouteService:ApiKey"];

        if (string.IsNullOrWhiteSpace(baseUrl))
        {
            Log.Error("OpenRouteService:BaseUri or OpenRouteService:ApiKey is not configured. Please configure it in appsettings.json");
            return null;
        }

        try
        {
            return baseUrl.AppendPathSegment("v2/directions/cycling-electric")
                .SetQueryParam("api_key", apiKey)
                .SetQueryParam("start", $"{FromLocation.X},{FromLocation.Y}", true)
                .SetQueryParam("end", $"{ToLocation.X},{ToLocation.Y}", true)
                .GetStringAsync().GetAwaiter().GetResult();
        }
        catch (FlurlHttpException e)
        {
            Log.Error(e, "Error while fetching route from OpenRouteService");
            return null;
        }
    }
}