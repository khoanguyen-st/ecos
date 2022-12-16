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
        public string PermissionName { get; set; } = null!;
        public string Permission { get; set; }
        public int MaxRecords { get; set; }
        public ApplicationFunctionList ApplicationFunction { get; set; } = null!;
    }
}
