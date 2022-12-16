using KAS.Entity.DB.ECOS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAS.ECOS.SERVICE.DTOs.UserDevice
{
    public class AddUserDeviceDTO
    {
        public string DeviceName { get; set; } = null!;
        public string? LatestIPAccess { get; set; }
        public string? LatestLocation { get; set; }
        public DateTime LatestAccessDate { get; set; } = DateTime.UtcNow;
        public string? OSName { get; set; }
        public string? OSVer { get; set; }
        public bool IsAcive { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public virtual ICollection<OrganizationDeviceList>? OrganizationDevices { get; set; }
        public virtual ICollection<EndUserRoleList>? EndUserRoles { get; set; }
        public virtual ICollection<AccessHistoryList>? AccessHistories { get; set; }
    }
}
