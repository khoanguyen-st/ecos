namespace KAS.ECOS.API.Entity
{
    public class CustomerDTO
    {
        public CustomerDTO() { }
        public string  ID { get; set; } = "";
        public string  Name { get; set; } = "";
        public string? ParentID { get; set; } = null;
        public string Address { get; set; } = "";
        public string Email { get; set; } = "";
        public string HandPhone { get; set; } = "";
        public string Description { get; set; } = "";
        public string SMAC_CUSTOMERCODE { get; set; } = "";
        public List<CustomerProfilesDTO> profiles { get; set; } = new List<CustomerProfilesDTO>();
    }
    public class CustomerProfilesDTO
    {
        public CustomerProfilesDTO() { }
        public string KasProductID { get; set; } = "";
        public List<ProfileEntity> App { get; set; } = new List<ProfileEntity>();
        public List<ProfileEntity> DB { get; set; } = new List<ProfileEntity>();
    }
}
