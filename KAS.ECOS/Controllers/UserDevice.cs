﻿using AutoMapper;
using KAS.ECOS.SERVICE.DTOs.OrganizationUser;
using KAS.ECOS.SERVICE.DTOs.UserDevice;
using KAS.Entity.DB.ECOS.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KAS.ECOS.API.Controllers
{
    [Route("api/UserDevice")]
    [ApiController]
    public class UserDevice : ControllerBase
    {
        private readonly ECOSContext _context;
        private readonly IMapper _mapper;

        public UserDevice(ECOSContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> AddUserDevice(AddUserDeviceDTO userDeviceDto)
        {
            try
            {
                var newUserDevice = _mapper.Map<UserDeviceList>(userDeviceDto);
                _context.UserDeviceLists.Add(newUserDevice);
                await _context.SaveChangesAsync();

                return CreatedAtRoute("", newUserDevice);
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
    }
}
