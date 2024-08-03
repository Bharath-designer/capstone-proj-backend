using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Brokerless.Migrations
{
    /// <inheritdoc />
    public partial class updatedindexforpropertyuserviewed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2024, 8, 3, 14, 45, 1, 578, DateTimeKind.Local).AddTicks(3542));

            migrationBuilder.CreateIndex(
                name: "IX_PropertyUserViewed_CreatedOn",
                table: "PropertyUserViewed",
                column: "CreatedOn");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyUserViewed_UserId",
                table: "PropertyUserViewed",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PropertyUserViewed_CreatedOn",
                table: "PropertyUserViewed");

            migrationBuilder.DropIndex(
                name: "IX_PropertyUserViewed_UserId",
                table: "PropertyUserViewed");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2024, 7, 29, 18, 55, 12, 850, DateTimeKind.Local).AddTicks(3013));
        }
    }
}
