using System;
using System.Collections.Generic;

namespace KAS.Entity.DB.ECOS
{
    /// <summary>
    /// Quản lý danh sách khách hàng
    /// </summary>
    public partial class Customer
    {
        public Customer()
        {
            Devies = new HashSet<Devy>();
            Roles = new HashSet<Role>();
        }

        public string Id { get; set; } = null!;
        public string? ParentId { get; set; }
        public string Name { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public bool IsDeleted { get; set; }

        public virtual CustomersInfo CustomersInfo { get; set; } = null!;
        public virtual CustomersProfile CustomersProfile { get; set; } = null!;
        public virtual ICollection<Devy> Devies { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
    }
}
