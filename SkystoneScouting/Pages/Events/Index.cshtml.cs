﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SkystoneScouting.Models;
using SkystoneScouting.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<ActionResult> OnGetAsync()
        {
            if (!User.Identity.IsAuthenticated)
                return Forbid();
            AllEvents = await _context.Event.AsNoTracking().ToListAsync();
            AuthorizedEvents = new List<Event>();
            foreach (var Event in AllEvents)
            {
                if (AuthorizationCheck.Event(_context, Event.ID, User.Identity.Name))
                    AuthorizedEvents.Add(Event);
            }
            CurrentEvents = AuthorizedEvents.Where(e => e.StartDate <= DateTime.Today && e.EndDate >= DateTime.Today).ToList();
            UpcomingEvents = AuthorizedEvents.Where(e => e.StartDate > DateTime.Today).ToList();
            PastEvents = AuthorizedEvents.Where(e => e.EndDate < DateTime.Today).ToList();
            return Page();
        }

        public async Task<ActionResult> OnGetDelete(string EventID)
        {
            if (EventID == null)
                return NotFound();
            if (!AuthorizationCheck.Event(_context, EventID, User.Identity.Name))
                return Forbid();

            Event Event = await _context.Event.FindAsync(EventID);

            if (Event != null)
            {
                List<Team> RemovedTeams = await _context.Team.Where(t => t.EventID == EventID).ToListAsync();
                List<OfficialMatch> RemovedOfficialMatches = await _context.OfficialMatch.Where(t => t.EventID == EventID).ToListAsync();
                _context.Team.RemoveRange(RemovedTeams);
                _context.OfficialMatch.RemoveRange(RemovedOfficialMatches);
                _context.Event.Remove(Event);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }

        #endregion Public Methods
    }
}