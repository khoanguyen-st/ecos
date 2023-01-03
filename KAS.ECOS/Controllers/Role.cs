using AutoMapper;
using KAS.ECOS.API.Entity;
using KAS.ECOS.API.Policy;
using KAS.ECOS.SERVICE.Services;
using KAS.Entity.DB.ECOS.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace KAS.ECOS.API.Controllers
{
    [Route("api/Role")]
    [ApiController]
    public class Role : ControllerBase
    {
        private readonly IMapper _mapper; 
        private readonly IRoleService _roleService;

        public Role(IMapper mapper, IRoleService roleService)
        {
            _mapper = mapper;
            _roleService = roleService;
        }

        [UserAuthorize("ECOS_ROLE_CREATE")]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_roleService.GetRoleLists());
        }
        
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var role = _roleService.GetRoleById(id);
            if (role == null)
            {
                return NotFound();
            }
            var mapper = _mapper.Map<GetRoleListDto>(role);
            
            return Ok(mapper);  
        }

        [UserAuthorize("ECOS_ROLE_CREATE")]
        [HttpPost]
        public async Task<ActionResult<RoleList>> Post(AddRoleListDto roles)
        {
            var mapper = _mapper.Map<RoleList>(roles);

            try
            {
                await _roleService.CreateRoleList(mapper, roles.Permissions);
                return CreatedAtAction("Get", new { id = mapper.Id }, mapper);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [UserAuthorize("ECOS_ROLE_UPDATE")]
        [HttpPut("{id}")]
        public async  Task<IActionResult> Put(Guid id, UpdateRoleListDto roleList)
        {
            try
            {
                await _roleService.UpdateRoleList(roleList, id, roleList.Permissions);
                return NoContent();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [UserAuthorize("ECOS_ROLE_DELETE")]
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _roleService.DeleteRoleList(id);
                return NoContent();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
