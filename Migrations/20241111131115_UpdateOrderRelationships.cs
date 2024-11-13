using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inventory_Management_API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOrderRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StockOrderItems_ProductId",
                table: "StockOrderItems");

            migrationBuilder.DropIndex(
                name: "IX_CustomerOrderItems_ProductId",
                table: "CustomerOrderItems");

            migrationBuilder.CreateIndex(
                name: "IX_StockOrderItems_ProductId",
                table: "StockOrderItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerOrderItems_ProductId",
                table: "CustomerOrderItems",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StockOrderItems_ProductId",
                table: "StockOrderItems");

            migrationBuilder.DropIndex(
                name: "IX_CustomerOrderItems_ProductId",
                table: "CustomerOrderItems");

            migrationBuilder.CreateIndex(
                name: "IX_StockOrderItems_ProductId",
                table: "StockOrderItems",
                column: "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerOrderItems_ProductId",
                table: "CustomerOrderItems",
                column: "ProductId",
                unique: true);
        }
    }
}
