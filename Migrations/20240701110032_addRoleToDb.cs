using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace auth.Migrations
{
    /// <inheritdoc />
    public partial class addRoleToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1874d48a-23ed-4e3a-b503-687bf196b892", null, "Admin", "ADMIN" },
                    { "e628d26b-6876-4c41-a282-3c8bef836bcb", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                table: "Students",
                columns: new[] { "Id", "EnrolledDate", "Name" },
                values: new object[] { 1, null, "Ei Myat " });
        }
    }
}
