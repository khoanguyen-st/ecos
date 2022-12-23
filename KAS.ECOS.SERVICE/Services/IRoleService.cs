using KAS.ECOS.API.Entity;
using KAS.Entity.DB.ECOS.Entities;

namespace KAS.ECOS.SERVICE.Services;

public interface IRoleService
{
    public Task SyncRoleApplicationFunctionPermissionList(RoleList roleToCreate, List<string> permissions);
    public List<RoleList> GetRoleLists();
    public RoleList GetRoleById(Guid id);
    public Task<RoleList> CreateRoleList(RoleList roleToCreate, List<string> permissions);
    public Task UpdateRoleList(UpdateRoleListDto roleList, Guid id, List<string> permissions);
    public void DeleteRoleList(Guid id);
    public Task SyncUpdateRoleApplicationFunctionPermissionList(RoleList roleToUpdate, List<string> permissions);
    public Task SyncDeleteRole(RoleList roleToDelete);
}
