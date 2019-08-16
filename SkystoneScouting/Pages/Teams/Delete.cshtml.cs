using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SkystoneScouting.Models;
using SkystoneScouting.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkystoneScouting.Pages.Teams
{
    public class DeleteModel : PageModel
    {
        #region Private Fields

        private readonly SkystoneScouting.Data.ApplicationDbContext _context;

        #endregion Private Fields

        #region Public Constructors

        public DeleteModel(SkystoneScouting.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion Public Constructors

        #region Public Properties

        [BindProperty]
        public Team Team { get; set; }

        #endregion Public Properties

        #region Public Methods

        public async Task<IActionResult> OnGetAsync(string EventID, string TeamID)
        {
            if (EventID == null || TeamID == null)
                return NotFound();
            if (!AuthorizationCheck.Team(_context, TeamID, User.Identity.Name))
                return Forbid();

            Team = await _context.Team.FirstOrDefaultAsync(m => m.ID == TeamID);

            if (Team == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string EventID, string TeamID)
        {
            if (EventID == null || TeamID == null)
                return NotFound();
            if (!AuthorizationCheck.Team(_context, TeamID, User.Identity.Name))
                return Forbid();

            IList<ScheduledMatch> RemovedScheduledMatches = await _context.ScheduledMatch.Where(s => s.Red1TeamID == TeamID || s.Red2TeamID == TeamID || s.Blue1TeamID == TeamID || s.Blue2TeamID == TeamID).ToListAsync();
            Team = await _context.Team.FindAsync(TeamID);

            if (Team != null)
            {
                _context.Team.Remove(Team);
                _context.RemoveRange(RemovedScheduledMatches);
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