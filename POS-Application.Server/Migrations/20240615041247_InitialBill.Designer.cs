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
    [Migration("20240615041247_InitialBill")]
    partial class InitialBill
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
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.Property<decimal>("BasePrice")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("BillId")
                        .IsRequired()
                        .HasColumnType("varchar(10)");

                    b.Property<string>("BillId1")
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

                    b.HasIndex("BillId1");

                    b.HasIndex("ItemId")
                        .IsUnique();

                    b.ToTable("BillItems");
                });

            modelBuilder.Entity("POS_Application.Server.Models.Discount", b =>
                {
                    b.Property<string>("DiscountCode")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("BillId")
                        .HasColumnType("varchar(10)");

                    b.Property<decimal>("DiscountPercentage")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("DiscountCode");

                    b.HasIndex("BillId");

                    b.ToTable("Discount");
                });

            modelBuilder.Entity("POS_Application.Server.Models.Ingredient", b =>
                {
                    b.Property<string>("IngredientId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("BillItemItemId")
                        .HasColumnType("varchar(10)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("IngredientId");

                    b.HasIndex("BillItemItemId");

                    b.ToTable("Ingredient");
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

            modelBuilder.Entity("POS_Application.Server.Models.BillItem", b =>
                {
                    b.HasOne("POS_Application.Server.Models.Bill", null)
                        .WithMany()
                        .HasForeignKey("BillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("POS_Application.Server.Models.Bill", null)
                        .WithMany("Items")
                        .HasForeignKey("BillId1");
                });

            modelBuilder.Entity("POS_Application.Server.Models.Discount", b =>
                {
                    b.HasOne("POS_Application.Server.Models.Bill", null)
                        .WithMany("Discounts")
                        .HasForeignKey("BillId");
                });

            modelBuilder.Entity("POS_Application.Server.Models.Ingredient", b =>
                {
                    b.HasOne("POS_Application.Server.Models.BillItem", null)
                        .WithMany("Ingredients")
                        .HasForeignKey("BillItemItemId");
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
#pragma warning restore 612, 618
        }
    }
}
