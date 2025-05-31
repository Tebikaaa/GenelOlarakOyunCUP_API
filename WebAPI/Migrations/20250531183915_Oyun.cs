using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class Oyun : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skorlar_Oyun_OyunId",
                table: "Skorlar");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Oyun",
                table: "Oyun");

            migrationBuilder.RenameTable(
                name: "Oyun",
                newName: "Oyunlar");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Oyunlar",
                table: "Oyunlar",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Skorlar_Oyunlar_OyunId",
                table: "Skorlar",
                column: "OyunId",
                principalTable: "Oyunlar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skorlar_Oyunlar_OyunId",
                table: "Skorlar");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Oyunlar",
                table: "Oyunlar");

            migrationBuilder.RenameTable(
                name: "Oyunlar",
                newName: "Oyun");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Oyun",
                table: "Oyun",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Skorlar_Oyun_OyunId",
                table: "Skorlar",
                column: "OyunId",
                principalTable: "Oyun",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
