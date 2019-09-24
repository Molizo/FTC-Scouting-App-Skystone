using Microsoft.EntityFrameworkCore.Migrations;

namespace SkystoneScouting.Migrations
{
    public partial class AddedMissingTeamAutoFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Auto_FirstReturnedStoneType",
                table: "Team",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Auto_StonesRetured",
                table: "Team",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Auto_FirstReturnedStoneType",
                table: "Team");

            migrationBuilder.DropColumn(
                name: "Auto_StonesRetured",
                table: "Team");
        }
    }
}
