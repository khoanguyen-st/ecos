using System;
using System.Collections.Generic;

namespace KAS.Entity.DB.ECOS
{
    public partial class Ping
    {
        /// <summary>
        /// Tên sản phẩm của KAS
        /// </summary>
        public string KasProductName { get; set; } = null!;
        /// <summary>
        /// Mã khách hàng
        /// </summary>
        public string CustomerId { get; set; } = null!;
        /// <summary>
        /// Mã chi nhánh
        /// </summary>
        public string SiteId { get; set; } = null!;
        /// <summary>
        /// Lần cuối đăng nhập
        /// </summary>
        public DateTime PingTime { get; set; }
        /// <summary>
        /// Phiên bản APP
        /// </summary>
        public string? Version { get; set; }
        /// <summary>
        /// Thông tin thiết bị
        /// </summary>
        public string? MachineInfo { get; set; }
        /// <summary>
        /// Thông tin user
        /// </summary>
        public string? UserInfo { get; set; }
        /// <summary>
        /// User đăng nhập
        /// </summary>
        public string UserId { get; set; } = null!;
    }
}
