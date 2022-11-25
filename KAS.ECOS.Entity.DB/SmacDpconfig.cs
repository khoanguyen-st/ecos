using System;
using System.Collections.Generic;

namespace KAS.Entity.DB.ECOS
{
    public partial class SmacDpconfig
    {
        /// <summary>
        /// Mã khách hàng SMAC (CustomerCode)
        /// </summary>
        public string CustomerId { get; set; } = null!;
        /// <summary>
        /// Key cấu hình
        /// </summary>
        public string ConfigKey { get; set; } = null!;
        /// <summary>
        /// Giá trị cấu hình
        /// </summary>
        public string ConfigValue { get; set; } = null!;
        public bool IsDeleted { get; set; }
        public string? DpclientId { get; set; }
    }
}
