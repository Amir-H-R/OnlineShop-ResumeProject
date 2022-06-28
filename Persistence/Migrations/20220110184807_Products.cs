using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class Products : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_ParentCategoryCategoryId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_ParentCategoryCategoryId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "ParentCategoryCategoryId",
                table: "Categories");

            migrationBuilder.RenameColumn(
                name: "ParentId",
                table: "Categories",
                newName: "ParentCategoryId");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Categories",
                newName: "Id");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 1L,
                column: "CreateDate",
                value: new DateTime(2022, 1, 10, 22, 18, 7, 484, DateTimeKind.Local).AddTicks(8078));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 2L,
                column: "CreateDate",
                value: new DateTime(2022, 1, 10, 22, 18, 7, 484, DateTimeKind.Local).AddTicks(8108));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 3L,
                column: "CreateDate",
                value: new DateTime(2022, 1, 10, 22, 18, 7, 484, DateTimeKind.Local).AddTicks(8121));

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentCategoryId",
                table: "Categories",
                column: "ParentCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_ParentCategoryId",
                table: "Categories",
                column: "ParentCategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_ParentCategoryId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_ParentCategoryId",
                table: "Categories");

            migrationBuilder.RenameColumn(
                name: "ParentCategoryId",
                table: "Categories",
                newName: "ParentId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Categories",
                newName: "CategoryId");

            migrationBuilder.AddColumn<long>(
                name: "ParentCategoryCategoryId",
                table: "Categories",
                type: "bigint",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 1L,
                column: "CreateDate",
                value: new DateTime(2021, 12, 30, 0, 28, 18, 596, DateTimeKind.Local).AddTicks(4830));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 2L,
                column: "CreateDate",
                value: new DateTime(2021, 12, 30, 0, 28, 18, 596, DateTimeKind.Local).AddTicks(4873));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 3L,
                column: "CreateDate",
                value: new DateTime(2021, 12, 30, 0, 28, 18, 596, DateTimeKind.Local).AddTicks(4886));

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentCategoryCategoryId",
                table: "Categories",
                column: "ParentCategoryCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_ParentCategoryCategoryId",
                table: "Categories",
                column: "ParentCategoryCategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId");
        }
    }
}
