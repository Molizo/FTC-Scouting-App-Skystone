using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace SkystoneScouting
{
    public class Program
    {
        /*Add
            .UseKestrel()
            .UseContentRoot(Directory.GetCurrentDirectory())
            .UseIISIntegration()
          before
            .UseStartup
          for deployment to IIS*/

        #region Public Methods

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
    WebHost.CreateDefaultBuilder(args)
        .UseStartup<Startup>()
        .UseSentry("https://21c3e8d7321848eab851f9b7af4a2d0f@sentry.io/1482560");

        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        #endregion Public Methods
    }
}