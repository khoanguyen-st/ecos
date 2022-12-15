using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAS.Entity.DB.ECOS.Entities
{
    public class OrganizationProfileList
    {
        public Guid Id { get; set; }
        public Guid OrganizationId { get; set; }
        public string OrganizationProfileName { get; set; } = null!;
        public OrganizationList Organization { get; set; } = null!;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public ICollection<OrganizationDatabaseList>? OrganizationDatabaseLists { get; set; }
    }
}
