using KAS.Entity.DB.ECOS.Entities;

namespace KAS.ECOS.SERVICE.Services;

public interface IRoleService
{
    void SyncRoleApplicationFuntionPermissionList(ECOSContext _context, RoleList mapper, List<string> permissions, Boolean isUpdate = false);
}
