using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OrderMgmnt.DAL.Migrations
{
    public partial class Change1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PreferredDate",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Orders");

            migrationBuilder.AddColumn<decimal>(
                name: "VenderWalletAmount",
                table: "Venders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "ClientFillDate",
                table: "Orders",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ClientPreferredDate",
                table: "Orders",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "Orders",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "OrderCancelDate",
                table: "Orders",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "OrderFinishDate",
                table: "Orders",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "VenderPreferredDate",
                table: "Orders",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VenderWalletAmount",
                table: "Venders");

            migrationBuilder.DropColumn(
                name: "ClientFillDate",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ClientPreferredDate",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderCancelDate",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderFinishDate",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "VenderPreferredDate",
                table: "Orders");

            migrationBuilder.AddColumn<DateTime>(
                name: "PreferredDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<byte>(
                name: "State",
                table: "Orders",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }
    }
}
