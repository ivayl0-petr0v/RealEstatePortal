using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RealEstatePortal.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedRolesAndAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "11111111-1111-1111-1111-111111111111", null, "Administrator", "ADMINISTRATOR" },
                    { "22222222-2222-2222-2222-222222222222", null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "33333333-3333-3333-3333-333333333333", 0, "ea70f270-aa03-433d-abc8-6d2975316116", "ApplicationUser", "admin@test.com", true, false, null, "ADMIN@TEST.COM", "ADMIN@TEST.COM", "AQAAAAIAAYagAAAAEDGGSyhtnGeF1Bp6F4OQBZqkOsNoFEDN5LHiVOZGwqkPeBaODPTApERPgRFtlLzOkg==", null, false, "3ab95c23-a8d6-42c9-b09e-336c128b1665", false, "admin@test.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "11111111-1111-1111-1111-111111111111", "33333333-3333-3333-3333-333333333333" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "22222222-2222-2222-2222-222222222222");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "11111111-1111-1111-1111-111111111111", "33333333-3333-3333-3333-333333333333" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "11111111-1111-1111-1111-111111111111");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "33333333-3333-3333-3333-333333333333");
        }
    }
}
