using KAS.Entity.DB.ECOS.Entities;

namespace KAS.ECOS.API.Entity
{
    public class GetOrganizationListDto
    {
        public string Id { get; set; } = null!;
        public string OrganizationName { get; set; } = null!;
        public string? OrganizationDescription { get; set; } = null!;
        public string OrganizationCode { get; set; } = null!;
    }
}