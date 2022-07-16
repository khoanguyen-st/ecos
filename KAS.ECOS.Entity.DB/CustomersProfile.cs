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
        /// IP App IP, phục vụ cho FrontEnd. Sau này đây chính là GatewayAPI
        /// </summary>
        public string? KasProductApi { get; set; }
        /// <summary>
        /// Mặc định là False
        /// </summary>
        public bool IsDelete { get; set; }
        /// <summary>
        /// Yêu cầu mã hóa
        /// </summary>
        public string? M1connectionString { get; set; }
        /// <summary>
        /// Yêu cầu mã hóa
        /// </summary>
        public string? M2connectionString { get; set; }
        /// <summary>
        /// Yêu cầu mã hóa
        /// </summary>
        public string? P1connectionString { get; set; }
        /// <summary>
        /// Yêu cầu mã hóa
        /// </summary>
        public string? P2connectionString { get; set; }

        public virtual Customer Customer { get; set; } = null!;
        public virtual KasProduct KasProductNavigation { get; set; } = null!;
    }
}
