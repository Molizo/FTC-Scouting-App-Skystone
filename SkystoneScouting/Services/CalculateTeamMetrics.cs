using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SkystoneScouting.Models;

namespace SkystoneScouting.Services
{
    public class CalculateTeamMetrics
    {
        #region Public Methods

        public static IList<Team> AvgRPandAvgTBP(IList<Team> Teams, IList<ScheduledMatch> Matches)
        {
            foreach (var Team in Teams)
            {
                List<int?> AvgTBPScores = new List<int?>();
                foreach (var Match in Matches)
                {
                    if (Match.Red1TeamID == Team.ID || Match.Red2TeamID == Team.ID)
                    {
                        if (Match.RedScore > Match.BlueScore)
                            Team.AvgRP += 2;
                        else if (Match.RedScore == Match.BlueScore)
                            Team.AvgRP++;
                        AvgTBPScores.Add(Match.BlueScore);
                    }
                    else if (Match.Red1TeamID == Team.ID || Match.Red2TeamID == Team.ID)
                    {
                        if (Match.BlueScore > Match.RedScore)
                            Team.AvgRP += 2;
                        else if (Match.BlueScore == Match.RedScore)
                            Team.AvgRP++;
                        AvgTBPScores.Add(Match.RedScore);
                    }
                }
                Team.AvgRP /= AvgTBPScores.Count;

                AvgTBPScores.Remove(AvgTBPScores.AsQueryable().OrderBy(e => e.GetValueOrDefault()).First());
                if (AvgTBPScores.Count >= 7)
                    AvgTBPScores.Remove(AvgTBPScores.AsQueryable().OrderBy(e => e.GetValueOrDefault()).First());

                foreach (var Score in AvgTBPScores)
                    Team.AvgTBP += Score;
                Team.AvgTBP /= AvgTBPScores.Count;

                Team.AvgRP = Math.Round(Team.AvgRP.GetValueOrDefault(), 1);
                Team.AvgTBP = Math.Round(Team.AvgTBP.GetValueOrDefault(), 1);
            }
            return Teams;
        }

        public static IList<Team> CalculateAllMetrics(IList<Team> Teams, IList<ScheduledMatch> Matches)
        {
            Teams = AvgRPandAvgTBP(Teams, Matches);
            return Teams;
        }

        #endregion Public Methods
    }
}