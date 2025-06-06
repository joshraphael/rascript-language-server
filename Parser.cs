using OmniSharp.Extensions.LanguageServer.Protocol;
using System.Text.RegularExpressions;
using OmniSharp.Extensions.LanguageServer.Protocol.Server;
using OmniSharp.Extensions.LanguageServer.Protocol.Window;

namespace RAScriptLanguageServer
{
    public class Parser
    {
        private readonly ILanguageServerFacade _router;
        private readonly string text;
        private readonly TextPositions textPositions;

        public Parser(ILanguageServerFacade router, string text)
        {
            _router = router;
            this.text = text;
            this.textPositions = new TextPositions(text);
        }

        public void Load()
        {
            if (text != null && text != "")
            {
                foreach (Match ItemMatch in Regex.Matches(text, @"(\bfunction\b)\s*(\w+)\s*\(([^\(\)]*)\)")) // keep in sync with syntax file rascript.tmLanguage.json #function-definitions regex
                {
                    _router.Window.LogInfo($"{ItemMatch.Value} : {this.textPositions.GetPosition(ItemMatch.Index)}");
                }
            }
        }
    }
}