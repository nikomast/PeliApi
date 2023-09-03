using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PeliApi.Migrations
{
    /// <inheritdoc />
    public partial class AddHighScoresTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Scores",
                table: "Scores");

            migrationBuilder.RenameTable(
                name: "Scores",
                newName: "HighScores");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HighScores",
                table: "HighScores",
                column: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_HighScores",
                table: "HighScores");

            migrationBuilder.RenameTable(
                name: "HighScores",
                newName: "Scores");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Scores",
                table: "Scores",
                column: "ID");
        }
    }
}
