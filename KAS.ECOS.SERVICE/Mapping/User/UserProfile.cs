using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using KAS.ECOS.SERVICE.DTOs.EndUser;

namespace KAS.ECOS.SERVICE.Mapping.User
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<AddEndUserDTO, Entity.DB.ECOS.Entities.EndUserList>().ReverseMap();
        }
    }
}
