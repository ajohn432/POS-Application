using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POS_Application.Server.Migrations
{
    /// <inheritdoc />
    public partial class Billv7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseBillItems_Bills_BillId",
                table: "BaseBillItems");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseDiscounts_Bills_BillId",
                table: "BaseDiscounts");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseIngredients_BaseBillItems_ItemId",
                table: "BaseIngredients");

            migrationBuilder.DropIndex(
                name: "IX_BaseIngredients_ItemId",
                table: "BaseIngredients");

            migrationBuilder.DropIndex(
                name: "IX_BaseDiscounts_BillId",
                table: "BaseDiscounts");

            migrationBuilder.DropIndex(
                name: "IX_BaseBillItems_BillId",
                table: "BaseBillItems");

            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "BaseIngredients");

            migrationBuilder.DropColumn(
                name: "BillId",
                table: "BaseDiscounts");

            migrationBuilder.DropColumn(
                name: "BillId",
                table: "BaseBillItems");

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
                    DiscountCode = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DiscountPercentage = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    BillId = table.Column<string>(type: "varchar(10)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillLinkedDiscounts", x => x.DiscountCode);
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
                        name: "FK_BillLinkedIngredients_BaseBillItems_ItemId",
                        column: x => x.ItemId,
                        principalTable: "BaseBillItems",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BillLinkedIngredients_BillLinkedBillItems_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "BillLinkedBillItems",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BillLinkedDiscounts");

            migrationBuilder.DropTable(
                name: "BillLinkedIngredients");

            migrationBuilder.DropTable(
                name: "BillLinkedBillItems");

            migrationBuilder.AddColumn<string>(
                name: "ItemId",
                table: "BaseIngredients",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "BillId",
                table: "BaseDiscounts",
                type: "varchar(10)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "BillId",
                table: "BaseBillItems",
                type: "varchar(10)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_BaseIngredients_ItemId",
                table: "BaseIngredients",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseDiscounts_BillId",
                table: "BaseDiscounts",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseBillItems_BillId",
                table: "BaseBillItems",
                column: "BillId");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseBillItems_Bills_BillId",
                table: "BaseBillItems",
                column: "BillId",
                principalTable: "Bills",
                principalColumn: "BillId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BaseDiscounts_Bills_BillId",
                table: "BaseDiscounts",
                column: "BillId",
                principalTable: "Bills",
                principalColumn: "BillId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BaseIngredients_BaseBillItems_ItemId",
                table: "BaseIngredients",
                column: "ItemId",
                principalTable: "BaseBillItems",
                principalColumn: "ItemId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
