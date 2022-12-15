using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAS.Entity.DB.ECOS.Entities
{
    public class OrganizationUserList
    {
        public Guid Id { get; set; }
        public Guid OrganizationId { get; set; }
        public Guid EndUserId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime DeletedDate { get; set;}
        public EndUserList EndUser { get; set; } = null!;
    }
}
