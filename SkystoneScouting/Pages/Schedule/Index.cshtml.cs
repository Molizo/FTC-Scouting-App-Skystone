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

namespace SkystoneScouting.Pages.Schedule
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

        public IQueryable<ScheduledMatch> AuthorizedScheduledMatches { get; set; }
        public IList<Team> AuthorizedTeams { get; set; }
        public double AverageScore { get; set; }
        public int BestScore { get; set; }
        public string eventID { get; set; }
        public IList<ScheduledMatch> FinalsScheduledMatches { get; set; }
        public double MatchesPerTeam { get; set; }
        public IList<ScheduledMatch> PracticeScheduledMatches { get; set; }
        public IList<ScheduledMatch> QualificationsScheduledMatches { get; set; }
        public IList<ScheduledMatch> SemifinalsScheduledMatches { get; set; }

        #endregion Public Properties

        #region Public Methods

        public async Task GetAndSeparateData(string EventID)
        {
            AuthorizedTeams = await _context.Team.AsNoTracking().Where(t => t.EventID == EventID).ToListAsync();
            AuthorizedScheduledMatches = _context.ScheduledMatch.AsNoTracking().Where(m => m.EventID == EventID).OrderBy(m => m.MatchNumber);
            QualificationsScheduledMatches = await AuthorizedScheduledMatches.Where(m => m.MatchType == MatchType.Qualification).ToListAsync();
            SemifinalsScheduledMatches = await AuthorizedScheduledMatches.Where(m => m.MatchType == MatchType.Semifinal).ToListAsync(); ;
            FinalsScheduledMatches = await AuthorizedScheduledMatches.Where(m => m.MatchType == MatchType.Final).ToListAsync();
            PracticeScheduledMatches = await AuthorizedScheduledMatches.Where(m => m.MatchType == MatchType.Practice).ToListAsync(); ;
        }

        public async Task<ActionResult> OnGetAsync(string EventID)
        {
            if (EventID == null)
                return NotFound();
            if (!AuthorizationCheck.Event(_context, EventID, User.Identity.Name))
                return Forbid();

            eventID = EventID;
            await GetAndSeparateData(EventID);

            int BestRedScore = AuthorizedScheduledMatches.Max(m => m.RedScore).GetValueOrDefault();
            int BestBlueScore = AuthorizedScheduledMatches.Max(m => m.BlueScore).GetValueOrDefault();
            BestScore = System.Math.Max(BestRedScore, BestBlueScore);

            try
            {
                int ScoresSum = 0;
                foreach (var Match in AuthorizedScheduledMatches)
                {
                    ScoresSum += Match.RedScore.GetValueOrDefault();
                    ScoresSum += Match.BlueScore.GetValueOrDefault();
                }
                AverageScore = ScoresSum / (AuthorizedScheduledMatches.Count() * 2);
                AverageScore = System.Math.Round(AverageScore, 2);

                MatchesPerTeam = AuthorizedScheduledMatches.Count() * 4 / AuthorizedTeams.Count;
                MatchesPerTeam = System.Math.Round(MatchesPerTeam, 2);
            }
            catch
            {
                AverageScore = 0;
                MatchesPerTeam = 0;
            }
            return Page();
        }

        #endregion Public Methods
    }
}