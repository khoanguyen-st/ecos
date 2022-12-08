using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAS.Entity.DB.ECOS.Entities
{
    public class EndUserTokenList
    {
        public string Id { get; set; } = null!;
        public string EndUserId { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public EndUserList EndUser { get; set; } = null!;
    }
}
