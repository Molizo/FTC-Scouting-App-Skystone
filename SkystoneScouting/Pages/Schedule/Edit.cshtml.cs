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

namespace SkystoneScouting.Pages.Schedule
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
        public ScheduledMatch ScheduledMatch { get; set; }

        #endregion Public Properties

        #region Public Methods

        public async Task<IActionResult> OnGetAsync(string EventID, string ScheduledMatchID)
        {
            if (ScheduledMatchID == null)
            {
                return NotFound();
            }

            if (!Services.AuthorizationCheck.ScheduledMatch(_context, ScheduledMatchID, User.Identity.Name))
                return Forbid();

            eventID = EventID;
            ScheduledMatch = await _context.ScheduledMatch.FirstOrDefaultAsync(m => m.ID == ScheduledMatchID);
            AuthorizedTeams = await _context.Team.Where(t => t.EventID == EventID).ToListAsync();
            if (ScheduledMatch == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string EventID, string ScheduledMatchID)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ScheduledMatch).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScheduledMatchExists(ScheduledMatch.ID))
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

        private bool ScheduledMatchExists(string id)
        {
            return _context.ScheduledMatch.Any(e => e.ID == id);
        }

        #endregion Private Methods
    }
}