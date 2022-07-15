using System;
using System.Collections.Generic;

namespace KAS.Entity.DB.ECOS
{
    /// <summary>
    /// Quản lý cấp phát Token
    /// </summary>
    public partial class TokensLog
    {
        public string CustomerId { get; set; } = null!;
        public string RoleName { get; set; } = null!;
        public string User { get; set; } = null!;
        public string DeviceId { get; set; } = null!;
        public string Token { get; set; } = null!;
        public DateTimeOffset TokenCreate { get; set; }
        public DateTimeOffset? TokenExpired { get; set; }
        public bool IsDeleted { get; set; }
        public string KasProductsId { get; set; } = null!;

        public virtual RolesUser RolesUser { get; set; } = null!;
    }
}
