using PetRegistry.Domain.Entities;

namespace PetRegistry.Domain.Interfaces
{
    public interface ISecurityService
    {
        public string GenerateVerifyCode();
        public string GenerateResetPasswordToken(string email);
        public string GenerateHashedPassword(string password);
        public string GenerateSha256(string input);
        public string GenerateJwt(User user, DateTime expires);
        public bool ValidatePassword(string password, string passwordHash);
        public bool ValidateJwt(string jwt);
    }
}
