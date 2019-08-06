using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkystoneScouting.Models;
using System.Threading.Tasks;

namespace SkystoneScouting.Pages.Teams
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

        public string eventID { get; set; }

        [BindProperty]
        public Team Team { get; set; }

        #endregion Public Properties

        #region Public Methods

        public IActionResult OnGet(string EventID)
        {
            eventID = EventID;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string EventID)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Team.EventID = EventID;
            _context.Team.Add(Team);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index", new
            {
                EventID = Team.EventID,
            });
        }

        #endregion Public Methods
    }
}