using AutoMapper;
using KAS.ECOS.SERVICE.DTOs.RoleApplicationPermission;
using KAS.Entity.DB.ECOS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAS.ECOS.SERVICE.Mapping.RoleApplicationFunctionPermission
{
    public class RoleApplicationFunctionPermissionProfile : Profile
    {
        public RoleApplicationFunctionPermissionProfile()
        {
            CreateMap<AddRoleApplicationFunctionPermissionDTO, RoleApplicationFunctionPermissionList>().ReverseMap();
        }
    }
}
