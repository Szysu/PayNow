using System;
using PayNow.ViewModels;
using PayNow.Views;
using ReactiveUI;

namespace PayNow;

public class ViewLocator : IViewLocator
{
    public IViewFor? ResolveView<T>(T? viewModel, string? contract = null) => viewModel switch
    {
        MainWindowViewModel context => new MainWindow { DataContext = context },
        ScootersListViewModel context => new ScootersListView { DataContext = context },
        RentalsListViewModel context => new RentalsListView { DataContext = context },
        MaintenancesListViewModel context => new MaintenancesListView { DataContext = context },
        CustomersListViewModel context => new CustomersListView { DataContext = context },
        _ => throw new ArgumentException($"Unknown ViewModel: {nameof(viewModel)}")
    };
}