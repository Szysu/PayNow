using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.ReactiveUI;
using PayNow.Infrastructure.Entities;
using PayNow.ViewModels;

namespace PayNow.Views;

public partial class CustomersListView : ReactiveUserControl<CustomersListViewModel>
{
    public CustomersListView()
    {
        InitializeComponent();
    }
    
    private void OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        ViewModel?.SaveChanges();
    }

    private void AddCustomer(object? sender, RoutedEventArgs e)
    {
        var newCustomer = new Customer();
        ViewModel?.Customers.Add(newCustomer);
        Grid.SelectedItem = newCustomer;
    }

    private void RemoveCustomer(object? sender, RoutedEventArgs e)
    {
        ViewModel?.Customers.Remove((Customer) Grid.SelectedItem);
        ViewModel?.SaveChanges();
    }
}