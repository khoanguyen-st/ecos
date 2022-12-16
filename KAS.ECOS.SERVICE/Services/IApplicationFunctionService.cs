using KAS.Entity.DB.ECOS.Entities;

namespace KAS.ECOS.API.Services
{
    public interface IApplicationFunctionService
    {
        Task AddApplicationFunction(Guid applicationId, ApplicationFunctionList applicationFunction);
        void DeleteApplicationFunction(ApplicationFunctionList applicationFunction);
        Task<ApplicationList>? GetApplicationExist(Guid applicationId);
        Task<ApplicationFunctionList?> GetApplicationFunction(Guid functionId);
        Task<IEnumerable<ApplicationFunctionList>> GetApplicationFunctions();
        Task<IEnumerable<ApplicationFunctionList>> GetApplicationFunctionsByApplicationId(Guid applicationid);
        Task<bool> IsApplicationExist(Guid applicationId);
        Task<bool> SaveChangesAsync();
    }
}