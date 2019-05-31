﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SkystoneScouting.Data;
using SkystoneScouting.Models;

namespace SkystoneScouting.Pages.Events
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

        public IList<Event> AllEvents { get; set; }
        public IList<Event> AuthorizedEvents { get; set; }
        public IList<Event> CurrentEvents { get; set; }
        public IList<Event> PastEvents { get; set; }
        public IList<Event> UpcomingEvents { get; set; }

        #endregion Public Properties

        #region Public Methods

        public async Task OnGetAsync()
        {
            if (!User.Identity.IsAuthenticated)
            {
                throw new Exception("Permission error - This session is not authorised to access this area");
            }
            AllEvents = await _context.Event.ToListAsync();
            AuthorizedEvents = new List<Event>();
            CurrentEvents = new List<Event>();
            UpcomingEvents = new List<Event>();
            PastEvents = new List<Event>();
            foreach (var Event in AllEvents)
            {
                if (Event.AllowedUsers == User.Identity.Name)
                    AuthorizedEvents.Add(Event);
            }
            foreach (var Event in AuthorizedEvents)
            {
                if (Event.StartDate <= DateTime.Today && Event.EndDate >= DateTime.Today)
                    CurrentEvents.Add(Event);
                else if (Event.StartDate > DateTime.Today)
                    UpcomingEvents.Add(Event);
                else
                    PastEvents.Add(Event);
            }
        }

        #endregion Public Methods
    }
}