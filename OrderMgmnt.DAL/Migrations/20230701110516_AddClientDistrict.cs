using Microsoft.EntityFrameworkCore.Migrations;

namespace OrderMgmnt.DAL.Migrations
{
    public partial class AddClientDistrict : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ClientAddress",
                table: "Orders",
                newName: "ClientAddressInfo");

            migrationBuilder.AddColumn<int>(
                name: "ClientDistrict",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientDistrict",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "ClientAddressInfo",
                table: "Orders",
                newName: "ClientAddress");
        }
    }
}
