using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OmniSharp.Extensions.LanguageServer.Server;
using Serilog;

namespace RAScriptLanguageServer
{
    public class Program
    {
        private static void Main(string[] args)
        {
            MainAsync(args).Wait();
        }
        // #pragma warning disable VSTHRD200 // Use "Async" suffix in names of methods that return an awaitable type
        private static async Task MainAsync(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                // .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day) // just use this for debug builds
                .MinimumLevel.Verbose()
                .CreateLogger();

            Log.Logger.Information("This only goes file...");
            var server = await LanguageServer.From(options =>
                options
                    .WithInput(Console.OpenStandardInput())
                    .WithOutput(Console.OpenStandardOutput())
                    .ConfigureLogging(
                        x => x
                            .AddSerilog(Log.Logger)
                            .AddLanguageProtocolLogging()
                            .SetMinimumLevel(LogLevel.Debug)
                    )
                    .WithServices(x => x.AddLogging(b => b.SetMinimumLevel(LogLevel.Debug)))
                    .WithServices(services =>
                    {
                        services
                        .AddSingleton<BufferManager>()
                        .AddSingleton<TextDocumentSyncHandler>();
                    })
                    .WithHandler<HoverProvider>()
                    .WithHandler<DefinitionProvider>()
                    .WithHandler<CompletionProvider>()
                ).ConfigureAwait(false);

            await server.WaitForExit.ConfigureAwait(false);
        }
        // #pragma warning restore VSTHRD200
    }
}
