using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using KAS.ECOS.SERVICE.DTOs.MomoConfig;
using KAS.Entity.DB.ECOS.Entities;

namespace KAS.ECOS.API.Controllers
{
    [Route("api/momo")]
    [ApiController]
    public class MomoConfig : ControllerBase
    {
        private readonly ECOSContext _context;
        private readonly IMapper _mapper;

        public MomoConfig(ECOSContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("GetConfig")]
        public ActionResult GetMomoConfig()
        {
            try
            {
                var token = Request.Headers.Authorization.ToString().Replace("Bearer ", string.Empty);

                var handler = new JwtSecurityTokenHandler();

                var jwt = handler.ReadJwtToken(token);

                var userId = jwt.Claims.FirstOrDefault(claim => claim.Type == "userId").Value;

                var momoConfig = _context.MomoConfigLists.Where(config => config.EndUserId == userId);

                return Ok(momoConfig);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public ActionResult AddMomoConfig(AddMomoConfigDto momoConfigDto)
        {
            try
            {
                if (!_context.Users.Any(u => u.Id == momoConfigDto.EndUserId))
                {
                    return BadRequest("This user is not exist!");
                }

                var momoConfig = _mapper.Map<MomoConfigList>(momoConfigDto);

                _context.MomoConfigLists.Add(momoConfig);

                _context.SaveChanges();

                return Ok(momoConfig);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
