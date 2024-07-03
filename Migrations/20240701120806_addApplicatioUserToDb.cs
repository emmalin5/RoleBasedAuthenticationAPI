using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace auth.Migrations
{
    /// <inheritdoc />
    public partial class addApplicatioUserToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1874d48a-23ed-4e3a-b503-687bf196b892");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e628d26b-6876-4c41-a282-3c8bef836bcb");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "26e4b194-5569-4f4f-95a2-f3033f90d65d", null, "User", "USER" },
                    { "6d99f4cc-c9d3-47b3-b580-f18d8638ba76", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "26e4b194-5569-4f4f-95a2-f3033f90d65d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6d99f4cc-c9d3-47b3-b580-f18d8638ba76");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1874d48a-23ed-4e3a-b503-687bf196b892", null, "Admin", "ADMIN" },
                    { "e628d26b-6876-4c41-a282-3c8bef836bcb", null, "User", "USER" }
                });
        }
    }
}
