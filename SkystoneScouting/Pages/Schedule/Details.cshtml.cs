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
    public class DetailsModel : PageModel
    {
        private readonly SkystoneScouting.Data.ApplicationDbContext _context;

        public DetailsModel(SkystoneScouting.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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
    }
}
