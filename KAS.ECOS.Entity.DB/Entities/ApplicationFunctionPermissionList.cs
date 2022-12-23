using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAS.Entity.DB.ECOS.Entities
{
    public class ApplicationFunctionPermissionList
    {
        public Guid Id { get; set; }
        public Guid ApplicationFunctionId { get; set; }
        public string Permission { get; set; } = null!;
        public int? MaxRecords { get; set; }
        public virtual ApplicationFunctionList? ApplicationFunction { get; set; }
    }
}
