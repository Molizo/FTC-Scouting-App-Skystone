using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using RestSharp;
using SkystoneScouting.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SkystoneScouting.Pages.Events
{
    public class ImportModel : PageModel
    {
        #region Private Fields

        private readonly SkystoneScouting.Data.ApplicationDbContext _context;

        #endregion Private Fields

        #region Public Constructors

        public ImportModel(SkystoneScouting.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion Public Constructors

        #region Public Properties

        [BindProperty]
        public string TOAEventKey { get; set; }

        #endregion Public Properties

        #region Public Methods

        public IActionResult OnGet()
        {
            if (!User.Identity.IsAuthenticated)
                return Forbid();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Event Event = new Event();
            List<Team> Teams = new List<Team>();
            var request = new RestRequest();

            var eventClient = new RestClient("https://theorangealliance.org/api/event/" + TOAEventKey);
            eventClient.AddDefaultHeader("Content-Type", "application/json");
            eventClient.AddDefaultHeader("X-TOA-Key", "2c34a76a8b426e3f9bc7bb5d6b309196d3ef9a80978d8a1d3eef769280a7d491");
            eventClient.AddDefaultHeader("X-Application-Origin", "QR-FTCScoutingAppSkystone");
            var response = eventClient.Get(request);

            if (response.Content.Contains("\"_code\""))
                return RedirectToPage("/Error");

            TOAEvent TOAEvent = JsonConvert.DeserializeObject<TOAEvent>(response.Content.TrimStart('[').TrimEnd(']'));

            Event.Name = TOAEvent.event_name;
            Event.AllowedUsers = User.Identity.Name + ',';
            Event.Address = TOAEvent.venue;
            Event.StartDate = DateTime.Parse(TOAEvent.start_date);
            Event.EndDate = DateTime.Parse(TOAEvent.end_date);

            _context.Event.Add(Event);

            var teamsClient = new RestClient("https://theorangealliance.org/api/event/" + TOAEventKey + "/teams");
            teamsClient.AddDefaultHeader("Content-Type", "application/json");
            teamsClient.AddDefaultHeader("X-TOA-Key", "2c34a76a8b426e3f9bc7bb5d6b309196d3ef9a80978d8a1d3eef769280a7d491");
            teamsClient.AddDefaultHeader("X-Application-Origin", "QR-FTCScoutingAppSkystone");
            var teamsResponse = teamsClient.Get(request);

            List<TOATeamExtended> TOATeamsExtended = JsonConvert.DeserializeObject<List<TOATeamExtended>>(teamsResponse.Content);

            foreach (var TOATeamExtended in TOATeamsExtended)
            {
                Team Team = new Team();
                Team.EventID = Event.ID;
                Team.TeamName = TOATeamExtended.team.team_name_short;
                Team.TeamNumber = TOATeamExtended.team.team_number;
                if (TOATeamExtended.team.city != "")
                    Team.TeamAddress += TOATeamExtended.team.city + ", ";

                if (TOATeamExtended.team.state_prov != "")
                    Team.TeamAddress += TOATeamExtended.team.state_prov + ", ";

                if (TOATeamExtended.team.country != "")
                    Team.TeamAddress += TOATeamExtended.team.country + ", ";

                if (TOATeamExtended.team.zip_code != "")
                    Team.TeamAddress += TOATeamExtended.team.zip_code + ", ";

                Team.TeamAddress = Team.TeamAddress.TrimEnd(' ');
                Team.TeamAddress = Team.TeamAddress.TrimEnd(',');
                Teams.Add(Team);
            }

            _context.Team.AddRange(Teams);

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        #endregion Public Methods
    }

    [JsonObject]
    public class TOAEvent
    {
        #region Public Properties

        public string active_tournament_level { get; set; }
        public string advance_event { get; set; }
        public string advance_spots { get; set; }
        public string alliance_count { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string division_key { get; set; }
        public string division_name { get; set; }
        public string end_date { get; set; }
        public string event_code { get; set; }
        public string event_key { get; set; }
        public string event_name { get; set; }
        public string event_region_number { get; set; }
        public string event_type_key { get; set; }
        public string field_count { get; set; }
        public string is_active { get; set; }
        public string is_public { get; set; }
        public string league_key { get; set; }
        public string match_count { get; set; }
        public string region_key { get; set; }
        public string season_key { get; set; }
        public string start_date { get; set; }
        public string state_prov { get; set; }
        public string team_count { get; set; }
        public string time_zone { get; set; }
        public string venue { get; set; }
        public string website { get; set; }
        public string week_key { get; set; }

        #endregion Public Properties
    }

    [JsonObject]
    public class TOATeam
    {
        #region Public Properties

        public string city { get; set; }
        public string country { get; set; }
        public string last_active { get; set; }
        public string league_key { get; set; }
        public string region_key { get; set; }
        public string robot_name { get; set; }
        public string rookie_year { get; set; }
        public string state_prov { get; set; }
        public string team_key { get; set; }

        public string team_name_long { get; set; }
        public string team_name_short { get; set; }
        public string team_number { get; set; }

        public string website { get; set; }
        public string zip_code { get; set; }

        #endregion Public Properties
    }

    [JsonObject]
    public class TOATeamExtended
    {
        #region Public Properties

        public string card_status { get; set; }
        public string event_key { get; set; }
        public string event_participant_key { get; set; }
        public string is_active { get; set; }
        public TOATeam team { get; set; }
        public string team_key { get; set; }
        public string team_number { get; set; }

        #endregion Public Properties
    }
}