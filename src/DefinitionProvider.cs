using OmniSharp.Extensions.LanguageServer.Protocol;
using OmniSharp.Extensions.LanguageServer.Protocol.Client.Capabilities;
using OmniSharp.Extensions.LanguageServer.Protocol.Document;
using OmniSharp.Extensions.LanguageServer.Protocol.Models;
using OmniSharp.Extensions.LanguageServer.Protocol.Server;
using OmniSharp.Extensions.LanguageServer.Protocol.Window;
using Range = OmniSharp.Extensions.LanguageServer.Protocol.Models.Range;

namespace RAScriptLanguageServer
{
    class DefinitionProvider : DefinitionHandlerBase
    {
        private readonly ILanguageServerFacade _router;
        private readonly BufferManager _bufferManager;
        private readonly TextDocumentSelector _textDocumentSelector = new TextDocumentSelector(
            new TextDocumentFilter
            {
                Pattern = "**/*.rascript"
            }
        );
        public DefinitionProvider(ILanguageServerFacade router, BufferManager bufferManager)
        {
            _router = router;
            _bufferManager = bufferManager;
        }
        public override Task<LocationOrLocationLinks?> Handle(DefinitionParams request, CancellationToken cancellationToken)
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
                    // Position? pos = buffer?.GetParser().GetLinkLocation(word);
                    // if (pos != null)
                    // {
                    //     var location = new LocationOrLocationLinks(new LocationOrLocationLink(new Location
                    //     {
                    //         Uri = request.TextDocument.Uri,
                    //         Range = new Range(pos, pos)
                    //     }));
                    //     return Task.FromResult<LocationOrLocationLinks?>(location);
                    // }
                }
            }
            return Task.FromResult<LocationOrLocationLinks?>(null);
        }

        protected override DefinitionRegistrationOptions CreateRegistrationOptions(DefinitionCapability capability, ClientCapabilities clientCapabilities) => new DefinitionRegistrationOptions() {
            DocumentSelector = _textDocumentSelector
        };
    }
}