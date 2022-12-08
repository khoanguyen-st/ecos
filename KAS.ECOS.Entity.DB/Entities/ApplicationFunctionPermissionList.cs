using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAS.Entity.DB.ECOS.Entities
{
    public class ApplicationFunctionPermissionList
    {
        public string Id { get; set; } = null!;
        public string ApplicationFunctionId { get; set; } = null!;
        public string PermissionName { get; set; } = null!;
        public short Permission { get; set; }
        public int MaxRecords { get; set; }
        public ApplicationFunctionList ApplicationFunction { get; set; } = null!;
    }
}
