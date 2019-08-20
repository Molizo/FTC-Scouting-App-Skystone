using Microsoft.EntityFrameworkCore.Migrations;

namespace SkystoneScouting.Migrations
{
    public partial class EnhancedScoutedMatchesModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MatchNumber",
                table: "ScoutedMatch",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MatchType",
                table: "ScoutedMatch",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MatchNumber",
                table: "ScoutedMatch");

            migrationBuilder.DropColumn(
                name: "MatchType",
                table: "ScoutedMatch");
        }
    }
}
