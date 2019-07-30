using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SkystoneScouting.Models;

namespace SkystoneScouting.Services
{
    public class AuthorizationCheck
    {
        #region Public Methods

        public static bool Event(SkystoneScouting.Data.ApplicationDbContext context, string EventID, string Username)
        {
            IList<Event> Events = new List<Event>();
            Events = context.Event.ToList();
            foreach (var Event in Events)
            {
                if (Event.ID == EventID && Event.AllowedUsers != String.Empty)
                {
                    try
                    {
                        if (Event.AllowedUsers.Split(',').ToList().Contains(Username))
                            return true;
                    }
                    catch
                    {
                        if (Event.AllowedUsers == Username)
                            return true;
                    }
                }
            }
            return false;
        }

        public static bool Team(SkystoneScouting.Data.ApplicationDbContext context, string TeamID, string Username)
        {
            IList<Team> Teams = new List<Team>();
            Teams = context.Team.ToList();
            foreach (var Team in Teams)
            {
                if (Team.ID == TeamID && AuthorizationCheck.Event(context, Team.EventID, Username))
                    return true;
            }
            return false;
        }

        #endregion Public Methods
    }
}