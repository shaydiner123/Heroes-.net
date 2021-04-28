using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Md_exercise.Migrations
{
    public partial class AddSuitColorsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SuitColors",
                table: "Heroes");

            migrationBuilder.CreateTable(
                name: "SuitCilors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuitCilors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HeroSuitColor",
                columns: table => new
                {
                    HeroesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SuitColorsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeroSuitColor", x => new { x.HeroesId, x.SuitColorsId });
                    table.ForeignKey(
                        name: "FK_HeroSuitColor_Heroes_HeroesId",
                        column: x => x.HeroesId,
                        principalTable: "Heroes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HeroSuitColor_SuitCilors_SuitColorsId",
                        column: x => x.SuitColorsId,
                        principalTable: "SuitCilors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HeroSuitColor_SuitColorsId",
                table: "HeroSuitColor",
                column: "SuitColorsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HeroSuitColor");

            migrationBuilder.DropTable(
                name: "SuitCilors");

            migrationBuilder.AddColumn<string>(
                name: "SuitColors",
                table: "Heroes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
