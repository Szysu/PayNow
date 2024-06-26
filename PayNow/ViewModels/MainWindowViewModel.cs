using System;
using Microsoft.Extensions.DependencyInjection;
using PayNow.Infrastructure;
using ReactiveUI;

namespace PayNow.ViewModels;

public partial class MainWindowViewModel : ViewModelBase, IScreen
{
    private readonly IServiceProvider _services;
    public RoutingState Router { get; } = new();

    public MainWindowViewModel(IServiceProvider serviceProvider)
    {
        _services = serviceProvider;
        NavigateToCustomersList();
    }

    public void NavigateToScootersList()
    {
        Router.Navigate.Execute(new ScootersListViewModel(this, _services.GetRequiredService<DatabaseContext>()));
    }

    public void NavigateToRentalsList()
    {
        Router.Navigate.Execute(new RentalsListViewModel(this, _services.GetRequiredService<DatabaseContext>()));
    }

    public void NavigateToMaintenancesList()
    {
        Router.Navigate.Execute(new MaintenancesListViewModel(this, _services.GetRequiredService<DatabaseContext>()));
    }

    public void NavigateToCustomersList()
    {
        Router.Navigate.Execute(new CustomersListViewModel(this, _services.GetRequiredService<DatabaseContext>()));
    }
}