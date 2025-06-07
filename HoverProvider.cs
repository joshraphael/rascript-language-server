using OmniSharp.Extensions.LanguageServer.Protocol.Models;
using OmniSharp.Extensions.LanguageServer.Protocol.Server;
using OmniSharp.Extensions.LanguageServer.Protocol.Client.Capabilities;
using OmniSharp.Extensions.LanguageServer.Protocol.Document;
using OmniSharp.Extensions.LanguageServer.Protocol.Window;
using System.Text;

namespace RAScriptLanguageServer
{
    class HoverProvider(ILanguageServerFacade router, BufferManager bufferManager) : HoverHandlerBase
    {

        private readonly TextDocumentSelector _textDocumentSelector = new TextDocumentSelector(
            new TextDocumentFilter
            {
                Pattern = "**/*.rascript"
            }
        );

        private readonly ILanguageServerFacade _router = router;
        private readonly BufferManager _bufferManager = bufferManager;

        public override Task<Hover?> Handle(HoverParams request, CancellationToken cancellationToken)
        {
            var documentPath = request.TextDocument.Uri.ToString();
            var line = request.Position.Line;
            var character = request.Position.Character;
            var buffer = _bufferManager.GetBuffer(documentPath);
            var txt = buffer?.GetDocumentText();
            if (txt != null && txt.Length > 0)
            {
                var word = buffer?.GetParser().GetWordAtPosition(txt, line, character);
                if (word != null && word.Length != 0)
                {
                    var content = new List<MarkedString>
                    {
                        new(word)
                    };
                    Hover result = new()
                    {
                        Contents = new MarkedStringsOrMarkupContent(content.ToArray())
                    };
                    return Task.FromResult<Hover?>(result);
                }
            }
            return Task.FromResult<Hover?>(null);
        }

        protected override HoverRegistrationOptions CreateRegistrationOptions(HoverCapability capability, ClientCapabilities clientCapabilities) => new HoverRegistrationOptions()
        {
            DocumentSelector = _textDocumentSelector
        };
    };
}