using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OmniSharp.Extensions.LanguageServer.Server;

namespace RAScriptLanguageServer
{
    public class Program
    {
        #pragma warning disable VSTHRD200 // Use "Async" suffix in names of methods that return an awaitable type
        public static async Task Main(string[] args)
        {
            var server = await LanguageServer.From(options =>
                options
                    .WithInput(Console.OpenStandardInput())
                    .WithOutput(Console.OpenStandardOutput())
                    .WithLoggerFactory(new LoggerFactory())
                    .AddDefaultLoggingProvider()
                    .WithServices(x => x.AddLogging(b => b.SetMinimumLevel(LogLevel.Trace)))
                    .WithServices(services =>
                    {
                        services
                        .AddSingleton<BufferManager>()
                        .AddSingleton<TextDocumentSyncHandler>();
                    })
                    .WithHandler<HoverProvider>()
                    .WithHandler<DefinitionProvider>()
                    .WithHandler<CompletionProvider>()
                );

            await server.WaitForExit;
        }
        #pragma warning restore VSTHRD200
    }
}
