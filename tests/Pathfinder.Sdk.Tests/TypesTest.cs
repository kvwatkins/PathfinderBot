namespace Pathfinder.Sdk.Tests;

using Pathfinder.Sdk;

public class TypesTest
{
    [Fact]
    public void ApiKeyValidFormat()
    {
        var actual = ApiKey.MkApiKey("xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx");
        Assert.True(actual.IsRight);
    }
    
    [Fact]
    public void ApiKeyInValidFormat()
    {
        var actual = ApiKey.MkApiKey("xxxxxxxx-xxxx");
        Assert.True(actual.IsLeft);
    }
}