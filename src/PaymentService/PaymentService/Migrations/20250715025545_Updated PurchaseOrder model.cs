using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PaymentService.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedPurchaseOrdermodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrders_ShippingAddresses_ShippingAddressId",
                table: "PurchaseOrders");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseOrders_ShippingAddressId",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "ShippingAddressId",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Products");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ShippingAddressId",
                table: "PurchaseOrders",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Products",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_ShippingAddressId",
                table: "PurchaseOrders",
                column: "ShippingAddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrders_ShippingAddresses_ShippingAddressId",
                table: "PurchaseOrders",
                column: "ShippingAddressId",
                principalTable: "ShippingAddresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
