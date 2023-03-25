using Microsoft.EntityFrameworkCore.Migrations;

namespace OrderMgmnt.DAL.Migrations
{
    public partial class MergeFirstNameLastName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientFirstname",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "ClientLastname",
                table: "Orders",
                newName: "ClientName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ClientName",
                table: "Orders",
                newName: "ClientLastname");

            migrationBuilder.AddColumn<string>(
                name: "ClientFirstname",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
