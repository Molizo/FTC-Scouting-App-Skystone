using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkystoneScouting.Models
{
    public enum DeliveredStoneType
    {
        None,
        Stone,
        Skystone
    }

    public class Team
    {
        #region Public Properties

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

        [Display(Name = "Average points")]
        public double? AvgPTS { get; set; }

        [Display(Name = "Average RP")]
        public double? AvgRP { get; set; }

        [Display(Name = "TBP")]
        public double? AvgTBP { get; set; }

        [Display(Name = "CCWM")]
        public double? CCWM { get; set; }

        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "DPR")]
        public double? DPR { get; set; }

        public int EndGame_CapstoneHeight { get; set; }
        public bool EndGame_CapstonePlaced { get; set; }
        public bool EndGame_FoundationMoved { get; set; }
        public bool EndGame_RobotParked { get; set; }
        public string EventID { get; set; }

        [Display(Name = "Expected points")]
        public int? ExpPTS { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ID { get; set; }

        [Display(Name = "OPR")]
        public double? OPR { get; set; }

        [Display(Name = "Team address")]
        public string TeamAddress { get; set; }

        [Display(Name = "Team name")]
        public string TeamName { get; set; }

        [Display(Name = "Team ID")]
        public string TeamNumber { get; set; }

        public int TeleOP_MaximumReachableHeight { get; set; }
        public int TeleOP_StonesDelivered { get; set; }
        public int TeleOP_StonesPlaced { get; set; }
        public int TeleOP_StonesReturned { get; set; }

        #endregion Public Properties
    }
}