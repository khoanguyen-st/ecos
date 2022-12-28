using KAS.Entity.DB.ECOS.Entities;
using Microsoft.AspNetCore.Authorization;

namespace KAS.ECOS.API.Policy
{
    public class UserAuthorizeHandler : AuthorizationHandler<UserAuthorizeRequirement>
    {
        private readonly ECOSContext _context;

        public UserAuthorizeHandler(ECOSContext context)
        {
            _context = context;
        }
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext ctx, UserAuthorizeRequirement requirement)
        {
            if (!ctx.User.HasClaim((c => c.Type == "userId")))
            {
                return Task.FromResult(0);
            }

            var userId = ctx.User.FindFirst(c => c.Type == "userId").Value;

            var applicationFunctionPermissonList =
                _context.ApplicationFunctionPermissionLists.FirstOrDefault(p => p.Permission == requirement.Permission);

            if (applicationFunctionPermissonList == null)
            {
                return Task.FromResult(0);
            }

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
                x.ApplicationFunctionPermissionId == applicationFunctionPermissonList.Id);

            if (permission == null)
            {
                return Task.FromResult(0);
            }

            ctx.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}
