using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class UserRolesBugFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 1L,
                column: "CreateDate",
                value: new DateTime(2021, 12, 27, 14, 10, 8, 651, DateTimeKind.Local).AddTicks(3674));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 2L,
                column: "CreateDate",
                value: new DateTime(2021, 12, 27, 14, 10, 8, 651, DateTimeKind.Local).AddTicks(3706));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 3L,
                column: "CreateDate",
                value: new DateTime(2021, 12, 27, 14, 10, 8, 651, DateTimeKind.Local).AddTicks(3719));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 1L,
                column: "CreateDate",
                value: new DateTime(2021, 12, 24, 17, 37, 59, 129, DateTimeKind.Local).AddTicks(780));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 2L,
                column: "CreateDate",
                value: new DateTime(2021, 12, 24, 17, 37, 59, 129, DateTimeKind.Local).AddTicks(812));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 3L,
                column: "CreateDate",
                value: new DateTime(2021, 12, 24, 17, 37, 59, 129, DateTimeKind.Local).AddTicks(824));
        }
    }
}
