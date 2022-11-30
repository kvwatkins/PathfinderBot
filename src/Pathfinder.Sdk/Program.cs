using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Sinks.File;
using System;
using Discord;
using Discord.WebSocket;
using Discord.Commands;
using Discord.Interactions;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Pathfinder.Sdk.Handlers;
using DiscordConfig = Models.Configuration.DiscordConfig;

namespace Pathfinder.Sdk
{
    class Program
    {
        public static IConfigurationRoot Configuration { get; set; }

        public static Task Main(string[] args) => new Program().MainAsync();

        public async Task MainAsync()
        {
            Console.WriteLine("Hello World");

            Configuration = BuildConfiguration();

            var serviceProvider = BuildServiceProvider();

            var bot = serviceProvider.GetRequiredService<IPf2eBot>();
            await bot.Run();

            Log.CloseAndFlush();
            Console.WriteLine("Goodbye Cruel World");
        }

        private static ServiceProvider BuildServiceProvider()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
            
            IServiceCollection services = new ServiceCollection();

            services
                .Configure<DiscordConfig>(c => Configuration.GetRequiredSection("discordConfig").Bind(c))
                .AddOptions()
                .AddLogging(builder => builder.AddSerilog(dispose: true))
                .AddSingleton<DiscordSocketClient>(_ => new DiscordSocketClient(new DiscordSocketConfig(){GatewayIntents = GatewayIntents.All}))
                .AddSingleton<CommandService>()
                .AddSingleton<InteractionService>()
                .AddSingleton<IPf2eBot, Pf2eBot>()
                .AddSingleton<ICommandHandler, CommandHandler>()
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

            if (isDevelopment)
            {
                builder.AddUserSecrets<Program>();
            }

            return builder.Build();
        }
    }
}