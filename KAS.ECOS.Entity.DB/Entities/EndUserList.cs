using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAS.Entity.DB.ECOS.Entities
{
    public class EndUserList
    {
        public string Id { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Password { get; set; } = null!;
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime DeletedDate { get; set; }
        public ICollection<AccessHistoryList> AccessHitories { get; set; }
        public ICollection<EndUserTokenList> EndUserTokens { get; set; }
        public ICollection<OrganizationUserList> OrganizationUsers { get; set; }
        public ICollection<EndUserCredentialHistoryList> EndUserCredentialHistories { get; set; }
    }
}
