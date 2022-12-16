using AutoMapper;
using KAS.Entity.DB.ECOS.Entities;
using Microsoft.EntityFrameworkCore;

namespace KAS.ECOS.API.Services
{
    public class EndUserRoleService : IEndUserRoleService
    {
        private readonly ECOSContext _context;
        public EndUserRoleService(ECOSContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<EndUserRoleList>> GetEndUserRoles()
        {
            return await _context.EndUserRoleLists
                .Include(r => r.UserDevice)
                .Include(r => r.Role)
                .Include(r => r.OrganizationUser)
                .ToListAsync();
        }
        public async Task<EndUserRoleList>? GetEndUserRole(Guid id)
        {
            return await _context.EndUserRoleLists.Where(r => r.Id == id).FirstOrDefaultAsync();
        }
        public async Task<bool> IsDeviceExist(Guid deviceId)
        {
            return await _context.UserDeviceLists.Where(d => d.Id == deviceId).AnyAsync();
        }
        public async Task<bool> IsRoleExist(Guid roleId)
        {
            return await _context.RoleLists.Where(r => r.Id == roleId).AnyAsync();
        }
        public async Task<bool> IsOrganizationUserExist(Guid organizationUserId)
        {
            return await _context.OrganizationUserLists.Where(r => r.Id == organizationUserId).AnyAsync();
        }
        public async Task<bool> IsEndUserRoleExist(Guid endUserRoleId)
        {
            return await _context.EndUserRoleLists.Where(r => r.Id == endUserRoleId).AnyAsync();
        }
        public void AddEndUserRole(EndUserRoleList endUserRole)
        {
            _context.EndUserRoleLists.Add(endUserRole);
        }
        public void DeleteEndUserRole(EndUserRoleList endUserRole)
        {
            _context.EndUserRoleLists.Remove(endUserRole);
        }
        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
