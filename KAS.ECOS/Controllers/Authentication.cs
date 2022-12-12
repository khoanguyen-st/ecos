using KAS.ECOS.API.Entity;
using KAS.Entity.DB.ECOS.Entities;
using KAS.ECOS.MIDDLEWARE.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace KAS.ECOS.API.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class Authentication : ControllerBase
    {
        private readonly ECOSContext _context;
        private readonly IConfiguration _configuration;

        public Authentication(ECOSContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<string> Get()
        {
            return "ok";
        }

        [HttpPost("Login")]
        public ActionResult<string> Login(InEntity ie)
        {
            var account = ie.getData<LoginDTO>();

            var validatedAccount = ValidateUser(account.UserName, account.Password);

            if (validatedAccount == null)
            {
                return Unauthorized();
            }

            var securityKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(_configuration["Authentication:SecretForKey"]));
            var signingCredentials = new SigningCredentials(
                securityKey, SecurityAlgorithms.HmacSha256);

            var claimsForToken = new List<Claim>();
            claimsForToken.Add(new Claim("user", validatedAccount.UserName.ToString()));

            var jwtSecurityToken = new JwtSecurityToken(
                _configuration["Authentication:Issuer"],
                _configuration["Authentication:Audience"],
                claimsForToken,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(1),
                signingCredentials);

            var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            var refreshToken = GenerateRefreshToken();

            return Ok(new
            {
                Token = tokenToReturn,
                RefreshToken = refreshToken
            });
        }

        [HttpPost("RefreshToken")]
        public ActionResult<string> RefreshToken(InEntity ie)
        {
            var tokens = ie.getData<RefreshTokenDTO>();

            if(tokens == null)
            {
                return BadRequest("Invalid client request");
            }

            string accessToken = tokens.AccessToken;
            string refreshToken = tokens.RefreshToken;

            var principal = GetPrincipalFromExpiredToken(accessToken);
            if(principal == null)
            {
                return BadRequest("Invalid access token or refresh token");
            }
            return Ok();
        }

        private LoginDTO ValidateUser(string userName, string password)
        {
            //var user = _context.EndUserLists.Where(u => u.Username == userName && u.Password == password).FirstOrDefault();
            //return new LoginDTO { UserName = userName, Password = password };
            return new LoginDTO("khoanguyen", "123123");
        }

        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
        {
            var tokenValidationParams = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(_configuration["Authentication:SecretForKey"]!)),
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParams, out SecurityToken securityToken);

            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }
    }

}