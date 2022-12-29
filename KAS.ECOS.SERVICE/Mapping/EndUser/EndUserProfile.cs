using AutoMapper;
using KAS.ECOS.SERVICE.DTOs.EndUser;
using KAS.Entity.DB.ECOS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAS.ECOS.SERVICE.Mapping.EndUser
{
    public class EndUserProfile : Profile
    {
        public EndUserProfile()
        {
            CreateMap<GetEndUserDTO, Entity.DB.ECOS.Entities.EndUserList>().ReverseMap();
            CreateMap<AddEndUserDTO, Entity.DB.ECOS.Entities.EndUserList>().ReverseMap();
            CreateMap<UpdateEndUserDTO, Entity.DB.ECOS.Entities.EndUserList>().ReverseMap();
        }
    }
}
