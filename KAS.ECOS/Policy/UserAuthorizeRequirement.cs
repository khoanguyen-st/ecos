using Microsoft.AspNetCore.Authorization;

namespace KAS.ECOS.API.Policy
{
    public class UserAuthorizeRequirement : IAuthorizationRequirement
    {
        public string Permission { get; set; }
        public UserAuthorizeRequirement(string permission)
        {
            Permission = permission;
        }
    }
}
