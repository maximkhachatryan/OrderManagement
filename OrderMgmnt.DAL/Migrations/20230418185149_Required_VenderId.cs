using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OrderMgmnt.DAL.Migrations
{
    public partial class Required_VenderId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Venders_VenderId",
                table: "Users");

            migrationBuilder.AlterColumn<Guid>(
                name: "VenderId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Venders_VenderId",
                table: "Users",
                column: "VenderId",
                principalTable: "Venders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Venders_VenderId",
                table: "Users");

            migrationBuilder.AlterColumn<Guid>(
                name: "VenderId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Venders_VenderId",
                table: "Users",
                column: "VenderId",
                principalTable: "Venders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
