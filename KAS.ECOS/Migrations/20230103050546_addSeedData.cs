using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KAS.ECOS.API.Migrations
{
    public partial class addSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedDate", "DeletedDate", "Email", "EmailConfirmed", "FirstName", "IsActive", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "Type", "UserName" },
                values: new object[,]
                {
                    { "a18be9c0-aa65-4af8-bd17-00bd9344e552", 0, "7d39058d-8c5c-49b5-ae3a-c44481f0e3b3", new DateTime(2023, 1, 3, 5, 5, 45, 823, DateTimeKind.Utc).AddTicks(1776), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "3rd@application.system", false, "3rd", true, "Application", false, null, "3RD@APPLICATION.SYSTEM", "3RDAPPLICATION", "AQAAAAEAACcQAAAAEDzfPX2PIO49cahcBWyODKoW19wfvZ16XVsR7HqykWdOOxQUGq47CEORcfsPV6N8lA==", null, false, "", false, "app", "3rdApplication" },
                    { "a18be9c0-aa65-4af8-bd17-00bd9344e575", 0, "85089084-8e9e-4f9c-8fdc-a67f072ccd91", new DateTime(2023, 1, 3, 5, 5, 45, 816, DateTimeKind.Utc).AddTicks(9925), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "super@admin.system", false, "super", true, "admin", false, null, "SUPER@ADMIN.SYSTEM", "SUPERADMIN", "AQAAAAEAACcQAAAAEGnMWHmcajodUTMeijtQ+f4OF9zla8G4sNLfPGd/b1wcFdiUd8ntlzILsvxPp16c8w==", null, false, "", false, "super", "superAdmin" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e552");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575");
        }
    }
}
