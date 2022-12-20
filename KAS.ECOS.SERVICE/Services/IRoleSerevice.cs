using KAS.ECOS.API.Entity;
using KAS.Entity.DB.ECOS.Entities;

namespace KAS.ECOS.SERVICE.Services;

public interface IRoleService
{
    public Task SyncRoleApplicationFuntionPermissionList(RoleList mapper, List<string> permissions, Boolean isUpdate = false);
    public List<RoleList> GetRoleLists();
    public RoleList GetRoleById(Guid id);
    public Task<RoleList> CreateRoleList(RoleList mapper, List<string> permissions);
    public Task UpdateRoleList(UpdateRoleListDto roleList, Guid id, List<string> permissions);
    public void DeleteRoleList(Guid id);
}
