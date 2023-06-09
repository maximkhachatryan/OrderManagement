using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OrderMgmnt.DAL.Migrations
{
    public partial class AddStatusRelatedDatesForOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PickUpDate",
                table: "Orders",
                newName: "DesiredPickUpDate");

            migrationBuilder.RenameColumn(
                name: "OrderFinishDate",
                table: "Orders",
                newName: "SentBackToVenderDate");

            migrationBuilder.RenameColumn(
                name: "OrderCancelDate",
                table: "Orders",
                newName: "RejectDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "AcceptDate",
                table: "Orders",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ActualPickUpDate",
                table: "Orders",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ClientRejectDate",
                table: "Orders",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeliveryStartDate",
                table: "Orders",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcceptDate",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ActualPickUpDate",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ClientRejectDate",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DeliveryStartDate",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "SentBackToVenderDate",
                table: "Orders",
                newName: "OrderFinishDate");

            migrationBuilder.RenameColumn(
                name: "RejectDate",
                table: "Orders",
                newName: "OrderCancelDate");

            migrationBuilder.RenameColumn(
                name: "DesiredPickUpDate",
                table: "Orders",
                newName: "PickUpDate");
        }
    }
}
