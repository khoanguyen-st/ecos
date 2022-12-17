using KAS.Entity.DB.ECOS.Entities;

namespace KAS.ECOS.API.Services
{
    public interface IApplicationFunctionPermissionService
    {
        Task AddApplicationFunctionPermission(Guid functionId, ApplicationFunctionPermissionList functionPermission);
        void DeleteApplicationFunction(ApplicationFunctionPermissionList functionPermission);
        Task<ApplicationFunctionList>? GetApplicationFunctionExist(Guid functionId);
        Task<ApplicationFunctionPermissionList?> GetApplicationFunctionPermission(Guid permissionId);
        Task<IEnumerable<ApplicationFunctionPermissionList>> GetApplicationFunctionPermissions();
        Task<IEnumerable<ApplicationFunctionPermissionList>> GetApplicationFunctionsPermissionByFunctionId(Guid functionId);
        Task<bool> IsApplicationFunctionExist(Guid functionId);
        Task<bool> IsApplicationFunctionPermissionExist(Guid permissionId);
        Task<bool> SaveChangesAsync();
    }
}