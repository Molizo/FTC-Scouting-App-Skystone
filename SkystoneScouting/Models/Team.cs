using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkystoneScouting.Models
{
    public class Team
    {
        #region Public Properties

        public bool Auto_BuildingFoundationReposition { get; set; }

        public int Auto_DeliveredSkystones { get; set; }

        public int Auto_DeliveredStones { get; set; }

        public string Auto_Description { get; set; }

        public bool Auto_NavigatedUnderSkybridge { get; set; }

        public int Auto_PlacedStones { get; set; }

        public int Auto_StonesDeliveredUnderAllianceSkybridge { get; set; }

        public int Auto_TimeToComplete { get; set; }

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