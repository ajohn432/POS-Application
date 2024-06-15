using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POS_Application.Server.Migrations
{
    /// <inheritdoc />
    public partial class Billv5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredient_BillItem_IngredientId",
                table: "Ingredient");

            migrationBuilder.AddColumn<string>(
                name: "ItemId",
                table: "Ingredient",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredient_ItemId",
                table: "Ingredient",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredient_BillItem_ItemId",
                table: "Ingredient",
                column: "ItemId",
                principalTable: "BillItem",
                principalColumn: "ItemId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredient_BillItem_ItemId",
                table: "Ingredient");

            migrationBuilder.DropIndex(
                name: "IX_Ingredient_ItemId",
                table: "Ingredient");

            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "Ingredient");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredient_BillItem_IngredientId",
                table: "Ingredient",
                column: "IngredientId",
                principalTable: "BillItem",
                principalColumn: "ItemId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
