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
        /// Lưu thông tin Server của API, phục vụ cho FrontEnd
        /// </summary>
        public string ProfileApi { get; set; } = null!;
        /// <summary>
        /// Mặc định là False
        /// </summary>
        public bool IsDelete { get; set; }
        /// <summary>
        /// Lưu trữ thông tin DB (M1,M2,P1,P2)
        /// </summary>
        public string ProfileDb { get; set; } = null!;

        public virtual Customer Customer { get; set; } = null!;
        public virtual KasProduct KasProductNavigation { get; set; } = null!;
    }
}
