﻿// <auto-generated />
using System;
using Brokerless.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Brokerless.Migrations
{
    [DbContext(typeof(BrokerlessDBContext))]
    [Migration("20240726164913_conversationUpdated3")]
    partial class conversationUpdated3
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Brokerless.Models.Chat", b =>
                {
                    b.Property<int>("ChatId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ChatId"));

                    b.Property<int>("ConversationId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ChatId");

                    b.HasIndex("ConversationId");

                    b.ToTable("Chats");
                });

            modelBuilder.Entity("Brokerless.Models.CommercialDetails", b =>
                {
                    b.Property<int>("CommercialDetailsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CommercialDetailsId"));

                    b.Property<int>("CarParking")
                        .HasColumnType("int");

                    b.Property<string>("CommercialType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Electricity")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FloorCount")
                        .HasColumnType("int");

                    b.Property<bool>("GatedSecurity")
                        .HasColumnType("bit");

                    b.Property<double?>("Height")
                        .HasColumnType("float");

                    b.Property<double>("Length")
                        .HasColumnType("float");

                    b.Property<string>("MeasurementUnit")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PropertyId")
                        .HasColumnType("int");

                    b.Property<int>("RestroomCount")
                        .HasColumnType("int");

                    b.Property<string>("WaterSupply")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Width")
                        .HasColumnType("float");

                    b.HasKey("CommercialDetailsId");

                    b.HasIndex("PropertyId")
                        .IsUnique();

                    b.ToTable("CommercialDetails");
                });

            modelBuilder.Entity("Brokerless.Models.Conversation", b =>
                {
                    b.Property<int>("ConversationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ConversationId"));

                    b.Property<bool>("HasUnreadMessage")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastUpdatedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("ConversationId");

                    b.ToTable("Conversations");
                });

            modelBuilder.Entity("Brokerless.Models.HostelDetails", b =>
                {
                    b.Property<int>("HostelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("HostelId"));

                    b.Property<string>("Food")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("GatedSecurity")
                        .HasColumnType("bit");

                    b.Property<string>("GenderPreference")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PropertyId")
                        .HasColumnType("int");

                    b.Property<string>("TypesOfRooms")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Wifi")
                        .HasColumnType("bit");

                    b.HasKey("HostelId");

                    b.HasIndex("PropertyId")
                        .IsUnique();

                    b.ToTable("HostelDetails");
                });

            modelBuilder.Entity("Brokerless.Models.HouseDetails", b =>
                {
                    b.Property<int>("HouseDetailsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("HouseDetailsId"));

                    b.Property<bool>("CarParking")
                        .HasColumnType("bit");

                    b.Property<string>("Electricity")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FloorCount")
                        .HasColumnType("int");

                    b.Property<string>("FurnishingDetails")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("GatedSecurity")
                        .HasColumnType("bit");

                    b.Property<bool>("HallAndKitchenAvailable")
                        .HasColumnType("bit");

                    b.Property<double?>("Height")
                        .HasColumnType("float");

                    b.Property<double>("Length")
                        .HasColumnType("float");

                    b.Property<string>("MeasurementUnit")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PropertyId")
                        .HasColumnType("int");

                    b.Property<int>("RestroomCount")
                        .HasColumnType("int");

                    b.Property<int>("RoomCount")
                        .HasColumnType("int");

                    b.Property<string>("WaterSupply")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Width")
                        .HasColumnType("float");

                    b.HasKey("HouseDetailsId");

                    b.HasIndex("PropertyId")
                        .IsUnique();

                    b.ToTable("HouseDetails");
                });

            modelBuilder.Entity("Brokerless.Models.LandDetails", b =>
                {
                    b.Property<int>("LandDetailsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LandDetailsId"));

                    b.Property<double>("Length")
                        .HasColumnType("float");

                    b.Property<string>("MeasurementUnit")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PropertyId")
                        .HasColumnType("int");

                    b.Property<double>("Width")
                        .HasColumnType("float");

                    b.Property<string>("ZoningType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LandDetailsId");

                    b.HasIndex("PropertyId")
                        .IsUnique();

                    b.ToTable("LandDetails");
                });

            modelBuilder.Entity("Brokerless.Models.ProductDetails", b =>
                {
                    b.Property<int>("ProductDetailsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductDetailsId"));

                    b.Property<string>("Manufacturer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PropertyId")
                        .HasColumnType("int");

                    b.Property<int?>("WarrantyPeriod")
                        .HasColumnType("int");

                    b.Property<int?>("WarrantyUnit")
                        .HasColumnType("int");

                    b.HasKey("ProductDetailsId");

                    b.HasIndex("PropertyId")
                        .IsUnique();

                    b.ToTable("ProductDetails");
                });

            modelBuilder.Entity("Brokerless.Models.Property", b =>
                {
                    b.Property<int>("PropertyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PropertyId"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("Deposit")
                        .HasColumnType("float");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ListingType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("LocationLat")
                        .HasColumnType("float");

                    b.Property<double>("LocationLon")
                        .HasColumnType("float");

                    b.Property<DateTime>("PostedOn")
                        .HasColumnType("datetime2");

                    b.Property<double?>("Price")
                        .HasColumnType("float");

                    b.Property<bool>("PriceNegotiable")
                        .HasColumnType("bit");

                    b.Property<int?>("PricePerUnit")
                        .HasColumnType("int");

                    b.Property<int?>("PropertyCategory")
                        .HasColumnType("int");

                    b.Property<string>("PropertyStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PropertyType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("Rent")
                        .HasColumnType("float");

                    b.Property<int?>("RentDuration")
                        .HasColumnType("int");

                    b.Property<int>("SellerId")
                        .HasColumnType("int");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isApproved")
                        .HasColumnType("bit");

                    b.HasKey("PropertyId");

                    b.HasIndex("SellerId");

                    b.ToTable("Properties");
                });

            modelBuilder.Entity("Brokerless.Models.PropertyFile", b =>
                {
                    b.Property<int>("FileId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FileId"));

                    b.Property<long>("FileSize")
                        .HasColumnType("bigint");

                    b.Property<string>("FileType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PropertyId")
                        .HasColumnType("int");

                    b.HasKey("FileId");

                    b.HasIndex("PropertyId");

                    b.ToTable("PropertyFiles");
                });

            modelBuilder.Entity("Brokerless.Models.PropertyUserViewed", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("PropertyId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("UserId", "PropertyId");

                    b.HasIndex("PropertyId");

                    b.ToTable("PropertyUserViewed");
                });

            modelBuilder.Entity("Brokerless.Models.SubscriptionTemplate", b =>
                {
                    b.Property<string>("SubsriptionName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("Currency")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MaxListingCount")
                        .HasColumnType("int");

                    b.Property<int>("MaxSellerViewCount")
                        .HasColumnType("int");

                    b.Property<double?>("Price")
                        .HasColumnType("float");

                    b.Property<int?>("Validity")
                        .HasColumnType("int");

                    b.HasKey("SubsriptionName");

                    b.ToTable("SubscriptionTemplates");

                    b.HasData(
                        new
                        {
                            SubsriptionName = "Free",
                            Description = "This subsctiption is default for user.",
                            MaxListingCount = 1,
                            MaxSellerViewCount = 1
                        },
                        new
                        {
                            SubsriptionName = "Silver",
                            Currency = 0,
                            Description = "This subsription is suitable for user who wants a basic limits",
                            MaxListingCount = 10,
                            MaxSellerViewCount = 10,
                            Price = 499.0,
                            Validity = 28
                        },
                        new
                        {
                            SubsriptionName = "Gold",
                            Currency = 0,
                            Description = "This subsription is suitable for user who wants to post, view frequently",
                            MaxListingCount = 50,
                            MaxSellerViewCount = 50,
                            Price = 999.0,
                            Validity = 28
                        });
                });

            modelBuilder.Entity("Brokerless.Models.Tag", b =>
                {
                    b.Property<string>("TagValue")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("TagValue");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("Brokerless.Models.Transaction", b =>
                {
                    b.Property<string>("TransactionId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ExpiresOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("SubscriptionTemplateName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("TransactionStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("TransactionId");

                    b.HasIndex("SubscriptionTemplateName");

                    b.HasIndex("UserId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("Brokerless.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("CountryCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberVerified")
                        .HasColumnType("bit");

                    b.Property<string>("ProfileUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserRole")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Brokerless.Models.UserSubscription", b =>
                {
                    b.Property<int>("UserSubscriptionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserSubscriptionId"));

                    b.Property<int>("AvailableListingCount")
                        .HasColumnType("int");

                    b.Property<int>("AvailableSellerViewCount")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ExpiresOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("SubscribedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("SubscriptionTemplateName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("UserSubscriptionId");

                    b.HasIndex("SubscriptionTemplateName");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("UserSubscriptions");
                });

            modelBuilder.Entity("ConversationUser", b =>
                {
                    b.Property<int>("ConversationsConversationId")
                        .HasColumnType("int");

                    b.Property<int>("UsersUserId")
                        .HasColumnType("int");

                    b.HasKey("ConversationsConversationId", "UsersUserId");

                    b.HasIndex("UsersUserId");

                    b.ToTable("UserConversation", (string)null);
                });

            modelBuilder.Entity("PropertyTag", b =>
                {
                    b.Property<int>("PropertiesPropertyId")
                        .HasColumnType("int");

                    b.Property<string>("TagsTagValue")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("PropertiesPropertyId", "TagsTagValue");

                    b.HasIndex("TagsTagValue");

                    b.ToTable("PropertyTag", (string)null);
                });

            modelBuilder.Entity("Brokerless.Models.Chat", b =>
                {
                    b.HasOne("Brokerless.Models.Conversation", "Conversation")
                        .WithMany("Chats")
                        .HasForeignKey("ConversationId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Conversation");
                });

            modelBuilder.Entity("Brokerless.Models.CommercialDetails", b =>
                {
                    b.HasOne("Brokerless.Models.Property", "Property")
                        .WithOne("CommercialDetails")
                        .HasForeignKey("Brokerless.Models.CommercialDetails", "PropertyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Property");
                });

            modelBuilder.Entity("Brokerless.Models.HostelDetails", b =>
                {
                    b.HasOne("Brokerless.Models.Property", "Property")
                        .WithOne("HostelDetails")
                        .HasForeignKey("Brokerless.Models.HostelDetails", "PropertyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Property");
                });

            modelBuilder.Entity("Brokerless.Models.HouseDetails", b =>
                {
                    b.HasOne("Brokerless.Models.Property", "Property")
                        .WithOne("HouseDetails")
                        .HasForeignKey("Brokerless.Models.HouseDetails", "PropertyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Property");
                });

            modelBuilder.Entity("Brokerless.Models.LandDetails", b =>
                {
                    b.HasOne("Brokerless.Models.Property", "Property")
                        .WithOne("LandDetails")
                        .HasForeignKey("Brokerless.Models.LandDetails", "PropertyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Property");
                });

            modelBuilder.Entity("Brokerless.Models.ProductDetails", b =>
                {
                    b.HasOne("Brokerless.Models.Property", "Property")
                        .WithOne("ProductDetails")
                        .HasForeignKey("Brokerless.Models.ProductDetails", "PropertyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Property");
                });

            modelBuilder.Entity("Brokerless.Models.Property", b =>
                {
                    b.HasOne("Brokerless.Models.User", "Seller")
                        .WithMany("Listings")
                        .HasForeignKey("SellerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Seller");
                });

            modelBuilder.Entity("Brokerless.Models.PropertyFile", b =>
                {
                    b.HasOne("Brokerless.Models.Property", "Property")
                        .WithMany("Files")
                        .HasForeignKey("PropertyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Property");
                });

            modelBuilder.Entity("Brokerless.Models.PropertyUserViewed", b =>
                {
                    b.HasOne("Brokerless.Models.Property", "Property")
                        .WithMany("UsersViewed")
                        .HasForeignKey("PropertyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Brokerless.Models.User", "User")
                        .WithMany("PropertiesViewed")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Property");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Brokerless.Models.Transaction", b =>
                {
                    b.HasOne("Brokerless.Models.SubscriptionTemplate", "SubscriptionTemplate")
                        .WithMany()
                        .HasForeignKey("SubscriptionTemplateName")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Brokerless.Models.User", "User")
                        .WithMany("Transactions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("SubscriptionTemplate");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Brokerless.Models.UserSubscription", b =>
                {
                    b.HasOne("Brokerless.Models.SubscriptionTemplate", "SubscriptionTemplate")
                        .WithMany()
                        .HasForeignKey("SubscriptionTemplateName")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Brokerless.Models.User", "User")
                        .WithOne("UserSubscription")
                        .HasForeignKey("Brokerless.Models.UserSubscription", "UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("SubscriptionTemplate");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ConversationUser", b =>
                {
                    b.HasOne("Brokerless.Models.Conversation", null)
                        .WithMany()
                        .HasForeignKey("ConversationsConversationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Brokerless.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UsersUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PropertyTag", b =>
                {
                    b.HasOne("Brokerless.Models.Property", null)
                        .WithMany()
                        .HasForeignKey("PropertiesPropertyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Brokerless.Models.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsTagValue")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Brokerless.Models.Conversation", b =>
                {
                    b.Navigation("Chats");
                });

            modelBuilder.Entity("Brokerless.Models.Property", b =>
                {
                    b.Navigation("CommercialDetails")
                        .IsRequired();

                    b.Navigation("Files");

                    b.Navigation("HostelDetails")
                        .IsRequired();

                    b.Navigation("HouseDetails")
                        .IsRequired();

                    b.Navigation("LandDetails")
                        .IsRequired();

                    b.Navigation("ProductDetails")
                        .IsRequired();

                    b.Navigation("UsersViewed");
                });

            modelBuilder.Entity("Brokerless.Models.User", b =>
                {
                    b.Navigation("Listings");

                    b.Navigation("PropertiesViewed");

                    b.Navigation("Transactions");

                    b.Navigation("UserSubscription")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
