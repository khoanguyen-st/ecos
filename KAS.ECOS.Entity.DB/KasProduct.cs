using System;
using System.Collections.Generic;

namespace KAS.Entity.DB.ECOS
{
    /// <summary>
    /// Các sản phẩm của KAS
    /// </summary>
    public partial class KasProduct
    {
        public KasProduct()
        {
            CustomersProfiles = new HashSet<CustomersProfile>();
            Devies = new HashSet<Devy>();
            KasProductsFunctions = new HashSet<KasProductsFunction>();
        }

        public string Id { get; set; } = null!;
        /// <summary>
        /// Mô tả sản phẩm của KAS. Tối đa 2000 ký tự
        /// </summary>
        public string? Description { get; set; }

        public virtual ICollection<CustomersProfile> CustomersProfiles { get; set; }
        public virtual ICollection<Devy> Devies { get; set; }
        public virtual ICollection<KasProductsFunction> KasProductsFunctions { get; set; }
    }
}
