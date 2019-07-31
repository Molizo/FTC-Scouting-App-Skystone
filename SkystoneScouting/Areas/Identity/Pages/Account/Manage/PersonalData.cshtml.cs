using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace SkystoneScouting.Areas.Identity.Pages.Account.Manage
{
    public class PersonalDataModel : PageModel
    {
        #region Private Fields

        private readonly ILogger<PersonalDataModel> _logger;
        private readonly UserManager<IdentityUser> _userManager;

        #endregion Private Fields

        #region Public Constructors

        public PersonalDataModel(
            UserManager<IdentityUser> userManager,
            ILogger<PersonalDataModel> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            return Page();
        }

        #endregion Public Methods
    }
}