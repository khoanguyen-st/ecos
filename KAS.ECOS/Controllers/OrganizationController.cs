using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using KAS.ECOS.API.Entity;
using KAS.Entity.DB.ECOS;
using KAS.Entity.DB.ECOS.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DbUpdateException = System.Data.Entity.Infrastructure.DbUpdateException;

namespace KAS.ECOS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private readonly ECOSContext _context;
        private readonly IMapper _mapper;

        public OrganizationController(ECOSContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        // GET: api/Organization
        [HttpGet]
        public IActionResult Get()
        {
            var mapper = _mapper.Map<List<GetOrganizationListDto>>(_context.OrganizationLists.ToList());

            return Ok(mapper);
        }

        // GET: api/Organization/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var organization = _context.OrganizationLists.Find(id);
            
            if (organization == null)
            {
                return NotFound();
            }
            
            var mapper = _mapper.Map<GetOrganizationListDto>(organization);

            return Ok(mapper);
        }

        [HttpPost]
        public async Task<ActionResult<OrganizationList>> Post(AddOrganizationListDto listOrganization)
        {
            var mapper = _mapper.Map<OrganizationList>(listOrganization);
            
            _context.OrganizationLists.Add(mapper);
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw;
            }

            return CreatedAtAction("Get", new { id = mapper.Id }, listOrganization);
        }

        // PUT: api/Organization/5
        [HttpPut("{id}")]
        public IActionResult Put(string id, UpdateOrganizationListDto organizationList)
        {
            var organization = _context.OrganizationLists.Find(id);
            
            if (organization == null)
            {
                return NotFound();
            }
            
            _mapper.Map(organizationList, organization);
            _context.SaveChanges();
            
            return NoContent();
        }

        // DELETE: api/Organization/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var organization = _context.OrganizationLists.Find(id);
            if (organization == null)
            {
                return NotFound();
            }
            
            _context.OrganizationLists.Remove(organization);
            _context.SaveChanges();
            
            return NoContent();
        }
    }
}
