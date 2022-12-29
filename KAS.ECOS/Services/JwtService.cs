using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using KAS.ECOS.SERVICE.DTOs.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace KAS.ECOS.API.Services
{
    public class JwtService
    {
        private readonly IConfiguration _configuration;
        private const int EXPIRATION_MINUTES = 30;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public AuthenticationResponseDTO CreateToken(IdentityUser user)
        {
            var expiration = DateTime.UtcNow.AddMinutes(EXPIRATION_MINUTES);

            var token = CreateJwtToken(
                CreateClaims(user),
                CreateSigningCredentials(),
                expiration);

            var tokenHandler = new JwtSecurityTokenHandler();

            return new AuthenticationResponseDTO
            {
                Token = tokenHandler.WriteToken(token),
                Expiration = expiration
            };
        }

        private JwtSecurityToken CreateJwtToken(IEnumerable<Claim> claims, SigningCredentials credentials, DateTime expiration) =>
            new(
                _configuration["Authentication:Issuer"],
                _configuration["Authentication:Audience"],
                claims,
                expires: expiration,
                signingCredentials: credentials
            );

        private static IEnumerable<Claim> CreateClaims(IdentityUser user) =>
            new[] {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("userId", user.Id),
                new Claim("email", user.Email)
            };

        private SigningCredentials CreateSigningCredentials() =>
            new(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_configuration["Authentication:SecretForKey"])
                ),
                SecurityAlgorithms.HmacSha256
            );
    }
}
