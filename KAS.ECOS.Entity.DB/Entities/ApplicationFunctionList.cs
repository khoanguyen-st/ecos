using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAS.Entity.DB.ECOS.Entities
{
    public class ApplicationFunctionList
    {
        public Guid Id { get; set; }
        public Guid ApplicationId { get; set; }
        public string FunctionName { get; set; } = null!;
        public string? FunctionDescription { get; set; }
        public string? ParentId { get; set; }
        public string Path { get; set; } = null!;
        public short Level { get; set; }
        public virtual ApplicationList? Application { get; set; }
        public virtual ICollection<ApplicationFunctionPermissionList>? ApplicationPermissions { get; set; }
    }
}
