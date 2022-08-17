using System;
using System.Collections.Generic;

namespace KAS.Entity.DB.ECOS
{
    /// <summary>
    /// Quản lý tài khoản và token
    /// </summary>
    public partial class RolesUser
    {
        public string CustomerId { get; set; } = null!;
        public string RoleName { get; set; } = null!;
        /// <summary>
        /// Tại 1 thời điểm, chỉ có 1 Token gắn với 1 User. 
        /// User và ProductID sẽ là duy nhất
        /// </summary>
        public string User { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public string Password { get; set; } = null!;
        /// <summary>
        /// Token là duy nhất
        /// </summary>
        public string Token { get; set; } = null!;
        public DateTime? TokenExpired { get; set; }
        public bool IsDeleted { get; set; }
        /// <summary>
        /// Tên sản phẩm của KAS. Khi login truyền vào Header
        /// </summary>
        public string ProductId { get; set; } = null!;

        public virtual Product Product { get; set; } = null!;
        public virtual Role Role { get; set; } = null!;
    }
}
