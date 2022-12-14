using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAS.Entity.DB.ECOS.Entities
{
    public class ECOSContext : DbContext
    {
        string cn;
        public ECOSContext(string conn)
        {
            cn = conn;
        }
        public ECOSContext(DbContextOptions<ECOSContext> options) : base(options)
        {
        }

        public virtual DbSet<OrganizationList> OrganizationLists { get; set; } = null!;
        public virtual DbSet<OrganizationProfileList> OrganizationProfileLists { get; set; } = null!;
        public virtual DbSet<OrganizationDatabaseList> OrganizationDatabaseLists { get; set; } = null!;
        public virtual DbSet<RoleList> RoleLists { get; set; } = null!;
        public virtual DbSet<ApplicationList> ApplicationLists { get; set; } = null!;
        public virtual DbSet<ApplicationFunctionList> ApplicationFunctionLists { get; set; } = null!;
        public virtual DbSet<ApplicationFunctionPermissionList> ApplicationFunctionPermissionLists { get; set; } = null!;
        public virtual DbSet<RoleApplicationFunctionPermissionList> RoleApplicationFunctionPermissionLists { get; set; } = null!;
        public virtual DbSet<EndUserList> EndUserLists { get; set; } = null!;
        public virtual DbSet<OrganizationUserList> OrganizationUserLists { get; set; } = null!;
        public virtual DbSet<EndUserTokenList> EndUserTokenLists { get; set; } = null!;
        public virtual DbSet<EndUserRoleList> EndUserRoleLists { get; set; } = null!;
        public virtual DbSet<EndUserCredentialHistoryList> EndUserCredentialHistoryLists { get; set; } = null!;
        public virtual DbSet<AccessHistoryList> AccessHistoryLists { get; set; } = null!;
        public virtual DbSet<UserDeviceList> UserDeviceLists { get; set; } = null!;
        public virtual DbSet<OrganizationDeviceList> OrganizationDeviceLists { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(cn);// "Host=127.0.0.1;Database=KAS.ECOS;Username=kasEcos_user01;Password=123");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EndUserList>()
                .HasData(
                new EndUserList()
                {
                    Id = "1",
                    FirstName = "Khoa",
                    LastName = "Nguyen",
                    Username = "khoanguyen",
                    Email = "khoanguyen@gmail.com",
                    PhoneNumber = "0763602013",
                    Password = "123123",
                    IsActive = true,
                });
            modelBuilder.Entity<OrganizationUserList>()
                .HasData(
                new OrganizationUserList()
                {
                    Id = "1",
                    OrganizationId = "1",
                    EndUserId = "1",
                    IsActive = true,
                });
            modelBuilder.Entity<OrganizationList>()
                .HasData(
                new OrganizationList()
                {
                    Id = "1",
                    OrganizationName = "Kas",
                    Address = "116 Mai Thuc Lan",
                    HandPhone = "0123456677",
                    Email = "kas@gmail.com",
                    CustomerCodeSmac = "123123",
                    IsActive = true
                });
            modelBuilder.Entity<UserDeviceList>()
                .HasData(
                new UserDeviceList()
                {
                    Id = "1",
                    DeviceName = "POS",
                    LatestIPAccess = "12.0.0.1",
                    LatestLocation = "Danang",
                    OSName = "Windows",
                    OSVer = "10",
                    IsAcive = true
                });
            modelBuilder.Entity<EndUserRoleList>()
                .HasData(
                new EndUserRoleList()
                {
                    Id = "1",
                    UserDeviceId = "1",
                    RoleId = "1",
                    OrganizationUserId = "1"
                });
            modelBuilder.Entity<RoleList>()
                .HasData(
                new RoleList()
                {
                    Id = "1",
                    OrganizationId = "1",
                    RoleName = "Admin",
                    RoleDescription = "Desc",
                    IsActive = true,
                },
                new RoleList()
                {
                    Id = "2",
                    OrganizationId = "1",
                    RoleName = "Employee",
                    RoleDescription = "Employee",
                    IsActive = true,
                });
            modelBuilder.Entity<ApplicationFunctionPermissionList>()
                .HasData(
                new ApplicationFunctionPermissionList()
                {
                    Id = "1",
                    ApplicationFunctionId = "1",
                    PermissionName = "CREATE",
                    Permission = 0b100,
                    MaxRecords = 10
                },
                new ApplicationFunctionPermissionList()
                {
                    Id = "2",
                    ApplicationFunctionId = "1",
                    PermissionName = "CREATE",
                    Permission = 0b100,
                    MaxRecords = 10
                });
            modelBuilder.Entity<ApplicationFunctionList>()
                .HasData(
                new ApplicationFunctionList()
                {
                    Id = "1",
                    ApplicationId = "1",
                    FunctionName = "CREATE",
                    FunctionDescription = "desc",
                    Path = "Path"
                });
            modelBuilder.Entity<ApplicationList>()
                .HasData(
                new ApplicationList()
                {
                    Id = "1",
                    ApplicationName = "ECOS",
                    ApplicationDescription = "desc"
                });
            modelBuilder.Entity<RoleApplicationFunctionPermissionList>()
                .HasData(
                new RoleApplicationFunctionPermissionList()
                {
                    Id = "1",
                    ApplicationFunctionPermissionId = "1",
                    RoleId = "1"
                },
                new RoleApplicationFunctionPermissionList()
                {
                    Id = "2",
                    ApplicationFunctionPermissionId = "1",
                    RoleId = "1"
                },
                new RoleApplicationFunctionPermissionList()
                {
                    Id = "3",
                    ApplicationFunctionPermissionId = "1",
                    RoleId = "1"
                },
                new RoleApplicationFunctionPermissionList()
                {
                    Id = "4",
                    ApplicationFunctionPermissionId = "2",
                    RoleId = "2"
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}
