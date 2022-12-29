
using KAS.Entity.DB.ECOS.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using KAS.ECOS.API.Services;
using KAS.ECOS.API.Entity;
using System.Security.Cryptography;

namespace KAS.ECOS.SERVICE.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly ECOSContext _context;

        public AuthService(IConfiguration configuration, ECOSContext context)
        {
            _configuration = configuration;
            _context = context;
        }
        public string Authenticate(LoggedUserDTO validatedAccount)
        {
            var securityKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(_configuration["Authentication:SecretForKey"]));
            var signingCredentials = new SigningCredentials(
                securityKey, SecurityAlgorithms.HmacSha256);

            var claimsForToken = new List<Claim>();
            claimsForToken.Add(new Claim("userId", validatedAccount.Id.ToString()));

            var jwtSecurityToken = new JwtSecurityToken(
                _configuration["Authentication:Issuer"],
                _configuration["Authentication:Audience"],
                claimsForToken,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(3),
                signingCredentials);

            var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            return tokenToReturn;
        }
        //public LoggedUserDTO? ValidateUser(string Email, string Password)
        //{
        //    var user = _context.EndUserLists.Where(u => u.Email == Email).FirstOrDefault();

        //    if (user == null)
        //    {
        //        return null;
        //    }

        //    int keySize = Int32.Parse(_configuration["Security:KeySize"]);
        //    int iterations = Int32.Parse(_configuration["Security:Iterations"]);
        //    byte[] salt = Encoding.ASCII.GetBytes(_configuration["Security:Salt"]);
        //    HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;
        //    var passwordHash = user.Password;

        //    var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(Password, salt, iterations, hashAlgorithm, keySize);
        //    if (hashToCompare.SequenceEqual(Convert.FromHexString(passwordHash)))
        //    {
        //        return new LoggedUserDTO(user.Id, user.Username, user.Email);
        //    }

        //    return null;
        //}
    }
}
