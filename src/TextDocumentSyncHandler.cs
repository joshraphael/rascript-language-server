using System.Text;
using OmniSharp.Extensions.LanguageServer.Protocol.Document;
using OmniSharp.Extensions.LanguageServer.Protocol;
using OmniSharp.Extensions.LanguageServer.Protocol.Client.Capabilities;
using OmniSharp.Extensions.LanguageServer.Protocol.Models;
using OmniSharp.Extensions.LanguageServer.Protocol.Server;
using OmniSharp.Extensions.LanguageServer.Protocol.Server.Capabilities;
using OmniSharp.Extensions.LanguageServer.Protocol.Window;
using MediatR;
using RASharp;


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
        private readonly FunctionDefinitions _functionDefinitions;
        private readonly RetroAchievements _retroachievements;

        public TextDocumentSyncHandler(ILanguageServerFacade router, BufferManager bufferManager)
        {
            _router = router;
            _bufferManager = bufferManager;
            _functionDefinitions = new FunctionDefinitions();
            _retroachievements = new RetroAchievements(RetroAchievements.RetroAchievementsHost, "");
            _bufferManager.SetRAClient(this._retroachievements);
        }

        public override TextDocumentAttributes GetTextDocumentAttributes(DocumentUri uri)
        {
            return new TextDocumentAttributes(uri, "rascript");
        }

        public override Task<Unit> Handle(DidOpenTextDocumentParams request, CancellationToken cancellationToken)
        {
            var documentPath = request.TextDocument.Uri.ToString();
            var text = request.TextDocument.Text;
            Parser p = new Parser(_router, _functionDefinitions, text);
            _ = _bufferManager.UpdateBufferAsync(documentPath, new StringBuilder(request.TextDocument.Text), p);
            return Unit.Task;
        }

        public override Task<Unit> Handle(DidChangeTextDocumentParams request, CancellationToken cancellationToken)
        {
            var documentPath = request.TextDocument.Uri.ToString();
            var text = request.ContentChanges.FirstOrDefault()?.Text;
            if (text != null)
            {
                Parser p = new Parser(_router, _functionDefinitions, text);
                _ = _bufferManager.UpdateBufferAsync(documentPath, new StringBuilder(text), p);
            }
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