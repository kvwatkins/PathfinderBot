using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Sinks.File;
using System;


namespace Pathfinder.Sdk
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World");

            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.Development.json", optional: true, reloadOnChange: true)
                .Build();

            // var pf2eBot = new Pf2eBot();
            // pf2eBot.Run().GetAwaiter().GetResult();

            Console.WriteLine("Goodbye Cruel World");
        }
    }
}