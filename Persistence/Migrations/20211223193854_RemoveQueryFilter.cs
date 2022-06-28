using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class RemoveQueryFilter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 1L,
                column: "CreateDate",
                value: new DateTime(2021, 12, 23, 23, 8, 53, 829, DateTimeKind.Local).AddTicks(7744));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 2L,
                column: "CreateDate",
                value: new DateTime(2021, 12, 23, 23, 8, 53, 829, DateTimeKind.Local).AddTicks(7784));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 3L,
                column: "CreateDate",
                value: new DateTime(2021, 12, 23, 23, 8, 53, 829, DateTimeKind.Local).AddTicks(7799));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 1L,
                column: "CreateDate",
                value: new DateTime(2021, 11, 26, 15, 3, 0, 213, DateTimeKind.Local).AddTicks(2379));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 2L,
                column: "CreateDate",
                value: new DateTime(2021, 11, 26, 15, 3, 0, 213, DateTimeKind.Local).AddTicks(2410));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 3L,
                column: "CreateDate",
                value: new DateTime(2021, 11, 26, 15, 3, 0, 213, DateTimeKind.Local).AddTicks(2422));
        }
    }
}
