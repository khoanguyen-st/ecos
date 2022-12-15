using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAS.Entity.DB.ECOS.Entities
{
    public class OrganizationDatabaseList
    {
        public Guid Id { get; set; }
        public Guid OrganizationProfileId { get; set; }
        public string DatabaseName { get; set; } = null!;
        public string ConnectionString { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public OrganizationProfileList OrganizationProfile { get; set; } = null!;
    }
}
