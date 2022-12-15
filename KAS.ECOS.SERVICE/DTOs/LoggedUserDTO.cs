namespace KAS.ECOS.API.Entity
{
    public class LoggedUserDTO
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public LoggedUserDTO(Guid id, string username, string email)
        {
            Id = id;
            UserName = username;
            Email = email;
        }
    }
}
