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
    }
}
