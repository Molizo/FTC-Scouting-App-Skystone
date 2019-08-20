using Microsoft.EntityFrameworkCore.Migrations;

namespace SkystoneScouting.Migrations
{
    public partial class AddedScoutedMatches : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ScoutedMatch",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    Alliance = table.Column<int>(nullable: false),
                    HumanPlayer = table.Column<bool>(nullable: false),
                    MajorPenalties = table.Column<int>(nullable: false),
                    MinorPenalties = table.Column<int>(nullable: false),
                    Score = table.Column<int>(nullable: true),
                    StartingPosition = table.Column<int>(nullable: false),
                    TeamID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScoutedMatch", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScoutedMatch");
        }
    }
}
