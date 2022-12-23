using KAS.ECOS.SERVICE.Services;
using KAS.Entity.DB.ECOS.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;

namespace KAS.ECOS.API.Services
{
    public class ApplicationFunctionService : IApplicationFunctionService
    {
        private readonly ECOSContext _context;

        public ApplicationFunctionService(ECOSContext context)
        {
            _context = context;
        }

        public async Task<bool> IsApplicationExist(Guid applicationId)
        {
            return await _context.ApplicationLists
                .Where(a => a.Id == applicationId)
                .AnyAsync();
        }
        public async Task AddApplicationFunction(Guid applicationId, ApplicationFunctionList? applicationFunction)
        {
            _context.ApplicationFunctionLists.Add(applicationFunction);
        }

        public void DeleteApplicationFunction(ApplicationFunctionList? applicationFunction)
        {
            _context.ApplicationFunctionLists.Remove(applicationFunction);
        }

        public async Task<ApplicationFunctionList?> GetApplicationFunction(Guid functionId)
        {
            var result = await _context.ApplicationFunctionLists
                .FirstOrDefaultAsync(f => f.Id == functionId);

            Console.WriteLine(JsonConvert.SerializeObject(result));

            return result;
        }

        public async Task<IEnumerable<ApplicationFunctionList>> GetApplicationFunctions()
        {
            return await _context.ApplicationFunctionLists
                .AsNoTracking()
                .Include(f => f.Application)
                .Include(f => f.ApplicationPermissions)
                .ToListAsync();
        }

        public async Task<IEnumerable<ApplicationFunctionList?>> GetApplicationFunctionsByApplicationId(Guid applicationId)
        {
            return await _context.ApplicationFunctionLists
                .AsNoTracking()
                .Where(f => f.ApplicationId == applicationId)
                .ToListAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
