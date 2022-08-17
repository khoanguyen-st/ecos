using System;
using System.Collections.Generic;

namespace KAS.Entity.DB.ECOS
{
    /// <summary>
    /// Các sản phẩm của KAS
    /// </summary>
    public partial class Product
    {
        public Product()
        {
            CustomersProfiles = new HashSet<CustomersProfile>();
            ProductsFunctions = new HashSet<ProductsFunction>();
            RolesUsers = new HashSet<RolesUser>();
        }

        public string Id { get; set; } = null!;
        /// <summary>
        /// Mô tả sản phẩm của KAS. Tối đa 2000 ký tự
        /// </summary>
        public string? Description { get; set; }

        public virtual ICollection<CustomersProfile> CustomersProfiles { get; set; }
        public virtual ICollection<ProductsFunction> ProductsFunctions { get; set; }
        public virtual ICollection<RolesUser> RolesUsers { get; set; }
    }
}
