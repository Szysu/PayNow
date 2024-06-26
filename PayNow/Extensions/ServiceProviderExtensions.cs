using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PayNow.Infrastructure;

namespace PayNow.Extensions;

public static class ServiceProviderExtensions
{
    public static DatabaseContext MigrateDatabase(this IServiceProvider serviceProvider)
    {
        var dbContext = serviceProvider.GetRequiredService<DatabaseContext>();
        dbContext.Database.Migrate();

        return dbContext;
    }
}