using KAS.Entity.DB.ECOS.Entities;

namespace KAS.ECOS.API.Entity
{
    public class GetRoleListDto
    {
        public Guid Id { get; set; }
        public Guid OrganizationId { get; set; }
        public string RoleName { get; set; } = null!;
        public string? RoleDescription { get; set; }
        public bool IsActive { get; set; } = true;
        public virtual OrganizationList? Organization { get; set; }
    }
}
