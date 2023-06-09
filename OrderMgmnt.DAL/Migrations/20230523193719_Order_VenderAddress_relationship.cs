﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OrderMgmnt.DAL.Migrations
{
    public partial class Order_VenderAddress_relationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_VenderAddresses_VenderAddressId",
                table: "Orders");

            migrationBuilder.AlterColumn<Guid>(
                name: "VenderAddressId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_VenderAddresses_VenderAddressId",
                table: "Orders",
                column: "VenderAddressId",
                principalTable: "VenderAddresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_VenderAddresses_VenderAddressId",
                table: "Orders");

            migrationBuilder.AlterColumn<Guid>(
                name: "VenderAddressId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_VenderAddresses_VenderAddressId",
                table: "Orders",
                column: "VenderAddressId",
                principalTable: "VenderAddresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
