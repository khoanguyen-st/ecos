using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAS.Entity.DB.ECOS.Entities
{
    public class RoleApplicationFunctionPermissionList
    {
        public Guid Id { get; set; }
        public Guid ApplicationFunctionPermissionId { get; set; }
        public Guid RoleId { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime DeletedDate { get; set; }
        public ApplicationFunctionPermissionList ApplicationFunctionPermission { get; set; } = null!;
        public RoleList Role { get; set; } = null!;
    }
}
