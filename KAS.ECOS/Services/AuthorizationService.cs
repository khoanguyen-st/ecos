using AutoMapper;
using KAS.ECOS.SERVICE.DTOs.Authorization;
using KAS.ECOS.SERVICE.Services;
using KAS.Entity.DB.ECOS.Entities;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;

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
    public bool CheckUserPermission(AuthorizationDto authorizationDto)
    {
        var handler = new JwtSecurityTokenHandler();

        var jwt = handler.ReadJwtToken(authorizationDto.Token);

        var userId = jwt.Claims.FirstOrDefault(claim => claim.Type == "userId").Value;
        
        var applicationFuntionPermissonList =
            _context.ApplicationFunctionPermissionLists.FirstOrDefault(x => x.Permission == authorizationDto.Permission);

        if(applicationFuntionPermissonList == null)
        {
            return false;
        }

        var Users = _userManager.Users.ToList();

        if(Users.Any(u => u.Id == userId && (u.Type == "super" || u.Type == "app")) && _context.ApplicationFunctionPermissionLists.Where(p => p.Permission == authorizationDto.Permission).Any())
        {
            return true;
        }

        var permission = (
            from endUser in Users
            join organizationUserList in _context.OrganizationUserLists on userId equals organizationUserList
                .EndUserId
                where Guid.Parse(authorizationDto.OrganizationId) == organizationUserList.OrganizationId
            join endUserRoleList in _context.EndUserRoleLists on organizationUserList.Id equals endUserRoleList
                .OrganizationUserId
            join roleList in _context.RoleLists on endUserRoleList.RoleId equals roleList.Id
            join roleApplicationFunctionPermissionList in _context.RoleApplicationFunctionPermissionLists on roleList.Id
                equals roleApplicationFunctionPermissionList.RoleId
            select roleApplicationFunctionPermissionList).FirstOrDefault(x =>
            x.ApplicationFunctionPermissionId == applicationFuntionPermissonList.Id);

        return permission != null;
    }

    public List<string> ListPermission(GetListPermissionsDto authorizationDto)
    {
        var handler = new JwtSecurityTokenHandler();

        var jwt = handler.ReadJwtToken(authorizationDto.Token);

        var userId = jwt.Claims.FirstOrDefault(claim => claim.Type == "userId").Value;

        //var applicationFuntionPermissonList =
        //    _context.ApplicationFunctionPermissionLists.FirstOrDefault(x => x.Permission == authorizationDto.Permission);

        //if (applicationFuntionPermissonList == null)
        //{
        //    return false;
        //}

        var Users = _userManager.Users.ToList();

        if (Users.Any(u => u.Id == userId && (u.Type == "super" || u.Type == "app")))
        {
            return _context.ApplicationFunctionPermissionLists.Select(a => a.Permission).ToList();
        }

        var permission = (
            from endUser in Users
            join organizationUserList in _context.OrganizationUserLists on userId equals organizationUserList
                .EndUserId
            where Guid.Parse(authorizationDto.OrganizationId) == organizationUserList.OrganizationId
            join endUserRoleList in _context.EndUserRoleLists on organizationUserList.Id equals endUserRoleList
                .OrganizationUserId
            join roleList in _context.RoleLists on endUserRoleList.RoleId equals roleList.Id
            join roleApplicationFunctionPermissionList in _context.RoleApplicationFunctionPermissionLists on roleList.Id
                equals roleApplicationFunctionPermissionList.RoleId
            join applicationFunctionPermissionList in _context.ApplicationFunctionPermissionLists on roleApplicationFunctionPermissionList.ApplicationFunctionPermissionId equals applicationFunctionPermissionList.Id
            select applicationFunctionPermissionList.Permission).ToList();

        return permission;
    }
}