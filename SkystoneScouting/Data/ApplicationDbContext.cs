using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SkystoneScouting.Models;

namespace SkystoneScouting.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        #region Public Constructors

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public DbSet<SkystoneScouting.Models.Event> Event { get; set; }
        public DbSet<SkystoneScouting.Models.Team> Team { get; set; }
        public DbSet<SkystoneScouting.Models.ScheduledMatch> ScheduledMatch { get; set; }
        public DbSet<SkystoneScouting.Models.UserActivity> UserActivity { get; set; }

        #endregion Public Properties
    }
}