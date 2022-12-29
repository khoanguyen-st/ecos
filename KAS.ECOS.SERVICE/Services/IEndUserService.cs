using KAS.Entity.DB.ECOS.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAS.ECOS.SERVICE.Services
{
    public interface IEndUserService
    {
        IEnumerable<EndUserList> GetEndUsers();
        Task<IdentityResult> AddEndUser(EndUserList user, string password, Guid organizationId = default, bool isAssignOrg = false);
        Task<EndUserList> UserEmailExist(string userEmail);
        Task<EndUserList> EndUserExist(string userId);
        bool OrganizationExist(Guid organizationId);
        string HashPassword(EndUserList user, string password);
        Task<EndUserList?> GetEndUserById(string userId);
        void DeleteEndUser(EndUserList user);
        Task<IdentityResult> UpdateEndUser(EndUserList user);
        Task<bool> SaveChangesAsync();
    }
}
