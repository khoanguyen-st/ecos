using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KAS.ECOS.API.Migrations
{
    public partial class extraFieldsUserSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "Users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "OrganizationUserLists",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "EndUserTokenLists",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "EndUserCredentialHistoryLists",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "AccessHistoryLists",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationUserLists_UserId",
                table: "OrganizationUserLists",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_EndUserTokenLists_UserId",
                table: "EndUserTokenLists",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_EndUserCredentialHistoryLists_UserId",
                table: "EndUserCredentialHistoryLists",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AccessHistoryLists_UserId",
                table: "AccessHistoryLists",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccessHistoryLists_Users_UserId",
                table: "AccessHistoryLists",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EndUserCredentialHistoryLists_Users_UserId",
                table: "EndUserCredentialHistoryLists",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EndUserTokenLists_Users_UserId",
                table: "EndUserTokenLists",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationUserLists_Users_UserId",
                table: "OrganizationUserLists",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccessHistoryLists_Users_UserId",
                table: "AccessHistoryLists");

            migrationBuilder.DropForeignKey(
                name: "FK_EndUserCredentialHistoryLists_Users_UserId",
                table: "EndUserCredentialHistoryLists");

            migrationBuilder.DropForeignKey(
                name: "FK_EndUserTokenLists_Users_UserId",
                table: "EndUserTokenLists");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganizationUserLists_Users_UserId",
                table: "OrganizationUserLists");

            migrationBuilder.DropIndex(
                name: "IX_OrganizationUserLists_UserId",
                table: "OrganizationUserLists");

            migrationBuilder.DropIndex(
                name: "IX_EndUserTokenLists_UserId",
                table: "EndUserTokenLists");

            migrationBuilder.DropIndex(
                name: "IX_EndUserCredentialHistoryLists_UserId",
                table: "EndUserCredentialHistoryLists");

            migrationBuilder.DropIndex(
                name: "IX_AccessHistoryLists_UserId",
                table: "AccessHistoryLists");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "OrganizationUserLists");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "EndUserTokenLists");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "EndUserCredentialHistoryLists");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AccessHistoryLists");
        }
    }
}
