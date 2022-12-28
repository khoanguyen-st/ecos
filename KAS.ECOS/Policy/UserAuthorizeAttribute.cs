using Microsoft.AspNetCore.Authorization;

namespace KAS.ECOS.API.Policy
{
    public class UserAuthorizeAttribute : AuthorizeAttribute
    {
        const string POLICY_PREFIX = "UserAuthorize";

        public UserAuthorizeAttribute(string requiredPermission) => Permission = requiredPermission;

        public string? Permission
        {
            get
            {
                return Policy?.Substring(POLICY_PREFIX.Length);
            } 
            set
            {
                Policy = $"{POLICY_PREFIX}{value}";
            }
        }
    }
}
