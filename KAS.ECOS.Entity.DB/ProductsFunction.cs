using System;
using System.Collections.Generic;

namespace KAS.Entity.DB.ECOS
{
    /// <summary>
    /// Danh sách tính năng của sản phẩm KAS
    /// </summary>
    public partial class ProductsFunction
    {
        public ProductsFunction()
        {
            ProductsFunctionsPermissions = new HashSet<ProductsFunctionsPermission>();
        }

        public string ProductId { get; set; } = null!;
        public string FunctionName { get; set; } = null!;
        public string? FunctionParent { get; set; }
        public string? FunctionPath { get; set; }
        public string? Description { get; set; }
        public bool IsDeleted { get; set; }
        /// <summary>
        /// Level của đệ quy Function
        /// </summary>
        public short Level { get; set; }

        public virtual Product Product { get; set; } = null!;
        public virtual ICollection<ProductsFunctionsPermission> ProductsFunctionsPermissions { get; set; }
    }
}
