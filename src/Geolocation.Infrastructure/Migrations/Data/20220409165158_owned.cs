using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Geolocation.Infrastructure.Migrations.Data
{
    public partial class owned : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tokens_Users_UserId",
                table: "Tokens");

            migrationBuilder.DropIndex(
                name: "IX_Tokens_UserId",
                table: "Tokens");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b613e65e-03ba-4cae-a0ae-29d711ba8a9e"));

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Tokens");

            migrationBuilder.AddColumn<int>(
                name: "RefreshTokens_Capacity",
                table: "Users",
                type: "integer",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "FirstName", "LastName", "Password", "Username" },
                values: new object[] { new Guid("4f5633c0-ec53-452d-a109-3278da888041"), "first", "user", "pass", "loggin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("4f5633c0-ec53-452d-a109-3278da888041"));

            migrationBuilder.DropColumn(
                name: "RefreshTokens_Capacity",
                table: "Users");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Tokens",
                type: "uuid",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "FirstName", "LastName", "Password", "Username" },
                values: new object[] { new Guid("b613e65e-03ba-4cae-a0ae-29d711ba8a9e"), "first", "user", "pass", "loggin" });

            migrationBuilder.CreateIndex(
                name: "IX_Tokens_UserId",
                table: "Tokens",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tokens_Users_UserId",
                table: "Tokens",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
