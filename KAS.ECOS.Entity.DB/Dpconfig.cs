using System;
using System.Collections.Generic;

namespace KAS.Entity.DB.ECOS
{
    public partial class Dpconfig
    {
        public Dpconfig()
        {
            DpconfigCustomers = new HashSet<DpconfigCustomer>();
        }

        /// <summary>
        /// Client ID của Dataproccessor
        /// </summary>
        public string Id { get; set; } = null!;
        /// <summary>
        /// 0: Chỉ chạy DB, 1: Chỉ chạy APP, 2: Vừa chạy APP và DB
        /// </summary>
        public short AppRun { get; set; }
        /// <summary>
        /// Số thread chạy tính dữ liệu chung
        /// </summary>
        public short MaxThread { get; set; }
        /// <summary>
        /// Số thread chạy tính bill
        /// </summary>
        public short MaxThreadBill { get; set; }
        /// <summary>
        /// Số thread chạy tính dữ liệu import
        /// </summary>
        public short MaxThreadImport { get; set; }
        /// <summary>
        /// Số giây gửi log 
        /// </summary>
        public short MaxSecondSendLog { get; set; }
        /// <summary>
        /// Chạy chức năng tạo SMAC tự động
        /// </summary>
        public bool CreateSmacAppRun { get; set; }
        /// <summary>
        /// Server code để lấy thông tin tạo SMAC tự động
        /// </summary>
        public string? CreateSmacServerCode { get; set; }
        /// <summary>
        /// Host của Monitor
        /// </summary>
        public string? MonitorHost { get; set; }
        /// <summary>
        /// Domain của KAS.SYNC
        /// </summary>
        public string? SyncApi { get; set; }
        /// <summary>
        /// Trạng thái dữ liệu
        /// </summary>
        public bool IsDeleted { get; set; }
        /// <summary>
        /// ConnectionString của mongodb
        /// </summary>
        public string? MongoHost { get; set; }
        /// <summary>
        /// ConnectionString của postgre
        /// </summary>
        public string? PostgreHost { get; set; }
        /// <summary>
        /// Cấu hình backup
        /// </summary>
        public string? BackupConfig { get; set; }

        public virtual ICollection<DpconfigCustomer> DpconfigCustomers { get; set; }
    }
}
