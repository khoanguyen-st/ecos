using System;
using System.Collections.Generic;

namespace KAS.Entity.DB.ECOS
{
    /// <summary>
    /// Table này lưu trữ các thông tin mở rộng của Customers
    /// </summary>
    public partial class CustomersInfo
    {
        public string Id { get; set; } = null!;
        /// <summary>
        /// Điện thoại cầm tay
        /// </summary>
        public string Anything { get; set; } = null!;

        public virtual Customer IdNavigation { get; set; } = null!;
    }
}
