using KAS.Entity.DB.ECOS;

namespace KAS.ECOS.API.Entity
{
    public class SmacConfigEntityItem
    {
        public string CustomerID { get; set; } = "";
        public string ClientID { get; set; } = "";
        public List<short> ExcuteCostPriceDB { get; set; } = new List<short>();
        public string SAPMaiSonConfig { get; set; } = "";
    }
    public class SmacConfigEntity
    {
        public string Id { get; set; } = "";
        /// <summary>
        /// 0: Chỉ chạy DB, 1: Chỉ chạy APP, 2: Vừa chạy APP và DB
        /// </summary>
        public short AppRun { get; set; } = 2;
        /// <summary>
        /// Số thread chạy tính dữ liệu chung
        /// </summary>
        public short MaxThread { get; set; } = 1;
        /// <summary>
        /// Số thread chạy tính bill
        /// </summary>
        public short MaxThreadBill { get; set; } = 1;
        /// <summary>
        /// Số thread chạy tính dữ liệu import
        /// </summary>
        public short MaxThreadImport { get; set; } = 1;
        /// <summary>
        /// Số giây gửi log 
        /// </summary>
        public short MaxSecondSendLog { get; set; } = 300;
        /// <summary>
        /// Chạy chức năng tạo SMAC tự động
        /// </summary>
        public bool CreateSmacAppRun { get; set; } = false;
        /// <summary>
        /// Server code để lấy thông tin tạo SMAC tự động
        /// </summary>
        public string CreateSmacServerCode { get; set; } = "";
        /// <summary>
        /// Host của Monitor
        /// </summary>
        public string MonitorHost { get; set; } = "";
        /// <summary>
        /// Domain của KAS.SYNC
        /// </summary>
        public string SyncApi { get; set; } = "";
        /// <summary>
        /// ConnectionString của mongodb
        /// </summary>
        public string MongoHost { get; set; } = "";
        /// <summary>
        /// ConnectionString của postgre
        /// </summary>
        public string PostgreHost { get; set; } = "";
        public string BackupConfig { get; set; } = "";

        public List<SmacConfigEntityItem> Customers { get; set; }=new List<SmacConfigEntityItem>();
    }
}
