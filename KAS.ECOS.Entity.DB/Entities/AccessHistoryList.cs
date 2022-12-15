using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAS.Entity.DB.ECOS.Entities
{
    public class AccessHistoryList
    {
        public Guid Id { get; set; }
        public Guid EndUserId { get; set; }
        public Guid UserDeviceId { get; set; }
        public string? IPAdress { get; set; }
        public string? Location { get; set; }
        public DateTime AccessDate { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public EndUserList EndUser { get; set; } = null!;
        public UserDeviceList UserDevice { get; set; } = null!;
    }
}
