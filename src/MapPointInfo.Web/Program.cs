using Microsoft.AspNetCore;
using NLog.Web;

namespace MapPointInfo.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            NLog.LogManager.Setup().LoadConfigurationFromAppSettings();
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .UseNLog()
            .UseKestrel()
            .UseStartup<Startup>();
    }
}