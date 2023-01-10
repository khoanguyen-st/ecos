using KAS.ECOS.SERVICE.DTOs.Authorization;
using KAS.Entity.DB.ECOS.Entities;

namespace KAS.ECOS.SERVICE.Services;

public interface IAuthorizationService
{
    public bool CheckUserPermission(AuthorizationDto authorizationDto);
    public List<string> ListPermission(GetListPermissionsDto authorizationDto);
}