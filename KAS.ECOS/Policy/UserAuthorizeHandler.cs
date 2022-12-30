using System.Data.Entity;
using KAS.Entity.DB.ECOS.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace KAS.ECOS.API.Policy
{
    public class UserAuthorizeHandler : AuthorizationHandler<UserAuthorizeRequirement>
    {
        private readonly ECOSContext _context;
        private readonly UserManager<EndUserList> _userManager;

        public UserAuthorizeHandler(ECOSContext context, UserManager<EndUserList> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext ctx, UserAuthorizeRequirement requirement)
        {
            if (!ctx.User.HasClaim((c => c.Type == "userId")))
            {
                return Task.FromResult(0);
            }

            var userId = ctx.User.FindFirst(c => c.Type == "userId")?.Value;

            if (_context.Users.Any(u => u.Id == userId && u.Type == "super"))
            {
                ctx.Succeed(requirement);
                return Task.CompletedTask;
            }

            var applicationFunctionPermissionList =
                _context.ApplicationFunctionPermissionLists.FirstOrDefault(p => p.Permission == requirement.Permission);

            if (applicationFunctionPermissionList == null)
            {
                return Task.FromResult(0);
            }

            var Users =  _userManager.Users.ToList();

            var permission = (
                from endUser in Users
                join organizationUserList in _context.OrganizationUserLists on userId equals organizationUserList
                    .EndUserId
                join endUserRoleList in _context.EndUserRoleLists on organizationUserList.Id equals endUserRoleList
                    .OrganizationUserId
                join roleList in _context.RoleLists on endUserRoleList.RoleId equals roleList.Id
                join roleApplicationFunctionPermissionList in _context.RoleApplicationFunctionPermissionLists on roleList.Id
                    equals roleApplicationFunctionPermissionList.RoleId
                select roleApplicationFunctionPermissionList).FirstOrDefault(x =>
                x.ApplicationFunctionPermissionId == applicationFunctionPermissionList.Id);

            if (permission == null)
            {
                return Task.FromResult(0);
            }

            ctx.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}
