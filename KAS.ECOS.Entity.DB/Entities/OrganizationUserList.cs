﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAS.Entity.DB.ECOS.Entities
{
    public class OrganizationUserList
    {
        public string Id { get; set; } = null!;
        public string OrganizationId { get; set; } = null!;
        public string EndUserId { get; set; } = null!;
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime DeletedDate { get; set;}
        public EndUserList EndUser { get; set; }
    }
}
