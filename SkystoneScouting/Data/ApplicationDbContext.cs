using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SkystoneScouting.Models;

namespace SkystoneScouting.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<SkystoneScouting.Models.Event> Event { get; set; }
        public DbSet<SkystoneScouting.Models.Team> Team { get; set; }
    }
}
