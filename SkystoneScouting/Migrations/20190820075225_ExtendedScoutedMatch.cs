using Microsoft.EntityFrameworkCore.Migrations;

namespace SkystoneScouting.Migrations
{
    public partial class ExtendedScoutedMatch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ScoutedMatch",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "RobotDisconnected",
                table: "ScoutedMatch",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "RobotMalfunction",
                table: "ScoutedMatch",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "ScoutedMatch");

            migrationBuilder.DropColumn(
                name: "RobotDisconnected",
                table: "ScoutedMatch");

            migrationBuilder.DropColumn(
                name: "RobotMalfunction",
                table: "ScoutedMatch");
        }
    }
}
