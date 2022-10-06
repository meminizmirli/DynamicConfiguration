using DynamicConfiguration.Infrastructure.Mongo.Configurations.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mongo.Hub.Context;

namespace DynamicConfiguration.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var mongoHubContext = services.GetService<IMongoHubContext>();

                new ConfigurationMongoSeeder(mongoHubContext).SeedOnProgramRun();
            }

            host.Run();
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
