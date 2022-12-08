namespace KAS.ECOS.API.Entity
{
    public class LoginDTO
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public LoginDTO(string username, string password)
        {
            UserName = username;
            Password = password;
        }
    }
}
