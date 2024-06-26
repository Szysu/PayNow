using System;
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;
using PayNow.Infrastructure;
using PayNow.Infrastructure.Entities;
using PayNow.Views;
using ReactiveUI;

namespace PayNow.ViewModels;

public class CustomersListViewModel : ViewModelBase, IRoutableViewModel
{
    private readonly DatabaseContext _dbContext;
    public ObservableCollection<Customer> Customers => _dbContext.Customers.Local.ToObservableCollection();

    public CustomersListViewModel(IScreen screen, DatabaseContext dbContext)
    {
        HostScreen = screen;
        _dbContext = dbContext;
        _dbContext.Customers.Load();
    }

    public string UrlPathSegment => "Ccustomers";
    public IScreen HostScreen { get; }

    public void SaveChanges()
    {
        try
        {
            var changes = _dbContext.SaveChanges();

            if (changes > 0)
            {
                Dialog.Instance.AddSuccess("Changes saved successfully.");
            }
        }
        catch (Exception e)
        {
            Dialog.Instance.AddError(e.InnerException?.Message ?? "An error occurred.");
        }
    }
}