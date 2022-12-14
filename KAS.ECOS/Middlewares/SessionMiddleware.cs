using KAS.ECOS.API.Entity;
using KAS.Entity.DB.ECOS.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace KAS.ECOS.API.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class SessionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        private readonly ECOSContext _ECOSContext;

        public SessionMiddleware(RequestDelegate next, IConfiguration configuration, ECOSContext ECOSContext)
        {
            _next = next;
            _configuration = configuration;
            _ECOSContext = ECOSContext;
        }

        public async Task Invoke(HttpContext context)
        {
            var request = context.Request;
            if (!request.Path.Value.Contains("Login"))
            { 
                var stream = request.Body;
                var originalContent = new StreamReader(stream).ReadToEndAsync().Result;
                var requestBody = JsonConvert.DeserializeObject<RequestDTO>(originalContent)!;

                var principal = GetPrincipalFromExpiredToken(requestBody.accessToken)!;

                var Username = principal.Claims.Where(p => p.Type == "user").First().Value;

                var User = _ECOSContext.EndUserLists.Where(u => u.Username == Username).First();

                var userInOrganization = _ECOSContext.OrganizationUserLists.Where(o => o.EndUserId == User.Id && o.OrganizationId == requestBody.organizationId).FirstOrDefault();

                var roleList = new HashSet<string>();

                if (userInOrganization != null)
                {
                    var userRoles = _ECOSContext.EndUserRoleLists.Where(role => role.UserDeviceId == requestBody.deviceId && role.OrganizationUserId == userInOrganization.Id).ToList();

                    foreach (var userRole in userRoles)
                    {
                        var role = _ECOSContext.RoleLists.Where(r => r.Id == userRole.RoleId).FirstOrDefault()!;
                        roleList.Add(role.Id);
                    }
                }

                foreach (var role in roleList)
                {
                    var permissions = _ECOSContext.RoleApplicationFunctionPermissionLists.Where(permission => permission.RoleId == role).ToList();
                    foreach (var permission in permissions)
                    {
                        requestBody.permissions.Add(permission.Id);
                    }
                }

                var json = JsonConvert.SerializeObject(requestBody);
                var requestContent = new StringContent(json, Encoding.UTF8, "application/json");
                stream = await requestContent.ReadAsStreamAsync();
                request.Body = stream;
            }

            await _next(context);
        }

        private ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:SecretForKey"])),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;

        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    //public static class SessionMiddlewareExtensions
    //{
    //    public static IApplicationBuilder UseSessionMiddleware(this IApplicationBuilder builder)
    //    {
    //        return builder.UseMiddleware<SessionMiddleware>();
    //    }
    //}
}
