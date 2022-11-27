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

        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World");

            Configuration = BuildConfiguration();

            var serviceProvider = BuildServiceProvider();

            var bot = serviceProvider.GetService<IPf2eBot>();
            bot.Run().GetAwaiter().GetResult();

            Console.WriteLine("Goodbye Cruel World");
        }

        private static ServiceProvider BuildServiceProvider()
        {
            IServiceCollection services = new ServiceCollection();

            services
                .Configure<DiscordConfig>(c => Configuration.GetRequiredSection("discordConfig").Bind(c))
                .AddOptions()
                .AddSingleton<IPf2eBot, Pf2eBot>()
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