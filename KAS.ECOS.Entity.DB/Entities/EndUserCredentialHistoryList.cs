using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAS.Entity.DB.ECOS.Entities
{
    public class EndUserCredentialHistoryList
    {
        public Guid Id { get; set; }
        public Guid EndUserId { get; set; }
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Password { get; set; } = null!;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public EndUserList EndUser { get; set; } = null!;
    }
}
