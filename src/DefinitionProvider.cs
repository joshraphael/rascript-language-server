using OmniSharp.Extensions.LanguageServer.Protocol.Client.Capabilities;
using OmniSharp.Extensions.LanguageServer.Protocol.Document;
using OmniSharp.Extensions.LanguageServer.Protocol.Models;
using OmniSharp.Extensions.LanguageServer.Protocol.Server;
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
            var buffer = _bufferManager.GetBuffer(documentPath);
            var txt = buffer?.GetDocumentText();
            if (buffer != null && txt != null && txt.Length > 0)
            {
                var word = buffer.GetParser().GetWordAtPosition(request.Position);
                if (word != null && word.Word.Length != 0)
                {
                    int startOffset = buffer.GetParser().GetOffsetAt(word.Start);
                    int endOffset = buffer.GetParser().GetOffsetAt(word.End);
                    string hoverClass = buffer.GetParser().DetectClass(startOffset);
                    if (txt[endOffset+1] != '(')
                    {
                        return Task.FromResult<LocationOrLocationLinks?>(null); // not a function (maybe string, or just varaible named the same)
                    }
                    int origWordOffset = buffer.GetParser().GetOffsetAt(request.Position);
                    List<ClassFunction>? list = buffer.GetParser().GetClassFunctionDefinitions(word.Word);
                    if (list != null)
                    {
                        WordScope scope = buffer.GetParser().GetScope(word.Start);
                        List<ClassFunction> filteredList = list.Where(buffer.GetParser().ClassFilter(scope.Global, scope.UsingThis, hoverClass)).ToList();
                        // can only link to one location, so anything that has multiple definitions wont work for code jumping
                        if (filteredList.Count == 1)
                        {
                            ClassFunction el = filteredList[0];
                            LocationOrLocationLinks location = new LocationOrLocationLinks(new LocationOrLocationLink(new Location
                            {
                                Uri = request.TextDocument.Uri,
                                Range = new Range(el.Pos, el.Pos)
                            }));
                            return Task.FromResult<LocationOrLocationLinks?>(location);
                        }
                    }
                }
            }
            return Task.FromResult<LocationOrLocationLinks?>(null);
        }

        protected override DefinitionRegistrationOptions CreateRegistrationOptions(DefinitionCapability capability, ClientCapabilities clientCapabilities) => new DefinitionRegistrationOptions() {
            DocumentSelector = _textDocumentSelector
        };
    }
}