using AutoMapper;
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

        [HttpPost]
        public ActionResult AddEndUsers(AddEndUserDTO user)
        {
            if(user == null)
            {
                return BadRequest("New user is null!");
            }

            if(_userService.UserEmailExist(user.Email))
            {
                return BadRequest("This email is already used!");
            }

            if(user.Password != user.PasswordConfirmed)
            {
                return BadRequest("Password Confirm is not correct!");
            }

            user.Password = _userService.HashPassword(user.Password);

            var newUser = _mapper.Map<EndUserList>(user);
            _userService.AddEndUser(newUser);
            return NoContent();
        }

        [HttpPost("Organization")]
        public ActionResult AddEndUserInitOrg(AddEndUserDTO user)
        {
            if (_userService.UserEmailExist(user.Email))
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

            user.Password = _userService.HashPassword(user.Password);

            var newUser = _mapper.Map<EndUserList>(user);
            _userService.AddEndUser(newUser, user.OrganizationId, true);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateEndUser(Guid id, UpdateEndUserDTO updateUser)
        {
            if(!_userService.EndUserExist(id))
            {
                return NotFound();
            }

            var existedUser = _userService.GetEndUserById(id);

            if (updateUser.Password != null)
            {
                if (updateUser.Password != updateUser.PasswordConfirmed)
                {
                    return BadRequest("Password Confirm is not correct!");
                }

                updateUser.Password = _userService.HashPassword(updateUser.Password);
            } else
            {
                updateUser.Password = existedUser.Password;
            }

            if(existedUser.Email != existedUser.Email && _userService.UserEmailExist(updateUser.Email))
            {
                return BadRequest("This email is already used!");
            }

            _mapper.Map(updateUser, existedUser);

            await _userService.SaveChangesAsync();

            return NoContent();
        }
    }
}
