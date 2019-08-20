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

namespace SkystoneScouting.Pages.OfficialMatches
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
        public OfficialMatch OfficialMatch { get; set; }

        #endregion Public Properties

        #region Public Methods

        public async Task<IActionResult> OnGetAsync(string EventID, string OfficialMatchID)
        {
            if (EventID == null || OfficialMatchID == null)
                return NotFound();
            if (!AuthorizationCheck.OfficialMatch(_context, OfficialMatchID, User.Identity.Name))
                return Forbid();

            OfficialMatch = await _context.OfficialMatch.FirstOrDefaultAsync(m => m.ID == OfficialMatchID);

            if (OfficialMatch == null)
                return NotFound();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string EventID, string OfficialMatchID)
        {
            if (EventID == null || OfficialMatchID == null)
                return NotFound();
            if (!AuthorizationCheck.OfficialMatch(_context, OfficialMatchID, User.Identity.Name))
                return Forbid();

            OfficialMatch = await _context.OfficialMatch.FindAsync(OfficialMatchID);

            if (OfficialMatch != null)
            {
                _context.OfficialMatch.Remove(OfficialMatch);
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