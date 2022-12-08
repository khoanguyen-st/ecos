using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAS.Entity.DB.ECOS.Entities
{
    public class OrganizationDatabaseList
    {
        public string Id { get; set; } = null!;
        public string OrganizationProfileId { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string ConnectionString { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public OrganizationProfileList OrganizationProfile { get; set; } = null!;
    }
}
