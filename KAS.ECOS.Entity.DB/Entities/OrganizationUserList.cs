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
        public bool IsActive { get; set; } = true;
        public bool IsAdmin { get; set; } = false;
        public DateTime RegistryDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? DeletedDate { get; set;}
        public virtual EndUserList? EndUser { get; set; }
        public virtual OrganizationList? Organization { get; set; }
    }
}
