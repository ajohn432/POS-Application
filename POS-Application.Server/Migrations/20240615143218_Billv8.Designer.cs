﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using POS_Application.Server.db;

#nullable disable

namespace POS_Application.Server.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240615143218_Billv8")]
    partial class Billv8
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("POS_Application.Server.Models.Bill", b =>
                {
                    b.Property<string>("BillId")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<decimal>("TipAmount")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("BillId");

                    b.HasIndex("BillId")
                        .IsUnique();

                    b.ToTable("Bills");
                });

            modelBuilder.Entity("POS_Application.Server.Models.BillItem", b =>
                {
                    b.Property<string>("ItemId")
                        .HasColumnType("varchar(255)");

                    b.Property<decimal>("BasePrice")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<bool>("IsInStock")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("ItemName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("ItemId");

                    b.ToTable("BaseBillItems");
                });

            modelBuilder.Entity("POS_Application.Server.Models.BillItemLinkedIngredient", b =>
                {
                    b.Property<string>("IngredientId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ItemId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(10, 2)");

                    b.HasKey("IngredientId");

                    b.HasIndex("ItemId");

                    b.ToTable("BillItemLinkedIngredients", (string)null);
                });

            modelBuilder.Entity("POS_Application.Server.Models.Discount", b =>
                {
                    b.Property<string>("DiscountCode")
                        .HasColumnType("varchar(255)");

                    b.Property<decimal>("DiscountPercentage")
                        .HasColumnType("decimal(5, 2)");

                    b.HasKey("DiscountCode");

                    b.ToTable("BaseDiscounts");
                });

            modelBuilder.Entity("POS_Application.Server.Models.Ingredient", b =>
                {
                    b.Property<string>("IngredientId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(10, 2)");

                    b.HasKey("IngredientId");

                    b.ToTable("BaseIngredients");
                });

            modelBuilder.Entity("POS_Application.Server.Models.LinkedBillItem", b =>
                {
                    b.Property<string>("ItemId")
                        .HasColumnType("varchar(255)");

                    b.Property<decimal>("BasePrice")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<string>("BillId")
                        .IsRequired()
                        .HasColumnType("varchar(10)");

                    b.Property<bool>("IsInStock")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("ItemName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("ItemId");

                    b.HasIndex("BillId");

                    b.ToTable("BillLinkedBillItems", (string)null);
                });

            modelBuilder.Entity("POS_Application.Server.Models.LinkedDiscount", b =>
                {
                    b.Property<string>("DiscountCode")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("BillId")
                        .IsRequired()
                        .HasColumnType("varchar(10)");

                    b.Property<decimal>("DiscountPercentage")
                        .HasColumnType("decimal(5, 2)");

                    b.HasKey("DiscountCode");

                    b.HasIndex("BillId");

                    b.ToTable("BillLinkedDiscounts", (string)null);
                });

            modelBuilder.Entity("POS_Application.Server.Models.LinkedIngredient", b =>
                {
                    b.Property<string>("IngredientId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ItemId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(10, 2)");

                    b.HasKey("IngredientId");

                    b.HasIndex("ItemId");

                    b.ToTable("BillLinkedIngredients", (string)null);
                });

            modelBuilder.Entity("POS_Application.Server.Models.TokenInfo", b =>
                {
                    b.Property<string>("Token")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<string>("EmployeeId")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<bool>("IsValid")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Token");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("Token")
                        .IsUnique();

                    b.ToTable("Tokens", (string)null);
                });

            modelBuilder.Entity("POS_Application.Server.Models.User", b =>
                {
                    b.Property<string>("EmployeeId")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("EmployeeId");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("Password");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("Role");

                    b.HasKey("EmployeeId");

                    b.HasIndex("EmployeeId")
                        .IsUnique();

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("POS_Application.Server.Models.BillItemLinkedIngredient", b =>
                {
                    b.HasOne("POS_Application.Server.Models.BillItem", "BillItem")
                        .WithMany("Ingredients")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BillItem");
                });

            modelBuilder.Entity("POS_Application.Server.Models.LinkedBillItem", b =>
                {
                    b.HasOne("POS_Application.Server.Models.Bill", "Bill")
                        .WithMany("Items")
                        .HasForeignKey("BillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bill");
                });

            modelBuilder.Entity("POS_Application.Server.Models.LinkedDiscount", b =>
                {
                    b.HasOne("POS_Application.Server.Models.Bill", "Bill")
                        .WithMany("Discounts")
                        .HasForeignKey("BillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bill");
                });

            modelBuilder.Entity("POS_Application.Server.Models.LinkedIngredient", b =>
                {
                    b.HasOne("POS_Application.Server.Models.LinkedBillItem", "BillItem")
                        .WithMany("Ingredients")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BillItem");
                });

            modelBuilder.Entity("POS_Application.Server.Models.TokenInfo", b =>
                {
                    b.HasOne("POS_Application.Server.Models.User", null)
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("POS_Application.Server.Models.Bill", b =>
                {
                    b.Navigation("Discounts");

                    b.Navigation("Items");
                });

            modelBuilder.Entity("POS_Application.Server.Models.BillItem", b =>
                {
                    b.Navigation("Ingredients");
                });

            modelBuilder.Entity("POS_Application.Server.Models.LinkedBillItem", b =>
                {
                    b.Navigation("Ingredients");
                });
#pragma warning restore 612, 618
        }
    }
}