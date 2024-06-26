using Microsoft.Extensions.DependencyInjection;
using PayNow.Infrastructure;
using PayNow.ViewModels;

namespace PayNow.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddViewModels(this IServiceCollection services)
    {
        services.AddTransient<MainWindowViewModel>();
        services.AddTransient<ScootersListViewModel>();
        services.AddTransient<RentalsListViewModel>();
    }

    public static void AddDatabase(this IServiceCollection services)
    {
        services.AddDbContext<DatabaseContext>(ServiceLifetime.Transient);
    }
}