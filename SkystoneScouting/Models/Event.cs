using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkystoneScouting.Models
{
    public class Event
    {
        #region Public Properties

        [Display(Name = "Address")]
        public string Address { get; set; }

        public string AllowedUsers { get; set; }

        [Display(Name = "End date")]
        public DateTime EndDate { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Start date")]
        public DateTime StartDate { get; set; }

        #endregion Public Properties
    }
}