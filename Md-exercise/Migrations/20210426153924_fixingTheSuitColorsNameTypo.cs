using Microsoft.EntityFrameworkCore.Migrations;

namespace Md_exercise.Migrations
{
    public partial class fixingTheSuitColorsNameTypo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HeroSuitColor_SuitCilors_SuitColorsId",
                table: "HeroSuitColor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SuitCilors",
                table: "SuitCilors");

            migrationBuilder.RenameTable(
                name: "SuitCilors",
                newName: "SuitColors");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SuitColors",
                table: "SuitColors",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HeroSuitColor_SuitColors_SuitColorsId",
                table: "HeroSuitColor",
                column: "SuitColorsId",
                principalTable: "SuitColors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HeroSuitColor_SuitColors_SuitColorsId",
                table: "HeroSuitColor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SuitColors",
                table: "SuitColors");

            migrationBuilder.RenameTable(
                name: "SuitColors",
                newName: "SuitCilors");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SuitCilors",
                table: "SuitCilors",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HeroSuitColor_SuitCilors_SuitColorsId",
                table: "HeroSuitColor",
                column: "SuitColorsId",
                principalTable: "SuitCilors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
