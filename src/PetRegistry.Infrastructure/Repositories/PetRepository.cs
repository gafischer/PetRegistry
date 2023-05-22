using PetRegistry.Domain.Entities;
using PetRegistry.Domain.Interfaces;
using PetRegistry.Domain.Persistence;
using PetRegistry.Domain.Repositories.Base;

namespace PetRegistry.Domain.Repositories
{
    public class PetRepository : BaseRepository<Pet>, IPetRepository
    {
        public PetRepository(BaseDbContext dbContext) : base(dbContext)
        { 
        }
    }
}
