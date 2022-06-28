using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class productfeatures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "ProductFeatures",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "DisplayName",
                table: "ProductFeatures",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 1L,
                column: "CreateDate",
                value: new DateTime(2022, 1, 13, 19, 21, 36, 100, DateTimeKind.Local).AddTicks(4216));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 2L,
                column: "CreateDate",
                value: new DateTime(2022, 1, 13, 19, 21, 36, 100, DateTimeKind.Local).AddTicks(4252));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 3L,
                column: "CreateDate",
                value: new DateTime(2022, 1, 13, 19, 21, 36, 100, DateTimeKind.Local).AddTicks(4266));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "Value",
                table: "ProductFeatures",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<long>(
                name: "DisplayName",
                table: "ProductFeatures",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 1L,
                column: "CreateDate",
                value: new DateTime(2022, 1, 10, 23, 46, 1, 950, DateTimeKind.Local).AddTicks(6909));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 2L,
                column: "CreateDate",
                value: new DateTime(2022, 1, 10, 23, 46, 1, 950, DateTimeKind.Local).AddTicks(6972));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 3L,
                column: "CreateDate",
                value: new DateTime(2022, 1, 10, 23, 46, 1, 950, DateTimeKind.Local).AddTicks(6985));
        }
    }
}
