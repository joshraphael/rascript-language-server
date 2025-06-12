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
        private readonly FunctionDefinition[] functionDefinitions;

        public Parser(ILanguageServerFacade router, FunctionDefinitions functionDefinitions, string text)
        {
            _router = router;
            this.text = text;
            this.textPositions = new TextPositions(_router, text);
            this.functionLocations = new Dictionary<string, Position>();
            this.comments = new Dictionary<string, string[]>();
            this.functionDefinitions = functionDefinitions.functionDefinitions;
            this.Load();
        }

        private void Load()
        {
            for (int i = 0; i < this.functionDefinitions.Length; i++)
            {
                FunctionDefinition fn = this.functionDefinitions[i];
                string comment = string.Join("\n", fn.CommentDoc);
                this.comments.Add(fn.Key, NewHoverData(fn.Key, comment, fn.URL, fn.Args));
            }
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

                                        // Trim end token
                                        trimmedLine = newLine.Split("*/"); // use whats after the star token
                                        if (trimmedLine.Length > 2)
                                        {
                                            trimmedLine = trimmedLine[..^1]; // remove last element
                                        }
                                        newLine = string.Join("", trimmedLine).TrimStart();
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
                            }
                            offset = offset + 1;
                        }
                    }
                    // do something
                    string[] args = ItemMatch.Groups.Values.ElementAt(3).ToString().Split(",").Select(s => s.Trim()).ToArray();
                    if (blockCommentStarStyle)
                    {
                        this.comments.Add(funcName, NewHoverData(funcName, comment, null, args));
                    }
                    else
                    {
                        this.comments.Add(funcName, NewHoverData(funcName, untrimmedComment, null, args));
                    }
                }
            }
        }

        private string[] NewHoverData(string key, string text, string? docUrl, string[] args)
        {
            string argStr = string.Join(", ", args);
            string[] commentLines = Regex.Split(text, @"\r?\n");
            List<string> lines = new List<string>();
            lines.Add($"```rascript\nfunction {key}({argStr})\n```");
            if (text != null && text != "")
            {
                lines.Add("---");
                string curr = "";
                bool codeBlock = false;
                for (int i = 0; i < commentLines.Length; i++)
                {
                    string line = Regex.Replace(commentLines[i], @"^\/\/", "").TrimStart();
                    if (line.StartsWith("```"))
                    {
                        codeBlock = !codeBlock;
                        if (codeBlock)
                        {
                            curr = line;
                        }
                        else
                        {
                            curr = curr + "\n" + line;
                            lines.Add(curr);
                            curr = "";
                        }
                        continue;
                    }
                    if (line.StartsWith('|') || line.StartsWith('*'))
                    {
                        line = line + "\n";
                    }
                    if (codeBlock)
                    {
                        curr = curr + "\n" + line;
                    }
                    else
                    {
                        if (line == "")
                        {
                            lines.Add(curr);
                            curr = "";
                        }
                        else
                        {
                            curr = curr + " " + line;
                        }
                    }
                }
                if (curr != "")
                {
                    lines.Add(curr);
                }
                if (codeBlock)
                {
                    lines.Add("```");
                }
            }
            if (docUrl != null && docUrl != "")
            {
                lines.Add("---");
                lines.Add($"[Wiki link for `{key}()`]({docUrl})");
            }
            return lines.ToArray();
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

        public string[]? GetHoverText(string word)
        {
            if (this.comments.ContainsKey(word))
            {
                return this.comments[word];
            }
            return null;
        }
    }
}