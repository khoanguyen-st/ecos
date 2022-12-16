using KAS.Entity.DB.ECOS.Entities;

namespace KAS.ECOS.SERVICE.Services
{
    public class RoleService: IRoleService
    {
        public async void SyncRoleApplicationFuntionPermissionList(ECOSContext _context, RoleList mapper, List<string> permissions, Boolean isUpdate = false)
        {
            if (isUpdate)
            {
                var roleApplicationFuntionPermissionLists =
                    _context.RoleApplicationFunctionPermissionLists.Where(u => u.RoleId == mapper.Id).ToList();
                _context.RoleApplicationFunctionPermissionLists.RemoveRange(roleApplicationFuntionPermissionLists);
            }
            try
            {
                foreach (var permission in permissions)
                {
                    var applicationPermission =
                        _context.ApplicationFunctionPermissionLists.FirstOrDefault(b => b.Permission == permission);

                    var roleApplicationFuntionPermissionList = new RoleApplicationFunctionPermissionList()
                    {
                        ApplicationFunctionPermissionId = applicationPermission.Id,
                        RoleId = mapper.Id
                    };
                    _context.RoleApplicationFunctionPermissionLists.Add(roleApplicationFuntionPermissionList);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
