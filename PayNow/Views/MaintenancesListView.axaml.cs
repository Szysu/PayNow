using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.ReactiveUI;
using PayNow.Infrastructure.Entities;
using PayNow.ViewModels;

namespace PayNow.Views;

public partial class MaintenancesListView : ReactiveUserControl<MaintenancesListViewModel>
{
    public MaintenancesListView()
    {
        InitializeComponent();
    }

    private void OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        ViewModel?.SaveChanges();
    }

    private void AddMaintenance(object? sender, RoutedEventArgs e)
    {
        var newMaintenance = new Maintenance();
        ViewModel?.Maintenances.Add(newMaintenance);
        Grid.SelectedItem = newMaintenance;
    }

    private void RemoveMaintenance(object? sender, RoutedEventArgs e)
    {
        ViewModel?.Maintenances.Remove((Maintenance) Grid.SelectedItem);
        ViewModel?.SaveChanges();
    }
}