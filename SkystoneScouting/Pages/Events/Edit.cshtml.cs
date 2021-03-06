﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SkystoneScouting.Models;
using SkystoneScouting.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SkystoneScouting.Pages.Events
{
    public class EditModel : PageModel
    {
        #region Private Fields

        private readonly SkystoneScouting.Data.ApplicationDbContext _context;

        #endregion Private Fields

        #region Public Constructors

        public EditModel(SkystoneScouting.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion Public Constructors

        #region Public Properties

        [BindProperty]
        public Event Event { get; set; }

        #endregion Public Properties

        #region Public Methods

        public async Task<IActionResult> OnGetAsync(string EventID)
        {
            // Authorization checks
            if (!User.Identity.IsAuthenticated || !AuthorizationCheck.Event(_context, EventID, User.Identity.Name))
                return Forbid();

            if (EventID == null)
            {
                return NotFound();
            }

            Event = await _context.Event.FirstOrDefaultAsync(m => m.ID == EventID);

            if (Event == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //Fixes for the allowed user tags so no one user can remove themselves from the event
            if (Event.AllowedUsers != String.Empty)
            {
                try
                {
                    if (!Event.AllowedUsers.Split(',').ToList().Contains(User.Identity.Name))
                        Event.AllowedUsers = User.Identity.Name + "," + Event.AllowedUsers;
                }
                catch
                {
                    if (Event.AllowedUsers != User.Identity.Name)
                        Event.AllowedUsers = User.Identity.Name + "," + Event.AllowedUsers;
                }
            }
            else
                Event.AllowedUsers = User.Identity.Name;

            _context.Attach(Event).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(Event.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        #endregion Public Methods

        #region Private Methods

        private bool EventExists(string EventID)
        {
            return _context.Event.Any(e => e.ID == EventID);
        }

        #endregion Private Methods
    }
}