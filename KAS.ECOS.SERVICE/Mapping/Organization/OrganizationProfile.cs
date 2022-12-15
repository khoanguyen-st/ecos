using AutoMapper;
using KAS.ECOS.SERVICE.DTOs.Application;
using KAS.Entity.DB.ECOS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KAS.ECOS.API.Entity;

namespace KAS.ECOS.SERVICE.Mapping.Application
{
    public class OrganizationProfile : Profile
    {
        public OrganizationProfile()
        {
            CreateMap<OrganizationList, AddOrganizationListDto>().ReverseMap();
            CreateMap<GetOrganizationListDto, OrganizationList>().ReverseMap();
            CreateMap<UpdateOrganizationListDto, OrganizationList>().ReverseMap();
        }
    }
}