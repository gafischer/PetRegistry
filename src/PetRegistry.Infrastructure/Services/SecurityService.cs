using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PetRegistry.Domain.Configuration;
using PetRegistry.Domain.Entities;
using PetRegistry.Domain.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace PetRegistry.Infrastructure.Services
{
    public class SecurityService : ISecurityService
    {
        private readonly SecurityConfiguration _securityConfiguration;

        public SecurityService(IOptions<SecurityConfiguration> options)
        {
            _securityConfiguration = options.Value;
        }

        public string GenerateVerifyCode()
        {
            int codeLength = 6;
            RandomNumberGenerator rng = RandomNumberGenerator.Create();

            var bytes = new byte[codeLength];
            rng.GetBytes(bytes);

            var code = "";
            var usedDigits = new HashSet<int>();
            for (int i = 0; i < codeLength; i++)
            {
                int digit = bytes[i] % 10;
                while (usedDigits.Contains(digit))
                {
                    rng.GetBytes(bytes);
                    digit = bytes[0] % 10;
                }
                usedDigits.Add(digit);
                code += digit;
            }

            return code;
        }

        public string GenerateResetPasswordToken(string email)
        {
            var timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmssffff");
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
            }
            var combined = Encoding.UTF8.GetBytes(email + timestamp + Convert.ToBase64String(randomNumber));
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] data = sha256Hash.ComputeHash(combined);
                StringBuilder sBuilder = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }
                return sBuilder.ToString();
            }
        }

        public string GenerateHashedPassword(string password)
        {
            return HashPassword(password);
        }

        public bool ValidatePassword(string password, string passwordHash)
        {
            return Verify(password, passwordHash);
        }

        public string GenerateSha256(string input)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] data = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder sBuilder = new();

                for (int i = 0; i < data.Length; i++)
                    sBuilder.Append(data[i].ToString("x2"));

                return sBuilder.ToString();
            }
        }

        public string GenerateJwt(User user, DateTime expires)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_securityConfiguration.Jwt?.SecretKey!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Username!)
            };

            var token = new JwtSecurityToken(
                issuer: _securityConfiguration.Jwt?.Issuer!,
                audience: _securityConfiguration.Jwt?.Audience!,
                claims: claims,
                expires: expires,
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public bool ValidateJwt(string jwt)
        {
            var secretKey = _securityConfiguration.Jwt?.SecretKey!;
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = securityKey,
                ValidateIssuer = false,
                ValidateAudience = false
            };

            try
            {
                tokenHandler.ValidateToken(jwt, validationParameters, out var validatedToken);

                return true;
            }
            catch (SecurityTokenValidationException)
            {
                return false;
            }
        }
    }
}
