using System;
using System.Collections.Generic;

namespace KAS.Entity.DB.ECOS
{
    public partial class DpconfigCustomer
    {
        /// <summary>
        /// Mã khách hàng (customerCode)
        /// </summary>
        public string CustomerId { get; set; } = null!;
        /// <summary>
        /// Mã DataProccessor
        /// </summary>
        public string ClientId { get; set; } = null!;
        /// <summary>
        /// Thời gian tính giá vốn tự động 0-23
        /// </summary>
        public short[]? ExcuteCostPriceDb { get; set; }
        /// <summary>
        /// Cau hinh SAP cho MaiSon
        /// </summary>
        public string? SapmaiSonConfig { get; set; }

        public virtual Dpconfig Client { get; set; } = null!;
    }
}
