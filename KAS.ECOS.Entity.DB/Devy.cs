using System;
using System.Collections.Generic;

namespace KAS.Entity.DB.ECOS
{
    public partial class Devy
    {
        /// <summary>
        /// Mã thiết bị
        /// </summary>
        public string DeviceId { get; set; } = null!;
        /// <summary>
        /// Mã khách hàng
        /// </summary>
        public string CustomerId { get; set; } = null!;
        /// <summary>
        /// Mã sản phẩm của KAS
        /// </summary>
        public string KasProductsId { get; set; } = null!;
        /// <summary>
        /// Thông tin chi tiết của thiết bị. Lưu mã json object
        /// </summary>
        public string? DeviceInfo { get; set; }
        /// <summary>
        /// Ngày kích hoạt.Hệ thống tự tạo, không cần gán giá trị. 
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// Tên thiết bị
        /// </summary>
        public string DeviceName { get; set; } = null!;
        /// <summary>
        /// IP truy cập lần cuối
        /// </summary>
        public string Ip { get; set; } = null!;
        /// <summary>
        /// Thời gian truy cập lần cuối
        /// </summary>
        public DateTime? AccessDate { get; set; }

        public virtual Customer Customer { get; set; } = null!;
        public virtual KasProduct KasProducts { get; set; } = null!;
    }
}
