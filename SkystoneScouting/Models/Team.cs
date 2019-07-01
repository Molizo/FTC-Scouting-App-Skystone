﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkystoneScouting.Models
{
    public class Team
    {
        #region Public Properties

        [Display(Name = "Average points")]
        public int AvgPTS { get; set; }

        [Display(Name = "CCWM")]
        public double CCWM { get; set; }

        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "DPR")]
        public double DPR { get; set; }

        public string EventID { get; set; }

        [Display(Name = "Expected points")]
        public int ExpPTS { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Display(Name = "OPR")]
        public double OPR { get; set; }

        [Display(Name = "Team address")]
        public string TeamAddress { get; set; }

        [Display(Name = "Team name")]
        public string TeamName { get; set; }

        [Display(Name = "Team ID")]
        public string TeamNumber { get; set; }

        #endregion Public Properties
    }
}