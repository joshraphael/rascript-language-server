using System.Text;
using OmniSharp.Extensions.LanguageServer.Protocol.Document;
using OmniSharp.Extensions.LanguageServer.Protocol;
using OmniSharp.Extensions.LanguageServer.Protocol.Client.Capabilities;
using OmniSharp.Extensions.LanguageServer.Protocol.Models;
using OmniSharp.Extensions.LanguageServer.Protocol.Server;
using OmniSharp.Extensions.LanguageServer.Protocol.Server.Capabilities;
using OmniSharp.Extensions.LanguageServer.Protocol.Window;
using MediatR;


namespace RAScriptLanguageServer
{
    internal class TextDocumentSyncHandler : TextDocumentSyncHandlerBase
    {
        private readonly TextDocumentSelector _textDocumentSelector = new TextDocumentSelector(
            new TextDocumentFilter {
                Pattern = "**/*.rascript"
            }
        );
        private readonly ILanguageServerFacade _router;
        private readonly BufferManager _bufferManager;

        public TextDocumentSyncHandler(ILanguageServerFacade router, BufferManager bufferManager)
        {
            _router = router;
            _bufferManager = bufferManager;
        }

        public override TextDocumentAttributes GetTextDocumentAttributes(DocumentUri uri)
        {
            return new TextDocumentAttributes(uri, "rascript");
        }

        public override Task<Unit> Handle(DidOpenTextDocumentParams request, CancellationToken cancellationToken)
        {
            var documentPath = request.TextDocument.Uri.ToString();
            var text = request.TextDocument.Text;
            _bufferManager.UpdateBuffer(request.TextDocument.Uri.ToString(), new StringBuilder(request.TextDocument.Text));
            _router.Window.LogInfo($"Opened buffer for document: {documentPath}\n{text}");
            return Unit.Task;
        }

        public override Task<Unit> Handle(DidChangeTextDocumentParams request, CancellationToken cancellationToken)
        {
            var documentPath = request.TextDocument.Uri.ToString();
            var text = request.ContentChanges.FirstOrDefault()?.Text;
            _bufferManager.UpdateBuffer(documentPath, new StringBuilder(text));
            _router.Window.LogInfo($"Updated buffer for document: {documentPath}\n{text}");
            return Unit.Task;
        }

        public override Task<Unit> Handle(DidSaveTextDocumentParams request, CancellationToken cancellationToken)
        {
            return Unit.Task;
        }

        public override Task<Unit> Handle(DidCloseTextDocumentParams request, CancellationToken cancellationToken)
        {
            return Unit.Task;
        }

        protected override TextDocumentSyncRegistrationOptions CreateRegistrationOptions(TextSynchronizationCapability capability, ClientCapabilities clientCapabilities) => new TextDocumentSyncRegistrationOptions() {
            DocumentSelector = _textDocumentSelector,
            Change = TextDocumentSyncKind.Full,
            Save = new SaveOptions() { IncludeText = true }
        };
    }
}