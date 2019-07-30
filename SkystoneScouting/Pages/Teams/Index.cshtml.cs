using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SkystoneScouting.Data;
using SkystoneScouting.Models;
using SkystoneScouting.Services;

namespace SkystoneScouting.Pages.Teams
{
    public class IndexModel : PageModel
    {
        #region Private Fields

        private readonly SkystoneScouting.Data.ApplicationDbContext _context;

        #endregion Private Fields

        #region Public Constructors

        public IndexModel(SkystoneScouting.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion Public Constructors

        #region Public Properties

        public IList<Team> AllTeams { get; set; }
        public IList<Team> AuthorizedTeams { get; set; }
        public string eventID { get; set; }
        public IList<Team> NotScoutedTeams { get; set; }
        public IList<Team> ScoutedTeams { get; set; }

        #endregion Public Properties

        #region Public Methods

        public async Task OnGetAsync(string EventID)
        {
            eventID = EventID;
            AllTeams = await _context.Team.ToListAsync();
            AuthorizedTeams = new List<Team>();
            NotScoutedTeams = new List<Team>();
            ScoutedTeams = new List<Team>();
            foreach (var Team in AllTeams)
            {
                if (AuthorizationCheck.Team(_context, Team.ID, User.Identity.Name))
                    AuthorizedTeams.Add(Team);
            }
        }

        #endregion Public Methods
    }
}