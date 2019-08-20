using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SkystoneScouting.Data;
using SkystoneScouting.Models;

namespace SkystoneScouting.Pages.ScoutedMatches
{
    public class DeleteModel : PageModel
    {
        private readonly SkystoneScouting.Data.ApplicationDbContext _context;

        public DeleteModel(SkystoneScouting.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ScoutedMatch ScoutedMatch { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ScoutedMatch = await _context.ScoutedMatch.FirstOrDefaultAsync(m => m.ID == id);

            if (ScoutedMatch == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ScoutedMatch = await _context.ScoutedMatch.FindAsync(id);

            if (ScoutedMatch != null)
            {
                _context.ScoutedMatch.Remove(ScoutedMatch);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
