using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace DynamicConfiguration.CacheConsumer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                        .CaptureStartupErrors(true);
                })
                .ConfigureAppConfiguration((context, config) =>
                {
                    var env = context.HostingEnvironment.EnvironmentName;
                    var serilogPath = "appsettings." + env + ".json";
                    config.AddJsonFile(serilogPath, optional: true);
                });
    }
}
