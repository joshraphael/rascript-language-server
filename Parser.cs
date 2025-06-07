using OmniSharp.Extensions.LanguageServer.Protocol;
using System.Text.RegularExpressions;
using OmniSharp.Extensions.LanguageServer.Protocol.Server;
using OmniSharp.Extensions.LanguageServer.Protocol.Window;
using OmniSharp.Extensions.LanguageServer.Protocol.Models;
using System.Collections.Generic;
using System.Text;

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
            this.Load();
        }

        private void Load()
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

        public string GetWordAtPosition(string txt, long lineNum, long character)
        {
            var lines = txt.Split('\n');
            var line = lines[lineNum];
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

        public static bool IsWordLetter(char c)
        {
            return char.IsLetterOrDigit(c) || c == '_';
        }

        public Position? GetLinkLocation(string word)
        {
            if (this.functionLocations.ContainsKey(word))
            {
                return this.functionLocations[word];
            }
            return null;
        }
    }
}