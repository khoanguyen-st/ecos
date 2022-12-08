using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAS.Entity.DB.ECOS.Entities
{
    public class ApplicationFunctionList
    {
        public string Id { get; set; } = null!;
        public string ApplicationId { get; set; } = null!;
        public string FunctionName { get; set; } = null!;
        public string FunctionDescription { get; set; } = null!;
        public string? ParentId { get; set; }
        public string Path { get; set; } = null!;
        public short Level { get; set; }
        public ApplicationList Application { get; set; } = null!;
        public ICollection<ApplicationFunctionPermissionList> ApplicationPermissions { get; set; } = null!;
    }
}
