using KAS.Entity.DB.ECOS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAS.ECOS.SERVICE.DTOs.OrganizationUser
{
    public class AddOrganizationUserDTO
    {
        public Guid OrganizationId { get; set; }
        public Guid EndUserId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
