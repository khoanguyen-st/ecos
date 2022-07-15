using System;
using System.Collections.Generic;

namespace KAS.Entity.DB.ECOS
{
    public partial class Devy
    {
        public string DeviceId { get; set; } = null!;
        public string CustomerId { get; set; } = null!;
        public string KasProductsId { get; set; } = null!;
        /// <summary>
        /// Thông tin chi tiết của thiết bị
        /// </summary>
        public string? DeviceInfo { get; set; }
        /// <summary>
        /// Hệ thống tự tạo, không cần gán giá trị
        /// </summary>
        public DateTime CreateDate { get; set; }
        public DateTime? ExpiredDate { get; set; }

        public virtual Customer Customer { get; set; } = null!;
        public virtual KasProduct KasProducts { get; set; } = null!;
    }
}
