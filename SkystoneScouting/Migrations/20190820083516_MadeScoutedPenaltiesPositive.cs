using Microsoft.EntityFrameworkCore.Migrations;

namespace SkystoneScouting.Migrations
{
    public partial class MadeScoutedPenaltiesPositive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "MinorPenalties",
                table: "ScoutedMatch",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<long>(
                name: "MajorPenalties",
                table: "ScoutedMatch",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "MinorPenalties",
                table: "ScoutedMatch",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<int>(
                name: "MajorPenalties",
                table: "ScoutedMatch",
                nullable: false,
                oldClrType: typeof(long));
        }
    }
}
