using System;
using System.Collections.Generic;

namespace KAS.Entity.DB.ECOS
{
    /// <summary>
    /// Phân quyền tính năng.
    /// 
    /// Tại thời điểm đăng nhập:
    /// 	- Input: KAS.Product.ID , CustomerID,RoleName
    /// 	- Nếu giá trị trả về danh sách  FunctionName ==0, thì trả về thông báo tài khoản không hợp lệ. Dùng cho trường hợp 1 tài khoản truy cập vào nhiều sản phẩm
    /// </summary>
    public partial class KasProductsFunctionsPermission
    {
        public string KasProductId { get; set; } = null!;
        public string FunctionName { get; set; } = null!;
        public string CustomerId { get; set; } = null!;
        public string RoleName { get; set; } = null!;
        /// <summary>
        /// Phân quyền tính năng theo Enum(hệ số Bit)
        /// </summary>
        public short Permission { get; set; }
        /// <summary>
        /// Mặc định là 0, không giới hạn. Field này dùng để giới hạn số lượng record cho các tính năng dùng thử miễn phí
        /// </summary>
        public int MaxRecords { get; set; }
        public bool IsDeleted { get; set; }
        /// <summary>
        /// Ngày hết hạn của Function, hạn sử dụng của Token sẽ bằng thời ngày hết hạn gần nhất của tất cả tính năng, và tối đa là 1 năm
        /// </summary>
        public DateTime? Expired { get; set; }

        public virtual KasProductsFunction KasProductsFunction { get; set; } = null!;
        public virtual Role Role { get; set; } = null!;
    }
}
