using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Geolocation.Infrastructure.Migrations.Data
{
    public partial class list : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("142d108c-cdbb-408a-a4a7-7759ff486a05"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "FirstName", "LastName", "Password", "Username" },
                values: new object[] { new Guid("b613e65e-03ba-4cae-a0ae-29d711ba8a9e"), "first", "user", "pass", "loggin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b613e65e-03ba-4cae-a0ae-29d711ba8a9e"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "FirstName", "LastName", "Password", "Username" },
                values: new object[] { new Guid("142d108c-cdbb-408a-a4a7-7759ff486a05"), "first", "user", "pass", "loggin" });
        }
    }
}
