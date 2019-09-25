using Microsoft.EntityFrameworkCore.Migrations;

namespace SkystoneScouting.Migrations
{
    public partial class AddedScoutedMatchesFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "Auto_PlacedStones",
                table: "ScoutedMatch");

            migrationBuilder.DropColumn(
                name: "Auto_StonesDeliveredUnderAllianceSkybridge",
                table: "ScoutedMatch");

            migrationBuilder.DropColumn(
                name: "Auto_TimeToComplete",
                table: "ScoutedMatch");

            migrationBuilder.DropColumn(
                name: "TeleOP_Description",
                table: "ScoutedMatch");

            migrationBuilder.RenameColumn(
                name: "TeleOP_StonesDeliveredUnderAllianceSkybridge",
                table: "ScoutedMatch",
                newName: "TeleOP_TallestSkyscraperHeight");

            migrationBuilder.RenameColumn(
                name: "TeleOP_SkyscraperLevel",
                table: "ScoutedMatch",
                newName: "TeleOP_StonesReturned");

            migrationBuilder.RenameColumn(
                name: "TeleOP_ParkedInBuildingSite",
                table: "ScoutedMatch",
                newName: "EndGame_RobotParked");

            migrationBuilder.RenameColumn(
                name: "TeleOP_NumberOfStonesPlaced",
                table: "ScoutedMatch",
                newName: "TeleOP_StonesPlaced");

            migrationBuilder.RenameColumn(
                name: "TeleOP_NumberOfSkyscrapers",
                table: "ScoutedMatch",
                newName: "TeleOP_StonesDelivered");

            migrationBuilder.RenameColumn(
                name: "TeleOP_HasPlacedCapstone",
                table: "ScoutedMatch",
                newName: "EndGame_FoundationMoved");

            migrationBuilder.RenameColumn(
                name: "TeleOP_CapstoneLevel",
                table: "ScoutedMatch",
                newName: "EndGame_CapstoneHeight");

            migrationBuilder.RenameColumn(
                name: "TeleOP_BuildingPlatformReposition",
                table: "ScoutedMatch",
                newName: "Auto_StonesRetured");

            migrationBuilder.RenameColumn(
                name: "StartingPosition",
                table: "ScoutedMatch",
                newName: "Auto_StonesPlaced");

            migrationBuilder.RenameColumn(
                name: "Auto_NavigatedUnderSkybridge",
                table: "ScoutedMatch",
                newName: "EndGame_CapstonePlaced");

            migrationBuilder.RenameColumn(
                name: "Auto_BuildingFoundationReposition",
                table: "ScoutedMatch",
                newName: "Auto_RobotNavigated");

            migrationBuilder.AddColumn<int>(
                name: "Auto_FirstReturnedStoneType",
                table: "ScoutedMatch",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Auto_FoundationRepositioned",
                table: "ScoutedMatch",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Auto_Stone1Type",
                table: "ScoutedMatch",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Auto_Stone2Type",
                table: "ScoutedMatch",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Auto_Stone3Type",
                table: "ScoutedMatch",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Auto_Stone4Type",
                table: "ScoutedMatch",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Auto_Stone5Type",
                table: "ScoutedMatch",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Auto_Stone6Type",
                table: "ScoutedMatch",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Auto_FirstReturnedStoneType",
                table: "ScoutedMatch");

            migrationBuilder.DropColumn(
                name: "Auto_FoundationRepositioned",
                table: "ScoutedMatch");

            migrationBuilder.DropColumn(
                name: "Auto_Stone1Type",
                table: "ScoutedMatch");

            migrationBuilder.DropColumn(
                name: "Auto_Stone2Type",
                table: "ScoutedMatch");

            migrationBuilder.DropColumn(
                name: "Auto_Stone3Type",
                table: "ScoutedMatch");

            migrationBuilder.DropColumn(
                name: "Auto_Stone4Type",
                table: "ScoutedMatch");

            migrationBuilder.DropColumn(
                name: "Auto_Stone5Type",
                table: "ScoutedMatch");

            migrationBuilder.DropColumn(
                name: "Auto_Stone6Type",
                table: "ScoutedMatch");

            migrationBuilder.RenameColumn(
                name: "TeleOP_TallestSkyscraperHeight",
                table: "ScoutedMatch",
                newName: "TeleOP_StonesDeliveredUnderAllianceSkybridge");

            migrationBuilder.RenameColumn(
                name: "TeleOP_StonesReturned",
                table: "ScoutedMatch",
                newName: "TeleOP_SkyscraperLevel");

            migrationBuilder.RenameColumn(
                name: "TeleOP_StonesPlaced",
                table: "ScoutedMatch",
                newName: "TeleOP_NumberOfStonesPlaced");

            migrationBuilder.RenameColumn(
                name: "TeleOP_StonesDelivered",
                table: "ScoutedMatch",
                newName: "TeleOP_NumberOfSkyscrapers");

            migrationBuilder.RenameColumn(
                name: "EndGame_RobotParked",
                table: "ScoutedMatch",
                newName: "TeleOP_ParkedInBuildingSite");

            migrationBuilder.RenameColumn(
                name: "EndGame_FoundationMoved",
                table: "ScoutedMatch",
                newName: "TeleOP_HasPlacedCapstone");

            migrationBuilder.RenameColumn(
                name: "EndGame_CapstonePlaced",
                table: "ScoutedMatch",
                newName: "Auto_NavigatedUnderSkybridge");

            migrationBuilder.RenameColumn(
                name: "EndGame_CapstoneHeight",
                table: "ScoutedMatch",
                newName: "TeleOP_CapstoneLevel");

            migrationBuilder.RenameColumn(
                name: "Auto_StonesRetured",
                table: "ScoutedMatch",
                newName: "TeleOP_BuildingPlatformReposition");

            migrationBuilder.RenameColumn(
                name: "Auto_StonesPlaced",
                table: "ScoutedMatch",
                newName: "StartingPosition");

            migrationBuilder.RenameColumn(
                name: "Auto_RobotNavigated",
                table: "ScoutedMatch",
                newName: "Auto_BuildingFoundationReposition");

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

            migrationBuilder.AddColumn<string>(
                name: "TeleOP_Description",
                table: "ScoutedMatch",
                nullable: true);
        }
    }
}
