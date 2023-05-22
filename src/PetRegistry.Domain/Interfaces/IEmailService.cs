using PetRegistry.Domain.Entities;

namespace PetRegistry.Application.Common.Interfaces
{
    public interface IEmailService
    {
        Task SendRegisterEmailAsync(User user, string verifyCode);
        Task SendLockoutEmailAsync(User user);
    }
}
