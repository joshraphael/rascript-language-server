using OmniSharp.Extensions.LanguageServer.Protocol.Models;
using OmniSharp.Extensions.LanguageServer.Protocol.Server;
using OmniSharp.Extensions.LanguageServer.Protocol.Client.Capabilities;
using OmniSharp.Extensions.LanguageServer.Protocol.Document;
using OmniSharp.Extensions.LanguageServer.Protocol.Window;

namespace RAScriptLanguageServer
{
    class HoverProvider : HoverHandlerBase
    {

        private readonly TextDocumentSelector _textDocumentSelector = new TextDocumentSelector(
            new TextDocumentFilter {
                Pattern = "**/*.rascript"
            }
        );

        private readonly ILanguageServerFacade _router;
        private readonly BufferManager _bufferManager;

        public HoverProvider(ILanguageServerFacade router, BufferManager bufferManager)
        {
            _router = router;
            _bufferManager = bufferManager;
        }


        public string GetWordAtPosition(string documentPath, long lineNum, long character)
        {
            var buffer = _bufferManager.GetBuffer(documentPath);
            var txt = buffer.ToString();
            var line = txt.Split('\n')[lineNum];
            _router.Window.LogInfo($"{line}");
            return line;
        }

        public override Task<Hover> Handle(HoverParams request, CancellationToken cancellationToken)
        {
            var documentPath = request.TextDocument.Uri.ToString();
            var line = request.Position.Line;
            var character = request.Position.Character;
            _router.Window.LogInfo($"Opssssssssned buffer for document:");
            var content = new List<MarkedString>
            {
                new MarkedString(GetWordAtPosition(documentPath, line, character))
            };
            var result = new Hover
            {
                Contents = new MarkedStringsOrMarkupContent(
                    content.ToArray())
            };
            return Task.FromResult(result);
        }

        protected override HoverRegistrationOptions CreateRegistrationOptions(HoverCapability capability, ClientCapabilities clientCapabilities) => new HoverRegistrationOptions() {
            DocumentSelector = _textDocumentSelector
        };
    }
}