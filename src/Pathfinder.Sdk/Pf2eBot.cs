using Discord;
using Discord.WebSocket;
using System.Threading.Tasks;
using Models.Configuration;

namespace Pathfinder.Sdk
{
    internal class Pf2eBot : IPf2eBot
    {
        private readonly DiscordSocketClient _client;
        private readonly Models.Configuration.DiscordConfig _config;

        public Pf2eBot(Models.Configuration.DiscordConfig config)
        {
            _config = config;
            _client = new DiscordSocketClient();
        }

        public async Task Run()
        {
            //_client.Log += Log;
            //_client.MessageReceived += ClientOnMessageReceived;

            await _client.LoginAsync(TokenType.Bot, _config.Bot.Token);
            await _client.StartAsync();

            // Block this task until the program is closed.
            await Task.Delay(-1);
        }
    }
}