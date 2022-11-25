using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace KAS.Entity.DB.ECOS
{
    public partial class KASECOSContext : DbContext
    {
        string cn;
        public KASECOSContext(string conn)
        {
            cn = conn;
        }

        public KASECOSContext(DbContextOptions<KASECOSContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<CustomersContentPermission> CustomersContentPermissions { get; set; } = null!;
        public virtual DbSet<CustomersInfo> CustomersInfos { get; set; } = null!;
        public virtual DbSet<CustomersProfile> CustomersProfiles { get; set; } = null!;
        public virtual DbSet<Device> Devices { get; set; } = null!;
        public virtual DbSet<Dpconfig> Dpconfigs { get; set; } = null!;
        public virtual DbSet<DpconfigCustomer> DpconfigCustomers { get; set; } = null!;
        public virtual DbSet<EcosUser> EcosUsers { get; set; } = null!;
        public virtual DbSet<Ping> Pings { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<ProductsFunction> ProductsFunctions { get; set; } = null!;
        public virtual DbSet<ProductsFunctionsPermission> ProductsFunctionsPermissions { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<RolesUser> RolesUsers { get; set; } = null!;
        public virtual DbSet<SmacSession> SmacSessions { get; set; } = null!;
        public virtual DbSet<TokensLog> TokensLogs { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql(cn);// "Host=127.0.0.1;Database=KAS.ECOS;Username=kasEcos_user01;Password=123");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasComment("Quản lý danh sách khách hàng");

                entity.HasIndex(e => e.Id, "uCustomers_CustomerID")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .HasColumnName("ID");

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .HasComment("Địa chỉ");

                entity.Property(e => e.CreatedDate)
                    .HasPrecision(6)
                    .HasDefaultValueSql("now()")
                    .HasComment("Ngày tạo");

                entity.Property(e => e.CustomerCodeSmac)
                    .HasMaxLength(50)
                    .HasColumnName("CustomerCode_Smac")
                    .HasComment("mã khách hàng trên SMAC");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''::character varying")
                    .HasComment("Ghi chú");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("''::character varying");

                entity.Property(e => e.HandPhone)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("''::character varying")
                    .HasComment("Số điện thoại khách hàng");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("isDeleted")
                    .HasComment("Mặc định là false");

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.ParentId)
                    .HasMaxLength(50)
                    .HasColumnName("ParentID");
            });

            modelBuilder.Entity<CustomersContentPermission>(entity =>
            {
                entity.HasKey(e => new { e.CustomerIdSource, e.RoleName })
                    .HasName("Customers.Content.Permissions_pkey");

                entity.ToTable("Customers.Content.Permissions");

                entity.HasComment("Phân quyền nội dung");

                entity.Property(e => e.CustomerIdSource)
                    .HasMaxLength(50)
                    .HasColumnName("CustomerID.Source")
                    .HasComment("Mã khách hàng / công ty/ Phòng ban xem dữ liệu của CustomerID.Destination");

                entity.Property(e => e.RoleName).HasMaxLength(50);

                entity.Property(e => e.CustomerIdDestination)
                    .HasColumnType("character varying[]")
                    .HasColumnName("CustomerID.Destination")
                    .HasComment("Mã phòng ban được xem dữ liệu bởi CustomerID.Source");

                entity.Property(e => e.IsAllow).HasColumnName("isAllow");

                entity.HasOne(d => d.Role)
                    .WithOne(p => p.CustomersContentPermission)
                    .HasForeignKey<CustomersContentPermission>(d => new { d.CustomerIdSource, d.RoleName })
                    .HasConstraintName("ROLES_Customers.Content.Permission_2K");
            });

            modelBuilder.Entity<CustomersInfo>(entity =>
            {
                entity.ToTable("Customers.Info");

                entity.HasComment("Table này lưu trữ các thông tin mở rộng của Customers");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .HasColumnName("ID");

                entity.Property(e => e.Anything)
                    .HasMaxLength(50)
                    .HasComment("Điện thoại cầm tay");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.CustomersInfo)
                    .HasForeignKey<CustomersInfo>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Customers_CustomersInfo_ID");
            });

            modelBuilder.Entity<CustomersProfile>(entity =>
            {
                entity.HasKey(e => new { e.CustomerId, e.ProductId })
                    .HasName("Customers.Profiles_pkey");

                entity.ToTable("Customers.Profiles");

                entity.HasComment("Danh sách thông tin cấu hình của Customers.");

                entity.Property(e => e.CustomerId)
                    .HasMaxLength(50)
                    .HasColumnName("CustomerID");

                entity.Property(e => e.ProductId)
                    .HasMaxLength(50)
                    .HasColumnName("ProductID")
                    .HasComment("Tên sản phẩm của");

                entity.Property(e => e.IsDelete)
                    .HasColumnName("isDelete")
                    .HasComment("Mặc định là False");

                entity.Property(e => e.ProfileApi)
                    .HasColumnType("json")
                    .HasColumnName("ProfileAPI")
                    .HasComment("Lưu thông tin Server của API, phục vụ cho FrontEnd");

                entity.Property(e => e.ProfileDb)
                    .HasColumnType("json")
                    .HasColumnName("ProfileDB")
                    .HasComment("Lưu trữ thông tin DB (M1,M2,P1,P2)");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CustomersProfiles)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("Customers_Customers.Profiles_CustomerID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.CustomersProfiles)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("Products_Customer.Profiles_ID");
            });

            modelBuilder.Entity<Device>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("nextval('\"Devies_ID_seq\"'::regclass)")
                    .HasComment("Mã tăng tự động");

                entity.Property(e => e.AccessDate)
                    .HasPrecision(6)
                    .HasDefaultValueSql("now()")
                    .HasComment("Thời gian truy cập lần cuối");

                entity.Property(e => e.CreateDate)
                    .HasPrecision(6)
                    .HasDefaultValueSql("now()")
                    .HasComment("Ngày kích hoạt.Hệ thống tự tạo, không cần gán giá trị. ");

                entity.Property(e => e.CustomerId)
                    .HasMaxLength(50)
                    .HasColumnName("CustomerID")
                    .HasComment("Mã khách hàng");

                entity.Property(e => e.CustomerIdroot)
                    .HasMaxLength(50)
                    .HasColumnName("CustomerIDRoot")
                    .HasDefaultValueSql("''::character varying")
                    .HasComment("Mã khách hàng có ParantID là null");

                entity.Property(e => e.DeviceId)
                    .HasMaxLength(50)
                    .HasColumnName("DeviceID")
                    .HasComment("Mã duy nhất của thiết bị. Window sẽ dùng thuật toán riêng, các hệ điều hành khác sẽ dùng function lấy mã thiết bị duy nhất");

                entity.Property(e => e.DeviceName)
                    .HasMaxLength(200)
                    .HasDefaultValueSql("''::character varying")
                    .HasComment("Tên thiết bị");

                entity.Property(e => e.Ip)
                    .HasMaxLength(50)
                    .HasColumnName("IP")
                    .HasDefaultValueSql("''::character varying")
                    .HasComment("IP truy cập lần cuối");

                entity.Property(e => e.Location)
                    .HasColumnType("json")
                    .HasComment("Lưu trữ thông tin địa điểm truy cập");

                entity.Property(e => e.Osname)
                    .HasMaxLength(50)
                    .HasColumnName("OSName")
                    .HasComment("Tên hệ điều hành");

                entity.Property(e => e.Osver)
                    .HasMaxLength(50)
                    .HasColumnName("OSVer")
                    .HasComment("Phiên bản hệ điều hành");

                entity.Property(e => e.ProductsId)
                    .HasMaxLength(50)
                    .HasColumnName("ProductsID")
                    .HasComment("Mã sản phẩm của KAS");
            });

            modelBuilder.Entity<Dpconfig>(entity =>
            {
                entity.ToTable("DPConfig");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .HasColumnName("ID")
                    .HasComment("Client ID của Dataproccessor");

                entity.Property(e => e.AppRun)
                    .HasDefaultValueSql("2")
                    .HasComment("0: Chỉ chạy DB, 1: Chỉ chạy APP, 2: Vừa chạy APP và DB");

                entity.Property(e => e.BackupConfig)
                    .HasColumnType("json")
                    .HasComment("Cấu hình backup");

                entity.Property(e => e.CreateSmacAppRun).HasComment("Chạy chức năng tạo SMAC tự động");

                entity.Property(e => e.CreateSmacServerCode)
                    .HasMaxLength(50)
                    .HasComment("Server code để lấy thông tin tạo SMAC tự động");

                entity.Property(e => e.IsDeleted).HasComment("Trạng thái dữ liệu");

                entity.Property(e => e.MaxSecondSendLog)
                    .HasDefaultValueSql("300")
                    .HasComment("Số giây gửi log ");

                entity.Property(e => e.MaxThread)
                    .HasDefaultValueSql("1")
                    .HasComment("Số thread chạy tính dữ liệu chung");

                entity.Property(e => e.MaxThreadBill)
                    .HasDefaultValueSql("1")
                    .HasComment("Số thread chạy tính bill");

                entity.Property(e => e.MaxThreadImport)
                    .HasDefaultValueSql("1")
                    .HasComment("Số thread chạy tính dữ liệu import");

                entity.Property(e => e.MongoHost)
                    .HasMaxLength(50)
                    .HasComment("ConnectionString của mongodb");

                entity.Property(e => e.MonitorHost)
                    .HasMaxLength(50)
                    .HasComment("Host của Monitor");

                entity.Property(e => e.PostgreHost)
                    .HasMaxLength(100)
                    .HasComment("ConnectionString của postgre");

                entity.Property(e => e.SyncApi)
                    .HasMaxLength(100)
                    .HasColumnName("SyncAPI")
                    .HasComment("Domain của KAS.SYNC");
            });

            modelBuilder.Entity<DpconfigCustomer>(entity =>
            {
                entity.HasKey(e => e.CustomerId)
                    .HasName("DPConfig.Customers_pkey");

                entity.ToTable("DPConfig.Customers");

                entity.Property(e => e.CustomerId)
                    .HasMaxLength(50)
                    .HasColumnName("CustomerID")
                    .HasComment("Mã khách hàng (customerCode)");

                entity.Property(e => e.ClientId)
                    .HasMaxLength(50)
                    .HasColumnName("ClientID")
                    .HasComment("Mã DataProccessor");

                entity.Property(e => e.ExcuteCostPriceDb)
                    .HasColumnName("ExcuteCostPriceDB")
                    .HasComment("Thời gian tính giá vốn tự động 0-23");

                entity.Property(e => e.SapmaiSonConfig)
                    .HasColumnType("json")
                    .HasColumnName("SAPMaiSonConfig")
                    .HasComment("Cau hinh SAP cho MaiSon");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.DpconfigCustomers)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("DPConfig_Customers_ClientID");
            });

            modelBuilder.Entity<EcosUser>(entity =>
            {
                entity.HasKey(e => e.PhoneNumber)
                    .HasName("ECOS.User_pkey");

                entity.ToTable("ECOS.User");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(20)
                    .HasComment("Số điện thoại được quyền login vào ECOS");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasComment("Thông tin bổ sung cần lưu ý");

                entity.Property(e => e.FullName)
                    .HasMaxLength(255)
                    .HasComment("Tên người truy cập vào ECOS");

                entity.Property(e => e.Ip)
                    .HasMaxLength(50)
                    .HasColumnName("IP")
                    .HasComment("IP truy cập");

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.Property(e => e.LastAccesDate)
                    .HasPrecision(6)
                    .HasDefaultValueSql("now()")
                    .HasComment("Lần cuối truy cập");
            });

            modelBuilder.Entity<Ping>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Ping");

                entity.Property(e => e.CustomerId)
                    .HasMaxLength(50)
                    .HasColumnName("CustomerID")
                    .HasComment("Mã khách hàng");

                entity.Property(e => e.KasProductName)
                    .HasMaxLength(50)
                    .HasComment("Tên sản phẩm của KAS");

                entity.Property(e => e.MachineInfo)
                    .HasMaxLength(255)
                    .HasComment("Thông tin thiết bị");

                entity.Property(e => e.PingTime)
                    .HasPrecision(6)
                    .HasDefaultValueSql("now()")
                    .HasComment("Lần cuối đăng nhập");

                entity.Property(e => e.SiteId)
                    .HasMaxLength(50)
                    .HasColumnName("SiteID")
                    .HasComment("Mã chi nhánh");

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .HasComment("User đăng nhập");

                entity.Property(e => e.UserInfo)
                    .HasMaxLength(255)
                    .HasComment("Thông tin user");

                entity.Property(e => e.Version)
                    .HasMaxLength(50)
                    .HasComment("Phiên bản APP");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasComment("Các sản phẩm của KAS");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .HasColumnName("ID");

                entity.Property(e => e.Description)
                    .HasMaxLength(2000)
                    .HasComment("Mô tả sản phẩm của KAS. Tối đa 2000 ký tự");
            });

            modelBuilder.Entity<ProductsFunction>(entity =>
            {
                entity.HasKey(e => new { e.ProductId, e.FunctionName })
                    .HasName("KAS.Products.Functions_pkey");

                entity.ToTable("Products.Functions");

                entity.HasComment("Danh sách tính năng của sản phẩm KAS");

                entity.Property(e => e.ProductId)
                    .HasMaxLength(50)
                    .HasColumnName("Product.ID");

                entity.Property(e => e.FunctionName).HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(2000);

                entity.Property(e => e.FunctionParent).HasMaxLength(50);

                entity.Property(e => e.FunctionPath).HasMaxLength(2000);

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.Property(e => e.Level).HasComment("Level của đệ quy Function");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductsFunctions)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("Products_KAS.Products.Function_ID3");
            });

            modelBuilder.Entity<ProductsFunctionsPermission>(entity =>
            {
                entity.HasKey(e => new { e.ProductId, e.FunctionName, e.CustomerId, e.RoleName })
                    .HasName("KAS.Products.Functions.Permissions_pkey");

                entity.ToTable("Products.Functions.Permissions");

                entity.HasComment("Phân quyền tính năng.\r\n\r\nTại thời điểm đăng nhập:\r\n	- Input: KAS.Product.ID , CustomerID,RoleName\r\n	- Nếu giá trị trả về danh sách  FunctionName ==0, thì trả về thông báo tài khoản không hợp lệ. Dùng cho trường hợp 1 tài khoản truy cập vào nhiều sản phẩm");

                entity.Property(e => e.ProductId)
                    .HasMaxLength(50)
                    .HasColumnName("Product.ID");

                entity.Property(e => e.FunctionName).HasMaxLength(50);

                entity.Property(e => e.CustomerId)
                    .HasMaxLength(50)
                    .HasColumnName("CustomerID");

                entity.Property(e => e.RoleName).HasMaxLength(50);

                entity.Property(e => e.Expired)
                    .HasPrecision(6)
                    .HasComment("Ngày hết hạn của Function, hạn sử dụng của Token sẽ bằng thời ngày hết hạn gần nhất của tất cả tính năng, và tối đa là 1 năm");

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.Property(e => e.MaxRecords).HasComment("Mặc định là 0, không giới hạn. Field này dùng để giới hạn số lượng record cho các tính năng dùng thử miễn phí");

                entity.Property(e => e.Permission).HasComment("Phân quyền tính năng theo Enum(hệ số Bit)");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.ProductsFunctionsPermissions)
                    .HasForeignKey(d => new { d.CustomerId, d.RoleName })
                    .HasConstraintName("ROLES.Products.Function.Permissions.FK2");

                entity.HasOne(d => d.ProductsFunction)
                    .WithMany(p => p.ProductsFunctionsPermissions)
                    .HasForeignKey(d => new { d.ProductId, d.FunctionName })
                    .HasConstraintName("Products.Functions.Permissions_F1");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => new { e.CustomerId, e.RoleName })
                    .HasName("ROLES_pkey");

                entity.ToTable("ROLES");

                entity.HasComment("Tạo ra các vai trò sử dụng tính năng (phân theo Customer), Nếu User được thêm vào Role này tương đương sẽ có các quyền tương tự ");

                entity.Property(e => e.CustomerId)
                    .HasMaxLength(50)
                    .HasColumnName("CustomerID");

                entity.Property(e => e.RoleName)
                    .HasMaxLength(50)
                    .HasComment("Tên Vai trò");

                entity.Property(e => e.CreateDate)
                    .HasPrecision(6)
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Description)
                    .HasMaxLength(2000)
                    .HasComment("Mô tả vai trò");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("isDeleted")
                    .HasDefaultValueSql("false");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Roles)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("Customers_ROLES_CustomerID");
            });

            modelBuilder.Entity<RolesUser>(entity =>
            {
                entity.HasKey(e => new { e.User, e.ProductId, e.CustomerId })
                    .HasName("ROLES.Users_pkey");

                entity.ToTable("ROLES.Users");

                entity.HasComment("Quản lý tài khoản và token");

                entity.HasIndex(e => e.Token, "TokenUnique")
                    .IsUnique();

                entity.HasIndex(e => new { e.User, e.ProductId }, "usernameUnique")
                    .IsUnique();

                entity.Property(e => e.User)
                    .HasMaxLength(50)
                    .HasComment("Tại 1 thời điểm, chỉ có 1 Token gắn với 1 User. \r\nUser và ProductID sẽ là duy nhất");

                entity.Property(e => e.ProductId)
                    .HasMaxLength(50)
                    .HasColumnName("ProductID")
                    .HasComment("Tên sản phẩm của KAS. Khi login truyền vào Header");

                entity.Property(e => e.CustomerId)
                    .HasMaxLength(50)
                    .HasColumnName("CustomerID");

                entity.Property(e => e.CreateDate)
                    .HasPrecision(6)
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.RoleName).HasMaxLength(50);

                entity.Property(e => e.Token)
                    .HasMaxLength(50)
                    .IsFixedLength()
                    .HasComment("Token là duy nhất");

                entity.Property(e => e.TokenExpired)
                    .HasPrecision(6)
                    .HasColumnName("Token.Expired");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.RolesUsers)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("Product_RUS01");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RolesUsers)
                    .HasForeignKey(d => new { d.CustomerId, d.RoleName })
                    .HasConstraintName("ROLES_ROLES.Users_K2");
            });

            modelBuilder.Entity<SmacSession>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("SmacSession");

                entity.Property(e => e.CustomerId)
                    .HasMaxLength(50)
                    .HasColumnName("CustomerID")
                    .HasComment("Mã Smac (CustomerCode)");

                entity.Property(e => e.Data).HasComment("Thông tin đăng nhập");

                entity.Property(e => e.LastUpdated)
                    .HasPrecision(6)
                    .HasDefaultValueSql("now()")
                    .HasComment("Lần cuối cập nhật");

                entity.Property(e => e.Session)
                    .HasMaxLength(50)
                    .HasComment("Session do smac cấp");
            });

            modelBuilder.Entity<TokensLog>(entity =>
            {
                entity.HasKey(e => e.Stt)
                    .HasName("Tokens.Log_pkey");

                entity.ToTable("Tokens.Log");

                entity.HasComment("Quản lý cấp phát Token");

                entity.Property(e => e.Stt).HasColumnName("STT");

                entity.Property(e => e.CustomerId)
                    .HasMaxLength(50)
                    .HasColumnName("CustomerID");

                entity.Property(e => e.DeviceId)
                    .HasMaxLength(50)
                    .HasColumnName("DeviceID");

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.Property(e => e.KasProductsId)
                    .HasMaxLength(50)
                    .HasColumnName("KAS.Products.ID");

                entity.Property(e => e.RoleName).HasMaxLength(50);

                entity.Property(e => e.Token).HasMaxLength(2000);

                entity.Property(e => e.TokenCreate)
                    .HasColumnType("time(6) with time zone")
                    .HasColumnName("Token.Create")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.TokenExpired)
                    .HasColumnType("time(6) with time zone")
                    .HasColumnName("Token.Expired");

                entity.Property(e => e.User).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
