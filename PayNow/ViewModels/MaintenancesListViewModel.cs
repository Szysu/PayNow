using System;
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;
using PayNow.Infrastructure;
using PayNow.Infrastructure.Entities;
using PayNow.Views;
using ReactiveUI;

namespace PayNow.ViewModels;

public class MaintenancesListViewModel : ViewModelBase, IRoutableViewModel
{
    private readonly DatabaseContext _dbContext;
    public ObservableCollection<Maintenance> Maintenances => _dbContext.Maintenances.Local.ToObservableCollection();

    public MaintenancesListViewModel(IScreen screen, DatabaseContext dbContext)
    {
        HostScreen = screen;
        _dbContext = dbContext;
        _dbContext.Maintenances.Load();
    }

    public string UrlPathSegment => "Maintenances";
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