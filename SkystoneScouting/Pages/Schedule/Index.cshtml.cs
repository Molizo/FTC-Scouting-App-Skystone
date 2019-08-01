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
    public class IndexModel : PageModel
    {
        private readonly SkystoneScouting.Data.ApplicationDbContext _context;

        public IndexModel(SkystoneScouting.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<ScheduledMatch> ScheduledMatch { get;set; }

        public async Task OnGetAsync()
        {
            ScheduledMatch = await _context.ScheduledMatch.ToListAsync();
        }
    }
}
