using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAS.Entity.DB.ECOS.Entities
{
    public class UserDeviceList
    {
        public Guid Id { get; set; }
        public string DeviceName { get; set; } = null!;
        public string? LatestIPAccess { get; set; }
        public string? LatestLocation { get; set; }
        public DateTime LatestAccessDate { get; set; }
        public string? OSName { get; set; }
        public string? OSVer { get; set; }
        public bool IsAcive { get; set; }
        public DateTime CreatedDate { get; set; }
        public ICollection<OrganizationDeviceList>? OrganizationDevices { get; set; }
        public ICollection<EndUserRoleList>? EndUserRoles { get; set; }
        public ICollection<AccessHistoryList>? AccessHistories { get; set; }
    }
}
