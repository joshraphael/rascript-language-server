using OmniSharp.Extensions.LanguageServer.Protocol;
using System.Text.RegularExpressions;
using OmniSharp.Extensions.LanguageServer.Protocol.Server;
using OmniSharp.Extensions.LanguageServer.Protocol.Window;
using OmniSharp.Extensions.LanguageServer.Protocol.Models;
using System.Collections.Generic;

namespace RAScriptLanguageServer
{
    public class Parser
    {
        private readonly ILanguageServerFacade _router;
        private readonly string text;
        private readonly TextPositions textPositions;
        private readonly Dictionary<string, Position> functionLocations;

        public Parser(ILanguageServerFacade router, string text)
        {
            _router = router;
            this.text = text;
            this.textPositions = new TextPositions(text);
            this.functionLocations = new Dictionary<string, Position>();
        }

        public void Load()
        {
            if (text != null && text != "")
            {
                foreach (Match ItemMatch in Regex.Matches(text, @"(\bfunction\b)\s*(\w+)\s*\(([^\(\)]*)\)")) // keep in sync with syntax file rascript.tmLanguage.json #function-definitions regex
                {
                    string funcName = ItemMatch.Groups.Values.ElementAt(2).ToString();
                    Position pos = this.textPositions.GetPosition(ItemMatch.Index);
                    functionLocations.Add(funcName, pos);
                }
            }
        }
    }
}