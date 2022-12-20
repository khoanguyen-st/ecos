using AutoMapper;
using KAS.ECOS.API.Entity;
using KAS.ECOS.SERVICE.Services;
using KAS.Entity.DB.ECOS.Entities;
using Microsoft.AspNetCore.Mvc;

namespace KAS.ECOS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IMapper _mapper; 
        private readonly IRoleService _roleService;

        public RoleController(IMapper mapper, IRoleService roleService)
        {
            _mapper = mapper;
            _roleService = roleService;
        }
        
        // GET: api/Organization
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_roleService.GetRoleLists());
        }

        // GET: api/Organization/5
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
        

        // PUT: api/Organization/5
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, UpdateRoleListDto roleList)
        {
            try
            {
                _roleService.UpdateRoleList(roleList, id, roleList.Permissions);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        // DELETE: api/Organization/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _roleService.DeleteRoleList(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
    }
}
