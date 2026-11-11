using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OnlineStore.Migrations
{
    /// <inheritdoc />
    public partial class SeedProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "CreatedAt", "DeletedAt", "Description", "Price", "StockQuantity", "Title" },
                values: new object[,]
                {
                    { 1, "Smartphones", new DateTime(2026, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Latest Apple iPhone with advanced performance and camera.", 750m, 20, "iPhone 17" },
                    { 2, "Smartphones", new DateTime(2026, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Premium Samsung smartphone with AMOLED display.", 680m, 15, "Samsung Galaxy S26" },
                    { 3, "Laptops", new DateTime(2026, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Powerful laptop suitable for work, study and everyday use.", 850m, 10, "Dell Inspiron 15" },
                    { 4, "Laptops", new DateTime(2026, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Slim and lightweight laptop with excellent performance.", 780m, 12, "HP Pavilion 14" },
                    { 5, "Headphones", new DateTime(2026, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Wireless noise-cancelling headphones with premium sound.", 350m, 25, "Sony WH-1000XM6" },
                    { 6, "Headphones", new DateTime(2026, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Wireless earbuds with active noise cancellation.", 250m, 30, "Apple AirPods Pro" },
                    { 7, "Accessories", new DateTime(2026, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Ergonomic wireless mouse designed for productivity.", 95m, 40, "Logitech MX Master 3S" },
                    { 8, "Keyboards", new DateTime(2026, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Compact mechanical wireless keyboard for work and gaming.", 110m, 18, "Keychron K2" },
                    { 9, "Accessories", new DateTime(2026, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "High-capacity portable power bank with fast charging.", 60m, 50, "Anker Power Bank 20,000mAh" },
                    { 10, "Monitors", new DateTime(2026, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "27-inch Full HD monitor suitable for work and entertainment.", 220m, 22, "Samsung 27-inch Monitor" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10);
        }
    }
}
