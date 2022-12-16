using AutoMapper;
using KAS.ECOS.API.Entity;
using KAS.Entity.DB.ECOS.Entities;
using Microsoft.EntityFrameworkCore;

namespace KAS.ECOS.SERVICE.Services
{
    public class RoleService: IRoleService
    {
        private readonly ECOSContext _context;
        private readonly IMapper _mapper;
        public RoleService(ECOSContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public List<RoleList> GetRoleLists()
        {
            return _context.RoleLists.Include(u => u.Organization).ToList();
        }

        public RoleList GetRoleById(Guid id)
        {
            return _context.RoleLists.Include(u => u.Organization).FirstOrDefault(x => x.Id == id);
        }

        public async Task<RoleList> CreateRoleList(RoleList mapper, List<string> permissions)
        {
            try
            {
                _context.RoleLists.Add(mapper);
                await _context.SaveChangesAsync();
                // this.SyncRoleApplicationFuntionPermissionList(mapper, permissions);
                return mapper;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void UpdateRoleList(UpdateRoleListDto roleList, Guid id, List<string> permissions)
        {
            try
            {
                var role = _context.RoleLists.Find(id);
                _mapper.Map(roleList, role);
                _context.SaveChanges();
                // this.SyncRoleApplicationFuntionPermissionList(mapper, permissions, true);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void DeleteRoleList(Guid id)
        {
            try
            {
                var role = _context.RoleLists.Find(id);
            
                _context.RoleLists.Remove(role);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        
        public async void SyncRoleApplicationFuntionPermissionList(RoleList mapper, List<string> permissions, Boolean isUpdate = false)
        {
            try
            {
                if (isUpdate)
                {
                    var roleApplicationFuntionPermissionLists =
                        _context.RoleApplicationFunctionPermissionLists.Where(u => u.RoleId == mapper.Id).ToList();
                    _context.RoleApplicationFunctionPermissionLists.RemoveRange(roleApplicationFuntionPermissionLists);
                }
                
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
                throw e;
            }
        }
    }
}
