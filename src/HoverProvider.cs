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
                if (word != null && word.Word.Length != 0 && word.Start != -1 && word.End != -1)
                {
                    string hoverClass = buffer.GetParser().DetectClass(word.Start);
                    int offset = word.Start - 1;

                    // Special case: this keyword should show the class hover info
                    if (word.Word == "this")
                    {
                        List<HoverData>? definitions = buffer.GetParser().GetHoverData(hoverClass);
                        if (definitions != null)
                        {
                            foreach (HoverData hoverData in definitions)
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