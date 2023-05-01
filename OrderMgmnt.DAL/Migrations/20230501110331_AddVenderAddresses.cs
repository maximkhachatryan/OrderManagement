using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OrderMgmnt.DAL.Migrations
{
    public partial class AddVenderAddresses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VenderAddresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    District = table.Column<int>(type: "int", nullable: false),
                    AddressInfo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VenderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VenderAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VenderAddresses_Venders_VenderId",
                        column: x => x.VenderId,
                        principalTable: "Venders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VenderAddresses_VenderId",
                table: "VenderAddresses",
                column: "VenderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VenderAddresses");
        }
    }
}
