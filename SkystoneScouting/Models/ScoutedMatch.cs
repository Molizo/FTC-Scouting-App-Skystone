using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkystoneScouting.Models
{
    public enum Alliance
    {
        Red,
        Blue
    }

    public enum StartingPosition
    {
        Left,
        Right
    }

    public class ScoutedMatch
    {
        #region Public Properties

        public Alliance Alliance { get; set; }

        public bool Auto_BuildingFoundationReposition { get; set; }
        public int Auto_DeliveredSkystones { get; set; }
        public int Auto_DeliveredStones { get; set; }
        public string Auto_Description { get; set; }
        public bool Auto_NavigatedUnderSkybridge { get; set; }
        public int Auto_PlacedStones { get; set; }
        public int Auto_StonesDeliveredUnderAllianceSkybridge { get; set; }
        public int Auto_TimeToComplete { get; set; }
        public string Description { get; set; }
        public bool HumanPlayer { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ID { get; set; }

        public uint MajorPenalties { get; set; }
        public string MatchNumber { get; set; }
        public MatchType MatchType { get; set; }
        public uint MinorPenalties { get; set; }
        public bool RobotDisconnected { get; set; }
        public bool RobotMalfunction { get; set; }
        public int Score { get; set; }
        public StartingPosition StartingPosition { get; set; }
        public string TeamID { get; set; }
        public int TeleOP_BuildingPlatformReposition { get; set; }
        public int TeleOP_CapstoneLevel { get; set; }
        public string TeleOP_Description { get; set; }
        public bool TeleOP_HasPlacedCapstone { get; set; }
        public int TeleOP_NumberOfSkyscrapers { get; set; }
        public int TeleOP_NumberOfStonesPlaced { get; set; }
        public bool TeleOP_ParkedInBuildingSite { get; set; }
        public int TeleOP_SkyscraperLevel { get; set; }
        public int TeleOP_StonesDeliveredUnderAllianceSkybridge { get; set; }

        #endregion Public Properties
    }
}