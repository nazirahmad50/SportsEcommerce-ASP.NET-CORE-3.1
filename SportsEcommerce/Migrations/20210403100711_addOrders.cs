﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace SportsEcommerce.Migrations
{
    public partial class addOrders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Line1 = table.Column<string>(nullable: false),
                    Line2 = table.Column<string>(nullable: true),
                    Line3 = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: false),
                    State = table.Column<string>(nullable: false),
                    Zip = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: false),
                    GiftWrap = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                });

            migrationBuilder.CreateTable(
                name: "Cartline",
                columns: table => new
                {
                    CartLineId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductID = table.Column<long>(nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    OrderId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cartline", x => x.CartLineId);
                    table.ForeignKey(
                        name: "FK_Cartline_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cartline_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cartline_OrderId",
                table: "Cartline",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Cartline_ProductID",
                table: "Cartline",
                column: "ProductID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cartline");

            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
