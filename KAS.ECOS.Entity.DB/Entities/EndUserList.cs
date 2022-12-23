using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAS.Entity.DB.ECOS.Entities
{
    public class EndUserList
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Password { get; set; } = null!;
        public bool IsActive { get; set; } = true;
        public string? Type { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime DeletedDate { get; set; }
        public virtual ICollection<AccessHistoryList>? AccessHistories { get; set; }
        public virtual ICollection<EndUserTokenList>? EndUserTokens { get; set; }
        public virtual ICollection<OrganizationUserList>? OrganizationUsers { get; set; }
        public virtual ICollection<EndUserCredentialHistoryList>? EndUserCredentialHistories { get; set; }
    }
}
