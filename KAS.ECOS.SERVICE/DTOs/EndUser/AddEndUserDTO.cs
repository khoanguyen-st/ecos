using KAS.Entity.DB.ECOS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAS.ECOS.SERVICE.DTOs.EndUser
{
    public class AddEndUserDTO
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string PasswordConfirmed { get; set; } = null!;
        public bool IsActive { get; set; }
        public virtual ICollection<AccessHistoryList>? AccessHitories { get; set; }
        public virtual ICollection<EndUserTokenList>? EndUserTokens { get; set; }
        public virtual ICollection<OrganizationUserList>? OrganizationUsers { get; set; }
        public virtual ICollection<EndUserCredentialHistoryList>? EndUserCredentialHistories { get; set; }
    }
}
