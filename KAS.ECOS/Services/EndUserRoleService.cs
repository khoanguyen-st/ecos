using AutoMapper;
using KAS.Entity.DB.ECOS.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

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
                .AsNoTracking()
                .Include(r => r.UserDevice)
                .Include(r => r.Role)
                .Include(r => r.OrganizationUser)
                .ToListAsync();
        }
        public async Task<EndUserRoleList>? GetEndUserRole(Guid id)
        {
            return await _context.EndUserRoleLists
                .AsNoTracking()
                .Where(r => r.Id == id)
                .FirstOrDefaultAsync();
        }
        public async Task<bool> IsDeviceExist(Guid? deviceId)
        {
            return await _context.UserDeviceLists.Where(d => d.Id == deviceId).AnyAsync();
        }
        public async Task<bool> IsRoleExist(Guid roleId)
        {
            return await _context.RoleLists.Where(r => r.Id == roleId).AnyAsync();
        }
        public async Task<Guid> FindOrganizationUserId(Guid userId)
        {
            var organizationUserId = await _context.OrganizationUserLists
                .Where(u => u.EndUserId == userId)
                .Select(u => u.Id)
                .FirstOrDefaultAsync();

            return organizationUserId;
        }
        public async Task<bool> IsEndUserRoleExist(Guid endUserRoleId)
        {
            return await _context.EndUserRoleLists.Where(r => r.Id == endUserRoleId).AnyAsync();
        }
        public async Task AddEndUserRole(EndUserRoleList endUserRole)
        {
            Console.WriteLine(JsonConvert.SerializeObject(endUserRole));
            await _context.EndUserRoleLists.AddAsync(endUserRole);
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
