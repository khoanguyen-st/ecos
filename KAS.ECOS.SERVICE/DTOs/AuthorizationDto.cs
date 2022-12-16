namespace KAS.ECOS.API.Entity;

public class AuthorizationDto
{
    public Guid UserId { get; set; }
    public string Permission { get; set; } = null!;
}