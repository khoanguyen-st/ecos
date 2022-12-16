using AutoMapper;
using KAS.ECOS.API.Entity;
using KAS.ECOS.SERVICE.DTOs.Application;
using KAS.ECOS.SERVICE.Services;
using KAS.Entity.DB.ECOS.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KAS.ECOS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Authorization : ControllerBase
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IMapper _mapper;

        public Authorization(IAuthorizationService authorizationService, IMapper mapper)
        {
            _authorizationService = authorizationService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetApplications([FromQuery] AuthorizationDto rqParams)
        {
            return Ok(_authorizationService.CheckUserPermission(rqParams));
        }
    }
}
