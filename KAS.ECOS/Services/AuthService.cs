using KAS.ECOS.API.Entity;

namespace KAS.ECOS.API.Services
{
    public class AuthService
    {
        public LoginDTO ValidateUser(string userName, string password)
        {
            //var user = _context.EndUserLists.Where(u => u.Username == userName && u.Password == password).FirstOrDefault();
            //return new LoginDTO { UserName = userName, Password = password };
            return new LoginDTO("khoanguyen", "123123");
        }
    }
}
