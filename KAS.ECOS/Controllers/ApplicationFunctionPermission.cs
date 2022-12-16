using AutoMapper;
using KAS.ECOS.API.Services;
using KAS.ECOS.SERVICE.DTOs.ApplicationFunctionPermission;
using KAS.ECOS.SERVICE.DTOs.ApplicationFuntion;
using KAS.ECOS.SERVICE.Services;
using KAS.Entity.DB.ECOS.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;

namespace KAS.ECOS.API.Controllers
{
    [Route("api/ApplicationFunctionPermission")]
    [ApiController]
    public class ApplicationFunctionPermission : ControllerBase
    {
        private readonly ApplicationFunctionPermissionService _applicationFunctionPermissionService;
        private readonly ECOSContext _context;
        private readonly IMapper _mapper;

        public ApplicationFunctionPermission(ECOSContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _applicationFunctionPermissionService = new ApplicationFunctionPermissionService(context);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApplicationFunctionPermissionList>>> GetApplicationFunctionPermissions()
        {
            var functionList = await _applicationFunctionPermissionService.GetApplicationFunctionPermissions();
            return Ok(functionList);
        }
        [HttpPost]
        public async Task<ActionResult> AddApplicationFunctionPermission(AddApplicationFunctionPermissionDTO functionPermission)
        {
            if (functionPermission == null)
            {
                return BadRequest("Function is null!");
            }

            if (!await _applicationFunctionPermissionService.IsApplicationFunctionExist(functionPermission.ApplicationFunctionId))
            {
                return NotFound();
            }

            var newApplicationFunction = _mapper.Map<ApplicationFunctionPermissionList>(functionPermission);

            await _applicationFunctionPermissionService.AddApplicationFunctionPermission(functionPermission.ApplicationFunctionId, newApplicationFunction);
            await _applicationFunctionPermissionService.SaveChangesAsync();

            return CreatedAtRoute("", newApplicationFunction);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateApplicationFunctionPermission(Guid id, UpdateApplicationFunctionPermissionDTO functionPermission)
        {
            if (!await _applicationFunctionPermissionService.IsApplicationFunctionPermissionExist(id))
            {
                return NotFound();
            }

            var existedPermission = await _applicationFunctionPermissionService.GetApplicationFunctionPermission(id);

            _mapper.Map(functionPermission, existedPermission);

            await _applicationFunctionPermissionService.SaveChangesAsync();

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteApplicationFunctionPermission(Guid id)
        {
            if (!await _applicationFunctionPermissionService.IsApplicationFunctionPermissionExist(id))
            {
                return NotFound();
            }

            var existedPermission = await _applicationFunctionPermissionService.GetApplicationFunctionPermission(id);
            _applicationFunctionPermissionService.DeleteApplicationFunction(existedPermission);

            await _applicationFunctionPermissionService.SaveChangesAsync();

            return NoContent();
        }
    }
}
