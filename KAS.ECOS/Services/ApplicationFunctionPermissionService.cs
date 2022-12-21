using KAS.Entity.DB.ECOS.Entities;
using Microsoft.EntityFrameworkCore;

namespace KAS.ECOS.API.Services
{
    public class ApplicationFunctionPermissionService : IApplicationFunctionPermissionService
    {
        private readonly ECOSContext _context;

        public ApplicationFunctionPermissionService(ECOSContext context)
        {
            _context = context;
        }

        public async Task<ApplicationFunctionList>? GetApplicationFunctionExist(Guid functionId)
        {
            return await _context.ApplicationFunctionLists
                .AsNoTracking()
                .Where(a => a.Id == functionId)
                .FirstOrDefaultAsync();
        }
        public async Task<bool> IsApplicationFunctionExist(Guid functionId)
        {
            return await _context.ApplicationFunctionLists
                .Where(a => a.Id == functionId)
                .AnyAsync();
        }
        public async Task<bool> IsApplicationFunctionPermissionExist(Guid permissionId)
        {
            return await _context.ApplicationFunctionPermissionLists
                .Where(a => a.Id == permissionId)
                .AnyAsync();
        }
        public async Task AddApplicationFunctionPermission(Guid functionId, ApplicationFunctionPermissionList functionPermission)
        {
            _context.ApplicationFunctionPermissionLists.Add(functionPermission);
        }

        public void DeleteApplicationFunction(ApplicationFunctionPermissionList functionPermission)
        {
            _context.ApplicationFunctionPermissionLists.Remove(functionPermission);
        }

        public async Task<ApplicationFunctionPermissionList?> GetApplicationFunctionPermission(Guid permissionId)
        {
            return await _context.ApplicationFunctionPermissionLists
                    .Include(p => p.ApplicationFunction)
                    .Where(f => f.Id == permissionId)
                    .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ApplicationFunctionPermissionList>> GetApplicationFunctionPermissions()
        {
            return await _context.ApplicationFunctionPermissionLists
                .AsNoTracking()
                .Include(p => p.ApplicationFunction)
                    .ThenInclude(f => f.Application)
                .ToListAsync();
        }

        public async Task<IEnumerable<ApplicationFunctionPermissionList>> GetApplicationFunctionsPermissionByFunctionId(Guid functionId)
        {
            return await _context.ApplicationFunctionPermissionLists
                .AsNoTracking()
                .Where(f => f.ApplicationFunctionId == functionId)
                .ToListAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
