using AutoMapper;
using KAS.ECOS.SERVICE.DTOs.OrganizationUser;
using KAS.Entity.DB.ECOS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAS.ECOS.SERVICE.Mapping.OrganizationUser
{
    public class OrganizationUserProfile : Profile
    {
        public OrganizationUserProfile()
        {
            CreateMap<AddOrganizationUserDTO, OrganizationUserList>().ReverseMap();
        }
    }
}
