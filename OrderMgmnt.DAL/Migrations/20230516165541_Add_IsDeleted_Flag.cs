using Microsoft.EntityFrameworkCore.Migrations;

namespace OrderMgmnt.DAL.Migrations
{
    public partial class Add_IsDeleted_Flag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRemoved",
                table: "VenderAddresses",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRemoved",
                table: "VenderAddresses");
        }
    }
}
