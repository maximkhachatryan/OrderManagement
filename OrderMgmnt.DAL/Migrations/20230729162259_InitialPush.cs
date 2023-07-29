using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OrderMgmnt.DAL.Migrations
{
    public partial class InitialPush : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Venders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "varchar(6)", unicode: false, maxLength: 6, nullable: false, defaultValue: ""),
                    BrandName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstagramLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WebsiteLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VenderWalletAmount = table.Column<decimal>(type: "decimal(12,2)", precision: 12, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Venders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VenderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Venders_VenderId",
                        column: x => x.VenderId,
                        principalTable: "Venders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VenderAddresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    District = table.Column<int>(type: "int", nullable: false),
                    AddressInfo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsRemoved = table.Column<bool>(type: "bit", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderCode = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DesiredPickUpDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeliveryPaymentByClient = table.Column<bool>(type: "bit", nullable: false),
                    ShouldProductPriceBePaid = table.Column<bool>(type: "bit", nullable: false),
                    ProductPrice = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true),
                    OtherNotes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClientFillDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AcceptDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RejectDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ActualPickUpDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeliveryStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeliveryEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ClientRejectDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SentBackToVenderDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ClientName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientDistrict = table.Column<int>(type: "int", nullable: true),
                    ClientAddressInfo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientChangeDeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ClientNotes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VenderAddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_VenderAddresses_VenderAddressId",
                        column: x => x.VenderAddressId,
                        principalTable: "VenderAddresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderCode",
                table: "Orders",
                column: "OrderCode",
                unique: true)
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_VenderAddressId",
                table: "Orders",
                column: "VenderAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserName",
                table: "Users",
                column: "UserName");

            migrationBuilder.CreateIndex(
                name: "IX_Users_VenderId",
                table: "Users",
                column: "VenderId");

            migrationBuilder.CreateIndex(
                name: "IX_VenderAddresses_VenderId",
                table: "VenderAddresses",
                column: "VenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Venders_Code",
                table: "Venders",
                column: "Code",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "VenderAddresses");

            migrationBuilder.DropTable(
                name: "Venders");
        }
    }
}
