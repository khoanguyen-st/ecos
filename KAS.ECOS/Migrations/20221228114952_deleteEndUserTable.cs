using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KAS.ECOS.API.Migrations
{
    public partial class deleteEndUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccessHistoryLists_EndUserLists_EndUserId",
                table: "AccessHistoryLists");

            migrationBuilder.DropForeignKey(
                name: "FK_AccessHistoryLists_Users_UserId",
                table: "AccessHistoryLists");

            migrationBuilder.DropForeignKey(
                name: "FK_EndUserCredentialHistoryLists_EndUserLists_EndUserId",
                table: "EndUserCredentialHistoryLists");

            migrationBuilder.DropForeignKey(
                name: "FK_EndUserCredentialHistoryLists_Users_UserId",
                table: "EndUserCredentialHistoryLists");

            migrationBuilder.DropForeignKey(
                name: "FK_EndUserTokenLists_EndUserLists_EndUserId",
                table: "EndUserTokenLists");

            migrationBuilder.DropForeignKey(
                name: "FK_EndUserTokenLists_Users_UserId",
                table: "EndUserTokenLists");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganizationUserLists_EndUserLists_EndUserId",
                table: "OrganizationUserLists");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganizationUserLists_Users_UserId",
                table: "OrganizationUserLists");

            migrationBuilder.DropTable(
                name: "EndUserLists");

            migrationBuilder.DropIndex(
                name: "IX_OrganizationUserLists_EndUserId",
                table: "OrganizationUserLists");

            migrationBuilder.DropIndex(
                name: "IX_EndUserTokenLists_EndUserId",
                table: "EndUserTokenLists");

            migrationBuilder.DropIndex(
                name: "IX_EndUserCredentialHistoryLists_EndUserId",
                table: "EndUserCredentialHistoryLists");

            migrationBuilder.DropIndex(
                name: "IX_AccessHistoryLists_EndUserId",
                table: "AccessHistoryLists");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "OrganizationUserLists",
                newName: "EndUserId1");

            migrationBuilder.RenameIndex(
                name: "IX_OrganizationUserLists_UserId",
                table: "OrganizationUserLists",
                newName: "IX_OrganizationUserLists_EndUserId1");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "EndUserTokenLists",
                newName: "EndUserId1");

            migrationBuilder.RenameIndex(
                name: "IX_EndUserTokenLists_UserId",
                table: "EndUserTokenLists",
                newName: "IX_EndUserTokenLists_EndUserId1");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "EndUserCredentialHistoryLists",
                newName: "EndUserId1");

            migrationBuilder.RenameIndex(
                name: "IX_EndUserCredentialHistoryLists_UserId",
                table: "EndUserCredentialHistoryLists",
                newName: "IX_EndUserCredentialHistoryLists_EndUserId1");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "AccessHistoryLists",
                newName: "EndUserId1");

            migrationBuilder.RenameIndex(
                name: "IX_AccessHistoryLists_UserId",
                table: "AccessHistoryLists",
                newName: "IX_AccessHistoryLists_EndUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AccessHistoryLists_Users_EndUserId1",
                table: "AccessHistoryLists",
                column: "EndUserId1",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EndUserCredentialHistoryLists_Users_EndUserId1",
                table: "EndUserCredentialHistoryLists",
                column: "EndUserId1",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EndUserTokenLists_Users_EndUserId1",
                table: "EndUserTokenLists",
                column: "EndUserId1",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationUserLists_Users_EndUserId1",
                table: "OrganizationUserLists",
                column: "EndUserId1",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccessHistoryLists_Users_EndUserId1",
                table: "AccessHistoryLists");

            migrationBuilder.DropForeignKey(
                name: "FK_EndUserCredentialHistoryLists_Users_EndUserId1",
                table: "EndUserCredentialHistoryLists");

            migrationBuilder.DropForeignKey(
                name: "FK_EndUserTokenLists_Users_EndUserId1",
                table: "EndUserTokenLists");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganizationUserLists_Users_EndUserId1",
                table: "OrganizationUserLists");

            migrationBuilder.RenameColumn(
                name: "EndUserId1",
                table: "OrganizationUserLists",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_OrganizationUserLists_EndUserId1",
                table: "OrganizationUserLists",
                newName: "IX_OrganizationUserLists_UserId");

            migrationBuilder.RenameColumn(
                name: "EndUserId1",
                table: "EndUserTokenLists",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_EndUserTokenLists_EndUserId1",
                table: "EndUserTokenLists",
                newName: "IX_EndUserTokenLists_UserId");

            migrationBuilder.RenameColumn(
                name: "EndUserId1",
                table: "EndUserCredentialHistoryLists",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_EndUserCredentialHistoryLists_EndUserId1",
                table: "EndUserCredentialHistoryLists",
                newName: "IX_EndUserCredentialHistoryLists_UserId");

            migrationBuilder.RenameColumn(
                name: "EndUserId1",
                table: "AccessHistoryLists",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AccessHistoryLists_EndUserId1",
                table: "AccessHistoryLists",
                newName: "IX_AccessHistoryLists_UserId");

            migrationBuilder.CreateTable(
                name: "EndUserLists",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: true),
                    Username = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EndUserLists", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationUserLists_EndUserId",
                table: "OrganizationUserLists",
                column: "EndUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EndUserTokenLists_EndUserId",
                table: "EndUserTokenLists",
                column: "EndUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EndUserCredentialHistoryLists_EndUserId",
                table: "EndUserCredentialHistoryLists",
                column: "EndUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AccessHistoryLists_EndUserId",
                table: "AccessHistoryLists",
                column: "EndUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccessHistoryLists_EndUserLists_EndUserId",
                table: "AccessHistoryLists",
                column: "EndUserId",
                principalTable: "EndUserLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AccessHistoryLists_Users_UserId",
                table: "AccessHistoryLists",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EndUserCredentialHistoryLists_EndUserLists_EndUserId",
                table: "EndUserCredentialHistoryLists",
                column: "EndUserId",
                principalTable: "EndUserLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EndUserCredentialHistoryLists_Users_UserId",
                table: "EndUserCredentialHistoryLists",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EndUserTokenLists_EndUserLists_EndUserId",
                table: "EndUserTokenLists",
                column: "EndUserId",
                principalTable: "EndUserLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EndUserTokenLists_Users_UserId",
                table: "EndUserTokenLists",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationUserLists_EndUserLists_EndUserId",
                table: "OrganizationUserLists",
                column: "EndUserId",
                principalTable: "EndUserLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationUserLists_Users_UserId",
                table: "OrganizationUserLists",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
