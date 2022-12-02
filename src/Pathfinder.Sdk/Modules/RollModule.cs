using System.Text.RegularExpressions;
using Discord.Commands;
namespace Pathfinder.Sdk.Modules;

public class RollModule
{
    public class InfoModule: ModuleBase<SocketCommandContext>
    {
        private static int _roll(string rawInput) {
            //TODO Regex Validation
            return Roll.EvaluateDiceExpression(rawInput);
        } 
        
        [Command("roll")]
        [Summary("rolls dice")]
        public Task RollAsync([Remainder] [Summary("The text to echo")] string echo)
            => ReplyAsync(_roll(echo).ToString());
                
        [Command("r")]
        [Summary("rolls dice")]
        public Task RollAlaisAsync([Remainder] [Summary("The text to echo")] string echo)
            => ReplyAsync(_roll(echo).ToString());
    }
}