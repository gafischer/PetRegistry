using AutoMapper;
using PetRegistry.Data.DatabaseContext;
using PetRegistry.Data.Entities;
using PetRegistry.Domain.CQRS.Commands;
using PetRegistry.Domain.Entities.Concrete;

namespace PetRegistry.Data.CQRS.Commands
{
    public class PetRepository : IPetRepository
    {

        private readonly BaseDbContext _dbContext;
        private readonly IMapper _mapper;

        public PetRepository(BaseDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<int> CreatePetAsync(Pet pet)
        {
            var petEntity = new PetEntity
            {
                Id = pet.Id != 0 ? pet.Id : 0,
                Name = pet.Name,
                Breed = pet.Breed,
                Sex = pet.Sex,
                Description = pet.Description,
                Neutered = pet.Neutered,
                NeuterDate = pet.NeuterDate,
                Specie = pet.Specie,
                Weight = pet.Weight,
                Color = pet.Color,
                BirthDate = pet.BirthDate
            };

            await _dbContext.Pets.AddAsync(petEntity);
            await _dbContext.SaveChangesAsync();

            return petEntity.Id;
        }
       
        public async Task<Pet> UpdatePetAsync(Pet pet)
        {
            var petEntity = _mapper.Map<PetEntity>(pet);

            _dbContext.Update(petEntity);
            await _dbContext.SaveChangesAsync();

            return pet;
        }
    }
}
