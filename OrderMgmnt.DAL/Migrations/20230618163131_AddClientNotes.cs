using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OrderMgmnt.DAL.Migrations
{
    public partial class AddClientNotes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientPreferredDate",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "ClientNotes",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientNotes",
                table: "Orders");

            migrationBuilder.AddColumn<DateTime>(
                name: "ClientPreferredDate",
                table: "Orders",
                type: "datetime2",
                nullable: true);
        }
    }
}
