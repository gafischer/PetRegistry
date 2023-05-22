using PetRegistry.Domain.Entities;
using PetRegistry.Domain.Interfaces.Base;

namespace PetRegistry.Domain.Interfaces
{
    public interface IPetRepository : IAsyncRepository<Pet>
    {
    }
}
