using AutoMapper;
using KAS.ECOS.SERVICE.DTOs.GrantUser;
using KAS.Entity.DB.ECOS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAS.ECOS.SERVICE.Mapping.EndUserRole
{
    public class EndUserRoleProfile : Profile
    {
        public EndUserRoleProfile()
        {
            CreateMap<AddEndUserRoleDTO, EndUserRoleList>().ReverseMap();
        }
    }
}
