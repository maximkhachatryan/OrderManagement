using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OrderMgmnt.DAL.Migrations
{
    public partial class ChangeOrderEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Venders_VenderId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_VenderId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "VenderPreferredDate",
                table: "Orders");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeliveryPaymentByClient",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "OtherNotes",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PickUpDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ProductDescription",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ProductPrice",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "ShouldProductPriceBePaid",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "VenderAddressId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_VenderAddressId",
                table: "Orders",
                column: "VenderAddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_VenderAddresses_VenderAddressId",
                table: "Orders",
                column: "VenderAddressId",
                principalTable: "VenderAddresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_VenderAddresses_VenderAddressId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_VenderAddressId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "IsDeliveryPaymentByClient",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OtherNotes",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PickUpDate",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ProductDescription",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ProductPrice",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ShouldProductPriceBePaid",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "VenderAddressId",
                table: "Orders");

            migrationBuilder.AddColumn<DateTime>(
                name: "VenderPreferredDate",
                table: "Orders",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_VenderId",
                table: "Orders",
                column: "VenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Venders_VenderId",
                table: "Orders",
                column: "VenderId",
                principalTable: "Venders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
