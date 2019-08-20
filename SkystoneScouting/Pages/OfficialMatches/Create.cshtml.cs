using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SkystoneScouting.Data;
using SkystoneScouting.Models;
using SkystoneScouting.Services;

namespace SkystoneScouting.Pages.OfficialMatches
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

        public IList<Team> AuthorizedTeams { get; set; }
        public string eventID { get; set; }

        [BindProperty]
        public OfficialMatch OfficialMatch { get; set; }

        public IList<OfficialMatch> OfficialMatches { get; set; }

        #endregion Public Properties

        #region Public Methods

        public IActionResult OnGet(string EventID)
        {
            if (EventID == null)
                return NotFound();
            if (!AuthorizationCheck.Event(_context, EventID, User.Identity.Name))
                return Forbid();

            AuthorizedTeams = _context.Team.AsNoTracking().Where(t => t.EventID == EventID).AsQueryable<Team>().OrderBy(s => s.TeamNumber.PadLeft(5)).ToList<Team>();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string EventID)
        {
            if (EventID == null)
                return NotFound();
            if (!AuthorizationCheck.Event(_context, EventID, User.Identity.Name))
                return Forbid();

            if (!ModelState.IsValid && OfficialMatch.Red1TeamID != null && OfficialMatch.Red2TeamID != null && OfficialMatch.Blue1TeamID != null && OfficialMatch.Blue2TeamID != null)
            {
                return Page();
            }
            OfficialMatch.EventID = EventID;
            _context.OfficialMatch.Add(OfficialMatch);

            IList<Models.OfficialMatch> AuthorizedOfficialMatches = await _context.OfficialMatch.Where(s => s.EventID == EventID).ToListAsync();
            AuthorizedOfficialMatches.Add(OfficialMatch);
            AuthorizedTeams = await _context.Team.AsNoTracking().Where(t => t.EventID == EventID).ToListAsync();
            IList<Team> TeamsWithScores = CalculateTeamMetrics.CalculateAllMetrics(AuthorizedTeams, AuthorizedOfficialMatches);

            _context.Team.UpdateRange(TeamsWithScores);
            await _context.SaveChangesAsync();

            var _EventID = EventID;
            return RedirectToPage("./Index", new
            {
                EventID = _EventID,
            });
        }

        #endregion Public Methods
    }
}