using System;
using System.Collections.Generic;

namespace KAS.Entity.DB.ECOS
{
    public partial class SmacSession
    {
        /// <summary>
        /// Mã Smac (CustomerCode)
        /// </summary>
        public string CustomerId { get; set; } = null!;
        /// <summary>
        /// Session do smac cấp
        /// </summary>
        public string Session { get; set; } = null!;
        /// <summary>
        /// Thông tin đăng nhập
        /// </summary>
        public string Data { get; set; } = null!;
        /// <summary>
        /// Lần cuối cập nhật
        /// </summary>
        public DateTime LastUpdated { get; set; }
    }
}
