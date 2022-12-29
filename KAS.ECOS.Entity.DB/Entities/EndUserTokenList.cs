using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAS.Entity.DB.ECOS.Entities
{
    public class EndUserTokenList
    {
        public Guid Id { get; set; }
        public string EndUserId { get; set; } = null!;
        public string? RefreshToken { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public virtual EndUserList? EndUser { get; set; }
    }
}
