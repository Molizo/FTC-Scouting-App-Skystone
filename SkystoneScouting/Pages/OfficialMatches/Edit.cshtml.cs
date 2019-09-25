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
    public class EditModel : PageModel
    {
        #region Private Fields

        private readonly SkystoneScouting.Data.ApplicationDbContext _context;

        #endregion Private Fields

        #region Public Constructors

        public EditModel(SkystoneScouting.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion Public Constructors

        #region Public Properties

        public IList<Team> AuthorizedTeams { get; set; }
        public string eventID { get; set; }

        [BindProperty]
        public OfficialMatch OfficialMatch { get; set; }

        #endregion Public Properties

        #region Public Methods

        public async Task<IActionResult> OnGetAsync(string EventID, string OfficialMatchID)
        {
            if (EventID == null || OfficialMatchID == null)
                return NotFound();
            if (!AuthorizationCheck.OfficialMatch(_context, OfficialMatchID, User.Identity.Name))
                return Forbid();

            eventID = EventID;
            OfficialMatch = await _context.OfficialMatch.FirstOrDefaultAsync(m => m.ID == OfficialMatchID);
            AuthorizedTeams = await _context.Team.AsNoTracking().Where(t => t.EventID == EventID).OrderBy(t => t.TeamNumber.PadLeft(5)).ToListAsync();
            if (OfficialMatch == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<ActionResult> OnGetQuickAddTeam(string EventID, string TeamNumber, string TeamName, string OfficialMatchID)
        {
            if (EventID == null)
                return NotFound();
            if (!AuthorizationCheck.Event(_context, EventID, User.Identity.Name))
                return Forbid();

            Team Team = new Team
            {
                EventID = EventID,
                TeamNumber = TeamNumber,
                TeamName = TeamName
            };

            await _context.AddAsync(Team);
            await _context.SaveChangesAsync();

            var _EventID = EventID;
            var _OfficialMatchID = OfficialMatchID;
            return RedirectToPage("./Edit", new { EventID = _EventID, OfficialMatchID = _OfficialMatchID, });
        }

        public async Task<IActionResult> OnPostAsync(string EventID, string OfficialMatchID)
        {
            if (EventID == null || OfficialMatchID == null)
                return NotFound();

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(OfficialMatch).State = EntityState.Modified;

            IList<Models.OfficialMatch> AuthorizedOfficialMatches = await _context.OfficialMatch.Where(s => s.EventID == EventID).ToListAsync();
            AuthorizedTeams = await _context.Team.AsNoTracking().Where(t => t.EventID == EventID).ToListAsync();
            IList<Team> TeamsWithScores = CalculateTeamMetrics.CalculateAllMetrics(AuthorizedTeams, AuthorizedOfficialMatches);

            _context.Team.UpdateRange(TeamsWithScores);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OfficialMatchExists(OfficialMatch.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            eventID = EventID;
            return RedirectToPage("./Index", new
            {
                EventID = eventID,
            });
        }

        #endregion Public Methods

        #region Private Methods

        private bool OfficialMatchExists(string id)
        {
            return _context.OfficialMatch.Any(e => e.ID == id);
        }

        #endregion Private Methods
    }
}