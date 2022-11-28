using Discord;
using Discord.WebSocket;
using System.Threading.Tasks;
using Models.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Pathfinder.Sdk.Handlers;
using Discord.Commands;

namespace Pathfinder.Sdk
{
    internal class Pf2eBot : IPf2eBot
    {
        private readonly DiscordSocketClient _client;
        private readonly Models.Configuration.DiscordConfig _config;
        private readonly ILogger<Pf2eBot> _log;
        private readonly ICommandHandler _commandHandler;

        public Pf2eBot(IOptions<Models.Configuration.DiscordConfig> config, ILogger<Pf2eBot> log,
            DiscordSocketClient client, ICommandHandler commandHandler)
        {
            _config = config.Value ?? throw new ArgumentNullException(nameof(config));
            _log = log;
            _client = client;
            _commandHandler = commandHandler;
        }

        public async Task Run()
        {
            _log.LogInformation("I am alive!!!");
            
            await _client.LoginAsync(TokenType.Bot, _config.Bot.Token);
            await _client.StartAsync();            

            // Initialize Commands
            await _commandHandler.InitializeCommandsAsync();

            // Block this task until the program is closed.
            await Task.Delay(-1);
        }
    }
}