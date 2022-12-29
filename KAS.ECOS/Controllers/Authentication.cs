using System.Security.Claims;
using KAS.ECOS.API.Entity;
using KAS.Entity.DB.ECOS.Entities;
using KAS.ECOS.MIDDLEWARE.Entity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using KAS.ECOS.API.Services;
using Microsoft.AspNetCore.Identity;

namespace KAS.ECOS.API.Controllers
{
    [ApiController]
    [Route("api/authentication")]
    public class Authentication : Controller
    {
        private readonly IAuthService _authService;
        private readonly UserManager<EndUserList> _userManager;
        private readonly JwtService _jwtService;

        public Authentication(IAuthService authService, UserManager<EndUserList> userManager, JwtService jwtService)
        {
            _authService = authService;
            _userManager = userManager;
            _jwtService = jwtService;
        }

        [HttpPost]
        [Route(("Login"))]
        public async Task<ActionResult<string>> LoginSession(LoginDTO account)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(account.email);

                if (user == null)
                {
                    return BadRequest("Bad credentials");
                }

                var isPasswordValid = await _userManager.CheckPasswordAsync(user, account.password);

                if (!isPasswordValid)
                {
                    return BadRequest("Bad credentials");
                }

                var token = _jwtService.CreateToken(user);

                return Ok(token);
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
    }

}