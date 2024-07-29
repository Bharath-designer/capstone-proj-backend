using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Brokerless.Migrations
{
    /// <inheritdoc />
    public partial class conversationUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conversations_Users_ConversationWithUserId",
                table: "Conversations");

            migrationBuilder.DropForeignKey(
                name: "FK_Conversations_Users_UserId",
                table: "Conversations");

            migrationBuilder.DropTable(
                name: "ConversationChat");

            migrationBuilder.DropIndex(
                name: "IX_Conversations_ConversationWithUserId",
                table: "Conversations");

            migrationBuilder.DropIndex(
                name: "IX_Conversations_UserId",
                table: "Conversations");

            migrationBuilder.DropColumn(
                name: "ConversationWithUserId",
                table: "Conversations");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Conversations");

            migrationBuilder.AddColumn<int>(
                name: "ConversationId",
                table: "Chats",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "UserConversation",
                columns: table => new
                {
                    ConversationsConversationId = table.Column<int>(type: "int", nullable: false),
                    UsersUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserConversation", x => new { x.ConversationsConversationId, x.UsersUserId });
                    table.ForeignKey(
                        name: "FK_UserConversation_Conversations_ConversationsConversationId",
                        column: x => x.ConversationsConversationId,
                        principalTable: "Conversations",
                        principalColumn: "ConversationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserConversation_Users_UsersUserId",
                        column: x => x.UsersUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Chats_ConversationId",
                table: "Chats",
                column: "ConversationId");

            migrationBuilder.CreateIndex(
                name: "IX_UserConversation_UsersUserId",
                table: "UserConversation",
                column: "UsersUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_Conversations_ConversationId",
                table: "Chats",
                column: "ConversationId",
                principalTable: "Conversations",
                principalColumn: "ConversationId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chats_Conversations_ConversationId",
                table: "Chats");

            migrationBuilder.DropTable(
                name: "UserConversation");

            migrationBuilder.DropIndex(
                name: "IX_Chats_ConversationId",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "ConversationId",
                table: "Chats");

            migrationBuilder.AddColumn<int>(
                name: "ConversationWithUserId",
                table: "Conversations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Conversations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ConversationChat",
                columns: table => new
                {
                    ChatsChatId = table.Column<int>(type: "int", nullable: false),
                    ConversationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConversationChat", x => new { x.ChatsChatId, x.ConversationId });
                    table.ForeignKey(
                        name: "FK_ConversationChat_Chats_ChatsChatId",
                        column: x => x.ChatsChatId,
                        principalTable: "Chats",
                        principalColumn: "ChatId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConversationChat_Conversations_ConversationId",
                        column: x => x.ConversationId,
                        principalTable: "Conversations",
                        principalColumn: "ConversationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Conversations_ConversationWithUserId",
                table: "Conversations",
                column: "ConversationWithUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Conversations_UserId",
                table: "Conversations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ConversationChat_ConversationId",
                table: "ConversationChat",
                column: "ConversationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Conversations_Users_ConversationWithUserId",
                table: "Conversations",
                column: "ConversationWithUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Conversations_Users_UserId",
                table: "Conversations",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
