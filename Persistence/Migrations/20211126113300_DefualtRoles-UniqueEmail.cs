using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class DefualtRolesUniqueEmail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "CreateDate", "IsRemoved", "Name", "RemoveTime", "UpdateDate" },
                values: new object[] { 1L, new DateTime(2021, 11, 26, 15, 3, 0, 213, DateTimeKind.Local).AddTicks(2379), false, "Admin", null, null });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "CreateDate", "IsRemoved", "Name", "RemoveTime", "UpdateDate" },
                values: new object[] { 2L, new DateTime(2021, 11, 26, 15, 3, 0, 213, DateTimeKind.Local).AddTicks(2410), false, "Operator", null, null });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "CreateDate", "IsRemoved", "Name", "RemoveTime", "UpdateDate" },
                values: new object[] { 3L, new DateTime(2021, 11, 26, 15, 3, 0, 213, DateTimeKind.Local).AddTicks(2422), false, "Customer", null, null });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Email",
                table: "Users");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 3L);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
