using KAS.ECOS.API.Entity;

namespace KAS.ECOS.API.Services
{
    public interface IAuthService
    {
        string Authenticate(LoggedUserDTO validatedAccount);
        //LoggedUserDTO ValidateUser(string Email, string Password);
    }
}
