using AutoMapper;
using KAS.Entity.DB.ECOS.Entities;
using KAS.ECOS.API.Entity;

namespace KAS.ECOS.SERVICE.Mapping.Application
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<RoleList, AddRoleListDto>().ReverseMap();
            CreateMap<RoleList, UpdateRoleListDto>().ReverseMap();
            CreateMap<RoleList, GetRoleListDto>().ReverseMap();
        }
    }
}
