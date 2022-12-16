using AutoMapper;
using KAS.ECOS.SERVICE.DTOs.ApplicationFunctionPermission;
using KAS.Entity.DB.ECOS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAS.ECOS.SERVICE.Mapping.ApplicationFunctionPermission
{
    public class ApplicationFunctionPermissionProfile : Profile
    {
        public ApplicationFunctionPermissionProfile()
        {
            CreateMap<AddApplicationFunctionPermissionDTO, ApplicationFunctionPermissionList>().ReverseMap();
            CreateMap<UpdateApplicationFunctionPermissionDTO, ApplicationFunctionPermissionList>().ReverseMap();
        }
    }
}
