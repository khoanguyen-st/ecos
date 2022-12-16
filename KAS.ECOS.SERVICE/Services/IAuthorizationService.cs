using KAS.ECOS.API.Entity;

namespace KAS.ECOS.SERVICE.Services;

public interface IAuthorizationService
{
    public bool CheckUserPermission(AuthorizationDto mapper);
}