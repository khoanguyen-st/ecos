using AutoMapper;
using KAS.ECOS.SERVICE.DTOs.OrganizationUser;
using KAS.Entity.DB.ECOS.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;

namespace KAS.ECOS.API.Controllers
{
    [Route("api/OrganizationUser")]
    [ApiController]
    public class OrganizationUser : ControllerBase
    {
        private readonly ECOSContext _context;
        private readonly IMapper _mapper;

        public OrganizationUser(ECOSContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<OrganizationUserList>> GetOrganizationUsers()
        {
            var organizationUserList = _context.OrganizationUserLists
                .AsNoTracking()
                .Include(u => u.EndUser)
                .Include(u => u.Organization)
                .ToList();
            return Ok(organizationUserList);
        }

        [HttpPost]
        public async Task<ActionResult> AddOrganizationUser(AddOrganizationUserDTO organizationUserDto)
        {
            try
            {
                var newOrganizationUser = _mapper.Map<OrganizationUserList>(organizationUserDto);
                _context.OrganizationUserLists.Add(newOrganizationUser);
                await _context.SaveChangesAsync();

                return CreatedAtRoute("", newOrganizationUser);
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
    }
}
