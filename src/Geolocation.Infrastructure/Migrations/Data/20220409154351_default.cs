using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Geolocation.Infrastructure.Migrations.Data
{
    public partial class @default : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefreshToken_Users_UserId",
                table: "RefreshToken");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RefreshToken",
                table: "RefreshToken");

            migrationBuilder.RenameTable(
                name: "RefreshToken",
                newName: "Tokens");

            migrationBuilder.RenameIndex(
                name: "IX_RefreshToken_UserId",
                table: "Tokens",
                newName: "IX_Tokens_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tokens",
                table: "Tokens",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "FirstName", "LastName", "Password", "Username" },
                values: new object[] { new Guid("142d108c-cdbb-408a-a4a7-7759ff486a05"), "first", "user", "pass", "loggin" });

            migrationBuilder.AddForeignKey(
                name: "FK_Tokens_Users_UserId",
                table: "Tokens",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tokens_Users_UserId",
                table: "Tokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tokens",
                table: "Tokens");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("142d108c-cdbb-408a-a4a7-7759ff486a05"));

            migrationBuilder.RenameTable(
                name: "Tokens",
                newName: "RefreshToken");

            migrationBuilder.RenameIndex(
                name: "IX_Tokens_UserId",
                table: "RefreshToken",
                newName: "IX_RefreshToken_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RefreshToken",
                table: "RefreshToken",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshToken_Users_UserId",
                table: "RefreshToken",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
