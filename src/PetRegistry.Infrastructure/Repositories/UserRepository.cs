using Microsoft.EntityFrameworkCore;
using PetRegistry.Domain.Entities;
using PetRegistry.Domain.Interfaces;
using PetRegistry.Domain.Persistence;
using PetRegistry.Domain.Repositories.Base;

namespace PetRegistry.Domain.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(BaseDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<User?> GetUserByUsernameOrEmail(string usernameOrEmail)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == usernameOrEmail || u.Email == usernameOrEmail);
        }
    }
}
