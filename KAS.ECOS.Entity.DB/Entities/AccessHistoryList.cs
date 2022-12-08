using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAS.Entity.DB.ECOS.Entities
{
    public class AccessHistoryList
    {
        public string Id { get; set; } = null!;
        public string EndUserId { get; set; } = null!;
        public string UserDeviceId { get; set; } = null!;
        public string IPAdress { get; set; } = null!;
        public string Location { get; set; } = null!;
        public DateTime AccessDate { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public EndUserList EndUser { get; set; } = null!;
        public UserDeviceList UserDevice { get; set; } = null!;
    }
}
