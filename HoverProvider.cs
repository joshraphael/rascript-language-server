using OmniSharp.Extensions.LanguageServer.Protocol.Models;
using OmniSharp.Extensions.LanguageServer.Protocol.Server;
using OmniSharp.Extensions.LanguageServer.Protocol.Client.Capabilities;
using OmniSharp.Extensions.LanguageServer.Protocol.Document;
using OmniSharp.Extensions.LanguageServer.Protocol.Window;
using System.Text;

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
            var index = Convert.ToInt32(character);

            if (index >= line.Length)
            {
                return "";
            }
            var initialChar = line[Convert.ToInt32(character)];
            StringBuilder word = new StringBuilder();
            if (IsWordLetter(initialChar))
            {
                word.Append(initialChar);
                var hasLeft = index > 0;
                var hasRight = index < line.Length - 1;
                if (hasLeft)
                {
                    for (int i = index - 1; i >= 0; i--)
                    {
                        if (IsWordLetter(line[i]))
                        {
                            word.Insert(0, line[i]); // Prepend
                            continue;
                        }
                        break;
                    }
                }
                if (hasRight)
                {
                    for (int i = index + 1; i < line.Length; i++)
                    {
                        if (IsWordLetter(line[i]))
                        {
                            word.Append(line[i]);
                            continue;
                        }
                        break;
                    }
                }
            }
            return word.ToString();
        }

        private bool IsWordLetter(char c)
        {
            return char.IsLetterOrDigit(c) || c == '_';
        }

        public override Task<Hover> Handle(HoverParams request, CancellationToken cancellationToken)
        {
            var documentPath = request.TextDocument.Uri.ToString();
            var line = request.Position.Line;
            var character = request.Position.Character;
            // _router.Window.LogInfo($"Opssssssssned buffer for document:");
            var result = new Hover();
            var word = GetWordAtPosition(documentPath, line, character);
            if (word.Length != 0) {
                var content = new List<MarkedString>
                {
                    new MarkedString(GetWordAtPosition(documentPath, line, character))
                };
                result = new Hover
                {
                    Contents = new MarkedStringsOrMarkupContent(
                        content.ToArray())
                };
            };
            return Task.FromResult(result);
        }

        protected override HoverRegistrationOptions CreateRegistrationOptions(HoverCapability capability, ClientCapabilities clientCapabilities) => new HoverRegistrationOptions() {
            DocumentSelector = _textDocumentSelector
        };
    }
}