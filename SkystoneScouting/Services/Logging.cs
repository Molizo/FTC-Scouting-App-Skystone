using SkystoneScouting.Data;
using System;
using System.Threading.Tasks;

namespace SkystoneScouting.Services
{
    public class Logging
    {
        #region Public Methods

        public static async Task LogUserActivity(ApplicationDbContext context, string Username, string Action)
        {
            Models.UserActivity userActivity = new Models.UserActivity();
            userActivity.DateTime = DateTime.UtcNow;
            userActivity.Action = Action;
            userActivity.PerformedBy = Username;
            await context.UserActivity.AddAsync(userActivity);
            _ = context.SaveChangesAsync();
        }

        #endregion Public Methods
    }
}