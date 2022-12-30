using AutoMapper;
using KAS.ECOS.API.Policy;
using KAS.ECOS.SERVICE.DTOs.EndUser;
using KAS.ECOS.SERVICE.Services;
using KAS.Entity.DB.ECOS.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KAS.ECOS.API.Controllers
{
    [Route("api/EndUser")]
    [ApiController]
    public class EndUsers : ControllerBase
    {
        private readonly IEndUserService _userService;
        private readonly IMapper _mapper;

        public EndUsers(IEndUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<GetEndUserDTO>> GetEndUsers()
        {
            var endUserList = _userService.GetEndUsers();
            return Ok(_mapper.Map<IEnumerable<GetEndUserDTO>>(endUserList));
        }

        //[UserAuthorize("ECOS_ENDUSER_CREATE")]
        [HttpPost]
        public async Task<ActionResult> AddEndUsers(AddEndUserDTO user)
        {
            try
            {
                if (_userService.UserEmailExist(user.Email) == null)
                {
                    return BadRequest("This email is already used!");
                }

                if (user.Password != user.PasswordConfirmed)
                {
                    return BadRequest("Password Confirm is not correct!");
                }

                var newUser = _mapper.Map<EndUserList>(user);
                var result = await _userService.AddEndUser(newUser, user.Password);

                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors);
                }

                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [UserAuthorize("ECOS_ENDUSER_CREATE")]
        [HttpPost("Organization")]
        public async Task<ActionResult> AddEndUserInitOrg(AddEndUserDTO user)
        {
            try
            {

                if (_userService.UserEmailExist(user.Email) == null)
                {
                    return BadRequest("This email is already used!");
                }

                if (user.Password != user.PasswordConfirmed)
                {
                    return BadRequest("Password Confirm is not correct!");
                }

                if (!_userService.OrganizationExist(user.OrganizationId))
                {
                    return BadRequest("This organization is not exist!");
                }

                var newUser = _mapper.Map<EndUserList>(user);
                var result = await _userService.AddEndUser(newUser, user.Password, user.OrganizationId, true);
                
                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors);
                }

                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [UserAuthorize("ECOS_ENDUSER_UPDATE")]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateEndUser(string id, UpdateEndUserDTO updateUser)
        {
            if(_userService.EndUserExist(id) == null)
            {
                return NotFound();
            }

            var existedUser = await _userService.GetEndUserById(id);

            if (existedUser == null)
            {
                return BadRequest("This user is not exist!");
            }

            if (updateUser.Email != existedUser.Email && _userService.UserEmailExist(updateUser.Email) != null)
            {
                return BadRequest("This email is already used!");
            }

            if (updateUser.Password != null)
            {
                if (updateUser.Password != updateUser.PasswordConfirmed)
                {
                    return BadRequest("Password Confirm is not correct!");
                }

                existedUser.PasswordHash = _userService.HashPassword(existedUser, updateUser.Password);
            }

            var result = await _userService.UpdateEndUser(existedUser);

            if (!result.Succeeded)
            {
                return BadRequest();
            }

            return NoContent();
        }
    }
}
