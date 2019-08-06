using Microsoft.EntityFrameworkCore.Migrations;

namespace SkystoneScouting.Migrations
{
    public partial class RenamedMatchNameToMatchNumberInSchMatchModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MatchName",
                table: "ScheduledMatch",
                newName: "MatchNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MatchNumber",
                table: "ScheduledMatch",
                newName: "MatchName");
        }
    }
}
