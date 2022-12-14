namespace KAS.ECOS.API.Entity
{
    public class LoggedUserDTO
    {
        public string Id { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public LoggedUserDTO(string id, string username, string email)
        {
            Id = id;
            UserName = username;
            Email = email;
        }
    }
}
