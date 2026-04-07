using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RealEstatePortal.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedRealEstates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "33333333-3333-3333-3333-333333333333",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a29cf631-1bcf-41a2-82cf-79bbb98f9e00", "AQAAAAIAAYagAAAAEJTV77ZtEMelMd9UuE0oTawOSiCvzLiCRgByE+XrL67DDszJf50bxJHaaGAVY1en2w==", "2c0a31b3-a75a-4f46-97f5-915351a1ca84" });

            migrationBuilder.InsertData(
                table: "RealEstates",
                columns: new[] { "Id", "Address", "AgentId", "Area", "BathroomsCount", "BedroomsCount", "CategoryId", "CityId", "CompletionStatus", "ConstructionType", "ConstructionYear", "Description", "Exposure", "Furnishing", "IsDeleted", "Price", "RealEstateFloor", "RoomsCount", "Status", "TotalFloors", "TransactionType" },
                values: new object[,]
                {
                    { new Guid("3f9c5b2a-8d1e-4f7a-b2c6-9e4a1d5c8b7f"), "bul. Cherni Vrah 45", new Guid("d7445e1e-e99e-4cae-82f4-515775351513"), 120m, 2, 3, 1, 1, "Fully finished", "Brick", 2022, "Spacious 3-bedroom apartment in a premium location with underground parking and 24/7 security.", "South", "Unfurnished", false, 250000m, 5, 4, 0, 8, 0 },
                    { new Guid("5c6b7a89-0d1e-4f2a-93b4-c5d6e7f8a9b0"), "Trakia, bl. 100", new Guid("72d70512-ee7b-4f2e-bd62-baf7773f85fd"), 55m, 1, 1, 1, 2, "Ready for living", "Panel", 1985, "Affordable apartment for rent in a quiet neighborhood. Close to public transport and supermarkets.", "West", "Basic Furniture", false, 400m, 2, 2, 0, 8, 1 },
                    { new Guid("7e8f9a0b-1c2d-43e4-85f6-7a8b9c0d1e2f"), "Briz, ul. Sveti Nikola", new Guid("d7445e1e-e99e-4cae-82f4-515775351513"), 70m, 1, 1, 1, 3, "Ready for living", "Brick", 2019, "Modern 1-bedroom apartment for rent with sea view. Fully furnished.", "South-East", "Fully Furnished", false, 650m, 3, 2, 0, 5, 1 },
                    { new Guid("a1b2c3d4-e5f6-47a8-9b0c-1d2e3f4a5b6c"), "ul. Ivan Vazov 12", new Guid("d7445e1e-e99e-4cae-82f4-515775351513"), 95m, 1, 2, 2, 2, "Fully finished", "Brick", 2015, "Cozy house in the central area. Recently renovated with a small private garden.", "East-West", "Fully Furnished", false, 145000m, 1, 3, 0, 2, 0 },
                    { new Guid("d4c3b2a1-6f5e-48a7-b0c9-d1e2f3a4b5c6"), "Mladost 4, Business Park", new Guid("72d70512-ee7b-4f2e-bd62-baf7773f85fd"), 155m, 3, 4, 3, 1, "Fully finished", "Brick", 2021, "Luxury penthouse right next to Business Park Sofia. Huge terrace and smart home system.", "South-West", "Semi-furnished", false, 320000m, 11, 5, 0, 12, 0 },
                    { new Guid("f1e2d3c4-b5a6-4978-8c0b-a19b2c3d4e5f"), "Lazur, bl. 77", new Guid("72d70512-ee7b-4f2e-bd62-baf7773f85fd"), 65m, 1, 1, 1, 4, "Fully finished", "Brick", 1998, "Excellent investment property near the Sea Garden. Needs minor cosmetic repairs.", "North-East", "Unfurnished", false, 115000m, 4, 2, 0, 6, 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RealEstates",
                keyColumn: "Id",
                keyValue: new Guid("3f9c5b2a-8d1e-4f7a-b2c6-9e4a1d5c8b7f"));

            migrationBuilder.DeleteData(
                table: "RealEstates",
                keyColumn: "Id",
                keyValue: new Guid("5c6b7a89-0d1e-4f2a-93b4-c5d6e7f8a9b0"));

            migrationBuilder.DeleteData(
                table: "RealEstates",
                keyColumn: "Id",
                keyValue: new Guid("7e8f9a0b-1c2d-43e4-85f6-7a8b9c0d1e2f"));

            migrationBuilder.DeleteData(
                table: "RealEstates",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-47a8-9b0c-1d2e3f4a5b6c"));

            migrationBuilder.DeleteData(
                table: "RealEstates",
                keyColumn: "Id",
                keyValue: new Guid("d4c3b2a1-6f5e-48a7-b0c9-d1e2f3a4b5c6"));

            migrationBuilder.DeleteData(
                table: "RealEstates",
                keyColumn: "Id",
                keyValue: new Guid("f1e2d3c4-b5a6-4978-8c0b-a19b2c3d4e5f"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "33333333-3333-3333-3333-333333333333",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ea70f270-aa03-433d-abc8-6d2975316116", "AQAAAAIAAYagAAAAEDGGSyhtnGeF1Bp6F4OQBZqkOsNoFEDN5LHiVOZGwqkPeBaODPTApERPgRFtlLzOkg==", "3ab95c23-a8d6-42c9-b09e-336c128b1665" });
        }
    }
}
