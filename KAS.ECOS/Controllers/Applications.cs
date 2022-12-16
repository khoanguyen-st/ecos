using AutoMapper;
using KAS.ECOS.SERVICE.DTOs.Application;
using KAS.ECOS.SERVICE.Services;
using KAS.Entity.DB.ECOS.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KAS.ECOS.API.Controllers
{
    [Route("api/Application")]
    [ApiController]
    public class Applications : ControllerBase
    {
        private readonly IApplicationService _applicationService;
        private readonly IMapper _mapper;

        public Applications(IApplicationService applicationService, IMapper mapper)
        {
            _applicationService = applicationService;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ApplicationList>> GetApplications()
        {
            var applicationList = _applicationService.GetApplications();
            return Ok(_mapper.Map<IEnumerable<AddApplicationDTO>>(applicationList));
        }

        [HttpPost]
        public ActionResult CreateApplication(AddApplicationDTO application)
        {
            if(application == null)
            {
                return BadRequest("New application is null!");
            }

            var newApplication = _mapper.Map<ApplicationList>(application);
            _applicationService.AddApplication(newApplication);

            return CreatedAtRoute("", newApplication);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateApplication(string id, UpdateApplicationDTO application)
        {
            if(!_applicationService.ApplicationExist(id))
            {
                return NotFound();
            }

            var existedApplication = _applicationService.GetApplicationById(id);

            _mapper.Map(application, existedApplication);

            await _applicationService.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteApplication(string id)
        {
            if (!_applicationService.ApplicationExist(id))
            {
                return NotFound();
            }

            var existedApplication = _applicationService.GetApplicationById(id);
            
            _applicationService.DeleteApplication(existedApplication);
            await _applicationService.SaveChangesAsync();

            return NoContent();
        }
    }
}
