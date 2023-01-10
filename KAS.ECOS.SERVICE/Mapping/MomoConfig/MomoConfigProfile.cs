using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using KAS.ECOS.SERVICE.DTOs.MomoConfig;
using KAS.Entity.DB.ECOS.Entities;

namespace KAS.ECOS.SERVICE.Mapping.MomoConfig
{
    public class MomoConfigProfile : Profile
    {
        public MomoConfigProfile()
        {
            CreateMap<AddMomoConfigDto, MomoConfigList>().ReverseMap();
        }
    }
}
