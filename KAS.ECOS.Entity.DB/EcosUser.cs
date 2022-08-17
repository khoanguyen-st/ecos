using System;
using System.Collections.Generic;

namespace KAS.Entity.DB.ECOS
{
    public partial class EcosUser
    {
        /// <summary>
        /// Số điện thoại được quyền login vào ECOS
        /// </summary>
        public string PhoneNumber { get; set; } = null!;
        /// <summary>
        /// Tên người truy cập vào ECOS
        /// </summary>
        public string FullName { get; set; } = null!;
        /// <summary>
        /// Lần cuối truy cập
        /// </summary>
        public DateTime LastAccesDate { get; set; }
        /// <summary>
        /// IP truy cập
        /// </summary>
        public string Ip { get; set; } = null!;
        /// <summary>
        /// Thông tin bổ sung cần lưu ý
        /// </summary>
        public string? Description { get; set; }
        public bool IsDeleted { get; set; }
    }
}
