using AutoMapper;
using KAS.ECOS.API.Entity;
using KAS.ECOS.SERVICE.Services;
using KAS.Entity.DB.ECOS.Entities;
using Microsoft.AspNetCore.Identity;

namespace KAS.ECOS.API.Services;

public class AuthorizationService: IAuthorizationService
{
    private readonly ECOSContext _context;
    private readonly IMapper _mapper;
    private readonly UserManager<EndUserList> _userManager;

    public AuthorizationService(ECOSContext context, IMapper mapper, UserManager<EndUserList> userManager)
    {
        _context = context;
        _mapper = mapper;
        _userManager = userManager;
    }
    public bool CheckUserPermission(AuthorizationDto mapper)
    {
        var applicationFuntionPermissonList =
            _context.ApplicationFunctionPermissionLists.FirstOrDefault(x => x.Permission == mapper.Permission);

        if(applicationFuntionPermissonList == null)
        {
            return false;
        }

        var Users = _userManager.Users.ToList();

        var permission = (
            from endUser in Users
            join organizationUserList in _context.OrganizationUserLists on mapper.UserId equals organizationUserList
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