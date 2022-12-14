namespace KAS.ECOS.API.Entity
{
    public class RequestDTO
    {
        //public RequestDTO()
        //{
        //    permissions = new HashSet<string>();
        //}
        public string accessToken { get; set; }
        public string refreshToken { get; set; }
        public string organizationId { get; set; }
        public string deviceId { get; set; }
        public ICollection<string> permissions { get; set; } = new List<string>();
    }
}
