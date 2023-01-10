using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KAS.ECOS.API.Migrations
{
    public partial class addMomoConfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MomoConfigLists",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PartnerCode = table.Column<string>(type: "text", nullable: false),
                    AccessKey = table.Column<string>(type: "text", nullable: false),
                    SecretKey = table.Column<string>(type: "text", nullable: false),
                    EndUserId = table.Column<string>(type: "text", nullable: false),
                    EndUserListId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MomoConfigLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MomoConfigLists_Users_EndUserListId",
                        column: x => x.EndUserListId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e552",
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "PasswordHash" },
                values: new object[] { "7fced1ab-d035-47fa-aded-5f0368b76b16", new DateTime(2023, 1, 9, 3, 39, 21, 37, DateTimeKind.Utc).AddTicks(8728), "AQAAAAEAACcQAAAAENKF+65FPoYDtYKm9a+BaPMmdvzclLi/p73OVPIk0m3zWxXWQkBBmOXdBRxZ71obDQ==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "PasswordHash" },
                values: new object[] { "d7f4dba0-e5d1-4087-bcca-410919f75985", new DateTime(2023, 1, 9, 3, 39, 21, 31, DateTimeKind.Utc).AddTicks(7350), "AQAAAAEAACcQAAAAEOFxTlx05BiRQ6fe+lUZdvIV+ENfP8HC+lTq70OaSTLJkHgd0za/r9LitrTURd40Nw==" });

            migrationBuilder.CreateIndex(
                name: "IX_MomoConfigLists_EndUserListId",
                table: "MomoConfigLists",
                column: "EndUserListId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MomoConfigLists");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e552",
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "PasswordHash" },
                values: new object[] { "7d39058d-8c5c-49b5-ae3a-c44481f0e3b3", new DateTime(2023, 1, 3, 5, 5, 45, 823, DateTimeKind.Utc).AddTicks(1776), "AQAAAAEAACcQAAAAEDzfPX2PIO49cahcBWyODKoW19wfvZ16XVsR7HqykWdOOxQUGq47CEORcfsPV6N8lA==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "PasswordHash" },
                values: new object[] { "85089084-8e9e-4f9c-8fdc-a67f072ccd91", new DateTime(2023, 1, 3, 5, 5, 45, 816, DateTimeKind.Utc).AddTicks(9925), "AQAAAAEAACcQAAAAEGnMWHmcajodUTMeijtQ+f4OF9zla8G4sNLfPGd/b1wcFdiUd8ntlzILsvxPp16c8w==" });
        }
    }
}
