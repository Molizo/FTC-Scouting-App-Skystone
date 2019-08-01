using Microsoft.EntityFrameworkCore.Migrations;

namespace SkystoneScouting.Migrations
{
    public partial class AddedScheduledMatchesModelForReal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ScheduledMatch",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    Blue1TeamID = table.Column<string>(nullable: true),
                    Blue2TeamID = table.Column<string>(nullable: true),
                    BlueScore = table.Column<int>(nullable: true),
                    EventID = table.Column<string>(nullable: true),
                    Red1TeamID = table.Column<string>(nullable: true),
                    Red2TeamID = table.Column<string>(nullable: true),
                    RedScore = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduledMatch", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScheduledMatch");
        }
    }
}
