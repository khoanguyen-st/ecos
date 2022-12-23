using KAS.ECOS.API.Entity;
using KAS.Entity.DB.ECOS.Entities;
using KAS.ECOS.MIDDLEWARE.Entity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using KAS.ECOS.API.Services;

namespace KAS.ECOS.API.Controllers
{
    [ApiController]
    [Route("api/authentication")]
    public class Authentication : Controller
    {
        private readonly IAuthService _authService;

        public Authentication(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route(("Login"))]
        public ActionResult<string> LoginSession(LoginDTO account)
        {
            try
            {
                var validatedAccount = _authService.ValidateUser(account.email, account.password);
                if (validatedAccount == null)
                {
                    return Unauthorized();
                }
                var tokenToReturn = _authService.Authenticate(validatedAccount);
                return Ok(new
                {
                    token = tokenToReturn
                });
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
    }

}