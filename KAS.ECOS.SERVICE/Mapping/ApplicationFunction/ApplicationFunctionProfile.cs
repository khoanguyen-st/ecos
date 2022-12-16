using AutoMapper;
using KAS.ECOS.SERVICE.DTOs.Application;
using KAS.ECOS.SERVICE.DTOs.ApplicationFuntion;
using KAS.Entity.DB.ECOS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAS.ECOS.SERVICE.Mapping.ApplicationFunction
{
    public class ApplicationFunctionProfile : Profile
    {
        public ApplicationFunctionProfile()
        {
            CreateMap<AddApplicationFunctionDTO, ApplicationFunctionList>().ReverseMap();
            CreateMap<UpdateApplicationFunctionDTO, ApplicationFunctionList>().ReverseMap();
        }
    }
}
