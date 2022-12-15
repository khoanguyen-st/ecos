using AutoMapper;
using KAS.ECOS.SERVICE.DTOs.Application;
using KAS.Entity.DB.ECOS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAS.ECOS.SERVICE.Mapping.Application
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            CreateMap<ApplicationList, AddApplicationDTO>();
            CreateMap<AddApplicationDTO, ApplicationList>();
            CreateMap<UpdateApplicationDTO, ApplicationList>();
        }
    }
}
