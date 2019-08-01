using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SkystoneScouting.Models;
using SkystoneScouting.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace SkystoneScouting.Pages.Teams
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

        public IList<Event> AllEvents { get; set; }
        public IList<Team> AllTeams { get; set; }
        public IList<Team> AuthorizedTeams { get; set; }
        public string AvgPTSSort { get; set; }
        public string CCWMSort { get; set; }
        public string DPRSort { get; set; }
        public string eventID { get; set; }
        public string ExpPTSSort { get; set; }
        public string IDSort { get; set; }
        public string NameSort { get; set; }
        public IList<Team> NotScoutedTeams { get; set; }
        public string OPRSort { get; set; }
        public IList<Team> ScoutedTeams { get; set; }

        public string teamID { get; set; }

        #endregion Public Properties

        #region Public Methods

        public async Task OnGetAsync(string EventID, string TeamID, string SortOrder)
        {
            if (EventID == "")
                NotFound();
            eventID = EventID;
            teamID = TeamID;
            AllTeams = await _context.Team.ToListAsync();
            AllEvents = await _context.Event.ToListAsync();
            AuthorizedTeams = new List<Team>();
            NotScoutedTeams = new List<Team>();
            ScoutedTeams = new List<Team>();
            foreach (var Team in AllTeams)
            {
                if (AuthorizationCheck.Team(_context, Team.ID, User.Identity.Name) && Team.EventID == EventID)
                    AuthorizedTeams.Add(Team);
            }
            foreach (var Team in AuthorizedTeams)
            {
                if (Team.ExpPTS == null)
                    NotScoutedTeams.Add(Team);
                else
                    ScoutedTeams.Add(Team);
            }

            SortTeams(SortOrder);
        }

        public void SortTeams(string SortOrder)
        {
            IDSort = System.String.IsNullOrEmpty(SortOrder) || SortOrder == "ID" ? "ID_desc" : "ID";
            NameSort = SortOrder == "Name" ? "Name_desc" : "Name";
            ExpPTSSort = SortOrder == "ExpPTS" ? "ExpPTS_desc" : "ExpPTS";
            AvgPTSSort = SortOrder == "AvgPTS" ? "AvgPT_desc" : "AvgPTS";
            OPRSort = SortOrder == "OPR" ? "OPR_desc" : "OPR";
            DPRSort = SortOrder == "DPR" ? "DPR_desc" : "DPR";
            CCWMSort = SortOrder == "CCWM" ? "CCWM" : "CCWM_desc";
            switch (SortOrder)
            {
                case "ID_desc":
                    NotScoutedTeams = NotScoutedTeams.AsEnumerable<Team>().OrderByDescending(s => s.TeamNumber.PadLeft(5)).ToList<Team>();
                    ScoutedTeams = ScoutedTeams.AsEnumerable<Team>().OrderByDescending(s => s.TeamNumber.PadLeft(5)).ToList<Team>();
                    break;

                case "Name":
                    NotScoutedTeams = NotScoutedTeams.AsEnumerable<Team>().OrderBy(s => s.TeamName).ToList<Team>();
                    ScoutedTeams = ScoutedTeams.AsEnumerable<Team>().OrderBy(s => s.TeamName).ToList<Team>();
                    break;

                case "Name_desc":
                    NotScoutedTeams = NotScoutedTeams.AsEnumerable<Team>().OrderByDescending(s => s.TeamName).ToList<Team>();
                    ScoutedTeams = ScoutedTeams.AsEnumerable<Team>().OrderByDescending(s => s.TeamName).ToList<Team>();
                    break;

                case "ExpPTS":
                    ScoutedTeams = ScoutedTeams.AsEnumerable<Team>().OrderBy(s => s.ExpPTS).ToList<Team>();
                    break;

                case "ExpPTS_desc":
                    ScoutedTeams = ScoutedTeams.AsEnumerable<Team>().OrderByDescending(s => s.ExpPTS).ToList<Team>();
                    break;

                case "AvgPTS":
                    NotScoutedTeams = NotScoutedTeams.AsEnumerable<Team>().OrderBy(s => s.AvgPTS).ToList<Team>();
                    ScoutedTeams = ScoutedTeams.AsEnumerable<Team>().OrderBy(s => s.AvgPTS).ToList<Team>();
                    break;

                case "AvgPTS_desc":
                    NotScoutedTeams = NotScoutedTeams.AsEnumerable<Team>().OrderByDescending(s => s.AvgPTS).ToList<Team>();
                    ScoutedTeams = ScoutedTeams.AsEnumerable<Team>().OrderByDescending(s => s.AvgPTS).ToList<Team>();
                    break;

                case "OPR":
                    NotScoutedTeams = NotScoutedTeams.AsEnumerable<Team>().OrderBy(s => s.OPR).ToList<Team>();
                    ScoutedTeams = ScoutedTeams.AsEnumerable<Team>().OrderBy(s => s.OPR).ToList<Team>();
                    break;

                case "OPR_desc":
                    NotScoutedTeams = NotScoutedTeams.AsEnumerable<Team>().OrderByDescending(s => s.OPR).ToList<Team>();
                    ScoutedTeams = ScoutedTeams.AsEnumerable<Team>().OrderByDescending(s => s.OPR).ToList<Team>();
                    break;

                case "DPR":
                    NotScoutedTeams = NotScoutedTeams.AsEnumerable<Team>().OrderBy(s => s.DPR).ToList<Team>();
                    ScoutedTeams = ScoutedTeams.AsEnumerable<Team>().OrderBy(s => s.DPR).ToList<Team>();
                    break;

                case "DPR_desc":
                    NotScoutedTeams = NotScoutedTeams.AsEnumerable<Team>().OrderByDescending(s => s.DPR).ToList<Team>();
                    ScoutedTeams = ScoutedTeams.AsEnumerable<Team>().OrderByDescending(s => s.DPR).ToList<Team>();
                    break;

                case "CCWM":
                    NotScoutedTeams = NotScoutedTeams.AsEnumerable<Team>().OrderBy(s => s.CCWM).ToList<Team>();
                    ScoutedTeams = ScoutedTeams.AsEnumerable<Team>().OrderBy(s => s.CCWM).ToList<Team>();
                    break;

                case "CCWM_desc":
                    NotScoutedTeams = NotScoutedTeams.AsEnumerable<Team>().OrderByDescending(s => s.CCWM).ToList<Team>();
                    ScoutedTeams = ScoutedTeams.AsEnumerable<Team>().OrderByDescending(s => s.CCWM).ToList<Team>();
                    break;

                default:
                    NotScoutedTeams = NotScoutedTeams.AsEnumerable<Team>().OrderBy(s => s.TeamNumber.PadLeft(5)).ToList<Team>();
                    ScoutedTeams = ScoutedTeams.AsEnumerable<Team>().OrderBy(s => s.TeamNumber.PadLeft(5)).ToList<Team>();
                    break;
            }
        }

        #endregion Public Methods
    }
}