using Microsoft.EntityFrameworkCore.Migrations;

namespace SkystoneScouting.Migrations
{
    public partial class AddedNameAndTypeToSchMatchModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MatchName",
                table: "ScheduledMatch",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MatchType",
                table: "ScheduledMatch",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MatchName",
                table: "ScheduledMatch");

            migrationBuilder.DropColumn(
                name: "MatchType",
                table: "ScheduledMatch");
        }
    }
}
