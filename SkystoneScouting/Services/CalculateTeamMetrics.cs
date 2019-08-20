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

        public static IList<Team> AvgRPandAvgTBP(IList<Team> Teams, IList<OfficialMatch> Matches)
        {
            foreach (var Team in Teams)
            {
                Team.AvgRP = 0;
                Team.AvgTBP = 0;
                List<int?> AvgTBPScores = new List<int?>();
                foreach (var Match in Matches)
                {
                    if ((Match.Red1TeamID == Team.ID && !Match.Red1Surrogate) || (Match.Red2TeamID == Team.ID && !Match.Red2Surrogate))
                    {
                        if (Match.RedScore > Match.BlueScore)
                            Team.AvgRP += 2;
                        else if (Match.RedScore == Match.BlueScore)
                            Team.AvgRP++;
                        AvgTBPScores.Add(Match.BlueScore);
                    }
                    else if ((Match.Blue1TeamID == Team.ID && !Match.Blue1Surrogate) || (Match.Blue2TeamID == Team.ID && !Match.Blue2Surrogate))
                    {
                        if (Match.BlueScore > Match.RedScore)
                            Team.AvgRP += 2;
                        else if (Match.BlueScore == Match.RedScore)
                            Team.AvgRP++;
                        AvgTBPScores.Add(Match.RedScore);
                    }
                }
                if (AvgTBPScores.Count > 0)
                    Team.AvgRP /= AvgTBPScores.Count;

                if (AvgTBPScores.Count > 1)
                    AvgTBPScores.Remove(AvgTBPScores.AsQueryable().OrderBy(e => e.GetValueOrDefault()).First());
                if (AvgTBPScores.Count >= 7)
                    AvgTBPScores.Remove(AvgTBPScores.AsQueryable().OrderBy(e => e.GetValueOrDefault()).First());

                foreach (var Score in AvgTBPScores)
                    Team.AvgTBP += Score;

                if (AvgTBPScores.Count > 0)
                    Team.AvgTBP /= AvgTBPScores.Count;

                Team.AvgRP = System.Math.Round(Team.AvgRP.GetValueOrDefault(), 2);
                Team.AvgTBP = System.Math.Round(Team.AvgTBP.GetValueOrDefault(), 2);
            }
            return Teams;
        }

        public static IList<Team> CalculateAllMetrics(IList<Team> Teams, IList<OfficialMatch> Matches)
        {
            Teams = AvgRPandAvgTBP(Teams, Matches);
            if (IsODCComputeable(Teams, Matches))
            {
                Teams = OPR(Teams, Matches);
                Teams = DPR(Teams, Matches);
                foreach (var Team in Teams)
                    Team.CCWM = System.Math.Round(Team.OPR.GetValueOrDefault() - Team.DPR.GetValueOrDefault(), 2);
            }
            else
                foreach (var Team in Teams)
                {
                    Team.OPR = null;
                    Team.DPR = null;
                    Team.CCWM = null;
                }
            return Teams;
        }

        public static IList<Team> DPR(IList<Team> Teams, IList<OfficialMatch> Matches)
        {
            //Variable initialisation
            double EPS = 0.0000001d;
            int MAXN = 1000;
            double[] X = new double[MAXN];
            IList<int> sum = new List<int>();
            int[,] teams = new int[1000, 1000];

            //Prepare Datatset

            // {{Team red 1, Team red 2, Blue team 1, Blue Team 2, RedScore, BlueScore},... - > i = Match nr &  j = Team nr {{0, 0, 0, 1, ...},...}
            //System.Diagnostics.Trace.TraceInformation("Preparing DPR dataset");

            foreach (var match in Matches)
            {
                sum.Add(match.BlueScore.GetValueOrDefault());
                sum.Add(match.RedScore.GetValueOrDefault());
            }
            //System.Diagnostics.Trace.TraceInformation("Added scores to sum");
            for (int teamNr = 0; teamNr < Teams.Count; teamNr++)
            {
                for (int matchNr = 0; matchNr < Matches.Count * 2; matchNr += 2)
                {
                    if (Teams[teamNr].ID == Matches[matchNr / 2].Red1TeamID || Teams[teamNr].ID == Matches[matchNr / 2].Red2TeamID)
                        teams[matchNr, teamNr] = 1;
                    else if (Teams[teamNr].ID == Matches[matchNr / 2].Red1TeamID || Teams[teamNr].ID == Matches[matchNr / 2].Red2TeamID)
                        teams[matchNr + 1, teamNr] = 1;
                    else
                        teams[matchNr, teamNr] = 0;
                    //System.Diagnostics.Trace.TraceInformation(teams[matchNr, teamNr].ToString());
                    //System.Diagnostics.Trace.TraceInformation(teams[matchNr + 1, teamNr].ToString());
                }
                //System.Diagnostics.Trace.TraceInformation(Teams[teamNr].teamID);
            }

            //Compute OPR

            //System.Diagnostics.Trace.TraceInformation("Computing DPR");
            int n = 0, m = 0;
            n = Matches.Count * 2;
            m = Teams.Count;
            int i = 0, j = 0, k;

            int[,] v1 = new int[n + 1, m + 1];
            int[,] v2 = new int[m + 1, n + 1];
            int[,] v3 = new int[n + 1, n + 1];
            int[] v4 = new int[n + 1];
            double[,] A = new double[MAXN, MAXN];

            System.Diagnostics.Trace.TraceInformation(n + " " + m);

            for (i = 0; i < n; i++)
                sum[i] = sum[i];

            for (i = 0; i < n; i++)
                for (j = 0; j < m; j++)
                {
                    v1[i, j] = teams[i, j];
                    v2[j, i] = v1[i, j];
                }
            for (i = 0; i < m; i++)
                for (j = 0; j < m; j++)
                {
                    v3[i, j] = 0;
                    for (k = 0; k <= n; k++)
                    {
                        //System.Diagnostics.Debug.WriteLine("{0} {1} {2}", i, j, k);
                        v3[i, j] += v2[i, k] * v1[k, j];
                    }
                }
            for (i = 0; i < m; i++)
            {
                for (j = 0; j < m; j++)
                {
                    A[i + 1, j + 1] = v3[i, j];
                    //System.Diagnostics.Trace.TraceInformation(v3[i, j].ToString());
                }
                v4[i] = 0;
                for (k = 0; k < n; k++)
                {
                    v4[i] += v2[i, k] * sum[k];
                }
                A[i + 1, m + 1] = v4[i];
                //System.Diagnostics.Trace.TraceInformation(v4[i].ToString());
            }
            int N = m;
            int M = m;
            i = 1;
            j = 1;
            double aux;

            while (i <= N && j <= M)
            {
                for (k = i; k <= N; k++)
                    if (A[k, j] < -EPS || A[k, j] > EPS)
                        break;

                if (k == N + 1)
                {
                    j++;
                    continue;
                }

                if (k != i)
                    for (int l = 1; l <= M + 1; ++l)
                    {
                        aux = A[i, l];
                        A[i, l] = A[k, l];
                        A[k, l] = aux;
                    }

                for (int l = j + 1; l <= M + 1; ++l)
                    A[i, l] = A[i, l] / A[i, j];
                A[i, j] = 1;

                for (int u = i + 1; u <= N; ++u)
                {
                    for (int l = j + 1; l <= M + 1; ++l)
                        A[u, l] -= A[u, j] * A[i, l];
                    A[u, j] = 0;
                }

                i++;
                j++;
            }

            for (i = N; i > 0; --i)
                for (j = 1; j <= M + 1; ++j)
                    if (A[i, j] > EPS || A[i, j] < -EPS)
                    {
                        if (j == M + 1)
                        {
                            throw new Exception("DPR error ocuured - Mathematical Error");
                        }

                        X[j] = A[i, M + 1];
                        for (k = j + 1; k <= M; ++k)
                            X[j] -= X[k] * A[i, k];

                        break;
                    }

            //for (i = 1; i <= M; ++i)
            //    System.Diagnostics.Trace.TraceInformation(Teams[i - 1].teamID + " " + X[i]);
            //System.Diagnostics.Trace.TraceInformation("Finished computing DPR");

            //Assign DPR scores to teams
            for (i = 0; i < Teams.Count; i++)
            {
                //System.Diagnostics.Trace.TraceInformation(Teams[i].teamID + " " + X[i + 1]);
                Teams[i].DPR = System.Math.Round(X[i + 1], 2);
            }
            return Teams;
        }

        public static bool IsODCComputeable(IList<Team> Teams, IList<OfficialMatch> Matches)
        {
            IList<int> teamsMatchCounter = new List<int>();

            for (int team = 0; team < Teams.Count; team++)
                teamsMatchCounter.Add(0);
            for (int team = 0; team < Teams.Count; team++)
            {
                for (int match = 0; match < Matches.Count; match++)
                {
                    if (Teams[team].ID == Matches[match].Red1TeamID || Teams[team].ID == Matches[match].Red2TeamID || Teams[team].ID == Matches[match].Blue1TeamID || Teams[team].ID == Matches[match].Blue2TeamID)
                        teamsMatchCounter[team]++;
                }
            }
            if (teamsMatchCounter.Contains(1) || teamsMatchCounter.Contains(0))
            {
                //System.Diagnostics.Trace.TraceWarning("OPR is not computable - Too few matches");
                return false;
            }
            else
                return true;
        }

        public static IList<Team> OPR(IList<Team> Teams, IList<OfficialMatch> Matches)
        {
            //Variable initialisation
            double EPS = 0.0000001d;
            int MAXN = 1000;
            double[] X = new double[MAXN];
            IList<int> sum = new List<int>();
            int[,] teams = new int[1000, 1000];

            //Prepare Datatset

            // {{Team red 1, Team red 2, Blue team 1, Blue Team 2, RedScore, BlueScore},... - > i = Match nr &  j = Team nr {{0, 0, 0, 1, ...},...}
            //System.Diagnostics.Trace.TraceInformation("Preparing OPR dataset");

            foreach (var match in Matches)
            {
                sum.Add(match.RedScore.GetValueOrDefault());
                sum.Add(match.BlueScore.GetValueOrDefault());
            }
            //System.Diagnostics.Trace.TraceInformation("Added scores to sum");
            for (int teamNr = 0; teamNr < Teams.Count; teamNr++)
            {
                for (int matchNr = 0; matchNr < Matches.Count * 2; matchNr += 2)
                {
                    if (Teams[teamNr].ID == Matches[matchNr / 2].Red1TeamID || Teams[teamNr].ID == Matches[matchNr / 2].Red2TeamID)
                        teams[matchNr, teamNr] = 1;
                    else if (Teams[teamNr].ID == Matches[matchNr / 2].Red1TeamID || Teams[teamNr].ID == Matches[matchNr / 2].Red2TeamID)
                        teams[matchNr + 1, teamNr] = 1;
                    else
                        teams[matchNr, teamNr] = 0;
                    //System.Diagnostics.Trace.TraceInformation(teams[matchNr, teamNr].ToString());
                    //System.Diagnostics.Trace.TraceInformation(teams[matchNr + 1, teamNr].ToString());
                }
                //System.Diagnostics.Trace.TraceInformation(Teams[teamNr].teamID);
            }

            //Compute OPR

            //System.Diagnostics.Trace.TraceInformation("Computing OPR");
            int n = 0, m = 0;
            n = Matches.Count * 2;
            m = Teams.Count;
            int i = 0, j = 0, k;

            int[,] v1 = new int[n + 1, m + 1];
            int[,] v2 = new int[m + 1, n + 1];
            int[,] v3 = new int[n + 1, n + 1];
            int[] v4 = new int[n + 1];
            double[,] A = new double[MAXN, MAXN];

            System.Diagnostics.Trace.TraceInformation(n + " " + m);

            for (i = 0; i < n; i++)
                sum[i] = sum[i];

            for (i = 0; i < n; i++)
                for (j = 0; j < m; j++)
                {
                    v1[i, j] = teams[i, j];
                    v2[j, i] = v1[i, j];
                }
            for (i = 0; i < m; i++)
                for (j = 0; j < m; j++)
                {
                    v3[i, j] = 0;
                    for (k = 0; k <= n; k++)
                    {
                        //System.Diagnostics.Debug.WriteLine("{0} {1} {2}", i, j, k);
                        v3[i, j] += v2[i, k] * v1[k, j];
                    }
                }
            for (i = 0; i < m; i++)
            {
                for (j = 0; j < m; j++)
                {
                    A[i + 1, j + 1] = v3[i, j];
                    //System.Diagnostics.Trace.TraceInformation(v3[i, j].ToString());
                }
                v4[i] = 0;
                for (k = 0; k < n; k++)
                {
                    v4[i] += v2[i, k] * sum[k];
                }
                A[i + 1, m + 1] = v4[i];
                //System.Diagnostics.Trace.TraceInformation(v4[i].ToString());
            }
            int N = m;
            int M = m;
            i = 1;
            j = 1;
            double aux;

            while (i <= N && j <= M)
            {
                for (k = i; k <= N; k++)
                    if (A[k, j] < -EPS || A[k, j] > EPS)
                        break;

                if (k == N + 1)
                {
                    j++;
                    continue;
                }

                if (k != i)
                    for (int l = 1; l <= M + 1; ++l)
                    {
                        aux = A[i, l];
                        A[i, l] = A[k, l];
                        A[k, l] = aux;
                    }

                for (int l = j + 1; l <= M + 1; ++l)
                    A[i, l] = A[i, l] / A[i, j];
                A[i, j] = 1;

                for (int u = i + 1; u <= N; ++u)
                {
                    for (int l = j + 1; l <= M + 1; ++l)
                        A[u, l] -= A[u, j] * A[i, l];
                    A[u, j] = 0;
                }

                i++;
                j++;
            }

            for (i = N; i > 0; --i)
                for (j = 1; j <= M + 1; ++j)
                    if (A[i, j] > EPS || A[i, j] < -EPS)
                    {
                        if (j == M + 1)
                        {
                            throw new Exception("OPR error ocuured - Mathematical Error");
                        }

                        X[j] = A[i, M + 1];
                        for (k = j + 1; k <= M; ++k)
                            X[j] -= X[k] * A[i, k];

                        break;
                    }

            //for (i = 1; i <= M; ++i)
            //    System.Diagnostics.Trace.TraceInformation(Teams[i - 1].teamID + " " + X[i]);
            //System.Diagnostics.Trace.TraceInformation("Finished computing OPR");

            //Assign OPR scores to teams
            for (i = 0; i < Teams.Count; i++)
            {
                //System.Diagnostics.Trace.TraceInformation(Teams[i].teamID + " " + X[i + 1]);
                Teams[i].OPR = System.Math.Round(X[i + 1], 2);
            }
            return Teams;
        }

        #endregion Public Methods
    }
}