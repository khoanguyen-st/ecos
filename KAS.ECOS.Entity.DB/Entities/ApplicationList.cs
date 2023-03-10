using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace KAS.Entity.DB.ECOS.Entities
{
    public class ApplicationList
    {
        public Guid Id { get; set; }
        public string ApplicationName { get; set; } = null!;
        public string? ApplicationDescription { get; set; }
        public virtual ICollection<ApplicationFunctionList>? ApplicationFunctions { get; set; }
    }
}
