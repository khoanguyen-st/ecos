using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAS.ECOS.SERVICE.DTOs.Application
{
    public class AddApplicationDTO
    {
        public string applicationName { get; set; } = null!;
        public string applicationDescription { get; set; } = string.Empty;
    }
}
