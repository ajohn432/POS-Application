using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POS_Application.Server.Migrations
{
    /// <inheritdoc />
    public partial class Billv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillItems_Bills_BillId",
                table: "BillItems");

            migrationBuilder.DropForeignKey(
                name: "FK_BillItems_Bills_BillId1",
                table: "BillItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Ingredient_BillItems_BillItemItemId",
                table: "Ingredient");

            migrationBuilder.DropIndex(
                name: "IX_Ingredient_BillItemItemId",
                table: "Ingredient");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BillItems",
                table: "BillItems");

            migrationBuilder.DropIndex(
                name: "IX_BillItems_BillId1",
                table: "BillItems");

            migrationBuilder.DropIndex(
                name: "IX_BillItems_ItemId",
                table: "BillItems");

            migrationBuilder.DropColumn(
                name: "BillItemItemId",
                table: "Ingredient");

            migrationBuilder.DropColumn(
                name: "BillId1",
                table: "BillItems");

            migrationBuilder.RenameTable(
                name: "BillItems",
                newName: "BillItem");

            migrationBuilder.RenameIndex(
                name: "IX_BillItems_BillId",
                table: "BillItem",
                newName: "IX_BillItem_BillId");

            migrationBuilder.AlterColumn<decimal>(
                name: "DiscountPercentage",
                table: "Discount",
                type: "decimal(5,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");

            migrationBuilder.AlterColumn<string>(
                name: "DiscountCode",
                table: "Discount",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<decimal>(
                name: "BasePrice",
                table: "BillItem",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");

            migrationBuilder.AlterColumn<string>(
                name: "ItemId",
                table: "BillItem",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldMaxLength: 10)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

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
                name: "FK_Ingredient_BillItem_IngredientId",
                table: "Ingredient",
                column: "IngredientId",
                principalTable: "BillItem",
                principalColumn: "ItemId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillItem_Bills_BillId",
                table: "BillItem");

            migrationBuilder.DropForeignKey(
                name: "FK_Ingredient_BillItem_IngredientId",
                table: "Ingredient");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BillItem",
                table: "BillItem");

            migrationBuilder.RenameTable(
                name: "BillItem",
                newName: "BillItems");

            migrationBuilder.RenameIndex(
                name: "IX_BillItem_BillId",
                table: "BillItems",
                newName: "IX_BillItems_BillId");

            migrationBuilder.AddColumn<string>(
                name: "BillItemItemId",
                table: "Ingredient",
                type: "varchar(10)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<decimal>(
                name: "DiscountPercentage",
                table: "Discount",
                type: "decimal(65,30)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,2)");

            migrationBuilder.AlterColumn<string>(
                name: "DiscountCode",
                table: "Discount",
                type: "varchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<decimal>(
                name: "BasePrice",
                table: "BillItems",
                type: "decimal(65,30)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "ItemId",
                table: "BillItems",
                type: "varchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "BillId1",
                table: "BillItems",
                type: "varchar(10)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BillItems",
                table: "BillItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredient_BillItemItemId",
                table: "Ingredient",
                column: "BillItemItemId");

            migrationBuilder.CreateIndex(
                name: "IX_BillItems_BillId1",
                table: "BillItems",
                column: "BillId1");

            migrationBuilder.CreateIndex(
                name: "IX_BillItems_ItemId",
                table: "BillItems",
                column: "ItemId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BillItems_Bills_BillId",
                table: "BillItems",
                column: "BillId",
                principalTable: "Bills",
                principalColumn: "BillId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BillItems_Bills_BillId1",
                table: "BillItems",
                column: "BillId1",
                principalTable: "Bills",
                principalColumn: "BillId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredient_BillItems_BillItemItemId",
                table: "Ingredient",
                column: "BillItemItemId",
                principalTable: "BillItems",
                principalColumn: "ItemId");
        }
    }
}
