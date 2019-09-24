using Microsoft.EntityFrameworkCore.Migrations;

namespace SkystoneScouting.Migrations
{
    public partial class AddedRobotInfoToTeamScouting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Auto_FoundationRepositioned",
                table: "Team",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Auto_RobotNavigated",
                table: "Team",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Auto_Stone1Type",
                table: "Team",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Auto_Stone2Type",
                table: "Team",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Auto_Stone3Type",
                table: "Team",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Auto_Stone4Type",
                table: "Team",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Auto_Stone5Type",
                table: "Team",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Auto_Stone6Type",
                table: "Team",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Auto_StonesPlaced",
                table: "Team",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EndGame_CapstoneHeight",
                table: "Team",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "EndGame_CapstonePlaced",
                table: "Team",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "EndGame_FoundationMoved",
                table: "Team",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "EndGame_RobotParked",
                table: "Team",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "TeleOP_MaximumReachableHeight",
                table: "Team",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TeleOP_StonesDelivered",
                table: "Team",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TeleOP_StonesPlaced",
                table: "Team",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TeleOP_StonesReturned",
                table: "Team",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Auto_BuildingFoundationReposition",
                table: "ScoutedMatch",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Auto_DeliveredSkystones",
                table: "ScoutedMatch",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Auto_DeliveredStones",
                table: "ScoutedMatch",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Auto_Description",
                table: "ScoutedMatch",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Auto_NavigatedUnderSkybridge",
                table: "ScoutedMatch",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Auto_PlacedStones",
                table: "ScoutedMatch",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Auto_StonesDeliveredUnderAllianceSkybridge",
                table: "ScoutedMatch",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Auto_TimeToComplete",
                table: "ScoutedMatch",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TeleOP_BuildingPlatformReposition",
                table: "ScoutedMatch",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TeleOP_CapstoneLevel",
                table: "ScoutedMatch",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TeleOP_Description",
                table: "ScoutedMatch",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TeleOP_HasPlacedCapstone",
                table: "ScoutedMatch",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "TeleOP_NumberOfSkyscrapers",
                table: "ScoutedMatch",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TeleOP_NumberOfStonesPlaced",
                table: "ScoutedMatch",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "TeleOP_ParkedInBuildingSite",
                table: "ScoutedMatch",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "TeleOP_SkyscraperLevel",
                table: "ScoutedMatch",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TeleOP_StonesDeliveredUnderAllianceSkybridge",
                table: "ScoutedMatch",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Auto_FoundationRepositioned",
                table: "Team");

            migrationBuilder.DropColumn(
                name: "Auto_RobotNavigated",
                table: "Team");

            migrationBuilder.DropColumn(
                name: "Auto_Stone1Type",
                table: "Team");

            migrationBuilder.DropColumn(
                name: "Auto_Stone2Type",
                table: "Team");

            migrationBuilder.DropColumn(
                name: "Auto_Stone3Type",
                table: "Team");

            migrationBuilder.DropColumn(
                name: "Auto_Stone4Type",
                table: "Team");

            migrationBuilder.DropColumn(
                name: "Auto_Stone5Type",
                table: "Team");

            migrationBuilder.DropColumn(
                name: "Auto_Stone6Type",
                table: "Team");

            migrationBuilder.DropColumn(
                name: "Auto_StonesPlaced",
                table: "Team");

            migrationBuilder.DropColumn(
                name: "EndGame_CapstoneHeight",
                table: "Team");

            migrationBuilder.DropColumn(
                name: "EndGame_CapstonePlaced",
                table: "Team");

            migrationBuilder.DropColumn(
                name: "EndGame_FoundationMoved",
                table: "Team");

            migrationBuilder.DropColumn(
                name: "EndGame_RobotParked",
                table: "Team");

            migrationBuilder.DropColumn(
                name: "TeleOP_MaximumReachableHeight",
                table: "Team");

            migrationBuilder.DropColumn(
                name: "TeleOP_StonesDelivered",
                table: "Team");

            migrationBuilder.DropColumn(
                name: "TeleOP_StonesPlaced",
                table: "Team");

            migrationBuilder.DropColumn(
                name: "TeleOP_StonesReturned",
                table: "Team");

            migrationBuilder.DropColumn(
                name: "Auto_BuildingFoundationReposition",
                table: "ScoutedMatch");

            migrationBuilder.DropColumn(
                name: "Auto_DeliveredSkystones",
                table: "ScoutedMatch");

            migrationBuilder.DropColumn(
                name: "Auto_DeliveredStones",
                table: "ScoutedMatch");

            migrationBuilder.DropColumn(
                name: "Auto_Description",
                table: "ScoutedMatch");

            migrationBuilder.DropColumn(
                name: "Auto_NavigatedUnderSkybridge",
                table: "ScoutedMatch");

            migrationBuilder.DropColumn(
                name: "Auto_PlacedStones",
                table: "ScoutedMatch");

            migrationBuilder.DropColumn(
                name: "Auto_StonesDeliveredUnderAllianceSkybridge",
                table: "ScoutedMatch");

            migrationBuilder.DropColumn(
                name: "Auto_TimeToComplete",
                table: "ScoutedMatch");

            migrationBuilder.DropColumn(
                name: "TeleOP_BuildingPlatformReposition",
                table: "ScoutedMatch");

            migrationBuilder.DropColumn(
                name: "TeleOP_CapstoneLevel",
                table: "ScoutedMatch");

            migrationBuilder.DropColumn(
                name: "TeleOP_Description",
                table: "ScoutedMatch");

            migrationBuilder.DropColumn(
                name: "TeleOP_HasPlacedCapstone",
                table: "ScoutedMatch");

            migrationBuilder.DropColumn(
                name: "TeleOP_NumberOfSkyscrapers",
                table: "ScoutedMatch");

            migrationBuilder.DropColumn(
                name: "TeleOP_NumberOfStonesPlaced",
                table: "ScoutedMatch");

            migrationBuilder.DropColumn(
                name: "TeleOP_ParkedInBuildingSite",
                table: "ScoutedMatch");

            migrationBuilder.DropColumn(
                name: "TeleOP_SkyscraperLevel",
                table: "ScoutedMatch");

            migrationBuilder.DropColumn(
                name: "TeleOP_StonesDeliveredUnderAllianceSkybridge",
                table: "ScoutedMatch");
        }
    }
}
