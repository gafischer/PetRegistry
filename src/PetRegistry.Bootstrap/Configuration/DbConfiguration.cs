using System.Data;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using PetRegistry.Data.DatabaseContext;

namespace PetRegistry.Bootstrap.Configuration
{
    public static class DbConfiguration
    {
        [ExcludeFromCodeCoverage]
        public static void AddPostgreSQL(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<BaseDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });

            services.AddTransient<IDbConnection>(options =>
                new NpgsqlConnection(connectionString));
        }
    }
}
