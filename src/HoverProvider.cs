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
            var t = request.Position;
            var buffer = _bufferManager.GetBuffer(documentPath);
            var txt = buffer?.GetDocumentText();
            if (buffer != null && txt != null && txt.Length > 0)
            {
                var word = buffer.GetParser().GetWordAtPosition(txt, request.Position);
                if (word != null && word.Word.Length != 0)
                {
                    int startingOffset = buffer.GetParser().GetOffsetAt(word.Start);
                    string hoverClass = buffer.GetParser().DetectClass(startingOffset);
                    int offset = startingOffset - 1;

                    // Special case: this keyword should show the class hover info
                    if (word.Word == "this")
                    {
                        List<HoverData>? classDefinitions = buffer.GetParser().GetHoverData(hoverClass);
                        if (classDefinitions != null)
                        {
                            foreach (HoverData hoverData in classDefinitions)
                            {
                                if (hoverData.ClassName == "")
                                {
                                    var content = new List<MarkedString>();
                                    foreach (var l in hoverData.Lines)
                                    {
                                        content.Add(new MarkedString(l));
                                    }
                                    Hover result = new()
                                    {
                                        Contents = new MarkedStringsOrMarkupContent(content)
                                    };
                                    return Task.FromResult<Hover?>(result);
                                }
                            }
                        }
                    }
                    WordScope scope = buffer.GetParser().GetScope(word.Start);
                    List<HoverData>? definitions = buffer.GetParser().GetHoverData(word.Word);
                    if (definitions != null)
                    {
                        WordType wordType = buffer.GetParser().GetWordType(word);
                        if (!wordType.Function && !wordType.Class)
                        {
                            // only provide hover data for classes and functions
                            return Task.FromResult<Hover?>(null);
                        }
                        // if we are hovering over the actual function signature itself, find it and return it
                        foreach (HoverData definition in definitions)
                        {
                            // magic number 9 here is length of word function plus a space in between the function name
                            if (startingOffset >= definition.Index && startingOffset <= definition.Index + 9 + definition.Key.Length)
                            {
                                var content = new List<MarkedString>();
                                foreach (var l in definition.Lines)
                                {
                                    content.Add(new MarkedString(l));
                                }
                                Hover result = new()
                                {
                                    Contents = new MarkedStringsOrMarkupContent(content)
                                };
                                return Task.FromResult<Hover?>(result);
                            }
                        }
                    }
                    // var hoverText = buffer?.GetParser().GetHoverText(word);
                    // if (hoverText != null && hoverText.Length > 0)
                    // {
                    //     var content = new List<MarkedString>();
                    //     foreach (var l in hoverText)
                    //     {
                    //         content.Add(new MarkedString(l));
                    //     }
                    //     Hover result = new()
                    //     {
                    //         Contents = new MarkedStringsOrMarkupContent(content.ToArray())
                    //     };
                    //     return Task.FromResult<Hover?>(result);
                    // }
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