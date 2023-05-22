using PetRegistry.Domain.Entities;
using PetRegistry.Domain.Interfaces.Base;

namespace PetRegistry.Domain.Interfaces
{
    public interface IUserRepository : IAsyncRepository<User>
    {
        Task<User?> GetUserByUsernameOrEmail(string usernameOrEmail);
    }
}
