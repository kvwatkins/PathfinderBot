using Discord;
using Discord.WebSocket;
using System.Threading.Tasks;
using Models.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;

namespace Pathfinder.Sdk
{
    internal class Pf2eBot : IPf2eBot
    {
        private readonly DiscordSocketClient _client;
        private readonly Models.Configuration.DiscordConfig _config;
        private readonly ILogger<Pf2eBot> _log;

        public Pf2eBot(IOptions<Models.Configuration.DiscordConfig> config, ILogger<Pf2eBot> log)
        {
            _config = config.Value ?? throw new ArgumentNullException(nameof(config));
            _log = log;
            _client = new DiscordSocketClient();
        }

        public async Task Run()
        {
            //_client.Log += Log;
            //_client.MessageReceived += ClientOnMessageReceived;

            _log.LogInformation("I am alive!!!");
            
            await _client.LoginAsync(TokenType.Bot, _config.Bot.Token);
            await _client.StartAsync();

            // Block this task until the program is closed.
            await Task.Delay(-1);
        }
    }
}