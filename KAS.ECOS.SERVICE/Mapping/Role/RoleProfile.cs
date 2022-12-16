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
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<RoleList, AddRoleListDto>()
                .ForMember(
                    dist => dist.OrganizationId,
                    opt => opt.MapFrom(src => src.OrganizationId)
                    )
                .ReverseMap();
            CreateMap<RoleList, UpdateRoleListDto>()
                .ForMember(
                    dist => dist.OrganizationId,
                    opt => opt.MapFrom(src => src.OrganizationId)
                )
                .ForMember(
                    dist => dist.Id,
                    opt => opt.MapFrom(src => src.Id)
                )
                .ReverseMap();
            CreateMap<RoleList, GetRoleListDto>().ReverseMap();
        }
    }
}
