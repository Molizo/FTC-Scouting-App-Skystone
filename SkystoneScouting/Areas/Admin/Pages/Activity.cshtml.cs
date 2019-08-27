using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SkystoneScouting.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkystoneScouting.Areas.Admin
{
    public class ActivityModel : PageModel
    {
        #region Private Fields

        private readonly SkystoneScouting.Data.ApplicationDbContext _context;

        #endregion Private Fields

        #region Public Constructors

        public ActivityModel(SkystoneScouting.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion Public Constructors

        #region Public Properties

        public IList<UserActivity> UserActivity { get; set; }

        #endregion Public Properties

        #region Public Methods

        public async Task OnGetAsync()
        {
            UserActivity = await _context.UserActivity.AsNoTracking().OrderByDescending(u => u.DateTime).Take(100).ToListAsync();
        }

        #endregion Public Methods
    }
}