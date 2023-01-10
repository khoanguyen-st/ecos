using KAS.Entity.DB.ECOS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAS.ECOS.SERVICE.DTOs.MomoConfig
{
    public class AddMomoConfigDto
    {
        public string PartnerCode { get; set; } = null!;
        public string AccessKey { get; set; } = null!;
        public string SecretKey { get; set; } = null!;
        public string EndUserId { get; set; } = null!;
        public virtual EndUserList? EndUserList { get; set; }
    }
}
