using AutoMapper;
using KAS.ECOS.API.Entity;
using KAS.ECOS.SERVICE.Services;
using KAS.Entity.DB.ECOS.Entities;

namespace KAS.ECOS.API.Services;

public class AuthorizationService: IAuthorizationService
{
    private readonly ECOSContext _context;
    private readonly IMapper _mapper;

    public AuthorizationService(ECOSContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public bool CheckUserPermission(AuthorizationDto mapper)
    {
        var applicationFuntionPermissonList =
            _context.ApplicationFunctionPermissionLists.FirstOrDefault(x => x.Permission == mapper.Permission);
        var permission = (
            from endUser in _context.EndUserLists
            join organizationUserList in _context.OrganizationUserLists on endUser.Id equals organizationUserList
                .EndUserId
            join endUserRoleList in _context.EndUserRoleLists on organizationUserList.Id equals endUserRoleList
                .OrganizationUserId
            join roleList in _context.RoleLists on endUserRoleList.RoleId equals roleList.Id
            join roleApplicationFunctionPermissionList in _context.RoleApplicationFunctionPermissionLists on roleList.Id
                equals roleApplicationFunctionPermissionList.RoleId
            select roleApplicationFunctionPermissionList).FirstOrDefault(x =>
            x.ApplicationFunctionPermissionId == applicationFuntionPermissonList.Id);
        return permission != null;
    }
}