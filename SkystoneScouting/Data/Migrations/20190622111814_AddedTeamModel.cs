using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SkystoneScouting.Data.Migrations
{
    public partial class AddedTeamModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Team",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AvgPTS = table.Column<int>(nullable: false),
                    CCWM = table.Column<double>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    DPR = table.Column<double>(nullable: false),
                    EventID = table.Column<string>(nullable: true),
                    ExpPTS = table.Column<int>(nullable: false),
                    OPR = table.Column<double>(nullable: false),
                    TeamAddress = table.Column<string>(nullable: true),
                    TeamName = table.Column<string>(nullable: true),
                    TeamNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Team");
        }
    }
}
