using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SkystoneScouting.Data;
using SkystoneScouting.Models;

namespace SkystoneScouting.Pages.Schedule
{
    public class CreateModel : PageModel
    {
        #region Private Fields

        private readonly SkystoneScouting.Data.ApplicationDbContext _context;

        #endregion Private Fields

        #region Public Constructors

        public CreateModel(SkystoneScouting.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion Public Constructors

        #region Public Properties

        public IList<Team> AllTeams { get; set; }
        public IList<Team> AuthorizedTeams { get; set; }

        public string eventID { get; set; }

        [BindProperty]
        public ScheduledMatch ScheduledMatch { get; set; }

        #endregion Public Properties

        #region Public Methods

        public IActionResult OnGet(string EventID)
        {
            AllTeams = _context.Team.ToList<Team>();
            AuthorizedTeams = new List<Team>();
            foreach (var Team in AllTeams)
            {
                if (Team.EventID == EventID)
                    AuthorizedTeams.Add(Team);
            }
            AuthorizedTeams = AuthorizedTeams.AsQueryable<Team>().OrderBy(s => s.TeamNumber.PadLeft(5)).ToList<Team>();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string EventID)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            ScheduledMatch.EventID = EventID;
            var _EventID = EventID;
            _context.ScheduledMatch.Add(ScheduledMatch);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index", new
            {
                EventID = _EventID,
            });
        }

        #endregion Public Methods
    }
}