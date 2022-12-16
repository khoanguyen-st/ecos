using KAS.Entity.DB.ECOS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAS.ECOS.SERVICE.Services
{
    public interface IApplicationFunctionService
    {
        Task<IEnumerable<ApplicationFunctionList>> GetApplicationFunctions();
        Task<IEnumerable<ApplicationFunctionList>> GetApplicationFunctionsByApplicationId(Guid applicationId);
        Task<ApplicationFunctionList> GetApplicationFunction(Guid applicationId, Guid functionId);
        Task AddApplicationFunction(Guid applicationId, ApplicationFunctionList applicationFunction);
        Task UpdateApplicationFunction(Guid applicationId, ApplicationFunctionList applicationFunction);
        void DeleteApplicationFunction(ApplicationFunctionList applicationFunction);
        Task<ApplicationList> GetApplicationExist(Guid applicationId);
        Task<bool> SaveChangesAsync();
    }
}
