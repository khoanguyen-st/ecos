using System;
using System.Collections.Generic;

namespace KAS.Entity.DB.ECOS
{
    /// <summary>
    /// Tạo ra các vai trò sử dụng tính năng (phân theo Customer), Nếu User được thêm vào Role này tương đương sẽ có các quyền tương tự 
    /// </summary>
    public partial class Role
    {
        public Role()
        {
            ProductsFunctionsPermissions = new HashSet<ProductsFunctionsPermission>();
            RolesUsers = new HashSet<RolesUser>();
        }

        public string CustomerId { get; set; } = null!;
        /// <summary>
        /// Tên Vai trò
        /// </summary>
        public string RoleName { get; set; } = null!;
        public bool? IsDeleted { get; set; }
        /// <summary>
        /// Mô tả vai trò
        /// </summary>
        public string? Description { get; set; }
        public DateTime? CreateDate { get; set; }

        public virtual Customer Customer { get; set; } = null!;
        public virtual CustomersContentPermission CustomersContentPermission { get; set; } = null!;
        public virtual ICollection<ProductsFunctionsPermission> ProductsFunctionsPermissions { get; set; }
        public virtual ICollection<RolesUser> RolesUsers { get; set; }
    }
}
