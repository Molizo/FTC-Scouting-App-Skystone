using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SkystoneScouting.Data;
using SkystoneScouting.Models;
using SkystoneScouting.Services;

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

        public IQueryable<ScoutedMatch> AuthorizedScoutedMatches { get; set; }
        public double AverageScore { get; set; }
        public string eventID { get; set; }
        public IList<ScoutedMatch> FinalsScoutedMatches { get; set; }
        public int HighestScore { get; set; }
        public long NumberOfPenalties { get; set; }
        public int NumberOfScoutedMatches { get; set; }
        public IList<ScoutedMatch> PracticeScoutedMatches { get; set; }
        public IList<ScoutedMatch> QualificationScoutedMatches { get; set; }
        public IList<ScoutedMatch> SemifinalsScoutedMatches { get; set; }
        public string teamID { get; set; }

        #endregion Public Properties

        #region Public Methods

        public async Task<ActionResult> OnGetAsync(string EventID, string TeamID)
        {
            if (EventID == null || TeamID == null)
                return NotFound();
            if (!AuthorizationCheck.Team(_context, TeamID, User.Identity.Name))
                return Forbid();
            eventID = EventID;
            teamID = TeamID;

            AuthorizedScoutedMatches = _context.ScoutedMatch.AsNoTracking().Where(s => s.TeamID == TeamID);
            QualificationScoutedMatches = await AuthorizedScoutedMatches.Where(s => s.MatchType == MatchType.Qualification).ToListAsync();
            SemifinalsScoutedMatches = await AuthorizedScoutedMatches.Where(s => s.MatchType == MatchType.Semifinal).ToListAsync();
            FinalsScoutedMatches = await AuthorizedScoutedMatches.Where(s => s.MatchType == MatchType.Final).ToListAsync();
            PracticeScoutedMatches = await AuthorizedScoutedMatches.Where(s => s.MatchType == MatchType.Practice).ToListAsync();

            NumberOfScoutedMatches = await AuthorizedScoutedMatches.CountAsync();
            if (NumberOfScoutedMatches != 0)
            {
                HighestScore = await AuthorizedScoutedMatches.MaxAsync(s => s.Score);
                AverageScore = Math.Round(await AuthorizedScoutedMatches.AverageAsync(s => s.Score), 2);
                NumberOfPenalties = await AuthorizedScoutedMatches.SumAsync(s => s.MinorPenalties + s.MajorPenalties);
            }
            return Page();
        }

        public async Task<IActionResult> OnGetDelete(string EventID, string TeamID, string ScoutedMatchID)
        {
            if (EventID == null || TeamID == null || ScoutedMatchID == null)
                return NotFound();
            if (!AuthorizationCheck.ScoutedMatch(_context, ScoutedMatchID, User.Identity.Name))
                return Forbid();

            ScoutedMatch ScoutedMatch = await _context.ScoutedMatch.FindAsync(ScoutedMatchID);

            if (ScoutedMatch != null)
            {
                Team ScoutedTeam = await _context.Team.FindAsync(TeamID);
                IList<int> Scores = await _context.ScoutedMatch.AsNoTracking().Where(s => s.TeamID == TeamID).Select(s => s.Score).ToListAsync();
                Scores.Remove(ScoutedMatch.Score);
                ScoutedTeam.AvgPTS = Math.Round(Scores.Average(), 1);
                _context.Team.Update(ScoutedTeam);
                _context.ScoutedMatch.Remove(ScoutedMatch);
                await _context.SaveChangesAsync();
            }

            var _EventID = EventID;
            var _TeamID = TeamID;
            return RedirectToPage("./Index", new
            {
                EventID = _EventID,
                TeamID = _TeamID,
            });
        }

        #endregion Public Methods
    }
}