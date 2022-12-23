using KAS.Entity.DB.ECOS.Entities;

namespace KAS.ECOS.API.Services
{
    public interface IEndUserRoleService
    {
        Task AddEndUserRole(EndUserRoleList endUserRole);
        void DeleteEndUserRole(EndUserRoleList endUserRole);
        Task<EndUserRoleList>? GetEndUserRole(Guid id);
        Task<IEnumerable<EndUserRoleList>> GetEndUserRoles();
        Task<bool> IsDeviceExist(Guid? deviceId);
        Task<bool> IsEndUserRoleExist(Guid endUserRoleId);
        Task<Guid> FindOrganizationUserId(Guid organizationUserId);
        Task<bool> IsRoleExist(Guid roleId);
        Task<bool> SaveChangesAsync();
    }
}