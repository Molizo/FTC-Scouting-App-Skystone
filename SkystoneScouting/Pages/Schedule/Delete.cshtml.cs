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
        public ScheduledMatch ScheduledMatch { get; set; }

        #endregion Public Properties

        #region Public Methods

        public async Task<IActionResult> OnGetAsync(string EventID, string ScheduledMatchID)
        {
            if (EventID == null || ScheduledMatchID == null)
                return NotFound();
            if (!AuthorizationCheck.ScheduledMatch(_context, ScheduledMatchID, User.Identity.Name))
                return Forbid();

            ScheduledMatch = await _context.ScheduledMatch.FirstOrDefaultAsync(m => m.ID == ScheduledMatchID);

            if (ScheduledMatch == null)
                return NotFound();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string EventID, string ScheduledMatchID)
        {
            if (EventID == null || ScheduledMatchID == null)
                return NotFound();
            if (!AuthorizationCheck.ScheduledMatch(_context, ScheduledMatchID, User.Identity.Name))
                return Forbid();

            ScheduledMatch = await _context.ScheduledMatch.FindAsync(ScheduledMatchID);

            if (ScheduledMatch != null)
            {
                _context.ScheduledMatch.Remove(ScheduledMatch);
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