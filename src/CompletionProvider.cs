using OmniSharp.Extensions.LanguageServer.Protocol.Client.Capabilities;
using OmniSharp.Extensions.LanguageServer.Protocol.Document;
using OmniSharp.Extensions.LanguageServer.Protocol.Models;
using OmniSharp.Extensions.LanguageServer.Protocol.Server;

namespace RAScriptLanguageServer
{
    class CompletionProvider : CompletionHandlerBase
    {
        private readonly ILanguageServerFacade _router;
        private readonly BufferManager _bufferManager;
        private readonly TextDocumentSelector _textDocumentSelector = new TextDocumentSelector(
            new TextDocumentFilter
            {
                Pattern = "**/*.rascript"
            }
        );
        public CompletionProvider(ILanguageServerFacade router, BufferManager bufferManager)
        {
            _router = router;
            _bufferManager = bufferManager;
        }

        public override Task<CompletionItem> Handle(CompletionItem request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new CompletionItem());
        }
        
        public override Task<CompletionList> Handle(CompletionParams request, CancellationToken cancellationToken)
        {
            var documentPath = request.TextDocument.Uri.ToString();
            var buffer = _bufferManager.GetBuffer(documentPath);
            var parser = buffer?.GetParser();
            List<CompletionItem> items = new List<CompletionItem>();
            if (parser != null)
            {
                HashSet<string> functionSet = [.. parser.completionFunctions];
                foreach (string fnName in functionSet)
                {
                    items.Add(new CompletionItem()
                    {
                        Label = fnName,
                        Kind = CompletionItemKind.Function,
                    });
                }
                HashSet<string> variableSet = [.. parser.completionVariables];
                foreach (string varName in variableSet)
                {
                    items.Add(new CompletionItem()
                    {
                        Label = varName,
                        Kind = CompletionItemKind.Variable,
                    });
                }
                HashSet<string> classSet = [.. parser.completionClasses];
                foreach (string className in classSet)
                {
                    items.Add(new CompletionItem()
                    {
                        Label = className,
                        Kind = CompletionItemKind.Class,
                    });
                }
            }
            return Task.FromResult<CompletionList>(items);
        }

        protected override CompletionRegistrationOptions CreateRegistrationOptions(CompletionCapability capability, ClientCapabilities clientCapabilities) => new CompletionRegistrationOptions()
        {
            DocumentSelector = _textDocumentSelector
        };
    }
}