using KAS.API.MIDDEWARE.Entity;
using KAS.ECOS.API.Entity;
using KAS.Entity.DB.ECOS.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KAS.ECOS.API.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class Authentication : ControllerBase
    {
        private readonly ECOSContext _context;

        public Authentication(ECOSContext context)
        {
            _context = context;
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
            return Ok();
        }

        private OutEntity ValidateUser(string userName, string password)
        {
            var user = _context.EndUserLists.Where(u => u.Username == userName).FirstOrDefault();
            if (user == null)
            {
                return new OutEntity("", "Không tồn tại tài khoản");
            }

            if(user.Password == password)
            {
                return new OutEntity(user, "");
            }

            return new OutEntity("", "Mật khẩu không hợp lệ!");
        }
    }
}
