using KAS.ECOS.SERVICE.Services;
using KAS.Entity.DB.ECOS.Entities;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace KAS.ECOS.API.Services
{
    public class EndUserService : IEndUserService
    {
        private readonly ECOSContext _context;
        private readonly UserManager<EndUserList> _userManager;
        private readonly IPasswordHasher<EndUserList> _passwordHasher;

        public EndUserService(ECOSContext context, UserManager<EndUserList> userManager, IPasswordHasher<EndUserList> passwordHasher)
        {
            _context = context;
            _userManager = userManager;
            _passwordHasher = passwordHasher;
        }
        public IEnumerable<EndUserList> GetEndUsers()
        {
            return _userManager.Users.ToList();
        }
        public async Task<IdentityResult> AddEndUser(EndUserList user, string password, Guid organizationId = default, bool isAssignOrg = false)
        {
            var result = await _userManager.CreateAsync(user, password);
            if (isAssignOrg && result.Succeeded)
            {
                var organizationUser = new OrganizationUserList()
                {
                    Id = new Guid(),
                    OrganizationId = organizationId,
                    EndUserId = user.Id,
                };

                await _context.OrganizationUserLists.AddAsync(organizationUser);
                await _context.SaveChangesAsync();
            }

            return result;

        }
        public async Task<EndUserList> UserEmailExist(string userEmail)
        {
            return await _userManager.FindByEmailAsync(userEmail);
        }

        public Task<EndUserList> EndUserExist(string userId)
        {
            return _userManager.FindByIdAsync(userId);
        }

        public bool OrganizationExist(Guid organizationId)
        {
            return _context.OrganizationLists.Any(o => o.Id == organizationId);
        }

        public string HashPassword(EndUserList user, string password)
        {
            return _passwordHasher.HashPassword(user, password);
        }
        public async Task<EndUserList?> GetEndUserById(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }
        public async void DeleteEndUser(EndUserList user)
        {
            await _userManager.DeleteAsync(user);
        }

        public async Task<IdentityResult> UpdateEndUser(EndUserList user)
        {
            return await _userManager.UpdateAsync(user);
        }
        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
