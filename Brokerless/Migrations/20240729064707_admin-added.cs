using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Brokerless.Migrations
{
    /// <inheritdoc />
    public partial class adminadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CountryCode", "CreatedOn", "Email", "FullName", "PhoneNumber", "PhoneNumberVerified", "ProfileUrl", "UserRole" },
                values: new object[] { 1, null, new DateTime(2024, 7, 29, 12, 17, 6, 839, DateTimeKind.Local).AddTicks(2049), "bharath060723@gmail.com", "Brokerless Admin", null, false, "https://lh3.googleusercontent.com/-c7zfo6Em20Y/AAAAAAAAAAI/AAAAAAAAAAA/ALKGfkniiqltD54bxzEjiVBwMM19Xk9Ikw/photo.jpg", "Admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1);
        }
    }
}
