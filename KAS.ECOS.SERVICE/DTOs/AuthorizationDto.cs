namespace KAS.ECOS.API.Entity;

public class AuthorizationDto
{
    public string UserId { get; set; } = null!;
    public string Permission { get; set; } = null!;
    public Guid OrganizationId;
}