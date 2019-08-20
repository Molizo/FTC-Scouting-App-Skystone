using Microsoft.EntityFrameworkCore;
using SkystoneScouting.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SkystoneScouting.Services
{
    public class AuthorizationCheck
    {
        #region Public Methods

        public static bool Event(SkystoneScouting.Data.ApplicationDbContext context, string EventID, string Username)
        {
            IList<Event> Events = new List<Event>();
            Events = context.Event.AsNoTracking().ToList();
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

        public static bool OfficialMatch(SkystoneScouting.Data.ApplicationDbContext context, string OfficialMatchID, string Username)
        {
            IList<OfficialMatch> OfficialMatches = new List<OfficialMatch>();
            OfficialMatches = context.OfficialMatch.AsNoTracking().ToList();
            foreach (var OfficialMatch in OfficialMatches)
            {
                if (OfficialMatch.ID == OfficialMatchID && AuthorizationCheck.Event(context, OfficialMatch.EventID, Username))
                    return true;
            }
            return false;
        }

        public static bool ScoutedMatch(Data.ApplicationDbContext context, string ScoutedMatchID, string Username)
        {
            IList<ScoutedMatch> ScoutedMatches = new List<ScoutedMatch>();
            ScoutedMatches = context.ScoutedMatch.AsNoTracking().ToList();
            foreach (var ScoutedMatch in ScoutedMatches)
            {
                if (ScoutedMatch.ID == ScoutedMatchID && AuthorizationCheck.Team(context, ScoutedMatch.TeamID, Username))
                    return true;
            }
            return false;
        }

        public static bool Team(SkystoneScouting.Data.ApplicationDbContext context, string TeamID, string Username)
        {
            IList<Team> Teams = new List<Team>();
            Teams = context.Team.AsNoTracking().ToList();
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