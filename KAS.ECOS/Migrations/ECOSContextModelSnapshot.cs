﻿// <auto-generated />
using System;
using KAS.Entity.DB.ECOS.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace KAS.ECOS.API.Migrations
{
    [DbContext(typeof(ECOSContext))]
    partial class ECOSContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("KAS.Entity.DB.ECOS.Entities.AccessHistoryList", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("AccessDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("EndUserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("IPAdress")
                        .HasColumnType("text");

                    b.Property<string>("Location")
                        .HasColumnType("text");

                    b.Property<Guid>("UserDeviceId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("EndUserId");

                    b.HasIndex("UserDeviceId");

                    b.ToTable("AccessHistoryLists");
                });

            modelBuilder.Entity("KAS.Entity.DB.ECOS.Entities.ApplicationFunctionList", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ApplicationId")
                        .HasColumnType("uuid");

                    b.Property<string>("FunctionDescription")
                        .HasColumnType("text");

                    b.Property<string>("FunctionName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<short>("Level")
                        .HasColumnType("smallint");

                    b.Property<string>("ParentId")
                        .HasColumnType("text");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationId");

                    b.ToTable("ApplicationFunctionLists");
                });

            modelBuilder.Entity("KAS.Entity.DB.ECOS.Entities.ApplicationFunctionPermissionList", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ApplicationFunctionId")
                        .HasColumnType("uuid");

                    b.Property<int?>("MaxRecords")
                        .HasColumnType("integer");

                    b.Property<string>("Permission")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationFunctionId");

                    b.ToTable("ApplicationFunctionPermissionLists");
                });

            modelBuilder.Entity("KAS.Entity.DB.ECOS.Entities.ApplicationList", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ApplicationDescription")
                        .HasColumnType("text");

                    b.Property<string>("ApplicationName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ApplicationLists");
                });

            modelBuilder.Entity("KAS.Entity.DB.ECOS.Entities.EndUserCredentialHistoryList", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("EndUserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("EndUserId");

                    b.ToTable("EndUserCredentialHistoryLists");
                });

            modelBuilder.Entity("KAS.Entity.DB.ECOS.Entities.EndUserList", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DeletedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("Type")
                        .HasColumnType("text");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("KAS.Entity.DB.ECOS.Entities.EndUserRoleList", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("OrganizationUserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("UserDeviceId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("OrganizationUserId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserDeviceId");

                    b.ToTable("EndUserRoleLists");
                });

            modelBuilder.Entity("KAS.Entity.DB.ECOS.Entities.EndUserTokenList", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("EndUserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("EndUserId");

                    b.ToTable("EndUserTokenLists");
                });

            modelBuilder.Entity("KAS.Entity.DB.ECOS.Entities.OrganizationDatabaseList", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ConnectionString")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("DatabaseName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("OrganizationProfileId")
                        .HasColumnType("uuid");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("OrganizationProfileId");

                    b.ToTable("OrganizationDatabaseLists");
                });

            modelBuilder.Entity("KAS.Entity.DB.ECOS.Entities.OrganizationDeviceList", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<Guid>("OrganizationId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserDeviceId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("OrganizationId");

                    b.HasIndex("UserDeviceId");

                    b.ToTable("OrganizationDeviceLists");
                });

            modelBuilder.Entity("KAS.Entity.DB.ECOS.Entities.OrganizationList", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("ExpiryDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("HandPhone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("OrganizationCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("OrganizationDescription")
                        .HasColumnType("text");

                    b.Property<string>("OrganizationName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("ParentId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("RegistryDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("OrganizationLists");
                });

            modelBuilder.Entity("KAS.Entity.DB.ECOS.Entities.OrganizationProfileList", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("OrganizationId")
                        .HasColumnType("uuid");

                    b.Property<string>("OrganizationProfileName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("OrganizationId");

                    b.ToTable("OrganizationProfileLists");
                });

            modelBuilder.Entity("KAS.Entity.DB.ECOS.Entities.OrganizationUserList", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("EndUserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("ExpiryDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("boolean");

                    b.Property<Guid>("OrganizationId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("RegistryDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("EndUserId");

                    b.HasIndex("OrganizationId");

                    b.ToTable("OrganizationUserLists");
                });

            modelBuilder.Entity("KAS.Entity.DB.ECOS.Entities.RoleApplicationFunctionPermissionList", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ApplicationFunctionPermissionId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationFunctionPermissionId");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleApplicationFunctionPermissionLists");
                });

            modelBuilder.Entity("KAS.Entity.DB.ECOS.Entities.RoleList", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsBaseRole")
                        .HasColumnType("boolean");

                    b.Property<Guid>("OrganizationId")
                        .HasColumnType("uuid");

                    b.Property<string>("RoleDescription")
                        .HasColumnType("text");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("OrganizationId");

                    b.ToTable("RoleLists");
                });

            modelBuilder.Entity("KAS.Entity.DB.ECOS.Entities.UserDeviceList", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("DeviceName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsAcive")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("LatestAccessDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("LatestIPAccess")
                        .HasColumnType("text");

                    b.Property<string>("LatestLocation")
                        .HasColumnType("text");

                    b.Property<string>("OSName")
                        .HasColumnType("text");

                    b.Property<string>("OSVer")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("UserDeviceLists");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaim", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogin", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserToken", (string)null);
                });

            modelBuilder.Entity("KAS.Entity.DB.ECOS.Entities.AccessHistoryList", b =>
                {
                    b.HasOne("KAS.Entity.DB.ECOS.Entities.EndUserList", "EndUser")
                        .WithMany("AccessHistories")
                        .HasForeignKey("EndUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KAS.Entity.DB.ECOS.Entities.UserDeviceList", "UserDevice")
                        .WithMany("AccessHistories")
                        .HasForeignKey("UserDeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EndUser");

                    b.Navigation("UserDevice");
                });

            modelBuilder.Entity("KAS.Entity.DB.ECOS.Entities.ApplicationFunctionList", b =>
                {
                    b.HasOne("KAS.Entity.DB.ECOS.Entities.ApplicationList", "Application")
                        .WithMany("ApplicationFunctions")
                        .HasForeignKey("ApplicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Application");
                });

            modelBuilder.Entity("KAS.Entity.DB.ECOS.Entities.ApplicationFunctionPermissionList", b =>
                {
                    b.HasOne("KAS.Entity.DB.ECOS.Entities.ApplicationFunctionList", "ApplicationFunction")
                        .WithMany("ApplicationPermissions")
                        .HasForeignKey("ApplicationFunctionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApplicationFunction");
                });

            modelBuilder.Entity("KAS.Entity.DB.ECOS.Entities.EndUserCredentialHistoryList", b =>
                {
                    b.HasOne("KAS.Entity.DB.ECOS.Entities.EndUserList", "EndUser")
                        .WithMany("EndUserCredentialHistories")
                        .HasForeignKey("EndUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EndUser");
                });

            modelBuilder.Entity("KAS.Entity.DB.ECOS.Entities.EndUserRoleList", b =>
                {
                    b.HasOne("KAS.Entity.DB.ECOS.Entities.OrganizationUserList", "OrganizationUser")
                        .WithMany()
                        .HasForeignKey("OrganizationUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KAS.Entity.DB.ECOS.Entities.RoleList", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KAS.Entity.DB.ECOS.Entities.UserDeviceList", "UserDevice")
                        .WithMany("EndUserRoles")
                        .HasForeignKey("UserDeviceId");

                    b.Navigation("OrganizationUser");

                    b.Navigation("Role");

                    b.Navigation("UserDevice");
                });

            modelBuilder.Entity("KAS.Entity.DB.ECOS.Entities.EndUserTokenList", b =>
                {
                    b.HasOne("KAS.Entity.DB.ECOS.Entities.EndUserList", "EndUser")
                        .WithMany("EndUserTokens")
                        .HasForeignKey("EndUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EndUser");
                });

            modelBuilder.Entity("KAS.Entity.DB.ECOS.Entities.OrganizationDatabaseList", b =>
                {
                    b.HasOne("KAS.Entity.DB.ECOS.Entities.OrganizationProfileList", "OrganizationProfile")
                        .WithMany("OrganizationDatabaseLists")
                        .HasForeignKey("OrganizationProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OrganizationProfile");
                });

            modelBuilder.Entity("KAS.Entity.DB.ECOS.Entities.OrganizationDeviceList", b =>
                {
                    b.HasOne("KAS.Entity.DB.ECOS.Entities.OrganizationList", "Organization")
                        .WithMany("OrganizationDeviceLists")
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KAS.Entity.DB.ECOS.Entities.UserDeviceList", "UserDevice")
                        .WithMany("OrganizationDevices")
                        .HasForeignKey("UserDeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Organization");

                    b.Navigation("UserDevice");
                });

            modelBuilder.Entity("KAS.Entity.DB.ECOS.Entities.OrganizationProfileList", b =>
                {
                    b.HasOne("KAS.Entity.DB.ECOS.Entities.OrganizationList", "Organization")
                        .WithMany("OrganizationProfileLists")
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Organization");
                });

            modelBuilder.Entity("KAS.Entity.DB.ECOS.Entities.OrganizationUserList", b =>
                {
                    b.HasOne("KAS.Entity.DB.ECOS.Entities.EndUserList", "EndUser")
                        .WithMany("OrganizationUsers")
                        .HasForeignKey("EndUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KAS.Entity.DB.ECOS.Entities.OrganizationList", "Organization")
                        .WithMany("OrganizationUserLists")
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EndUser");

                    b.Navigation("Organization");
                });

            modelBuilder.Entity("KAS.Entity.DB.ECOS.Entities.RoleApplicationFunctionPermissionList", b =>
                {
                    b.HasOne("KAS.Entity.DB.ECOS.Entities.ApplicationFunctionPermissionList", "ApplicationFunctionPermission")
                        .WithMany()
                        .HasForeignKey("ApplicationFunctionPermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KAS.Entity.DB.ECOS.Entities.RoleList", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApplicationFunctionPermission");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("KAS.Entity.DB.ECOS.Entities.RoleList", b =>
                {
                    b.HasOne("KAS.Entity.DB.ECOS.Entities.OrganizationList", "Organization")
                        .WithMany("RoleLists")
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Organization");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("KAS.Entity.DB.ECOS.Entities.EndUserList", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("KAS.Entity.DB.ECOS.Entities.EndUserList", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("KAS.Entity.DB.ECOS.Entities.EndUserList", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("KAS.Entity.DB.ECOS.Entities.ApplicationFunctionList", b =>
                {
                    b.Navigation("ApplicationPermissions");
                });

            modelBuilder.Entity("KAS.Entity.DB.ECOS.Entities.ApplicationList", b =>
                {
                    b.Navigation("ApplicationFunctions");
                });

            modelBuilder.Entity("KAS.Entity.DB.ECOS.Entities.EndUserList", b =>
                {
                    b.Navigation("AccessHistories");

                    b.Navigation("EndUserCredentialHistories");

                    b.Navigation("EndUserTokens");

                    b.Navigation("OrganizationUsers");
                });

            modelBuilder.Entity("KAS.Entity.DB.ECOS.Entities.OrganizationList", b =>
                {
                    b.Navigation("OrganizationDeviceLists");

                    b.Navigation("OrganizationProfileLists");

                    b.Navigation("OrganizationUserLists");

                    b.Navigation("RoleLists");
                });

            modelBuilder.Entity("KAS.Entity.DB.ECOS.Entities.OrganizationProfileList", b =>
                {
                    b.Navigation("OrganizationDatabaseLists");
                });

            modelBuilder.Entity("KAS.Entity.DB.ECOS.Entities.UserDeviceList", b =>
                {
                    b.Navigation("AccessHistories");

                    b.Navigation("EndUserRoles");

                    b.Navigation("OrganizationDevices");
                });
#pragma warning restore 612, 618
        }
    }
}
