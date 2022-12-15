﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAS.Entity.DB.ECOS.Entities
{
    public class RoleList
    {
        public Guid Id { get; set; }
        public Guid OrganizationId { get; set; }
        public string RoleName { get; set; } = null!;
        public string? RoleDescription { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime DeletedDate { get; set; }
        public OrganizationList Organization { get; set; } = null!;
    }
}
