using System;
using System.Collections.Generic;

namespace KAS.Entity.DB.ECOS
{
    /// <summary>
    /// Phân quyền nội dung
    /// </summary>
    public partial class CustomersContentPermission
    {
        /// <summary>
        /// Mã khách hàng / công ty/ Phòng ban xem dữ liệu của CustomerID.Destination
        /// </summary>
        public string CustomerIdSource { get; set; } = null!;
        public string RoleName { get; set; } = null!;
        public bool IsAllow { get; set; }
        /// <summary>
        /// Mã phòng ban được xem dữ liệu bởi CustomerID.Source
        /// </summary>
        public string[] CustomerIdDestination { get; set; } = null!;

        public virtual Role Role { get; set; } = null!;
    }
}
