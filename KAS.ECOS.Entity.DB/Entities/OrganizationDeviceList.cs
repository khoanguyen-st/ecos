using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAS.Entity.DB.ECOS.Entities
{
    public class OrganizationDeviceList
    {
        public string Id { get; set; } = null!;
        public string OrganizationId { get; set; } = null!;
        public string UserDeviceId { get; set; } = null!;
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime DeletedDate { get; set; } = DateTime.Now;
        public OrganizationList Organization { get; set; } = null!;
        public UserDeviceList UserDevice { get; set; } = null!;
    }
}
