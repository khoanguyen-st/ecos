using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAS.Entity.DB.ECOS.Entities
{
    public class RoleList
    {
        public string Id { get; set; } = null!;
        public string OrganizationId { get; set; } = null!;
        public string RoleName { get; set; } = null!;
        public string RoleDescription { get; set; } = null!;
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime DeletedDate { get; set; }
        public OrganizationList Organization { get; set; } = null!;
    }
}
