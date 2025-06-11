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
        private readonly Dictionary<string, string[]> comments;

        public Parser(ILanguageServerFacade router, string text)
        {
            _router = router;
            this.text = text;
            this.textPositions = new TextPositions(_router, text);
            this.functionLocations = new Dictionary<string, Position>();
            this.comments = new Dictionary<string, string[]>();
            this.Load();
        }

        private void Load()
        {
            var lines = this.textPositions.GetLines();
            if (text != null && text != "")
            {
                foreach (Match ItemMatch in Regex.Matches(text, @"(\bfunction\b)\s*(\w+)\s*\(([^\(\)]*)\)")) // keep in sync with syntax file rascript.tmLanguage.json #function-definitions regex
                {
                    string funcName = ItemMatch.Groups.Values.ElementAt(2).ToString();
                    Position pos = this.textPositions.GetPosition(ItemMatch.Index);
                    functionLocations.Add(funcName, pos);
                    string comment = "";
                    string untrimmedComment = "";
                    bool blockCommentStarStyle = true;
                    if (pos.Line > 0)
                    {
                        int offset = 1;
                        bool inBlock = false;
                        while (pos.Line - offset >= 0)
                        {
                            int lineNum = pos.Line - offset;
                            string? line = this.textPositions.GetLineAt(lineNum);
                            if (line != null)
                            {
                                line = line.TrimStart();
                                if (offset == 1)
                                {
                                    bool isBlock = Regex.IsMatch(line, @"^.*\*\/$");
                                    if (isBlock)
                                    {
                                        inBlock = true;
                                    }
                                }
                                if (inBlock) // Block comment
                                {
                                    bool endBlock = Regex.IsMatch(line, @"^.*\/\*.*$");
                                    if (endBlock)
                                    {
                                        // Trim start token
                                        string[] trimmedLine = Regex.Split(line, @"\/\*(.*)", RegexOptions.Singleline).Skip(1).ToArray();
                                        string newLine = string.Join("", trimmedLine).TrimStart();
                                        _router.Window.LogInfo($"{newLine}");

                                        // Trim end token
                                        trimmedLine = newLine.Split("*/"); // use whats after the star token
                                        if (trimmedLine.Length > 2)
                                        {
                                            trimmedLine = trimmedLine[..^1]; // remove last element
                                        }
                                        newLine = string.Join("", trimmedLine).TrimStart();
                                        if (blockCommentStarStyle)
                                        {
                                            bool starComment = Regex.IsMatch(line, @"^\*.*");
                                            if (!starComment)
                                            {
                                                blockCommentStarStyle = false;
                                            }
                                        }
                                        untrimmedComment = "//" + newLine + "\n" + untrimmedComment; // keep an untrimmed version of the comment in case the entire block is prefixed with stars

                                        // Trim first '*' token (in case they comment that way)
                                        trimmedLine = Regex.Split(newLine, @"^\*(.*)", RegexOptions.Singleline).ToArray();
                                        if (trimmedLine.Length > 2)
                                        {
                                            trimmedLine = trimmedLine[1..]; // remove first element
                                        }
                                        newLine = string.Join("", trimmedLine).TrimStart();
                                        comment = "//" + newLine + "\n" + comment;
                                        break;
                                    }
                                    else // at end of comment block
                                    {
                                        // Trim end token (guaranteed to not have text after end token if the user wants comments to appear in hover box)
                                        string[] trimmedLine = line.Split("*/");
                                        if (trimmedLine.Length > 2)
                                        {
                                            trimmedLine = trimmedLine[..^1]; // remove last element
                                        }
                                        string newLine = string.Join("", trimmedLine).TrimStart();

                                        if (blockCommentStarStyle)
                                        {
                                            bool starComment = Regex.IsMatch(newLine, @"^\*.*");
                                            if (!starComment)
                                            {
                                                blockCommentStarStyle = false;
                                            }
                                        }
                                        untrimmedComment = "//" + newLine + "\n" + untrimmedComment; // keep an untrimmed version of the comment in case the entire block is prefixed with stars

                                        // Trim first '*' token (in case they comment that way)
                                        trimmedLine = Regex.Split(newLine, @"^\*(.*)", RegexOptions.Singleline).ToArray();
                                        if (trimmedLine.Length > 2)
                                        {
                                            trimmedLine = trimmedLine[1..]; // remove first element
                                        }
                                        newLine = string.Join("", trimmedLine).TrimStart();
                                        comment = "//" + newLine + "\n" + comment;
                                    }
                                }
                                else // Single line comment
                                {
                                    bool isComment = Regex.IsMatch(line, @"^\/\/.*$");
                                    if (isComment)
                                    {
                                        comment = line + "\n" + comment;
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                _router.Window.LogInfo($"its null {lineNum}");
                            }
                            offset = offset + 1;
                        }
                    }
                    // do something
                    string[] args = ItemMatch.Groups.Values.ElementAt(3).ToString().Split(",");
                    _router.Window.LogInfo($"{ItemMatch.Groups.Values.ElementAt(3)}");
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