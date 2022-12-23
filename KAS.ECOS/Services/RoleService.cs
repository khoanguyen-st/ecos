using AutoMapper;
using KAS.ECOS.API.Entity;
using KAS.Entity.DB.ECOS.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace KAS.ECOS.SERVICE.Services;

public class RoleService : IRoleService
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
        return _context.RoleLists
            .AsNoTracking()
            .Include(u => u.Organization)
            .ToList();
    }

    public RoleList? GetRoleById(Guid id)
    {
        return _context.RoleLists
            .AsNoTracking()
            .Include(u => u.Organization)
            .FirstOrDefault(x => x.Id == id);
    }

    public async Task<RoleList> CreateRoleList(RoleList mapper, List<string> permissions)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            _context.RoleLists.Add(mapper);


            await SyncRoleApplicationFunctionPermissionList(mapper, permissions);

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return mapper;
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            throw e;
        }
    }

    public async Task UpdateRoleList(UpdateRoleListDto roleList, Guid id, List<string> permissions)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            var role = await _context.RoleLists.Where(r => r.Id == id).FirstOrDefaultAsync();

            _mapper.Map(roleList, role);

            await SyncUpdateRoleApplicationFunctionPermissionList(role, permissions);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
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

    public async Task SyncRoleApplicationFunctionPermissionList(RoleList roleToCreate, List<string> permissions)
    {
        try
        {
            var adminRole = await _context.RoleLists
                .Where(r => r.OrganizationId == roleToCreate.OrganizationId & r.IsBaseRole == true)
                .FirstOrDefaultAsync();

            foreach (var applicationPermission in permissions
                         .Select(permission => 
                             _context.ApplicationFunctionPermissionLists.FirstOrDefault(b => 
                                 b.Permission == permission)))
            {
                if (adminRole == null || applicationPermission == null ||
                    !await CheckRoleContainsPermission(applicationPermission.Id, adminRole.Id)) continue;

                var roleApplicationFunctionPermissionList = new RoleApplicationFunctionPermissionList()
                {
                    ApplicationFunctionPermissionId = applicationPermission.Id,
                    RoleId = roleToCreate.Id
                };

                _context.RoleApplicationFunctionPermissionLists.Add(roleApplicationFunctionPermissionList);
            }
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    public async Task SyncUpdateRoleApplicationFunctionPermissionList(RoleList roleToUpdate, List<string> permissions)
    {
        try
        {
            var roleApplicationFunctionPermissionLists =
                _context.RoleApplicationFunctionPermissionLists.Where(u => u.RoleId == roleToUpdate.Id).ToList();
            _context.RoleApplicationFunctionPermissionLists.RemoveRange(roleApplicationFunctionPermissionLists);

            foreach (var applicationPermission in permissions
                         .Select(permission =>
                             _context.ApplicationFunctionPermissionLists.FirstOrDefault(b =>
                                 b.Permission == permission)))
            {
                if (!await CheckRoleContainsPermission(applicationPermission.Id, roleToUpdate.Id))
                {
                    var rolePermissionToDelete =
                        _context.RoleApplicationFunctionPermissionLists
                            .FirstOrDefault(p => p.RoleId == roleToUpdate.Id && p.ApplicationFunctionPermissionId == applicationPermission.Id);
                    _context.RoleApplicationFunctionPermissionLists.Remove(rolePermissionToDelete);
                }
                else
                {
                    var roleApplicationFunctionPermissionList = new RoleApplicationFunctionPermissionList()
                    {
                        ApplicationFunctionPermissionId = applicationPermission.Id,
                        RoleId = roleToUpdate.Id
                    };

                    _context.RoleApplicationFunctionPermissionLists.Add(roleApplicationFunctionPermissionList);
                }
            }
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task SyncDeleteRole(RoleList roleToDelete)
    {
        var roleApplicationFunctionPermissions =
            await _context.RoleApplicationFunctionPermissionLists
                .Where(p => p.RoleId == roleToDelete.Id)
                .ToListAsync();
        _context.RoleApplicationFunctionPermissionLists.RemoveRange(roleApplicationFunctionPermissions);

        var endUserRoles =
            await _context.EndUserRoleLists
                .Where(r => r.RoleId == roleToDelete.Id)
                .ToListAsync();
        _context.EndUserRoleLists.RemoveRange(endUserRoles);

        await _context.SaveChangesAsync();
    }

    public async Task<bool> CheckRoleContainsPermission(Guid applicationFunctionPermissionId, Guid roleId)
    {

        var result = await _context.RoleApplicationFunctionPermissionLists
            .Where(p => 
                p.ApplicationFunctionPermissionId == applicationFunctionPermissionId && p.RoleId == roleId)
            .AnyAsync();
        return result;
    }
}