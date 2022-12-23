using KAS.Entity.DB.ECOS.Entities;

namespace KAS.ECOS.API.Entity
{
    public class UpdateRoleListDto
    {
        public Guid OrganizationId { get; set; }
        public string RoleName { get; set; } = null!;
        public string? RoleDescription { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsBaseRole { get; set; } = false;
        public List<string> Permissions { get; set; } = new List<string>();
    }
}
