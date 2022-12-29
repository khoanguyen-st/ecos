using KAS.Entity.DB.ECOS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAS.ECOS.SERVICE.DTOs.ApplicationFuntion
{
    public class AddApplicationFunctionDTO
    {
        public Guid ApplicationId { get; set; }
        public string FunctionName { get; set; } = null!;
        public string? FunctionDescription { get; set; }
        public string? ParentId { get; set; }
        public string Path { get; set; } = null!;
        public short Level { get; set; }
    }
}
