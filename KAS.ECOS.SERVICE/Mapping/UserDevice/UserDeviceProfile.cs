using AutoMapper;
using KAS.ECOS.SERVICE.DTOs.UserDevice;
using KAS.Entity.DB.ECOS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAS.ECOS.SERVICE.Mapping.UserDevice
{
    public class UserDeviceProfile : Profile
    {
        public UserDeviceProfile() 
        {
            CreateMap<AddUserDeviceDTO, UserDeviceList>().ReverseMap();    
        }
    }
}
