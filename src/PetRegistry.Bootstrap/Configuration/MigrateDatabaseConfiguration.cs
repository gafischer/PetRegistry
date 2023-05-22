using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PetRegistry.Data.DatabaseContext;

namespace PetRegistry.Bootstrap.Configuration
{
    [ExcludeFromCodeCoverage]
    public static class MigrateDatabaseConfiguration
    {
        public static IServiceProvider MigrateDatabaseOnStartup(
            this IServiceProvider service)
        {
            using (var scope = service.CreateScope())
            {
                var services = scope.ServiceProvider;

                var context = services.GetRequiredService<BaseDbContext>();
                if (context.Database.GetPendingMigrations().Any())
                {
                    context.Database.Migrate();
                }
            }

            return service;
        }

    }
}
