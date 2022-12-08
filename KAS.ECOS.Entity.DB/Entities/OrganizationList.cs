using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAS.Entity.DB.ECOS.Entities
{
    public class OrganizationList
    {
        public string Id { get; set; } = null!;
        public string OrganizationName { get; set; } = null!;
        public string? OrganizationDescription { get; set; }
        public string Address { get; set; } = null!;
        public string HandPhone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? ParentId { get; set; }
        public string CustomerCodeSmac { get; set; } = null!;
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? DeletedDate { get; set; }
        public ICollection<OrganizationProfileList> OrganizationProfileLists { get; set; }
        public ICollection<OrganizationDeviceList> OrganizationDeviceLists { get; set; }
        public ICollection<OrganizationUserList> OrganizationUserLists { get; set; }
        public ICollection<RoleList> RoleLists { get; set; }

    }
}
