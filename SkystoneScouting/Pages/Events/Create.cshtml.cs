using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkystoneScouting.Models;
using System;
using System.Threading.Tasks;

namespace SkystoneScouting.Pages.Events
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

        [BindProperty]
        public Event Event { get; set; }

        #endregion Public Properties

        #region Public Methods

        public IActionResult OnGet()
        {
            if (!User.Identity.IsAuthenticated)
                return Forbid();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Event.AllowedUsers != String.Empty)
                Event.AllowedUsers = User.Identity.Name + "," + Event.AllowedUsers;
            else
                Event.AllowedUsers = User.Identity.Name;

            _context.Event.Add(Event);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        #endregion Public Methods
    }
}