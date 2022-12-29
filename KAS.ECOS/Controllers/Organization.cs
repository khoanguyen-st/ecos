using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using KAS.ECOS.API.Entity;
using KAS.ECOS.API.Policy;
using KAS.ECOS.SERVICE.Services;
using KAS.Entity.DB.ECOS;
using KAS.Entity.DB.ECOS.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DbUpdateException = System.Data.Entity.Infrastructure.DbUpdateException;

namespace KAS.ECOS.API.Controllers
{
    [Route("api/Organization")]
    [ApiController]
    public class Organization : ControllerBase
    {
        private readonly ECOSContext _context;
        private readonly IMapper _mapper;
        private readonly IOrganizationService _organizationService;

        public Organization(ECOSContext context, IMapper mapper, IOrganizationService organizationService)
        {
            _context = context;
            _mapper = mapper;
            _organizationService = organizationService;
        }

        // GET: api/Organization
        [UserAuthorize("ECOS_ORGANIZATION_READ")]
        [HttpGet]
        public IActionResult Get()
        {
            var mapper = _organizationService.GetOrganizationLists();

            return Ok(mapper);
        }

        // GET: api/Organization/5
        [UserAuthorize("ECOS_ORGANIZATION_READ")]
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var organization = _organizationService.GetOrganizationById(id);
            
            if (organization == null)
            {
                return NotFound();
            }
            
            var mapper = _mapper.Map<GetOrganizationListDto>(organization);

            return Ok(mapper);
        }

        //[UserAuthorize("ECOS_ORGANIZATION_CREATE")]
        [HttpPost]
        public async Task<ActionResult<OrganizationList>> Post(AddOrganizationListDto listOrganization)
        {
            var mapper = _mapper.Map<OrganizationList>(listOrganization);
            
            try
            {
                await _organizationService.CreateOrganizationList(mapper);
                return CreatedAtAction("Get", new { id = mapper.Id }, mapper);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // PUT: api/Organization/5
        [UserAuthorize("ECOS_ORGANIZATION_UPDATE")]
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, UpdateOrganizationListDto organizationList)
        {
            try
            {
                _organizationService.UpdateOrganizationList(organizationList, id);
                return NoContent();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // DELETE: api/Organization/5
        [UserAuthorize("ECOS_ORGANIZATION_DELETE")]
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _organizationService.DeleteOrganizationList(id);
                return NoContent();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
