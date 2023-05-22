using System.Data;
using Dapper;
using PetRegistry.Data.CQRS.Queries.TSQL;
using PetRegistry.Domain.CQRS.Queries;
using PetRegistry.Domain.Entities.Concrete;

namespace PetRegistry.Data.CQRS.Queries
{
    public class PetQueryRepository : IPetQueryRepository
    {
        private readonly IDbConnection _dbConnection;

        public PetQueryRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<Pet>> GetPetsAsync()
        {
            var query = PetQueries.GetAllPets;
            var pets = await _dbConnection.QueryAsync<Pet>(query);

            return pets;
        }

        public async Task<Pet> GetPetByIdAsync(int id)
        {
            var query = PetQueries.GetPetById;
            var pet = await _dbConnection.QueryFirstOrDefaultAsync<Pet>(query, new { @Id = id });

            return pet;
        }
    }
}
