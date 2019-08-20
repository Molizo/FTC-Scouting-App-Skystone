using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SkystoneScouting.Data;
using SkystoneScouting.Models;

namespace SkystoneScouting.Pages.ScoutedMatches
{
    public class IndexModel : PageModel
    {
        #region Private Fields

        private readonly SkystoneScouting.Data.ApplicationDbContext _context;

        #endregion Private Fields

        #region Public Constructors

        public IndexModel(SkystoneScouting.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion Public Constructors

        #region Public Properties

        public string eventID { get; set; }
        public IList<ScoutedMatch> ScoutedMatch { get; set; }
        public string teamID { get; set; }

        #endregion Public Properties

        #region Public Methods

        public async Task OnGetAsync(string EventID, string TeamID)
        {
            eventID = EventID;
            teamID = TeamID;
            ScoutedMatch = await _context.ScoutedMatch.ToListAsync();
        }

        #endregion Public Methods
    }
}