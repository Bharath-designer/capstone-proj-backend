using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Brokerless.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Chats",
                columns: table => new
                {
                    ChatId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chats", x => x.ChatId);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionTemplates",
                columns: table => new
                {
                    SubscriptionTemplateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubsriptionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxListingCount = table.Column<int>(type: "int", nullable: false),
                    MaxSellerViewCount = table.Column<int>(type: "int", nullable: false),
                    Validity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionTemplates", x => x.SubscriptionTemplateId);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    TagValue = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.TagValue);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserRole = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfileUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberVerified = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Conversations",
                columns: table => new
                {
                    ConversationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conversations", x => x.ConversationId);
                    table.ForeignKey(
                        name: "FK_Conversations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Properties",
                columns: table => new
                {
                    PropertyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropertyType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PropertyCategory = table.Column<int>(type: "int", nullable: true),
                    ListingType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocationLat = table.Column<double>(type: "float", nullable: false),
                    LocationLon = table.Column<double>(type: "float", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PriceNegotiable = table.Column<bool>(type: "bit", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: true),
                    PricePerUnit = table.Column<int>(type: "int", nullable: true),
                    Rent = table.Column<double>(type: "float", nullable: true),
                    RentDuration = table.Column<int>(type: "int", nullable: true),
                    Deposit = table.Column<double>(type: "float", nullable: true),
                    PostedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PropertyStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SellerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Properties", x => x.PropertyId);
                    table.ForeignKey(
                        name: "FK_Properties_Users_SellerId",
                        column: x => x.SellerId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    TransactionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    TransactionStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpiresOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SubscriptionTemplateId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_Transactions_SubscriptionTemplates_SubscriptionTemplateId",
                        column: x => x.SubscriptionTemplateId,
                        principalTable: "SubscriptionTemplates",
                        principalColumn: "SubscriptionTemplateId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transactions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserSubscriptions",
                columns: table => new
                {
                    UserSubscriptionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubscribedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiresOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AvailableListingCount = table.Column<int>(type: "int", nullable: false),
                    AvailableSellerViewCount = table.Column<int>(type: "int", nullable: false),
                    Validity = table.Column<int>(type: "int", nullable: false),
                    SubscriptionTemplateId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSubscriptions", x => x.UserSubscriptionId);
                    table.ForeignKey(
                        name: "FK_UserSubscriptions_SubscriptionTemplates_SubscriptionTemplateId",
                        column: x => x.SubscriptionTemplateId,
                        principalTable: "SubscriptionTemplates",
                        principalColumn: "SubscriptionTemplateId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserSubscriptions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.CreateTable(
                name: "CommercialDetails",
                columns: table => new
                {
                    CommercialDetailsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommercialType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FloorCount = table.Column<int>(type: "int", nullable: false),
                    MeasurementUnit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Length = table.Column<double>(type: "float", nullable: false),
                    Width = table.Column<double>(type: "float", nullable: false),
                    Height = table.Column<double>(type: "float", nullable: true),
                    WaterSupply = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Electricity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RestroomCount = table.Column<int>(type: "int", nullable: false),
                    GatedSecurity = table.Column<bool>(type: "bit", nullable: false),
                    CarParking = table.Column<int>(type: "int", nullable: false),
                    PropertyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommercialDetails", x => x.CommercialDetailsId);
                    table.ForeignKey(
                        name: "FK_CommercialDetails_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "PropertyId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HostelDetails",
                columns: table => new
                {
                    HostelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypesOfRooms = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GenderPreference = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Food = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Wifi = table.Column<bool>(type: "bit", nullable: false),
                    GatedSecurity = table.Column<bool>(type: "bit", nullable: false),
                    PropertyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HostelDetails", x => x.HostelId);
                    table.ForeignKey(
                        name: "FK_HostelDetails_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "PropertyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HouseDetails",
                columns: table => new
                {
                    HouseDetailsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MeasurementUnit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Length = table.Column<double>(type: "float", nullable: false),
                    Width = table.Column<double>(type: "float", nullable: false),
                    Height = table.Column<double>(type: "float", nullable: true),
                    FloorCount = table.Column<int>(type: "int", nullable: false),
                    RoomCount = table.Column<int>(type: "int", nullable: false),
                    HallAndKitchenAvailable = table.Column<bool>(type: "bit", nullable: false),
                    RestroomCount = table.Column<int>(type: "int", nullable: false),
                    WaterSupply = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Electricity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GatedSecurity = table.Column<bool>(type: "bit", nullable: false),
                    CarParking = table.Column<int>(type: "int", nullable: false),
                    FurnishingDetails = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PropertyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HouseDetails", x => x.HouseDetailsId);
                    table.ForeignKey(
                        name: "FK_HouseDetails_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "PropertyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LandDetails",
                columns: table => new
                {
                    LandDetailsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MeasurementUnit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Length = table.Column<double>(type: "float", nullable: false),
                    Width = table.Column<double>(type: "float", nullable: false),
                    ZoningType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PropertyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LandDetails", x => x.LandDetailsId);
                    table.ForeignKey(
                        name: "FK_LandDetails_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "PropertyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductDetails",
                columns: table => new
                {
                    ProductDetailsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Manufacturer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WarrantyPeriod = table.Column<int>(type: "int", nullable: true),
                    WarrantyUnit = table.Column<int>(type: "int", nullable: true),
                    PropertyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductDetails", x => x.ProductDetailsId);
                    table.ForeignKey(
                        name: "FK_ProductDetails_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "PropertyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PropertyFiles",
                columns: table => new
                {
                    FileId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileSize = table.Column<long>(type: "bigint", nullable: false),
                    PropertyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyFiles", x => x.FileId);
                    table.ForeignKey(
                        name: "FK_PropertyFiles_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "PropertyId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PropertyTag",
                columns: table => new
                {
                    PropertiesPropertyId = table.Column<int>(type: "int", nullable: false),
                    TagsTagValue = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyTag", x => new { x.PropertiesPropertyId, x.TagsTagValue });
                    table.ForeignKey(
                        name: "FK_PropertyTag_Properties_PropertiesPropertyId",
                        column: x => x.PropertiesPropertyId,
                        principalTable: "Properties",
                        principalColumn: "PropertyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PropertyTag_Tags_TagsTagValue",
                        column: x => x.TagsTagValue,
                        principalTable: "Tags",
                        principalColumn: "TagValue",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommercialDetails_PropertyId",
                table: "CommercialDetails",
                column: "PropertyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ConversationChat_ConversationId",
                table: "ConversationChat",
                column: "ConversationId");

            migrationBuilder.CreateIndex(
                name: "IX_Conversations_UserId",
                table: "Conversations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_HostelDetails_PropertyId",
                table: "HostelDetails",
                column: "PropertyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HouseDetails_PropertyId",
                table: "HouseDetails",
                column: "PropertyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LandDetails_PropertyId",
                table: "LandDetails",
                column: "PropertyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductDetails_PropertyId",
                table: "ProductDetails",
                column: "PropertyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Properties_SellerId",
                table: "Properties",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyFiles_PropertyId",
                table: "PropertyFiles",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyTag_TagsTagValue",
                table: "PropertyTag",
                column: "TagsTagValue");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_SubscriptionTemplateId",
                table: "Transactions",
                column: "SubscriptionTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_UserId",
                table: "Transactions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSubscriptions_SubscriptionTemplateId",
                table: "UserSubscriptions",
                column: "SubscriptionTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSubscriptions_UserId",
                table: "UserSubscriptions",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommercialDetails");

            migrationBuilder.DropTable(
                name: "ConversationChat");

            migrationBuilder.DropTable(
                name: "HostelDetails");

            migrationBuilder.DropTable(
                name: "HouseDetails");

            migrationBuilder.DropTable(
                name: "LandDetails");

            migrationBuilder.DropTable(
                name: "ProductDetails");

            migrationBuilder.DropTable(
                name: "PropertyFiles");

            migrationBuilder.DropTable(
                name: "PropertyTag");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "UserSubscriptions");

            migrationBuilder.DropTable(
                name: "Chats");

            migrationBuilder.DropTable(
                name: "Conversations");

            migrationBuilder.DropTable(
                name: "Properties");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "SubscriptionTemplates");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
