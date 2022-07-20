using System;
using System.Collections.Generic;

namespace KAS.Entity.DB.ECOS
{
    public partial class Devy
    {
        /// <summary>
        /// Mã duy nhất của thiết bị. Window sẽ dùng thuật toán riêng, các hệ điều hành khác sẽ dùng function lấy mã thiết bị duy nhất
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
        public DateTime AccessDate { get; set; }
        /// <summary>
        /// Mã khách hàng có ParantID là null
        /// </summary>
        public string CustomerIdRoot { get; set; } = null!;
        /// <summary>
        /// Mã tăng tự động
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Lưu trữ thông tin địa điểm truy cập
        /// </summary>
        public string? Location { get; set; }
        /// <summary>
        /// Tên hệ điều hành
        /// </summary>
        public string Osname { get; set; } = null!;
        /// <summary>
        /// Phiên bản hệ điều hành
        /// </summary>
        public string Osver { get; set; } = null!;
    }
}
