using AutoMapper;
using KAS.ECOS.API.Policy;
using KAS.ECOS.SERVICE.DTOs.Application;
using KAS.ECOS.SERVICE.DTOs.Authorization;
using KAS.ECOS.SERVICE.Services;
using KAS.Entity.DB.ECOS.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KAS.ECOS.API.Controllers
{
    [Route("api/Authorization")]
    [ApiController]
    public class Authorization : ControllerBase
    {
        private readonly SERVICE.Services.IAuthorizationService _authorizationService;

        public Authorization(SERVICE.Services.IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetApplications([FromQuery] AuthorizationDto rqParams)
        {
            return Ok(_authorizationService.CheckUserPermission(rqParams));
        }

        [HttpGet("GetListPermissions")]
        public IActionResult GetApplicationPermissions([FromQuery] GetListPermissionsDto rqParams)
        {
            return Ok(_authorizationService.ListPermission(rqParams));
        }
    }
}
