using Microsoft.EntityFrameworkCore.Migrations;

namespace SkystoneScouting.Migrations
{
    public partial class ChangedRPandTBPtoDoublesForAvg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RP",
                table: "Team");

            migrationBuilder.DropColumn(
                name: "TBP",
                table: "Team");

            migrationBuilder.AddColumn<double>(
                name: "AvgRP",
                table: "Team",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "AvgTBP",
                table: "Team",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvgRP",
                table: "Team");

            migrationBuilder.DropColumn(
                name: "AvgTBP",
                table: "Team");

            migrationBuilder.AddColumn<int>(
                name: "RP",
                table: "Team",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TBP",
                table: "Team",
                nullable: true);
        }
    }
}
