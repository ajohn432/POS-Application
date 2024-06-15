using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POS_Application.Server.Migrations
{
    /// <inheritdoc />
    public partial class Billv8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillLinkedIngredients_BaseBillItems_ItemId",
                table: "BillLinkedIngredients");

            migrationBuilder.DropForeignKey(
                name: "FK_BillLinkedIngredients_BillLinkedBillItems_IngredientId",
                table: "BillLinkedIngredients");

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

            migrationBuilder.CreateIndex(
                name: "IX_BillItemLinkedIngredients_ItemId",
                table: "BillItemLinkedIngredients",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_BillLinkedIngredients_BillLinkedBillItems_ItemId",
                table: "BillLinkedIngredients",
                column: "ItemId",
                principalTable: "BillLinkedBillItems",
                principalColumn: "ItemId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillLinkedIngredients_BillLinkedBillItems_ItemId",
                table: "BillLinkedIngredients");

            migrationBuilder.DropTable(
                name: "BillItemLinkedIngredients");

            migrationBuilder.AddForeignKey(
                name: "FK_BillLinkedIngredients_BaseBillItems_ItemId",
                table: "BillLinkedIngredients",
                column: "ItemId",
                principalTable: "BaseBillItems",
                principalColumn: "ItemId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BillLinkedIngredients_BillLinkedBillItems_IngredientId",
                table: "BillLinkedIngredients",
                column: "IngredientId",
                principalTable: "BillLinkedBillItems",
                principalColumn: "ItemId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
