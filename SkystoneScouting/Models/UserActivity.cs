using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkystoneScouting.Models
{
    public class UserActivity
    {
        #region Public Properties

        public string Action { get; set; }

        public DateTime DateTime { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ID { get; set; }

        public string PerformedBy
        {
            get; set;
        }

        #endregion Public Properties
    }
}