using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAS.Entity.DB.ECOS.Entities
{
    public class OrganizationProfileList
    {
        public string Id { get; set; } = null!;
        public string OrganizationProfileName { get; set; } = null!;
        public string OrganizationId { get; set; } = null!;
        public OrganizationList Organization { get; set; } = null!;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public ICollection<OrganizationDatabaseList> OrganizationDatabaseLists { get; set; }
    }
}
