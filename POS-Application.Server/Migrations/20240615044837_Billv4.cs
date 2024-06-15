using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POS_Application.Server.Migrations
{
    /// <inheritdoc />
    public partial class Billv4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Discount_Bills_BillId",
                table: "Discount");

            migrationBuilder.UpdateData(
                table: "Discount",
                keyColumn: "BillId",
                keyValue: null,
                column: "BillId",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "BillId",
                table: "Discount",
                type: "varchar(10)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_Discount_Bills_BillId",
                table: "Discount",
                column: "BillId",
                principalTable: "Bills",
                principalColumn: "BillId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Discount_Bills_BillId",
                table: "Discount");

            migrationBuilder.AlterColumn<string>(
                name: "BillId",
                table: "Discount",
                type: "varchar(10)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(10)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_Discount_Bills_BillId",
                table: "Discount",
                column: "BillId",
                principalTable: "Bills",
                principalColumn: "BillId");
        }
    }
}
