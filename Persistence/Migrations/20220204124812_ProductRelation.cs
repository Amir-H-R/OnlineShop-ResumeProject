using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class ProductRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ProductId",
                table: "Carts",
                type: "bigint",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 1L,
                column: "CreateDate",
                value: new DateTime(2022, 2, 4, 16, 18, 11, 407, DateTimeKind.Local).AddTicks(9688));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 2L,
                column: "CreateDate",
                value: new DateTime(2022, 2, 4, 16, 18, 11, 407, DateTimeKind.Local).AddTicks(9731));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 3L,
                column: "CreateDate",
                value: new DateTime(2022, 2, 4, 16, 18, 11, 407, DateTimeKind.Local).AddTicks(9747));

            migrationBuilder.CreateIndex(
                name: "IX_Carts_ProductId",
                table: "Carts",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Products_ProductId",
                table: "Carts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Products_ProductId",
                table: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_Carts_ProductId",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Carts");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 1L,
                column: "CreateDate",
                value: new DateTime(2022, 2, 4, 12, 47, 22, 628, DateTimeKind.Local).AddTicks(5140));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 2L,
                column: "CreateDate",
                value: new DateTime(2022, 2, 4, 12, 47, 22, 628, DateTimeKind.Local).AddTicks(5303));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 3L,
                column: "CreateDate",
                value: new DateTime(2022, 2, 4, 12, 47, 22, 628, DateTimeKind.Local).AddTicks(5317));
        }
    }
}
