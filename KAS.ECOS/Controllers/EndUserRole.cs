using AutoMapper;
using KAS.ECOS.API.Services;
using KAS.ECOS.SERVICE.DTOs.GrantUser;
using KAS.ECOS.SERVICE.Services;
using KAS.Entity.DB.ECOS.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KAS.ECOS.API.Controllers
{
    [Route("api/GrantUser")]
    [ApiController]
    public class EndUserRole : ControllerBase
    {
        private readonly IEndUserRoleService _grantUserService;
        private readonly ECOSContext _context;
        private readonly IMapper _mapper;

        public EndUserRole(ECOSContext context, IMapper mapper, IEndUserRoleService grantUserService)
        {
            _context = context;
            _mapper = mapper;
            _grantUserService = grantUserService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EndUserRoleList>>> GetEndUserRole()
        {
            try
            {
                var userRoleList = await _grantUserService.GetEndUserRoles();
                return Ok(userRoleList);
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddEndUserRole(AddEndUserRoleDTO endUserRole)
        {
            try
            {
                if (!await _grantUserService.IsDeviceExist(endUserRole.UserDeviceId))
                {
                    return NotFound("Device is not found!");
                }

                if (!await _grantUserService.IsOrganizationUserExist(endUserRole.OrganizationUserId))
                {
                    return NotFound("Organization user is not found!");
                }

                if (!await _grantUserService.IsRoleExist(endUserRole.RoleId))
                {
                    return NotFound("Role is not found!");
                }

                var newEndUserRole = _mapper.Map<EndUserRoleList>(endUserRole);
                _grantUserService.AddEndUserRole(newEndUserRole);
                await _grantUserService.SaveChangesAsync();

                return CreatedAtRoute("", newEndUserRole);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEndUserRole(Guid id)
        {
            if (await _grantUserService.IsEndUserRoleExist(id))
            {
                return NotFound("This user role is not found!");
            }

            var existedUserRole = await _grantUserService.GetEndUserRole(id)!;
            _grantUserService.DeleteEndUserRole(existedUserRole);
            await _grantUserService.SaveChangesAsync();

            return NoContent();
        }
    }
}
