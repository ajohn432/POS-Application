using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POS_Application.Server.Migrations
{
    /// <inheritdoc />
    public partial class Billv10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BaseBillItems",
                columns: table => new
                {
                    ItemId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ItemName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    BasePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsInStock = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseBillItems", x => x.ItemId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BaseDiscounts",
                columns: table => new
                {
                    DiscountId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DiscountCode = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DiscountPercentage = table.Column<decimal>(type: "decimal(5,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseDiscounts", x => x.DiscountId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BaseIngredients",
                columns: table => new
                {
                    IngredientId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseIngredients", x => x.IngredientId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Bills",
                columns: table => new
                {
                    BillId = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CustomerName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TipAmount = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bills", x => x.BillId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BillItemLinkedIngredients",
                columns: table => new
                {
                    IngredientId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    ItemId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillItemLinkedIngredients", x => x.IngredientId);
                    table.ForeignKey(
                        name: "FK_BillItemLinkedIngredients_BaseBillItems_ItemId",
                        column: x => x.ItemId,
                        principalTable: "BaseBillItems",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BillLinkedBillItems",
                columns: table => new
                {
                    ItemId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ItemName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    BasePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsInStock = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    BillId = table.Column<string>(type: "varchar(10)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillLinkedBillItems", x => x.ItemId);
                    table.ForeignKey(
                        name: "FK_BillLinkedBillItems_Bills_BillId",
                        column: x => x.BillId,
                        principalTable: "Bills",
                        principalColumn: "BillId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BillLinkedDiscounts",
                columns: table => new
                {
                    DiscountId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DiscountCode = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DiscountPercentage = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    BillId = table.Column<string>(type: "varchar(10)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillLinkedDiscounts", x => x.DiscountId);
                    table.ForeignKey(
                        name: "FK_BillLinkedDiscounts_Bills_BillId",
                        column: x => x.BillId,
                        principalTable: "Bills",
                        principalColumn: "BillId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BillLinkedIngredients",
                columns: table => new
                {
                    IngredientId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    ItemId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillLinkedIngredients", x => x.IngredientId);
                    table.ForeignKey(
                        name: "FK_BillLinkedIngredients_BillLinkedBillItems_ItemId",
                        column: x => x.ItemId,
                        principalTable: "BillLinkedBillItems",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_BillItemLinkedIngredients_ItemId",
                table: "BillItemLinkedIngredients",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_BillLinkedBillItems_BillId",
                table: "BillLinkedBillItems",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_BillLinkedDiscounts_BillId",
                table: "BillLinkedDiscounts",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_BillLinkedIngredients_ItemId",
                table: "BillLinkedIngredients",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_BillId",
                table: "Bills",
                column: "BillId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BaseDiscounts");

            migrationBuilder.DropTable(
                name: "BaseIngredients");

            migrationBuilder.DropTable(
                name: "BillItemLinkedIngredients");

            migrationBuilder.DropTable(
                name: "BillLinkedDiscounts");

            migrationBuilder.DropTable(
                name: "BillLinkedIngredients");

            migrationBuilder.DropTable(
                name: "BaseBillItems");

            migrationBuilder.DropTable(
                name: "BillLinkedBillItems");

            migrationBuilder.DropTable(
                name: "Bills");
        }
    }
}
