using KAS.Entity.DB.ECOS.Entities;

namespace KAS.ECOS.API.Entity
{
    public class AddRoleListDto
    {
        public Guid OrganizationId { get; set; }
        public string RoleName { get; set; } = null!;
        public string? RoleDescription { get; set; }
        public bool IsActive { get; set; } = true;
        public List<string> Permissions { get; set; } = new List<string>();
    }
}
