using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SkystoneScouting.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LockoutModel : PageModel
    {
        #region Public Methods

        public void OnGet()
        {
        }

        #endregion Public Methods
    }
}