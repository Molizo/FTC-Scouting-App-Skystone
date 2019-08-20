using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SkystoneScouting.Models
{
    public enum Alliance
    {
        Red,
        Blue
    }

    public enum StartingPosition
    {
        One,
        Two
    }

    public class ScoutedMatch
    {
        #region Public Properties

        public Alliance Alliance { get; set; }

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
        public int? Score { get; set; }
        public StartingPosition StartingPosition { get; set; }
        public string TeamID { get; set; }

        #endregion Public Properties
    }
}