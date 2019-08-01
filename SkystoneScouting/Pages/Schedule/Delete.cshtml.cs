using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SkystoneScouting.Data;
using SkystoneScouting.Models;

namespace SkystoneScouting.Pages.Schedule
{
    public class DeleteModel : PageModel
    {
        private readonly SkystoneScouting.Data.ApplicationDbContext _context;

        public DeleteModel(SkystoneScouting.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ScheduledMatch ScheduledMatch { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ScheduledMatch = await _context.ScheduledMatch.FirstOrDefaultAsync(m => m.ID == id);

            if (ScheduledMatch == null)
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

            ScheduledMatch = await _context.ScheduledMatch.FindAsync(id);

            if (ScheduledMatch != null)
            {
                _context.ScheduledMatch.Remove(ScheduledMatch);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
