using KAS.Entity.DB.ECOS.Entities;

namespace KAS.ECOS.API.Entity
{
    public class AddOrganizationListDto
    {
        public string OrganizationName { get; set; } = null!;
        public string? OrganizationDescription { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string HandPhone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? ParentId { get; set; }
        public string OrganizationCode { get; set; } = null!;
    }
}