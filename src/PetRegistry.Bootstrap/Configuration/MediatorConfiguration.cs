using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PetRegistry.Application.Commands.Pets.CreatePet;
using PetRegistry.Application.Commands.Pets.UpdatePet;

namespace PetRegistry.Bootstrap.Configuration
{
    public static class MediatorConfiguration
    {
        public static IServiceCollection ConfigureMediatorServices(
            this IServiceCollection services)
        {
           services.AddMediatR(Assembly.GetExecutingAssembly());           

            return services;
        }
    }
}
