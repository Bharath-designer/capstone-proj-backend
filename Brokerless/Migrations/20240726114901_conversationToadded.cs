using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Brokerless.Migrations
{
    /// <inheritdoc />
    public partial class conversationToadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ConversationWithUserId",
                table: "Conversations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Conversations_ConversationWithUserId",
                table: "Conversations",
                column: "ConversationWithUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Conversations_Users_ConversationWithUserId",
                table: "Conversations",
                column: "ConversationWithUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conversations_Users_ConversationWithUserId",
                table: "Conversations");

            migrationBuilder.DropIndex(
                name: "IX_Conversations_ConversationWithUserId",
                table: "Conversations");

            migrationBuilder.DropColumn(
                name: "ConversationWithUserId",
                table: "Conversations");
        }
    }
}
