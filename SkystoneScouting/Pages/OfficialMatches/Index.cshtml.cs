using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SkystoneScouting.Models;
using SkystoneScouting.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkystoneScouting.Pages.OfficialMatches
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

        public IQueryable<OfficialMatch> AuthorizedOfficialMatches { get; set; }
        public IList<Team> AuthorizedTeams { get; set; }
        public double AverageScore { get; set; }
        public int BestScore { get; set; }
        public string eventID { get; set; }
        public IList<OfficialMatch> FinalsOfficialMatches { get; set; }
        public double MatchesPerTeam { get; set; }
        public IList<OfficialMatch> PracticeOfficialMatches { get; set; }
        public IList<OfficialMatch> QualificationsOfficialMatches { get; set; }
        public IList<OfficialMatch> SemifinalsOfficialMatches { get; set; }

        #endregion Public Properties

        #region Public Methods

        public async Task GetAndSeparateData(string EventID)
        {
            AuthorizedTeams = await _context.Team.AsNoTracking().Where(t => t.EventID == EventID).ToListAsync();
            AuthorizedOfficialMatches = _context.OfficialMatch.AsNoTracking().Where(m => m.EventID == EventID).OrderBy(m => m.MatchNumber);
            QualificationsOfficialMatches = await AuthorizedOfficialMatches.Where(m => m.MatchType == MatchType.Qualification).ToListAsync();
            SemifinalsOfficialMatches = await AuthorizedOfficialMatches.Where(m => m.MatchType == MatchType.Semifinal).ToListAsync(); ;
            FinalsOfficialMatches = await AuthorizedOfficialMatches.Where(m => m.MatchType == MatchType.Final).ToListAsync();
            PracticeOfficialMatches = await AuthorizedOfficialMatches.Where(m => m.MatchType == MatchType.Practice).ToListAsync(); ;
        }

        public async Task<ActionResult> OnGetAsync(string EventID)
        {
            if (EventID == null)
                return NotFound();
            if (!AuthorizationCheck.Event(_context, EventID, User.Identity.Name))
                return Forbid();

            eventID = EventID;
            await GetAndSeparateData(EventID);

            int BestRedScore = AuthorizedOfficialMatches.Max(m => m.RedScore).GetValueOrDefault();
            int BestBlueScore = AuthorizedOfficialMatches.Max(m => m.BlueScore).GetValueOrDefault();
            BestScore = System.Math.Max(BestRedScore, BestBlueScore);

            try
            {
                int ScoresSum = 0;
                foreach (var Match in AuthorizedOfficialMatches)
                {
                    ScoresSum += Match.RedScore.GetValueOrDefault();
                    ScoresSum += Match.BlueScore.GetValueOrDefault();
                }
                AverageScore = ScoresSum / (AuthorizedOfficialMatches.Count() * 2);
                AverageScore = System.Math.Round(AverageScore, 2);

                MatchesPerTeam = AuthorizedOfficialMatches.Count() * 4 / AuthorizedTeams.Count;
                MatchesPerTeam = System.Math.Round(MatchesPerTeam, 2);
            }
            catch
            {
                AverageScore = 0;
                MatchesPerTeam = 0;
            }
            return Page();
        }

        public async Task<ActionResult> OnGetDelete(string EventID, string OfficialMatchID)
        {
            if (EventID == null || OfficialMatchID == null)
                return NotFound();
            if (!AuthorizationCheck.OfficialMatch(_context, OfficialMatchID, User.Identity.Name))
                return Forbid();

            OfficialMatch OfficialMatch = await _context.OfficialMatch.FindAsync(OfficialMatchID);
            IList<OfficialMatch> AuthorizedOfficialMatches = await _context.OfficialMatch.Where(s => s.EventID == EventID).ToListAsync();
            AuthorizedTeams = await _context.Team.AsNoTracking().Where(t => t.EventID == EventID).ToListAsync();
            IList<Team> TeamsWithScores = CalculateTeamMetrics.CalculateAllMetrics(AuthorizedTeams, AuthorizedOfficialMatches);

            _context.Team.UpdateRange(TeamsWithScores);

            if (OfficialMatch != null)
            {
                _context.OfficialMatch.Remove(OfficialMatch);
                await _context.SaveChangesAsync();
            }

            var _EventID = EventID;
            return RedirectToPage("./Index", new
            {
                EventID = _EventID,
            });
        }

        #endregion Public Methods
    }
}