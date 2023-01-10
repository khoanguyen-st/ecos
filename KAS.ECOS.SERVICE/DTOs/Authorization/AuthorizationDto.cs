namespace KAS.ECOS.SERVICE.DTOs.Authorization;

public class AuthorizationDto
{
    public string Token { get; set; } = null!;
    public string Permission { get; set; } = null!;
    public string OrganizationId { get; set; } = null!;
}