using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SkystoneScouting.Models;
using SkystoneScouting.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkystoneScouting.Pages
{
    public class DashboardModel : PageModel
    {
        #region Private Fields

        private readonly SkystoneScouting.Data.ApplicationDbContext _context;

        #endregion Private Fields

        #region Public Constructors

        public DashboardModel(SkystoneScouting.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion Public Constructors

        #region Public Properties

        public string eventID { get; set; }

        public Stats stats { get; set; }

        #endregion Public Properties

        #region Public Methods

        public async Task<ActionResult> OnGetAsync(string EventID)
        {
            if (EventID == null)
                return NotFound();
            if (!AuthorizationCheck.Event(_context, EventID, User.Identity.Name))
                return Forbid();

            stats = new Stats();
            eventID = EventID;

            IList<Team> AuthorizedTeams = await _context.Team.AsNoTracking().Where(t => t.EventID == EventID).ToListAsync();
            IList<OfficialMatch> AuthorizedOfficialMatches = await _context.OfficialMatch.AsNoTracking().Where(s => s.EventID == EventID).ToListAsync();
            IList<int> AuthorizedScoutedMatches = new List<int>();

            stats.TopOPRTeams = AuthorizedTeams.OrderByDescending(t => t.OPR).Take(5).ToList();
            stats.TopDPRTeams = AuthorizedTeams.OrderByDescending(t => t.DPR).Take(5).ToList();
            stats.TopCCWMTeams = AuthorizedTeams.OrderByDescending(t => t.CCWM).Take(5).ToList();
            stats.TopRPTeams = AuthorizedTeams.OrderByDescending(t => t.AvgRP).Take(5).ToList();
            stats.TopTBPTeams = AuthorizedTeams.OrderByDescending(t => t.AvgTBP).Take(5).ToList();
            stats.TopAvgPTSTeams = AuthorizedTeams.OrderByDescending(t => t.AvgPTS).Take(5).ToList();

            stats.NrOfEvents = await _context.Event.CountAsync();
            stats.NrOfTeams = AuthorizedTeams.Count;
            stats.NrOfOfficialMatches = AuthorizedOfficialMatches.Count;
            stats.NrOfScoutedMatches = AuthorizedScoutedMatches.Count;

            return Page();
        }

        #endregion Public Methods

        #region Public Classes

        public class Stats
        {
            #region Public Properties

            public int NrOfEvents { get; set; }
            public int? NrOfOfficialMatches { get; set; }
            public int? NrOfScoutedMatches { get; set; }
            public int? NrOfTeams { get; set; }
            public IList<Team> TopAvgPTSTeams { get; set; }
            public IList<Team> TopCCWMTeams { get; set; }
            public IList<Team> TopDPRTeams { get; set; }
            public IList<Team> TopOPRTeams { get; set; }
            public IList<Team> TopRPTeams { get; set; }
            public IList<Team> TopTBPTeams { get; set; }

            #endregion Public Properties
        }

        #endregion Public Classes
    }
}