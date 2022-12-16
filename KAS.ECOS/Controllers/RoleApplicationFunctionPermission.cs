using AutoMapper;
using KAS.ECOS.SERVICE.DTOs.RoleApplicationPermission;
using KAS.ECOS.SERVICE.DTOs.UserDevice;
using KAS.Entity.DB.ECOS.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KAS.ECOS.API.Controllers
{
    [Route("api/RoleApplicationFunctionPermission")]
    [ApiController]
    public class RoleApplicationFunctionPermission : ControllerBase
    {
        private readonly ECOSContext _context;
        private readonly IMapper _mapper;

        public RoleApplicationFunctionPermission(ECOSContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> AddUserDevice(AddRoleApplicationFunctionPermissionDTO roleFunctionPermission)
        {
            try
            {
                var newRoleFunctionPermission = _mapper.Map<RoleApplicationFunctionPermissionList>(roleFunctionPermission);
                _context.RoleApplicationFunctionPermissionLists.Add(newRoleFunctionPermission);
                await _context.SaveChangesAsync();

                return CreatedAtRoute("", newRoleFunctionPermission);
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
    }
}
