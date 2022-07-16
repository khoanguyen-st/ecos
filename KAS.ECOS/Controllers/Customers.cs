using KAS.API.MIDDEWARE.Entity;
using Microsoft.AspNetCore.Mvc;

namespace KAS.ECOS.API.Controllers
{
  
   // [Route("api/KAS/ECOS")]
    public partial class EcosController : Controller
    {


        [HttpPost("Customer/Register")]
        public async Task<OutEntity> Customer_Register(InEntity ie)
        {
            try
            {
                return new OutEntity("", "Token không hợp lệ");
            }
            catch
            {
                return new OutEntity("", "Hiện tại hệ thống đang bận, vui lòng đăng nhập lại sau");
            }
        }
    }
}
