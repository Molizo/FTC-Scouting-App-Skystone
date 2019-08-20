using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SkystoneScouting.Models;
using SkystoneScouting.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkystoneScouting.Pages.Events
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
        public Event Event { get; set; }

        public string EventID { get; set; }

        #endregion Public Properties

        #region Public Methods

        public async Task<IActionResult> OnGetAsync(string EventID)
        {
            if (EventID == null)
            {
                return NotFound();
            }

            if (!User.Identity.IsAuthenticated || !AuthorizationCheck.Event(_context, EventID, User.Identity.Name))
                return Forbid();

            Event = await _context.Event.FirstOrDefaultAsync(m => m.ID == EventID);

            if (Event == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string EventID)
        {
            if (EventID == null)
            {
                return NotFound();
            }

            Event = await _context.Event.FindAsync(EventID);

            if (Event != null)
            {
                List<Team> RemovedTeams = await _context.Team.Where(t => t.EventID == EventID).ToListAsync();
                List<OfficialMatch> RemovedOfficialMatches = await _context.OfficialMatch.Where(t => t.EventID == EventID).ToListAsync();
                _context.Team.RemoveRange(RemovedTeams);
                _context.OfficialMatch.RemoveRange(RemovedOfficialMatches);
                _context.Event.Remove(Event);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }

        #endregion Public Methods
    }
}