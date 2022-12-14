using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KAS.ECOS.API.Migrations
{
    public partial class InitDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationLists",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    ApplicationName = table.Column<string>(type: "text", nullable: false),
                    ApplicationDescription = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationLists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EndUserLists",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EndUserLists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationLists",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    OrganizationName = table.Column<string>(type: "text", nullable: false),
                    OrganizationDescription = table.Column<string>(type: "text", nullable: true),
                    Address = table.Column<string>(type: "text", nullable: false),
                    HandPhone = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    ParentId = table.Column<string>(type: "text", nullable: true),
                    CustomerCodeSmac = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationLists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserDeviceLists",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    DeviceName = table.Column<string>(type: "text", nullable: false),
                    LatestIPAccess = table.Column<string>(type: "text", nullable: false),
                    LatestLocation = table.Column<string>(type: "text", nullable: false),
                    LatestAccessDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    OSName = table.Column<string>(type: "text", nullable: false),
                    OSVer = table.Column<string>(type: "text", nullable: false),
                    IsAcive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDeviceLists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationFunctionLists",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    ApplicationId = table.Column<string>(type: "text", nullable: false),
                    FunctionName = table.Column<string>(type: "text", nullable: false),
                    FunctionDescription = table.Column<string>(type: "text", nullable: false),
                    ParentId = table.Column<string>(type: "text", nullable: true),
                    Path = table.Column<string>(type: "text", nullable: false),
                    Level = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationFunctionLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationFunctionLists_ApplicationLists_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "ApplicationLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EndUserCredentialHistoryLists",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    EndUserId = table.Column<string>(type: "text", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EndUserCredentialHistoryLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EndUserCredentialHistoryLists_EndUserLists_EndUserId",
                        column: x => x.EndUserId,
                        principalTable: "EndUserLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EndUserTokenLists",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    EndUserId = table.Column<string>(type: "text", nullable: false),
                    RefreshToken = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EndUserTokenLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EndUserTokenLists_EndUserLists_EndUserId",
                        column: x => x.EndUserId,
                        principalTable: "EndUserLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationProfileLists",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    OrganizationProfileName = table.Column<string>(type: "text", nullable: false),
                    OrganizationId = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationProfileLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrganizationProfileLists_OrganizationLists_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "OrganizationLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationUserLists",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    OrganizationId = table.Column<string>(type: "text", nullable: false),
                    EndUserId = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    OrganizationListId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationUserLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrganizationUserLists_EndUserLists_EndUserId",
                        column: x => x.EndUserId,
                        principalTable: "EndUserLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrganizationUserLists_OrganizationLists_OrganizationListId",
                        column: x => x.OrganizationListId,
                        principalTable: "OrganizationLists",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RoleLists",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    OrganizationId = table.Column<string>(type: "text", nullable: false),
                    RoleName = table.Column<string>(type: "text", nullable: false),
                    RoleDescription = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleLists_OrganizationLists_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "OrganizationLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccessHistoryLists",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    EndUserId = table.Column<string>(type: "text", nullable: false),
                    UserDeviceId = table.Column<string>(type: "text", nullable: false),
                    IPAdress = table.Column<string>(type: "text", nullable: false),
                    Location = table.Column<string>(type: "text", nullable: false),
                    AccessDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessHistoryLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccessHistoryLists_EndUserLists_EndUserId",
                        column: x => x.EndUserId,
                        principalTable: "EndUserLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccessHistoryLists_UserDeviceLists_UserDeviceId",
                        column: x => x.UserDeviceId,
                        principalTable: "UserDeviceLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationDeviceLists",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    OrganizationId = table.Column<string>(type: "text", nullable: false),
                    UserDeviceId = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationDeviceLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrganizationDeviceLists_OrganizationLists_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "OrganizationLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrganizationDeviceLists_UserDeviceLists_UserDeviceId",
                        column: x => x.UserDeviceId,
                        principalTable: "UserDeviceLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationFunctionPermissionLists",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    ApplicationFunctionId = table.Column<string>(type: "text", nullable: false),
                    PermissionName = table.Column<string>(type: "text", nullable: false),
                    Permission = table.Column<short>(type: "smallint", nullable: false),
                    MaxRecords = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationFunctionPermissionLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationFunctionPermissionLists_ApplicationFunctionLists~",
                        column: x => x.ApplicationFunctionId,
                        principalTable: "ApplicationFunctionLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationDatabaseLists",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    OrganizationProfileId = table.Column<string>(type: "text", nullable: false),
                    DatabaseName = table.Column<string>(type: "text", nullable: false),
                    ConnectionString = table.Column<string>(type: "text", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationDatabaseLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrganizationDatabaseLists_OrganizationProfileLists_Organiza~",
                        column: x => x.OrganizationProfileId,
                        principalTable: "OrganizationProfileLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EndUserRoleLists",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserDeviceId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    OrganizationUserId = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EndUserRoleLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EndUserRoleLists_OrganizationUserLists_OrganizationUserId",
                        column: x => x.OrganizationUserId,
                        principalTable: "OrganizationUserLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EndUserRoleLists_RoleLists_RoleId",
                        column: x => x.RoleId,
                        principalTable: "RoleLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EndUserRoleLists_UserDeviceLists_UserDeviceId",
                        column: x => x.UserDeviceId,
                        principalTable: "UserDeviceLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleApplicationFunctionPermissionLists",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    ApplicationFunctionPermissionId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleApplicationFunctionPermissionLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleApplicationFunctionPermissionLists_ApplicationFunctionP~",
                        column: x => x.ApplicationFunctionPermissionId,
                        principalTable: "ApplicationFunctionPermissionLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleApplicationFunctionPermissionLists_RoleLists_RoleId",
                        column: x => x.RoleId,
                        principalTable: "RoleLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ApplicationLists",
                columns: new[] { "Id", "ApplicationDescription", "ApplicationName" },
                values: new object[] { "1", "desc", "ECOS" });

            migrationBuilder.InsertData(
                table: "EndUserLists",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "Email", "FirstName", "IsActive", "LastName", "Password", "PhoneNumber", "Username" },
                values: new object[] { "1", new DateTime(2022, 12, 13, 15, 48, 11, 225, DateTimeKind.Local).AddTicks(240), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "khoanguyen@gmail.com", "Khoa", true, "Nguyen", "123123", "0763602013", "khoanguyen" });

            migrationBuilder.InsertData(
                table: "OrganizationLists",
                columns: new[] { "Id", "Address", "CreatedDate", "CustomerCodeSmac", "DeletedDate", "Email", "HandPhone", "IsActive", "OrganizationDescription", "OrganizationName", "ParentId" },
                values: new object[] { "1", "116 Mai Thuc Lan", new DateTime(2022, 12, 13, 15, 48, 11, 225, DateTimeKind.Local).AddTicks(364), "123123", null, "kas@gmail.com", "0123456677", true, null, "Kas", null });

            migrationBuilder.InsertData(
                table: "UserDeviceLists",
                columns: new[] { "Id", "CreatedDate", "DeviceName", "IsAcive", "LatestAccessDate", "LatestIPAccess", "LatestLocation", "OSName", "OSVer" },
                values: new object[] { "1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "POS", true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "12.0.0.1", "Danang", "Windows", "10" });

            migrationBuilder.InsertData(
                table: "ApplicationFunctionLists",
                columns: new[] { "Id", "ApplicationId", "FunctionDescription", "FunctionName", "Level", "ParentId", "Path" },
                values: new object[] { "1", "1", "desc", "CREATE", (short)0, null, "Path" });

            migrationBuilder.InsertData(
                table: "OrganizationUserLists",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "EndUserId", "IsActive", "OrganizationId", "OrganizationListId" },
                values: new object[] { "1", new DateTime(2022, 12, 13, 15, 48, 11, 225, DateTimeKind.Local).AddTicks(350), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1", true, "1", null });

            migrationBuilder.InsertData(
                table: "RoleLists",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "IsActive", "OrganizationId", "RoleDescription", "RoleName" },
                values: new object[,]
                {
                    { "1", new DateTime(2022, 12, 13, 15, 48, 11, 225, DateTimeKind.Local).AddTicks(439), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "1", "Desc", "Admin" },
                    { "2", new DateTime(2022, 12, 13, 15, 48, 11, 225, DateTimeKind.Local).AddTicks(442), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "1", "Employee", "Employee" }
                });

            migrationBuilder.InsertData(
                table: "ApplicationFunctionPermissionLists",
                columns: new[] { "Id", "ApplicationFunctionId", "MaxRecords", "Permission", "PermissionName" },
                values: new object[,]
                {
                    { "1", "1", 10, (short)4, "CREATE" },
                    { "2", "1", 10, (short)4, "CREATE" }
                });

            migrationBuilder.InsertData(
                table: "EndUserRoleLists",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "OrganizationUserId", "RoleId", "UserDeviceId" },
                values: new object[] { "1", new DateTime(2022, 12, 13, 15, 48, 11, 225, DateTimeKind.Local).AddTicks(392), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1", "1", "1" });

            migrationBuilder.InsertData(
                table: "RoleApplicationFunctionPermissionLists",
                columns: new[] { "Id", "ApplicationFunctionPermissionId", "CreatedDate", "DeletedDate", "RoleId" },
                values: new object[,]
                {
                    { "1", "1", new DateTime(2022, 12, 13, 15, 48, 11, 225, DateTimeKind.Local).AddTicks(488), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1" },
                    { "2", "1", new DateTime(2022, 12, 13, 15, 48, 11, 225, DateTimeKind.Local).AddTicks(489), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1" },
                    { "3", "1", new DateTime(2022, 12, 13, 15, 48, 11, 225, DateTimeKind.Local).AddTicks(490), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1" },
                    { "4", "2", new DateTime(2022, 12, 13, 15, 48, 11, 225, DateTimeKind.Local).AddTicks(491), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "2" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccessHistoryLists_EndUserId",
                table: "AccessHistoryLists",
                column: "EndUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AccessHistoryLists_UserDeviceId",
                table: "AccessHistoryLists",
                column: "UserDeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationFunctionLists_ApplicationId",
                table: "ApplicationFunctionLists",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationFunctionPermissionLists_ApplicationFunctionId",
                table: "ApplicationFunctionPermissionLists",
                column: "ApplicationFunctionId");

            migrationBuilder.CreateIndex(
                name: "IX_EndUserCredentialHistoryLists_EndUserId",
                table: "EndUserCredentialHistoryLists",
                column: "EndUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EndUserRoleLists_OrganizationUserId",
                table: "EndUserRoleLists",
                column: "OrganizationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EndUserRoleLists_RoleId",
                table: "EndUserRoleLists",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_EndUserRoleLists_UserDeviceId",
                table: "EndUserRoleLists",
                column: "UserDeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_EndUserTokenLists_EndUserId",
                table: "EndUserTokenLists",
                column: "EndUserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationDatabaseLists_OrganizationProfileId",
                table: "OrganizationDatabaseLists",
                column: "OrganizationProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationDeviceLists_OrganizationId",
                table: "OrganizationDeviceLists",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationDeviceLists_UserDeviceId",
                table: "OrganizationDeviceLists",
                column: "UserDeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationProfileLists_OrganizationId",
                table: "OrganizationProfileLists",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationUserLists_EndUserId",
                table: "OrganizationUserLists",
                column: "EndUserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationUserLists_OrganizationListId",
                table: "OrganizationUserLists",
                column: "OrganizationListId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleApplicationFunctionPermissionLists_ApplicationFunctionP~",
                table: "RoleApplicationFunctionPermissionLists",
                column: "ApplicationFunctionPermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleApplicationFunctionPermissionLists_RoleId",
                table: "RoleApplicationFunctionPermissionLists",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleLists_OrganizationId",
                table: "RoleLists",
                column: "OrganizationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccessHistoryLists");

            migrationBuilder.DropTable(
                name: "EndUserCredentialHistoryLists");

            migrationBuilder.DropTable(
                name: "EndUserRoleLists");

            migrationBuilder.DropTable(
                name: "EndUserTokenLists");

            migrationBuilder.DropTable(
                name: "OrganizationDatabaseLists");

            migrationBuilder.DropTable(
                name: "OrganizationDeviceLists");

            migrationBuilder.DropTable(
                name: "RoleApplicationFunctionPermissionLists");

            migrationBuilder.DropTable(
                name: "OrganizationUserLists");

            migrationBuilder.DropTable(
                name: "OrganizationProfileLists");

            migrationBuilder.DropTable(
                name: "UserDeviceLists");

            migrationBuilder.DropTable(
                name: "ApplicationFunctionPermissionLists");

            migrationBuilder.DropTable(
                name: "RoleLists");

            migrationBuilder.DropTable(
                name: "EndUserLists");

            migrationBuilder.DropTable(
                name: "ApplicationFunctionLists");

            migrationBuilder.DropTable(
                name: "OrganizationLists");

            migrationBuilder.DropTable(
                name: "ApplicationLists");
        }
    }
}
