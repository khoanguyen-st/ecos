using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAS.ECOS.SERVICE.DTOs.Application
{
    public class GetApplicationDTO
    {
        public string ApplicationName { get; set; } = null!;
        public string ApplicationDescription { get; set; } = string.Empty;
    }
}
