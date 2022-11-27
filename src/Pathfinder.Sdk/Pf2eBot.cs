using Discord;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace Pathfinder.Sdk
{
    internal class Pf2eBot
    {
        private readonly DiscordSocketClient _client;

        public Pf2eBot()
        {
            _client = new DiscordSocketClient();
        }

        public async Task Run()
        {
            //_client.Log += Log;
            //_client.MessageReceived += ClientOnMessageReceived;

            var token = "Your Token Here";

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            // Block this task until the program is closed.
            await Task.Delay(-1);
        }
    }
}