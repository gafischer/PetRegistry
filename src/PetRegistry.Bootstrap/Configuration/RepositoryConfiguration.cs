using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using PetRegistry.Data.CQRS.Commands;
using PetRegistry.Data.CQRS.Queries;
using PetRegistry.Domain.CQRS.Commands;
using PetRegistry.Domain.CQRS.Queries;

namespace PetRegistry.Bootstrap.Configuration
{
    [ExcludeFromCodeCoverage]
    public static class RepositoryConfiguration
    {
        public static IServiceCollection ConfigureRepositoryServices(
            this IServiceCollection services)
        {
            services.AddScoped<IPetRepository, PetRepository>();
            services.AddScoped<IPetQueryRepository, PetQueryRepository>();

            return services;
        }
    }
}
