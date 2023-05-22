using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetRegistry.Application.Common.Interfaces;
using PetRegistry.Domain.Configuration;
using PetRegistry.Domain.Interfaces;
using PetRegistry.Domain.Interfaces.Base;
using PetRegistry.Domain.Persistence;
using PetRegistry.Domain.Repositories;
using PetRegistry.Domain.Repositories.Base;
using PetRegistry.Domain.Services;
using PetRegistry.Infrastructure.Services;

namespace PetRegistry.Domain
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<BaseDbContext>(options =>
            {
                options.UseNpgsql(connectionString).UseSnakeCaseNamingConvention();
            });

            services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IPetRepository, PetRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ISecurityService, SecurityService>();

            services.Configure<EmailConfiguration>(x => configuration.GetSection("EmailConfiguration").Bind(x));
            services.Configure<SecurityConfiguration>(x => configuration.GetSection("SecurityConfiguration").Bind(x));

            return services;
        }

        public static IServiceProvider MigrateDatabaseOnStartup(this IServiceProvider service)
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
