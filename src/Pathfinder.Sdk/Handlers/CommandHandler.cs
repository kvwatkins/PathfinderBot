using System.Reflection;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Logging;

namespace Pathfinder.Sdk.Handlers
{
    public class CommandHandler : ICommandHandler
    {
        private readonly CommandService _commands;
        private readonly DiscordSocketClient _client;
        private readonly IServiceProvider _provider;
        private readonly ILogger<CommandHandler> _log;

        public CommandHandler(DiscordSocketClient client, CommandService commands, IServiceProvider provider, ILogger<CommandHandler> log)
        {
            _commands = commands;
            _client = client;
            _provider = provider;
            _log = log;
        }

        public async Task InitializeCommandsAsync()
        {
            _log.LogInformation("Initializing CommandHandler");
            //load modules
            await _commands.AddModulesAsync(assembly: Assembly.GetEntryAssembly(), services: _provider);

            // Subscribe to messages
            _client.MessageReceived += HandleCommandAsync;

            _commands.CommandExecuted += async (optional, context, result) => 
            {
                if(!result.IsSuccess && result.Error != CommandError.UnknownCommand)
                {
                    // notify user of failure
                    await context.Channel.SendMessageAsync($"error: {result}");
                }
            };

            foreach (var module in _commands.Modules)
            {
                _log.LogInformation($"{nameof(CommandHandler)} | Commands, Module {module.Name} initialized");
            }
        }

        private async Task HandleCommandAsync(SocketMessage messageParam)
        {
            // Ignore system message
            var message = messageParam as SocketUserMessage;
            if(message == null)
            {
                return;
            }

            // Track command prefix
            int argPos = 0;

            //ignore bots
            if(!(message.HasCharPrefix('!', ref argPos) || message.HasMentionPrefix(_client.CurrentUser, ref argPos))
                || message.Author.IsBot)
                {
                    return;
                }
            
            // Create Websocket based command context based on message
            var context = new SocketCommandContext(_client, message);

            await _commands.ExecuteAsync(context: context,
                argPos: argPos,
                services: _provider);
        }
    }
}