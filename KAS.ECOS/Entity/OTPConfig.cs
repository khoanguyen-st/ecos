namespace KAS.ECOS.API.Entity
{
    public class SMSConfig
    {
        public SMSConfig() { }
        
        /// <summary>
        /// Mã chi nhánh
        /// </summary>
        public string SiteId { get; set; } = "";
        /// <summary>
        /// nội dung tin nhắn
        /// </summary>
        public string Content { get; set; } = "";
    }

    public class SMSConfigFirebase : SMSConfig
    {
        public SMSConfigFirebase() { }

        /// <summary>
        /// token của đơn vị cung cấp SMS
        /// </summary>
        public string Token { get; set; } = "";
        /// <summary>
        /// Mã xác thực domain (xử dụng cho firebase)
        /// </summary>
        public string AuthDomain { get; set; } = "";
        /// <summary>
        /// mã dự án (xử dụng cho firebase)
        /// </summary>
        public string ProjectId { get; set; } = "";
        /// <summary>
        /// mã bucket (xử dụng cho firebase)
        /// </summary>
        public string StorageBucket { get; set; } = "";
        /// <summary>
        /// mã messaging (xử dụng cho firebase)
        /// </summary>
        public string MessagingSenderId { get; set; } = "";
        /// <summary>
        /// mã ứng dụng (xử dụng cho firebase)
        /// </summary>
        public string AppId { get; set; } = "";
        /// <summary>
        /// (xử dụng cho firebase)
        /// </summary>
        public string MeasurementId { get; set; } = "";
        /// <summary>
        /// url đăng ký trên firebase (xử dụng cho firebase)
        /// </summary>
        public string UrlAuth { get; set; } = "";
    }

    public class SMSConfigESMS : SMSConfig
    {
        public SMSConfigESMS(){}

        /// <summary>
        /// Tên thương hiệu đăng ký với nhà cung cấp
        /// </summary>
        public string BrandName { get; set; } = "";
        /// <summary>
        /// Do ESMS cung cấp
        /// </summary>
        public string ApiKey { get; set; } = "";
        /// <summary>
        /// Do ESMS cung cấp
        /// </summary>
        public string SecretKey { get; set; } = "";
        /// <summary>
        /// Loại tin nhắn
        /// <= 2 là số chăm sóc khách hàng theo brand name, số không cố định
        /// > 2 && <= 8 là số cố dịnh
        /// </summary>
        public int SmsType { get; set; } = 0;
        /// <summary>
        /// 0 = không chứa mã unicode; 1= có chứa mã unicode
        /// </summary>
        public int IsUnicode { get; set; } = 0;
    }

    public class SMSConfigCMC : SMSConfig
    {
        public SMSConfigCMC(){}

        /// <summary>
        /// Tên thương hiệu đăng ký với nhà cung cấp
        /// </summary>
        public string BrandName { get; set; } = "";
        /// <summary>
        /// Do CMC cung cấp
        /// </summary>
        public string Username { get; set; } = "";
        /// <summary>
        /// Do CMC cung cấp
        /// </summary>
        public string Password { get; set; } = "";
        /// <summary>
        /// 0 = không chứa mã unicode; 1= có chứa mã unicode
        /// </summary>
        public int IsUnicode { get; set; } = 0;
    }
}
