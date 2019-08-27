using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SkystoneScouting.Models;
using SkystoneScouting.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkystoneScouting.Pages.ScoutedMatches
{
    public class CreateModel : PageModel
    {
        #region Private Fields

        private readonly SkystoneScouting.Data.ApplicationDbContext _context;

        #endregion Private Fields

        #region Public Constructors

        public CreateModel(SkystoneScouting.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion Public Constructors

        #region Public Properties

        public string eventID { get; set; }

        [BindProperty]
        public ScoutedMatch ScoutedMatch { get; set; }

        public string teamID { get; set; }

        #endregion Public Properties

        #region Public Methods

        public ActionResult OnGet(string EventID, string TeamID)
        {
            if (EventID == null || TeamID == null)
                return NotFound();
            if (!AuthorizationCheck.Team(_context, TeamID, User.Identity.Name))
                return Forbid();
            eventID = EventID;
            teamID = TeamID;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string EventID, string TeamID)
        {
            if (EventID == null || TeamID == null)
                return NotFound();
            if (!AuthorizationCheck.Team(_context, TeamID, User.Identity.Name))
                return Forbid();

            if (!ModelState.IsValid)
            {
                return Page();
            }

            eventID = EventID;
            teamID = TeamID;
            ScoutedMatch.TeamID = TeamID;
            ScoutedMatch.Score = CalculateTeamMetrics.ComputeScoutedMatchScore(ScoutedMatch);
            _context.ScoutedMatch.Add(ScoutedMatch);

            Team ScoutedTeam = await _context.Team.FindAsync(TeamID);
            IList<int> Scores = await _context.ScoutedMatch.AsNoTracking().Where(s => s.TeamID == TeamID).Select(s => s.Score).ToListAsync();
            Scores.Add(ScoutedMatch.Score);
            ScoutedTeam.AvgPTS = Math.Round(Scores.Average(), 1);

            _context.Team.Update(ScoutedTeam);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index", new
            {
                EventID = eventID,
                TeamID = teamID,
            });
        }

        #endregion Public Methods
    }
}