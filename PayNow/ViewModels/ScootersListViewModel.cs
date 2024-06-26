using System;
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;
using PayNow.Infrastructure;
using PayNow.Infrastructure.Entities;
using PayNow.Views;
using ReactiveUI;

namespace PayNow.ViewModels;

public class ScootersListViewModel : ViewModelBase, IRoutableViewModel
{
    private readonly DatabaseContext _dbContext;
    public ObservableCollection<Scooter> Scooters => _dbContext.Scooters.Local.ToObservableCollection();

    public ScootersListViewModel(IScreen screen, DatabaseContext dbContext)
    {
        HostScreen = screen;
        _dbContext = dbContext;
        _dbContext.Scooters.Load();
    }

    public string UrlPathSegment => "Scooters";
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