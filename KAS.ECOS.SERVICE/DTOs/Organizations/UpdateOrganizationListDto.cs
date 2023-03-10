using KAS.Entity.DB.ECOS.Entities;

namespace KAS.ECOS.API.Entity
{
    public class UpdateOrganizationListDto
    {
        public Guid Id { get; set; }
        public string OrganizationName { get; set; } = null!;
        public string? OrganizationDescription { get; set; } = null!;
        public string Address { get; set; } = string.Empty;
        public string HandPhone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? ParentId { get; set; }
        public string OrganizationCode { get; set; } = null!;
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? DeletedDate { get; set; }
    }
}