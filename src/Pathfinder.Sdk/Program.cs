using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Sinks.File;
using System;
using Models.Configuration;


namespace Pathfinder.Sdk
{
    class Program
    {
        public static IConfigurationRoot Configuration {get; set;}
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World");

            Configuration = BuildConfiguration();

            var services = BuildServiceProvider();

            // var token = configuration.GetRequiredSection("Discord");
            // var pf2eBot = new Pf2eBot();
            // pf2eBot.Run().GetAwaiter().GetResult();

            Console.WriteLine("Goodbye Cruel World");
        }

        private static ServiceProvider BuildServiceProvider()
        {
            IServiceCollection services = new ServiceCollection();

            services
                .Configure<DiscordConfig>(c => Configuration.GetRequiredSection("discordConfig").Bind(c))
                .AddOptions()
                .BuildServiceProvider();

            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider;
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var env = Environment.GetEnvironmentVariable("NETCORE_ENVIRONMENT");
            var isDevelopment = string.IsNullOrEmpty(env) || env.ToLower() == "development";

            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.Development.json", optional: true, reloadOnChange: true);
            
            if(isDevelopment)
            {
                builder.AddUserSecrets<Program>();
            }

            return builder.Build();
        }
    }
}