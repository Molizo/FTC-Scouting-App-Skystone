using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(SkystoneScouting.Areas.Identity.IdentityHostingStartup))]

namespace SkystoneScouting.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        #region Public Methods

        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }

        #endregion Public Methods
    }
}