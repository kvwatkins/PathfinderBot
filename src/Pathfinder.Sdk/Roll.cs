using System.Data;
using System.Text.RegularExpressions;

namespace Pathfinder.Sdk;

using LanguageExt;
using static LanguageExt.Prelude;

public static class Roll
{
    public static string DiceValidationString => @"^\d{1,3}d\d{1,3}";
    public static string DiceInputValidationString => @"^((\d{1,3}d\d{1,3}(\+)?)|(\d{1,3}d\d{1,3}(\+|\-)\d{1,3}(\+)?))+(?:\s(adv|dis))?$";

    public static int EvaluateDiceExpression(string expr)
    {
        var diceRegex = new Regex(DiceValidationString);

        var diceToReplace =new Dictionary<string,string>();   

        foreach (Match match in diceRegex.Matches(expr))
        {
            var rawDice = match.Value.Split('d');
            var (diceNumber, sides) = (Int32.Parse(rawDice[0]), Int32.Parse(rawDice[1]));
            var result = RollDice(diceNumber, sides);
            diceToReplace.Add(match.Value, result.ToString());
        }

        var newExpression = expr;
        foreach (var dice in diceToReplace)
        {
            var d = dice.Key;
            var value = dice.Value;
            newExpression = newExpression.ReplaceFirst(d, value);
        }

        DataTable dt = new DataTable();
        int answer = (int)Math.Floor((double)dt.Compute(newExpression, ""));

        return answer;
    }

    public static int RollDice(int diceNumber,int sides)
    {
        if (sides < 2 || diceNumber < 1) return -1;
        var random = new Random();
        return pipe(
            new int[diceNumber],
            dice => dice.Map(x => random.Next(1, sides)),
            rolls => rolls.Sum());
    }  
}