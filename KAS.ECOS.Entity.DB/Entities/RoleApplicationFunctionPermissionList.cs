using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAS.Entity.DB.ECOS.Entities
{
    public class RoleApplicationFunctionPermissionList
    {
        public string Id { get; set; } = null!;
        public string ApplicationFunctionPermissionId { get; set; } = null!;
        public string RoleId { get; set; } = null!;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime DeletedDate { get; set; }
        public ApplicationFunctionPermissionList ApplicationFunctionPermission { get; set; } = null!;
        public RoleList Role { get; set; } = null!;
    }
}
