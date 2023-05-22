namespace PetRegistry.Domain.Configuration
{
    public class SecurityConfiguration
    {
        public Jwt? Jwt { get; set; }
    }

    public class Jwt
    {
        public string SecretKey { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
    }
}
