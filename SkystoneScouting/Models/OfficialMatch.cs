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
        Semifinal,
        Final,
        Practice
    }

    public class OfficialMatch
    {
        #region Public Properties

        public bool Blue1Surrogate { get; set; }
        public string Blue1TeamID { get; set; }
        public bool Blue2Surrogate { get; set; }
        public string Blue2TeamID { get; set; }
        public int? BlueScore { get; set; }
        public string EventID { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ID { get; set; }

        public string MatchNumber { get; set; }
        public MatchType MatchType { get; set; }
        public bool Red1Surrogate { get; set; }
        public string Red1TeamID { get; set; }
        public bool Red2Surrogate { get; set; }
        public string Red2TeamID { get; set; }
        public int? RedScore { get; set; }

        #endregion Public Properties
    }
}