using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAS.Entity.DB.ECOS.Entities
{
    public class EndUserRoleList
    {
        public Guid Id { get; set; }
        public Guid? UserDeviceId { get; set; }
        public Guid RoleId { get; set; }
        public Guid OrganizationUserId { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? DeletedDate { get; set; }
        public virtual UserDeviceList? UserDevice { get; set; }
        public virtual RoleList? Role { get; set; }
        public virtual OrganizationUserList? OrganizationUser { get; set; }
    }
}
