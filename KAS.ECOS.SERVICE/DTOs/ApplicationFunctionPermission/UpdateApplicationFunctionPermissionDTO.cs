using KAS.Entity.DB.ECOS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAS.ECOS.SERVICE.DTOs.ApplicationFunctionPermission
{
    public class UpdateApplicationFunctionPermissionDTO
    {
        public Guid ApplicationFunctionId { get; set; }
        public string PermissionName { get; set; } = null!;
        public string Permission { get; set; }
        public int MaxRecords { get; set; }
    }
}
