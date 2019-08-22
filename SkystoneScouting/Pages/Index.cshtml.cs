using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SkystoneScouting.Pages
{
    public class IndexModel : PageModel
    {
        #region Public Methods

        public ActionResult OnGet()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToPage("/Events/Index");
            return Page();
        }

        #endregion Public Methods
    }
}