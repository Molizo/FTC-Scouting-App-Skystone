using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkystoneScouting.Models
{
    public enum Alliance
    {
        Red,
        Blue
    }

    public class ScoutedMatch
    {
        #region Public Properties

        public Alliance Alliance { get; set; }

        public DeliveredStoneType Auto_FirstReturnedStoneType { get; set; }
        public bool Auto_FoundationRepositioned { get; set; }
        public bool Auto_RobotNavigated { get; set; }
        public DeliveredStoneType Auto_Stone1Type { get; set; }
        public DeliveredStoneType Auto_Stone2Type { get; set; }
        public DeliveredStoneType Auto_Stone3Type { get; set; }
        public DeliveredStoneType Auto_Stone4Type { get; set; }
        public DeliveredStoneType Auto_Stone5Type { get; set; }
        public DeliveredStoneType Auto_Stone6Type { get; set; }
        public int Auto_StonesPlaced { get; set; }
        public int Auto_StonesRetured { get; set; }
        public string Description { get; set; }
        public int EndGame_CapstoneHeight { get; set; }
        public bool EndGame_CapstonePlaced { get; set; }
        public bool EndGame_FoundationMoved { get; set; }
        public bool EndGame_RobotParked { get; set; }
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
        public string TeamID { get; set; }

        public int TeleOP_StonesDelivered { get; set; }
        public int TeleOP_StonesPlaced { get; set; }
        public int TeleOP_StonesReturned { get; set; }
        public int TeleOP_TallestSkyscraperHeight { get; set; }

        #endregion Public Properties
    }
}