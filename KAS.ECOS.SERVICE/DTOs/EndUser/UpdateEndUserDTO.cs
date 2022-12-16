using KAS.Entity.DB.ECOS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAS.ECOS.SERVICE.DTOs.EndUser
{
    public class UpdateEndUserDTO
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string? Password { get; set; }
        public string? PasswordConfirmed { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime DeletedDate { get; set; }
        public ICollection<AccessHistoryList>? AccessHitories { get; set; }
        public ICollection<EndUserTokenList>? EndUserTokens { get; set; }
        public ICollection<OrganizationUserList>? OrganizationUsers { get; set; }
        public ICollection<EndUserCredentialHistoryList>? EndUserCredentialHistories { get; set; }
    }
}
