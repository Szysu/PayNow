using Avalonia.Controls;
using Avalonia.ReactiveUI;
using Mapsui;
using Mapsui.Animations;
using Mapsui.Layers;
using Mapsui.Nts.Providers;
using Mapsui.Providers;
using Mapsui.Styles;
using Mapsui.Tiling;
using PayNow.Infrastructure.Entities;
using PayNow.ViewModels;

namespace PayNow.Views;

public partial class RentalsListView : ReactiveUserControl<RentalsListViewModel>
{
    private readonly Layer _routeLayer = new();

    public RentalsListView()
    {
        InitializeComponent();
        SetupMap();
    }

    private void SetupMap()
    {
        MapControl.Map.CRS = "EPSG:3857";
        MapControl.Map.Layers.Add(OpenStreetMap.CreateTileLayer(), _routeLayer);

        _routeLayer.Style = new VectorStyle
        {
            Enabled = true,
            Line = new(Color.Red, 3)
        };
    }

    private void OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        var rental = (Rental) e.AddedItems[0]!;
        _routeLayer.DataSource = new ProjectingProvider(new GeoJsonProvider(rental.GetRouteAsGeoJson())
        {
            CRS = "EPSG:4326"
        })
        {
            CRS = "EPSG:3857"
        };
        MapControl.Map.Navigator.CenterOn(_routeLayer.DataSource.GetExtent()!.Centroid, 300, Easing.Linear);
        MapControl.Map.Navigator.ZoomToBox(_routeLayer.DataSource.GetExtent()!.Grow(300), MBoxFit.Fit, 300, Easing.Linear);
    }
}