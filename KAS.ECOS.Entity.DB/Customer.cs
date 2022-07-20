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
            CustomersProfiles = new HashSet<CustomersProfile>();
            Roles = new HashSet<Role>();
        }

        public string Id { get; set; } = null!;
        public string? ParentId { get; set; }
        public string Name { get; set; } = null!;
        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime CreatedDate { get; set; }
        /// <summary>
        /// Mặc định là false
        /// </summary>
        public bool IsDeleted { get; set; }
        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string Address { get; set; } = null!;
        /// <summary>
        /// Số điện thoại khách hàng
        /// </summary>
        public string HandPhone { get; set; } = null!;
        public string Email { get; set; } = null!;
        /// <summary>
        /// Ghi chú
        /// </summary>
        public string Description { get; set; } = null!;
        /// <summary>
        /// mã khách hàng trên SMAC
        /// </summary>
        public string CustomerCodeSmac { get; set; } = null!;

        public virtual CustomersInfo CustomersInfo { get; set; } = null!;
        public virtual ICollection<CustomersProfile> CustomersProfiles { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
    }
}
