using OmniSharp.Extensions.LanguageServer.Protocol;
using OmniSharp.Extensions.LanguageServer.Protocol.Client.Capabilities;
using OmniSharp.Extensions.LanguageServer.Protocol.Document;
using OmniSharp.Extensions.LanguageServer.Protocol.Models;
using OmniSharp.Extensions.LanguageServer.Protocol.Server;
using OmniSharp.Extensions.LanguageServer.Protocol.Window;
using Range = OmniSharp.Extensions.LanguageServer.Protocol.Models.Range;

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
                foreach (var k in parser.GetKeywords())
                {
                    CompletionItemKind kind = parser.GetKeywordCompletionItemKind(k) ?? CompletionItemKind.Text;
                    items.Add(new CompletionItem()
                    {
                        Label = k,
                        Kind = kind,
                    });
                }
            }
            // foreach (var k in buffer.GetParser().GetKeywords())
            // {

            // }
            // var list = new CompletionList(new List<CompletionItem>()
            // {
            //     new CompletionItem()
            //     {
            //         Label = "test",
            //         Kind = CompletionItemKind.Keyword,
            //     }
            // });
            return Task.FromResult<CompletionList>(items);
        }

        protected override CompletionRegistrationOptions CreateRegistrationOptions(CompletionCapability capability, ClientCapabilities clientCapabilities) => new CompletionRegistrationOptions()
        {
            DocumentSelector = _textDocumentSelector
        };
    }
}