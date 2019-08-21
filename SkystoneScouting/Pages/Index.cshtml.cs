using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SkystoneScouting.Pages
{
    public class IndexModel : PageModel
    {
        #region Public Methods

        public void OnGet()
        {
            if (User.Identity.IsAuthenticated)
                RedirectToPage("/Events");
        }

        #endregion Public Methods
    }
}