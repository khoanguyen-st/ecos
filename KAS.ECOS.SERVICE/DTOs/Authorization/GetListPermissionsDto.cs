using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAS.ECOS.SERVICE.DTOs.Authorization
{
    public class GetListPermissionsDto
    {
        public string Token { get; set; } = null!;
        public string OrganizationId { get; set; } = null!;
    }
}
