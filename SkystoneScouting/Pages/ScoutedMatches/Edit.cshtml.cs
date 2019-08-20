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

namespace SkystoneScouting.Pages.ScoutedMatches
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

        public string eventID { get; set; }

        [BindProperty]
        public ScoutedMatch ScoutedMatch { get; set; }

        public string teamID { get; set; }

        #endregion Public Properties

        #region Public Methods

        public async Task<IActionResult> OnGetAsync(string EventID, string TeamID, string ScoutedMatchID)
        {
            if (EventID == null || TeamID == null || ScoutedMatchID == null)
                return NotFound();
            if (!AuthorizationCheck.ScoutedMatch(_context, ScoutedMatchID, User.Identity.Name))
                return Forbid();

            eventID = EventID;
            teamID = TeamID;

            ScoutedMatch = await _context.ScoutedMatch.FirstOrDefaultAsync(s => s.ID == ScoutedMatchID);

            if (ScoutedMatch == null)
                return NotFound();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string EventID, string TeamID, string ScoutedMatchID)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (EventID == null || TeamID == null || ScoutedMatchID == null)
                return NotFound();
            if (!AuthorizationCheck.ScoutedMatch(_context, ScoutedMatchID, User.Identity.Name))
                return Forbid();

            eventID = EventID;
            teamID = TeamID;

            _context.Attach(ScoutedMatch).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScoutedMatchExists(ScoutedMatch.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index", new { EventID = eventID, TeamID = teamID, });
        }

        #endregion Public Methods

        #region Private Methods

        private bool ScoutedMatchExists(string id)
        {
            return _context.ScoutedMatch.Any(e => e.ID == id);
        }

        #endregion Private Methods
    }
}