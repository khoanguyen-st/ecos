using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace KAS.Entity.DB.ECOS
{
    public partial class KASECOSContext : DbContext
    {
        public KASECOSContext()
        {
        }

        public KASECOSContext(DbContextOptions<KASECOSContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<CustomersContentPermission> CustomersContentPermissions { get; set; } = null!;
        public virtual DbSet<CustomersInfo> CustomersInfos { get; set; } = null!;
        public virtual DbSet<CustomersProfile> CustomersProfiles { get; set; } = null!;
        public virtual DbSet<Devy> Devies { get; set; } = null!;
        public virtual DbSet<KasProduct> KasProducts { get; set; } = null!;
        public virtual DbSet<KasProductsFunction> KasProductsFunctions { get; set; } = null!;
        public virtual DbSet<KasProductsFunctionsPermission> KasProductsFunctionsPermissions { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<RolesUser> RolesUsers { get; set; } = null!;
        public virtual DbSet<TokensLog> TokensLogs { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=127.0.0.1;Database=KAS.ECOS;Username=kasEcos_user01;Password=123");
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

                entity.Property(e => e.CreatedDate)
                    .HasDefaultValueSql("now()")
                    .HasComment("Ngày tạo");

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

                entity.Property(e => e.HandPhone)
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
                entity.HasKey(e => new { e.CustomerId, e.KasProduct })
                    .HasName("Customers.Profiles_pkey");

                entity.ToTable("Customers.Profiles");

                entity.HasComment("Danh sách thông tin cấu hình của Customers.");

                entity.Property(e => e.CustomerId)
                    .HasMaxLength(50)
                    .HasColumnName("CustomerID");

                entity.Property(e => e.KasProduct)
                    .HasMaxLength(50)
                    .HasColumnName("KAS.Product")
                    .HasComment("Tên sản phẩm của KAS");

                entity.Property(e => e.IsDelete)
                    .HasColumnName("isDelete")
                    .HasComment("Mặc định là False");

                entity.Property(e => e.KasProductApi)
                    .HasMaxLength(50)
                    .HasColumnName("KAS.Product.API")
                    .HasComment("IP App IP, phục vụ cho FrontEnd. Sau này đây chính là GatewayAPI");

                entity.Property(e => e.M1connectionString)
                    .HasMaxLength(255)
                    .HasColumnName("M1ConnectionString")
                    .HasComment("Yêu cầu mã hóa");

                entity.Property(e => e.M2connectionString)
                    .HasMaxLength(255)
                    .HasColumnName("M2ConnectionString")
                    .HasComment("Yêu cầu mã hóa");

                entity.Property(e => e.P1connectionString)
                    .HasMaxLength(255)
                    .HasColumnName("P1ConnectionString")
                    .HasComment("Yêu cầu mã hóa");

                entity.Property(e => e.P2connectionString)
                    .HasMaxLength(255)
                    .HasColumnName("P2ConnectionString")
                    .HasComment("Yêu cầu mã hóa");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CustomersProfiles)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("Customers_Customers.Profiles_CustomerID");

                entity.HasOne(d => d.KasProductNavigation)
                    .WithMany(p => p.CustomersProfiles)
                    .HasForeignKey(d => d.KasProduct)
                    .HasConstraintName("KAS.Products_Customer.Profiles_ID");
            });

            modelBuilder.Entity<Devy>(entity =>
            {
                entity.HasKey(e => new { e.DeviceId, e.CustomerId, e.KasProductsId })
                    .HasName("Devies_pkey");

                entity.Property(e => e.DeviceId)
                    .HasMaxLength(50)
                    .HasColumnName("DeviceID")
                    .HasComment("Mã thiết bị");

                entity.Property(e => e.CustomerId)
                    .HasMaxLength(50)
                    .HasColumnName("CustomerID")
                    .HasComment("Mã khách hàng");

                entity.Property(e => e.KasProductsId)
                    .HasMaxLength(50)
                    .HasColumnName("KAS.Products.ID")
                    .HasComment("Mã sản phẩm của KAS");

                entity.Property(e => e.AccessDate).HasComment("Thời gian truy cập lần cuối");

                entity.Property(e => e.CreateDate)
                    .HasDefaultValueSql("now()")
                    .HasComment("Ngày kích hoạt.Hệ thống tự tạo, không cần gán giá trị. ");

                entity.Property(e => e.DeviceInfo)
                    .HasColumnType("json")
                    .HasComment("Thông tin chi tiết của thiết bị. Lưu mã json object");

                entity.Property(e => e.DeviceName)
                    .HasMaxLength(200)
                    .HasDefaultValueSql("''::character varying")
                    .HasComment("Tên thiết bị");

                entity.Property(e => e.Ip)
                    .HasMaxLength(50)
                    .HasColumnName("IP")
                    .HasDefaultValueSql("''::character varying")
                    .HasComment("IP truy cập lần cuối");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Devies)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("Customers_Devices_P1");

                entity.HasOne(d => d.KasProducts)
                    .WithMany(p => p.Devies)
                    .HasForeignKey(d => d.KasProductsId)
                    .HasConstraintName("KAS.Products_Devices_P1");
            });

            modelBuilder.Entity<KasProduct>(entity =>
            {
                entity.ToTable("KAS.Products");

                entity.HasComment("Các sản phẩm của KAS");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .HasColumnName("ID");

                entity.Property(e => e.Description)
                    .HasMaxLength(2000)
                    .HasComment("Mô tả sản phẩm của KAS. Tối đa 2000 ký tự");
            });

            modelBuilder.Entity<KasProductsFunction>(entity =>
            {
                entity.HasKey(e => new { e.KasProductId, e.FunctionName })
                    .HasName("KAS.Products.Functions_pkey");

                entity.ToTable("KAS.Products.Functions");

                entity.HasComment("Danh sách tính năng của sản phẩm KAS");

                entity.Property(e => e.KasProductId)
                    .HasMaxLength(50)
                    .HasColumnName("KAS.Product.ID");

                entity.Property(e => e.FunctionName).HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(2000);

                entity.Property(e => e.FunctionParent).HasMaxLength(50);

                entity.Property(e => e.FunctionPath).HasMaxLength(2000);

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.HasOne(d => d.KasProduct)
                    .WithMany(p => p.KasProductsFunctions)
                    .HasForeignKey(d => d.KasProductId)
                    .HasConstraintName("KAS.Products_KAS.Products.Function_ID3");
            });

            modelBuilder.Entity<KasProductsFunctionsPermission>(entity =>
            {
                entity.HasKey(e => new { e.KasProductId, e.FunctionName, e.CustomerId, e.RoleName })
                    .HasName("KAS.Products.Functions.Permissions_pkey");

                entity.ToTable("KAS.Products.Functions.Permissions");

                entity.HasComment("Phân quyền tính năng.\r\n\r\nTại thời điểm đăng nhập:\r\n	- Input: KAS.Product.ID , CustomerID,RoleName\r\n	- Nếu giá trị trả về danh sách  FunctionName ==0, thì trả về thông báo tài khoản không hợp lệ. Dùng cho trường hợp 1 tài khoản truy cập vào nhiều sản phẩm");

                entity.Property(e => e.KasProductId)
                    .HasMaxLength(50)
                    .HasColumnName("KAS.Product.ID");

                entity.Property(e => e.FunctionName).HasMaxLength(50);

                entity.Property(e => e.CustomerId)
                    .HasMaxLength(50)
                    .HasColumnName("CustomerID");

                entity.Property(e => e.RoleName).HasMaxLength(50);

                entity.Property(e => e.Expired).HasComment("Ngày hết hạn của Function, hạn sử dụng của Token sẽ bằng thời ngày hết hạn gần nhất của tất cả tính năng, và tối đa là 1 năm");

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.Property(e => e.MaxRecords).HasComment("Mặc định là 0, không giới hạn. Field này dùng để giới hạn số lượng record cho các tính năng dùng thử miễn phí");

                entity.Property(e => e.Permission).HasComment("Phân quyền tính năng theo Enum(hệ số Bit)");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.KasProductsFunctionsPermissions)
                    .HasForeignKey(d => new { d.CustomerId, d.RoleName })
                    .HasConstraintName("ROLES.KAS.Products.Function.Permissions.FK2");

                entity.HasOne(d => d.KasProductsFunction)
                    .WithMany(p => p.KasProductsFunctionsPermissions)
                    .HasForeignKey(d => new { d.KasProductId, d.FunctionName })
                    .HasConstraintName("KAS.Products.Functions.Permissions_F1");
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

                entity.Property(e => e.CreateDate).HasDefaultValueSql("now()");

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
                entity.HasKey(e => new { e.CustomerId, e.RoleName, e.User })
                    .HasName("ROLES.Users_pkey");

                entity.ToTable("ROLES.Users");

                entity.HasComment("Quản lý tài khoản và token");

                entity.HasIndex(e => e.Token, "TokenUnique")
                    .IsUnique();

                entity.HasIndex(e => e.User, "usernameUnique")
                    .IsUnique();

                entity.Property(e => e.CustomerId)
                    .HasMaxLength(50)
                    .HasColumnName("CustomerID");

                entity.Property(e => e.RoleName).HasMaxLength(50);

                entity.Property(e => e.User)
                    .HasMaxLength(50)
                    .HasComment("Tại 1 thời điểm, chỉ có 1 Token gắn với 1 User. \nNếu có nhiều User cùng tên (bắt buộc khác công ty), thì chỉ duy nhất 1 User được Active");

                entity.Property(e => e.CreateDate).HasDefaultValueSql("now()");

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.Token)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.TokenExpired).HasColumnName("Token.Expired");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RolesUsers)
                    .HasForeignKey(d => new { d.CustomerId, d.RoleName })
                    .HasConstraintName("ROLES_ROLES.Users_K2");
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
                    .HasColumnType("time with time zone")
                    .HasColumnName("Token.Create")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.TokenExpired)
                    .HasColumnType("time with time zone")
                    .HasColumnName("Token.Expired");

                entity.Property(e => e.User).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
