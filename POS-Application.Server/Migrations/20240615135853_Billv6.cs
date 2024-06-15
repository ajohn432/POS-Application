using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POS_Application.Server.Migrations
{
    /// <inheritdoc />
    public partial class Billv6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillItem_Bills_BillId",
                table: "BillItem");

            migrationBuilder.DropForeignKey(
                name: "FK_Discount_Bills_BillId",
                table: "Discount");

            migrationBuilder.DropForeignKey(
                name: "FK_Ingredient_BillItem_ItemId",
                table: "Ingredient");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ingredient",
                table: "Ingredient");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Discount",
                table: "Discount");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BillItem",
                table: "BillItem");

            migrationBuilder.RenameTable(
                name: "Ingredient",
                newName: "BaseIngredients");

            migrationBuilder.RenameTable(
                name: "Discount",
                newName: "BaseDiscounts");

            migrationBuilder.RenameTable(
                name: "BillItem",
                newName: "BaseBillItems");

            migrationBuilder.RenameIndex(
                name: "IX_Ingredient_ItemId",
                table: "BaseIngredients",
                newName: "IX_BaseIngredients_ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_Discount_BillId",
                table: "BaseDiscounts",
                newName: "IX_BaseDiscounts_BillId");

            migrationBuilder.RenameIndex(
                name: "IX_BillItem_BillId",
                table: "BaseBillItems",
                newName: "IX_BaseBillItems_BillId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BaseIngredients",
                table: "BaseIngredients",
                column: "IngredientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BaseDiscounts",
                table: "BaseDiscounts",
                column: "DiscountCode");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BaseBillItems",
                table: "BaseBillItems",
                column: "ItemId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropPrimaryKey(
                name: "PK_BaseIngredients",
                table: "BaseIngredients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BaseDiscounts",
                table: "BaseDiscounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BaseBillItems",
                table: "BaseBillItems");

            migrationBuilder.RenameTable(
                name: "BaseIngredients",
                newName: "Ingredient");

            migrationBuilder.RenameTable(
                name: "BaseDiscounts",
                newName: "Discount");

            migrationBuilder.RenameTable(
                name: "BaseBillItems",
                newName: "BillItem");

            migrationBuilder.RenameIndex(
                name: "IX_BaseIngredients_ItemId",
                table: "Ingredient",
                newName: "IX_Ingredient_ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_BaseDiscounts_BillId",
                table: "Discount",
                newName: "IX_Discount_BillId");

            migrationBuilder.RenameIndex(
                name: "IX_BaseBillItems_BillId",
                table: "BillItem",
                newName: "IX_BillItem_BillId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ingredient",
                table: "Ingredient",
                column: "IngredientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Discount",
                table: "Discount",
                column: "DiscountCode");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BillItem",
                table: "BillItem",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_BillItem_Bills_BillId",
                table: "BillItem",
                column: "BillId",
                principalTable: "Bills",
                principalColumn: "BillId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Discount_Bills_BillId",
                table: "Discount",
                column: "BillId",
                principalTable: "Bills",
                principalColumn: "BillId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredient_BillItem_ItemId",
                table: "Ingredient",
                column: "ItemId",
                principalTable: "BillItem",
                principalColumn: "ItemId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
