using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAS.Entity.DB.ECOS.Entities
{
    public class OrganizationList
    {
        public Guid Id { get; set; }
        public string OrganizationName { get; set; } = null!;
        public string? OrganizationDescription { get; set; }
        public string Address { get; set; } = null!;
        public string HandPhone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public Guid? ParentId { get; set; }
        public string OrganizationCode { get; set; } = null!;
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? DeletedDate { get; set; }
        public ICollection<OrganizationProfileList>? OrganizationProfileLists { get; set; }
        public ICollection<OrganizationDeviceList>? OrganizationDeviceLists { get; set; }
        public ICollection<OrganizationUserList>? OrganizationUserLists { get; set; }
        public ICollection<RoleList>? RoleLists { get; set; }

    }
}
