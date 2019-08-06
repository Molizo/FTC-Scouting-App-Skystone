using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkystoneScouting.Models
{
    public enum MatchType
    {
        Qualification,
        Elimination
    }

    public class ScheduledMatch
    {
        #region Public Properties

        public string Blue1TeamID { get; set; }
        public string Blue2TeamID { get; set; }
        public int? BlueScore { get; set; }
        public string EventID { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ID { get; set; }

        public string MatchNumber { get; set; }
        public MatchType MatchType { get; set; }
        public string Red1TeamID { get; set; }
        public string Red2TeamID { get; set; }
        public int? RedScore { get; set; }

        #endregion Public Properties
    }
}