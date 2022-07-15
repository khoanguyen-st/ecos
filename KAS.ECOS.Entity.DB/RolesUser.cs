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
        /// Nếu có nhiều User cùng tên (bắt buộc khác công ty), thì chỉ duy nhất 1 User được Active
        /// </summary>
        public string User { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public string Password { get; set; } = null!;
        public string Token { get; set; } = null!;
        public DateTime? TokenExpired { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Role Role { get; set; } = null!;
    }
}
