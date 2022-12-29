using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KAS.ECOS.API.Migrations
{
    public partial class updateEndUserIdType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropIndex(
                name: "IX_OrganizationUserLists_EndUserId1",
                table: "OrganizationUserLists");

            migrationBuilder.DropIndex(
                name: "IX_EndUserTokenLists_EndUserId1",
                table: "EndUserTokenLists");

            migrationBuilder.DropIndex(
                name: "IX_EndUserCredentialHistoryLists_EndUserId1",
                table: "EndUserCredentialHistoryLists");

            migrationBuilder.DropIndex(
                name: "IX_AccessHistoryLists_EndUserId1",
                table: "AccessHistoryLists");

            migrationBuilder.DropColumn(
                name: "EndUserId1",
                table: "OrganizationUserLists");

            migrationBuilder.DropColumn(
                name: "EndUserId1",
                table: "EndUserTokenLists");

            migrationBuilder.DropColumn(
                name: "EndUserId1",
                table: "EndUserCredentialHistoryLists");

            migrationBuilder.DropColumn(
                name: "EndUserId1",
                table: "AccessHistoryLists");

            migrationBuilder.AlterColumn<string>(
                name: "EndUserId",
                table: "OrganizationUserLists",
                type: "text",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<string>(
                name: "EndUserId",
                table: "EndUserTokenLists",
                type: "text",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<string>(
                name: "EndUserId",
                table: "EndUserCredentialHistoryLists",
                type: "text",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<string>(
                name: "EndUserId",
                table: "AccessHistoryLists",
                type: "text",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

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
                name: "FK_AccessHistoryLists_Users_EndUserId",
                table: "AccessHistoryLists",
                column: "EndUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EndUserCredentialHistoryLists_Users_EndUserId",
                table: "EndUserCredentialHistoryLists",
                column: "EndUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EndUserTokenLists_Users_EndUserId",
                table: "EndUserTokenLists",
                column: "EndUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationUserLists_Users_EndUserId",
                table: "OrganizationUserLists",
                column: "EndUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccessHistoryLists_Users_EndUserId",
                table: "AccessHistoryLists");

            migrationBuilder.DropForeignKey(
                name: "FK_EndUserCredentialHistoryLists_Users_EndUserId",
                table: "EndUserCredentialHistoryLists");

            migrationBuilder.DropForeignKey(
                name: "FK_EndUserTokenLists_Users_EndUserId",
                table: "EndUserTokenLists");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganizationUserLists_Users_EndUserId",
                table: "OrganizationUserLists");

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

            migrationBuilder.AlterColumn<Guid>(
                name: "EndUserId",
                table: "OrganizationUserLists",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "EndUserId1",
                table: "OrganizationUserLists",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "EndUserId",
                table: "EndUserTokenLists",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "EndUserId1",
                table: "EndUserTokenLists",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "EndUserId",
                table: "EndUserCredentialHistoryLists",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "EndUserId1",
                table: "EndUserCredentialHistoryLists",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "EndUserId",
                table: "AccessHistoryLists",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "EndUserId1",
                table: "AccessHistoryLists",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationUserLists_EndUserId1",
                table: "OrganizationUserLists",
                column: "EndUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_EndUserTokenLists_EndUserId1",
                table: "EndUserTokenLists",
                column: "EndUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_EndUserCredentialHistoryLists_EndUserId1",
                table: "EndUserCredentialHistoryLists",
                column: "EndUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_AccessHistoryLists_EndUserId1",
                table: "AccessHistoryLists",
                column: "EndUserId1");

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
    }
}
