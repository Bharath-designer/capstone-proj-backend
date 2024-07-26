using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Brokerless.Migrations
{
    /// <inheritdoc />
    public partial class updatedseedingdata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_SubscriptionTemplates_SubscriptionTemplateId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSubscriptions_SubscriptionTemplates_SubscriptionTemplateId",
                table: "UserSubscriptions");

            migrationBuilder.DropIndex(
                name: "IX_UserSubscriptions_SubscriptionTemplateId",
                table: "UserSubscriptions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_SubscriptionTemplateId",
                table: "Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubscriptionTemplates",
                table: "SubscriptionTemplates");

            migrationBuilder.DropColumn(
                name: "SubscriptionTemplateId",
                table: "UserSubscriptions");

            migrationBuilder.DropColumn(
                name: "SubscriptionTemplateId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "SubscriptionTemplateId",
                table: "SubscriptionTemplates");

            migrationBuilder.AddColumn<string>(
                name: "SubscriptionTemplateName",
                table: "UserSubscriptions",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SubscriptionTemplateName",
                table: "Transactions",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "Validity",
                table: "SubscriptionTemplates",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "SubsriptionName",
                table: "SubscriptionTemplates",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "Currency",
                table: "SubscriptionTemplates",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "SubscriptionTemplates",
                type: "float",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubscriptionTemplates",
                table: "SubscriptionTemplates",
                column: "SubsriptionName");

            migrationBuilder.InsertData(
                table: "SubscriptionTemplates",
                columns: new[] { "SubsriptionName", "Currency", "Description", "MaxListingCount", "MaxSellerViewCount", "Price", "Validity" },
                values: new object[,]
                {
                    { "Free", null, "This subsctiption is default for user.", 1, 1, null, null },
                    { "Gold", 0, "This subsription is suitable for user who wants to post, view frequently", 50, 50, 999.0, 28 },
                    { "Silver", 0, "This subsription is suitable for user who wants a basic limits", 10, 10, 499.0, 28 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserSubscriptions_SubscriptionTemplateName",
                table: "UserSubscriptions",
                column: "SubscriptionTemplateName");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_SubscriptionTemplateName",
                table: "Transactions",
                column: "SubscriptionTemplateName");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_SubscriptionTemplates_SubscriptionTemplateName",
                table: "Transactions",
                column: "SubscriptionTemplateName",
                principalTable: "SubscriptionTemplates",
                principalColumn: "SubsriptionName",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSubscriptions_SubscriptionTemplates_SubscriptionTemplateName",
                table: "UserSubscriptions",
                column: "SubscriptionTemplateName",
                principalTable: "SubscriptionTemplates",
                principalColumn: "SubsriptionName",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_SubscriptionTemplates_SubscriptionTemplateName",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSubscriptions_SubscriptionTemplates_SubscriptionTemplateName",
                table: "UserSubscriptions");

            migrationBuilder.DropIndex(
                name: "IX_UserSubscriptions_SubscriptionTemplateName",
                table: "UserSubscriptions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_SubscriptionTemplateName",
                table: "Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubscriptionTemplates",
                table: "SubscriptionTemplates");

            migrationBuilder.DeleteData(
                table: "SubscriptionTemplates",
                keyColumn: "SubsriptionName",
                keyValue: "Free");

            migrationBuilder.DeleteData(
                table: "SubscriptionTemplates",
                keyColumn: "SubsriptionName",
                keyValue: "Gold");

            migrationBuilder.DeleteData(
                table: "SubscriptionTemplates",
                keyColumn: "SubsriptionName",
                keyValue: "Silver");

            migrationBuilder.DropColumn(
                name: "SubscriptionTemplateName",
                table: "UserSubscriptions");

            migrationBuilder.DropColumn(
                name: "SubscriptionTemplateName",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "SubscriptionTemplates");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "SubscriptionTemplates");

            migrationBuilder.AddColumn<int>(
                name: "SubscriptionTemplateId",
                table: "UserSubscriptions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SubscriptionTemplateId",
                table: "Transactions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Validity",
                table: "SubscriptionTemplates",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SubsriptionName",
                table: "SubscriptionTemplates",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "SubscriptionTemplateId",
                table: "SubscriptionTemplates",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubscriptionTemplates",
                table: "SubscriptionTemplates",
                column: "SubscriptionTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSubscriptions_SubscriptionTemplateId",
                table: "UserSubscriptions",
                column: "SubscriptionTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_SubscriptionTemplateId",
                table: "Transactions",
                column: "SubscriptionTemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_SubscriptionTemplates_SubscriptionTemplateId",
                table: "Transactions",
                column: "SubscriptionTemplateId",
                principalTable: "SubscriptionTemplates",
                principalColumn: "SubscriptionTemplateId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSubscriptions_SubscriptionTemplates_SubscriptionTemplateId",
                table: "UserSubscriptions",
                column: "SubscriptionTemplateId",
                principalTable: "SubscriptionTemplates",
                principalColumn: "SubscriptionTemplateId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
