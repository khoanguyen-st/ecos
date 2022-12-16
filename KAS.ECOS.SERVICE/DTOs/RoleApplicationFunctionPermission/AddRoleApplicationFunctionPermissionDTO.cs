using KAS.Entity.DB.ECOS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAS.ECOS.SERVICE.DTOs.RoleApplicationPermission
{
    public class AddRoleApplicationFunctionPermissionDTO
    {
        public Guid ApplicationFunctionPermissionId { get; set; }
        public Guid RoleId { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime DeletedDate { get; set; }
    }
}
