using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;
using PayNow.Infrastructure;
using PayNow.Infrastructure.Entities;
using ReactiveUI;

namespace PayNow.ViewModels;

public class RentalsListViewModel : ViewModelBase, IRoutableViewModel
{
    private readonly DatabaseContext _dbContext;
    public ObservableCollection<Rental> Rentals => _dbContext.Rentals.Local.ToObservableCollection();

    public RentalsListViewModel(IScreen screen, DatabaseContext dbContext)
    {
        HostScreen = screen;
        _dbContext = dbContext;
        _dbContext.Rentals.Load();
    }

    public string UrlPathSegment => "Rentals";
    public IScreen HostScreen { get; }
}