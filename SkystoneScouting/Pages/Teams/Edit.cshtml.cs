using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SkystoneScouting.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SkystoneScouting.Pages.Teams
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
        public Team Team { get; set; }

        public string teamID { get; set; }

        #endregion Public Properties

        #region Public Methods

        public async Task<IActionResult> OnGetAsync(string EventID, string TeamID)
        {
            if (TeamID == null)
            {
                return NotFound();
            }

            teamID = TeamID;
            eventID = EventID;
            Team = await _context.Team.FirstOrDefaultAsync(m => m.ID == TeamID);

            if (Team == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Team).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamExists(Team.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index", new
            {
                EventID = eventID,
                TeamID = teamID,
            });
        }

        #endregion Public Methods

        #region Private Methods

        private bool TeamExists(string TeamID)
        {
            return _context.Team.Any(e => e.ID == TeamID);
        }

        #endregion Private Methods
    }
}