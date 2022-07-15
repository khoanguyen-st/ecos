using System;
using System.Collections.Generic;

namespace KAS.Entity.DB.ECOS
{
    /// <summary>
    /// Danh sách thông tin cấu hình của Customers.
    /// </summary>
    public partial class CustomersProfile
    {
        public string CustomerId { get; set; } = null!;
        /// <summary>
        /// Tên sản phẩm của KAS
        /// </summary>
        public string KasProduct { get; set; } = null!;
        /// <summary>
        /// Thông tin chi tiết phục vụ cho Front End, bao gồm url và thông số cấu hình
        /// </summary>
        public string? KasProductFrontEndProfile { get; set; }
        public bool IsDeleted { get; set; }
        /// <summary>
        /// Thông tin chi tiết phục vụ cho BackEnd, bao gồm url và thông số cấu hình.
        /// Chỉ những thiết bị được xác nhận là hệ thống của KAS mới truy vấn được
        /// </summary>
        public string? KasProductBackEndProfile { get; set; }

        public virtual Customer Customer { get; set; } = null!;
        public virtual KasProduct KasProductNavigation { get; set; } = null!;
    }
}
