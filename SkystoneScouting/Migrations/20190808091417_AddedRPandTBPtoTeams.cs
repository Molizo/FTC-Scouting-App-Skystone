using Microsoft.EntityFrameworkCore.Migrations;

namespace SkystoneScouting.Migrations
{
    public partial class AddedRPandTBPtoTeams : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RP",
                table: "Team",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TBP",
                table: "Team",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RP",
                table: "Team");

            migrationBuilder.DropColumn(
                name: "TBP",
                table: "Team");
        }
    }
}
