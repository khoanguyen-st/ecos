using AutoMapper;
using KAS.ECOS.API.Services;
using KAS.ECOS.SERVICE.DTOs.ApplicationFuntion;
using KAS.ECOS.SERVICE.Mapping.ApplicationFunction;
using KAS.ECOS.SERVICE.Services;
using KAS.Entity.DB.ECOS.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace KAS.ECOS.API.Controllers
{
    [Route("api/ApplicationFunction")]
    [ApiController]
    public class ApplicationFunctions : ControllerBase
    {
        private readonly ApplicationFunctionService _applicationFunctionService;
        private readonly ECOSContext _context;
        private readonly IMapper _mapper;

        public ApplicationFunctions(ECOSContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _applicationFunctionService = new ApplicationFunctionService(context);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApplicationFunctionList>>> GetApplicationFunctions()
        {
            var functionList = await _applicationFunctionService.GetApplicationFunctions();
            return Ok(functionList);
        }
        [HttpPost]
        public async Task<ActionResult> AddApplicationFunction(AddApplicationFunctionDTO applicationFunction)
        {
            if(applicationFunction == null)
            {
                return BadRequest("Function is null!");
            }

            if(!await _applicationFunctionService.IsApplicationExist(applicationFunction.ApplicationId))
            {
                return NotFound();
            }

            var newApplicationFunction = _mapper.Map<ApplicationFunctionList>(applicationFunction);

            await _applicationFunctionService.AddApplicationFunction(applicationFunction.ApplicationId, newApplicationFunction);
            await _applicationFunctionService.SaveChangesAsync();

            return CreatedAtRoute("", newApplicationFunction);
        }
        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdateApplicationFunction(Guid id, JsonPatchDocument<UpdateApplicationFunctionDTO> applicationFunction)
        {
            var existedApplicationFunction = await _applicationFunctionService.GetApplicationFunction(id);

            if (existedApplicationFunction == null)
            {
                return NotFound("This function not found!");
            }

            //if(await _applicationFunctionService.IsApplicationExist(existedApplicationFunction.ApplicationId))
            //{
            //    return NotFound("This application not found!");
            //}

            var functionToPatch = _mapper.Map<UpdateApplicationFunctionDTO>(existedApplicationFunction);

            applicationFunction.ApplyTo(functionToPatch, ModelState);

            if(!ModelState.IsValid || !TryValidateModel(functionToPatch))
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(functionToPatch, existedApplicationFunction);

            await _applicationFunctionService.SaveChangesAsync();

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteApplicationFunction(Guid id)
        {
            if (await _applicationFunctionService.IsApplicationExist(id))
            {
                return NotFound("This function not found!");
            }

            var existedApplicationFunction = await _applicationFunctionService.GetApplicationFunction(id)!;

            _applicationFunctionService.DeleteApplicationFunction(existedApplicationFunction);
            await _applicationFunctionService.SaveChangesAsync();

            return NoContent();
        }

    }
}
