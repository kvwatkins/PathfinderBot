using System.Text.RegularExpressions;

namespace Pathfinder.Sdk;
using LanguageExt;
using static LanguageExt.Prelude;

public enum TypeError { InvalidFormat }

public class ApiKey : NewType<ApiKey, string>
{   //ApiKey Format = xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx
    private static readonly string ApiFormatPattern = "[a-z0-9]{8}-[a-z0-9]{4}-[a-z0-9]{4}-[a-z0-9]{4}-[a-z0-9]{12}";
    private ApiKey(string value) : base(value) { }

    public static Either<TypeError, ApiKey> MkApiKey(string key)
    {
        var apiKeyRegex = new Regex(ApiFormatPattern);
        if (apiKeyRegex.IsMatch(key))
        {
            return Right(new ApiKey(key));
        }
        return Left(TypeError.InvalidFormat);
    }
}