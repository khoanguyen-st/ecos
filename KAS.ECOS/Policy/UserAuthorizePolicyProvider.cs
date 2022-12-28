using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace KAS.ECOS.API.Policy
{
    public class UserAuthorizePolicyProvider : IAuthorizationPolicyProvider
    {
        const string POLICY_PREFIX = "UserAuthorize";
        public DefaultAuthorizationPolicyProvider FallbackPolicyProvider { get; }

        public UserAuthorizePolicyProvider(IOptions<AuthorizationOptions> options)
        {
            FallbackPolicyProvider = new DefaultAuthorizationPolicyProvider(options);
        }
        public Task<AuthorizationPolicy> GetDefaultPolicyAsync()
        {
            return Task.FromResult(
                new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
        }

        public Task<AuthorizationPolicy> GetFallbackPolicyAsync()
        {
            return Task.FromResult<AuthorizationPolicy?>(null);
        }

        public Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            if (policyName.StartsWith(POLICY_PREFIX, StringComparison.OrdinalIgnoreCase))
            {
                var policy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme);
                policy.AddRequirements(new UserAuthorizeRequirement(policyName.Substring(POLICY_PREFIX.Length)));
                return Task.FromResult(policy.Build());
            }
            return Task.FromResult<AuthorizationPolicy?>(null);
        }
    }
}
