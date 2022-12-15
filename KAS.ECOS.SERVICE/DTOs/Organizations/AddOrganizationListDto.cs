using KAS.Entity.DB.ECOS.Entities;

namespace KAS.ECOS.API.Entity
{
    public class AddOrganizationListDto
    {
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
        public ICollection<OrganizationProfileList>? OrganizationProfileLists { get; set; }
        public ICollection<OrganizationDeviceList>? OrganizationDeviceLists { get; set; }
        public ICollection<OrganizationUserList>? OrganizationUserLists { get; set; }
        public ICollection<RoleList>? RoleLists { get; set; }
    }
}