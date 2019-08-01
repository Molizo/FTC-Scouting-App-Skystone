using Microsoft.EntityFrameworkCore.Migrations;

namespace SkystoneScouting.Migrations
{
    public partial class AddedNullablesToTeamStatistics : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "OPR",
                table: "Team",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<int>(
                name: "ExpPTS",
                table: "Team",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<double>(
                name: "DPR",
                table: "Team",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<double>(
                name: "CCWM",
                table: "Team",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<int>(
                name: "AvgPTS",
                table: "Team",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "OPR",
                table: "Team",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ExpPTS",
                table: "Team",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "DPR",
                table: "Team",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "CCWM",
                table: "Team",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AvgPTS",
                table: "Team",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
