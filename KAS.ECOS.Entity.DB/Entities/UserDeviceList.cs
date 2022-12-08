using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAS.Entity.DB.ECOS.Entities
{
    public class UserDeviceList
    {
        public string Id { get; set; } = null!;
        public string DeviceName { get; set; } = null!;
        public string LatestIPAccess { get; set; } = null!;
        public string LatestLocation { get; set; } = null!;
        public DateTime LatestAccessDate { get; set; }
        public string OSName { get; set; } = null!;
        public string OSVer { get; set; } = null!;
        public bool IsAcive { get; set; }
        public DateTime CreatedDate { get; set; }
        public ICollection<OrganizationDeviceList> OrganizationDevices { get; set; } = null!;
        public ICollection<EndUserRoleList> EndUserRoles { get; set; } = null!;
        public ICollection<AccessHistoryList> AccessHistories { get; set; } = null!;
    }
}
