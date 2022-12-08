using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAS.Entity.DB.ECOS.Entities
{
    public class ApplicationList
    {
        public string Id { get; set; } = null!;
        public string ApplicationName { get; set; } = null!;
        public string ApplicationDescription { get; set; } = null!;
    }
}
