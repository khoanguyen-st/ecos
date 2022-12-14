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
    }
}
