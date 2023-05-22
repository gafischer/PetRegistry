using PetRegistry.Domain.Entities.Base;
using System.Diagnostics.CodeAnalysis;

namespace PetRegistry.Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public class User : BaseEntity
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string? PasswordHash { get; set; }
        public string? VerifyCode { get; set; }
        public DateTime? VerifyCodeExpiration { get; set; }
        public string? ResetPasswordToken { get; set; }
        public DateTime? ResetPasswordTokenExpiration { get; set; }
        public DateTime? LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public DateTime? LastSignIn { get; set; }
    }
}
