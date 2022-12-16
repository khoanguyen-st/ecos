
using AutoMapper;
using KAS.ECOS.API.Entity;
using KAS.ECOS.SERVICE.Services;
using KAS.Entity.DB.ECOS.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DbUpdateException = System.Data.Entity.Infrastructure.DbUpdateException;

namespace KAS.ECOS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly ECOSContext _context;
        private readonly IMapper _mapper;
        private readonly IRoleService _roleService;

        public RoleController(ECOSContext context, IMapper mapper, IRoleService roleService)
        {
            _context = context;
            _mapper = mapper;
            _roleService = roleService;
        }
        
        // GET: api/Organization
        [HttpGet]
        public IActionResult Get()
        {
            var mapper = _mapper.Map<List<GetRoleListDto>>(_context.RoleLists.Include(u => u.Organization).ToList());

            return Ok(mapper);
        }

        // GET: api/Organization/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var role = _context.RoleLists.Include(u => u.Organization).FirstOrDefault(x => x.Id == id);
            
            if (role == null)
            {
                return NotFound();
            }
            
            var mapper = _mapper.Map<RoleList>(role);

            return Ok(mapper);  
        }

        [HttpPost]
        public async Task<ActionResult<RoleList>> Post([FromBody]AddRoleListDto roles)
        {
            var mapper = _mapper.Map<RoleList>(roles);
            
            _context.RoleLists.Add(mapper);
            
            try
            {
                await _context.SaveChangesAsync();
                // _roleService.SyncRoleApplicationFuntionPermissionList(_context, mapper, roles.Permissions);
            }
            catch (DbUpdateException)
            {
                throw;
            }
            
            return CreatedAtAction("Get", new { id = mapper.Id }, mapper);
           
        }
        

        // PUT: api/Organization/5
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, UpdateRoleListDto roleList)
        {
            var role = _context.RoleLists.Find(id);
            
            if (role == null)
            {
                return NotFound();
            }
            
            _mapper.Map(roleList, role);
            _context.SaveChanges();
            // _roleService.SyncRoleApplicationFuntionPermissionList(_context, mapper, roles.Permissions, true);
            return NoContent();
        }

        // DELETE: api/Organization/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var role = _context.RoleLists.Find(id);
            if (role == null)
            {
                return NotFound();
            }
            
            _context.RoleLists.Remove(role);
            _context.SaveChanges();
            
            return NoContent();
        }
    }
}
