using KAS.ECOS.SERVICE.Services;
using KAS.Entity.DB.ECOS.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KAS.ECOS.API.Controllers
{
    [Route("api/application")]
    [ApiController]
    public class Applications : ControllerBase
    {
        private readonly IApplicationService _applicationService;

        public Applications(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ApplicationList>> GetApplications()
        {
            var applicationList = _applicationService.GetApplications();
            return Ok(applicationList);
        }

    }
}
