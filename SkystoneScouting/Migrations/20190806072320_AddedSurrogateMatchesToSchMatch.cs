using Microsoft.EntityFrameworkCore.Migrations;

namespace SkystoneScouting.Migrations
{
    public partial class AddedSurrogateMatchesToSchMatch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Blue1Surrogate",
                table: "ScheduledMatch",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Blue2Surrogate",
                table: "ScheduledMatch",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Red1Surrogate",
                table: "ScheduledMatch",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Red2Surrogate",
                table: "ScheduledMatch",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Blue1Surrogate",
                table: "ScheduledMatch");

            migrationBuilder.DropColumn(
                name: "Blue2Surrogate",
                table: "ScheduledMatch");

            migrationBuilder.DropColumn(
                name: "Red1Surrogate",
                table: "ScheduledMatch");

            migrationBuilder.DropColumn(
                name: "Red2Surrogate",
                table: "ScheduledMatch");
        }
    }
}
