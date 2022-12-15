using KAS.ECOS.SERVICE.DTOs;
using KAS.ECOS.SERVICE.Services;
using KAS.Entity.DB.ECOS.Entities;
using System.Data.Entity;

namespace KAS.ECOS.API.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly ECOSContext _context;

        public ApplicationService(ECOSContext context)
        {
            _context = context;
        }
        public IEnumerable<ApplicationList> GetApplications()
        {
            return _context.ApplicationLists.ToList();
        }
        public void AddApplication(ApplicationList application)
        {
            _context.ApplicationLists.Add(application);
            _context.SaveChanges();
        }
        public bool ApplicationExist(string applicationId)
        {
            return _context.ApplicationLists.Any(a => a.Id == Guid.Parse(applicationId));
        }
        public ApplicationList? GetApplicationById(string applicationId)
        {
            return _context.ApplicationLists.Where(a => a.Id == Guid.Parse(applicationId)).FirstOrDefault();
        }
        public void DeleteApplication(ApplicationList application)
        {
            _context.ApplicationLists.Remove(application);
        }
        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
