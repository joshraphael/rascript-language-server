using System.Text.RegularExpressions;
using OmniSharp.Extensions.LanguageServer.Protocol.Server;
using OmniSharp.Extensions.LanguageServer.Protocol.Models;
using System.Text;
using RASharp.Models;
using OmniSharp.Extensions.LanguageServer.Protocol.Window;
using System.Collections;
using System.Security.AccessControl;
using Microsoft.VisualBasic;

namespace RAScriptLanguageServer
{
    public class Parser
    {
        public readonly ILanguageServerFacade _router;
        private readonly string _text;
        private readonly TextPositions textPositions;
        // private readonly Dictionary<string, Position> functionLocations;
        // private readonly Dictionary<string, string[]> comments;
        // private readonly Dictionary<string, CompletionItemKind> keywordKinds;
        // private readonly List<string> keywords;
        // private readonly FunctionDefinition[] functionDefinitions;
        private readonly CommentBounds[] commentBounds;
        private readonly Dictionary<string, ClassScope> classes;
        private readonly Dictionary<string, List<ClassFunction>> functionDefinitions;
        private readonly Dictionary<string, List<HoverData>> words;
        public readonly List<string> completionFunctions;
        public readonly List<string> completionVariables;
        public readonly List<string> completionClasses;
        private int gameID;
        private GetCodeNotes? codeNotes;

        // public Parser(ILanguageServerFacade router, FunctionDefinitions functionDefinitions, string text)
        // {
        //     _router = router;
        //     this._text = text;
        //     this._textPositions = new TextPositions(_router, text);
        //     this.functionLocations = new Dictionary<string, Position>();
        //     this.comments = new Dictionary<string, string[]>();
        //     this.keywordKinds = new Dictionary<string, CompletionItemKind>();
        //     this.keywords = new List<string>();
        //     this.functionDefinitions = functionDefinitions.functionDefinitions;
        //     this.commentBounds = this.GetCommentBoundsList();
        //     var data = this.GetClassData();
        //     this.classes = this.GetClassData();
        //     this.functionDefinitionsNew = new Dictionary<string, List<ClassFunction>>();
        //     this.words = new Dictionary<string, List<HoverData>>();
        //     this.completionFunctions = new List<string>();
        //     this.completionVariables = new List<string>();
        //     this.completionClasses = new List<string>();
        //     this.gameID = 0; // game id's start at 1 on RA
        //     this.Load();
        //     Dictionary<string, CompletionItemKind>.KeyCollection keyColl = this.keywordKinds.Keys;
        //     foreach (string k in keyColl)
        //     {
        //         this.keywords.Add(k);
        //     }
        // }

        public Parser(ILanguageServerFacade router, FunctionDefinitions builtinFunctionDefinitions, string text)
        {
            _router = router;
            this._text = text;
            this.textPositions = new TextPositions(_router, text);
            this.commentBounds = this.GetCommentBoundsList();
            this.classes = this.GetClassData();
            this.functionDefinitions = new Dictionary<string, List<ClassFunction>>();
            this.words = new Dictionary<string, List<HoverData>>();
            this.completionFunctions = new List<string>();
            this.completionVariables = new List<string>();
            this.completionClasses = new List<string>();

            // Parse each built in function in the document
            for (int i = 0; i < builtinFunctionDefinitions.functionDefinitions.Length; i++)
            {
                FunctionDefinition fn = builtinFunctionDefinitions.functionDefinitions[i];

                // Add hover data
                string comment = string.Join("\n", fn.CommentDoc);
                HoverData hover = this.NewHoverText(fn.Key, -1, "function", "", comment, fn.URL, fn.Args);
                List<HoverData>? data = this.GetHoverData(fn.Key);
                if (data != null)
                {
                    data.Add(hover);
                }
                else
                {
                    this.words[fn.Key] = new List<HoverData>
                    {
                        hover
                    };
                }

                // Add completion data
                completionFunctions.Add(fn.Key);
            }

            // Parse each class in the document
            foreach (var entry in this.classes)
            {
                string className = entry.Key;
                ClassScope classScope = entry.Value;

                // Add hover info
                Position pos = this.textPositions.GetPosition(classScope.Start);
                string comment = this.GetCommentText(pos);
                HoverData hover = this.NewHoverText(className, classScope.Start, "class", "", comment, "", classScope.ConstructorArgs);
                List<HoverData>? data = this.GetHoverData(className);
                if (data != null)
                {
                    data.Add(hover);
                }
                else
                {
                    this.words[className] = new List<HoverData>
                    {
                        hover
                    };
                }

                // Add completion data
                completionClasses.Add(className);
            }
            if (this._text != null && this._text != "")
            {
                // Parse each function in the document
                foreach (Match ItemMatch in Regex.Matches(text, @"(\bfunction\b)[\t ]*([a-zA-Z][\w]*)[\t ]*\(([^\(\)]*)\)")) // keep in sync with syntax file rascript.tmLanguage.json #function-definitions regex
                {
                    // dont parse if its in a comment
                    if (this.InCommentBounds(ItemMatch.Index))
                    {
                        continue;
                    }
                    string className = this.DetectClass(ItemMatch.Index);
                    Position pos = this.textPositions.GetPosition(ItemMatch.Index);
                    string comment = this.GetCommentText(pos);
                    string funcName = ItemMatch.Groups.Values.ElementAt(2).ToString();
                    string[] args = ItemMatch.Groups.Values.ElementAt(3).ToString().Split(",").Select(s => s.Trim()).ToArray();

                    // add definition info
                    ClassFunction definition = new ClassFunction
                    {
                        ClassName = className,
                        Name = funcName,
                        Pos = pos,
                        Args = args
                    };
                    List<ClassFunction>? data = this.GetClassFunctionDefinitions(funcName);
                    if (data != null)
                    {
                        data.Add(definition);
                    }
                    else
                    {
                        this.functionDefinitions[funcName] = new List<ClassFunction>
                        {
                            definition
                        };
                    }

                    // add hover info
                    HoverData hover = this.NewHoverText(funcName, ItemMatch.Index, "function", className, comment, "", args);
                    List<HoverData>? hoverData = this.GetHoverData(funcName);
                    if (hoverData != null)
                    {
                        hoverData.Add(hover);
                    }
                    else
                    {
                        this.words[funcName] = new List<HoverData>
                        {
                            hover
                        };
                    }

                    // add completion info
                    completionFunctions.Add(funcName);
                }

                // Parse each variable in the document
                foreach (Match ItemMatch in Regex.Matches(text, @"([a-zA-Z_][\w]*)[\t ]*="))
                {
                    // dont parse if its in a comment
                    if (this.InCommentBounds(ItemMatch.Index))
                    {
                        continue;
                    }

                    string varName = ItemMatch.Groups.Values.ElementAt(1).ToString();

                    // add completion info
                    completionVariables.Add(varName);
                }
            }
        }

        // public void loadCodeNotes(GetCodeNotes? codeNotes)
        // {
        //     this.codeNotes = codeNotes;
        //     if (this.codeNotes != null && this.codeNotes.Success)
        //     {
        //         foreach (var note in this.codeNotes.CodeNotes)
        //         {
        //             this.comments[note.Address] = [
        //                 $"`{note.Address}`",
        //                 "---",
        //                 $"```txt\n{note.Note}\n```",
        //                 "---",
        //                 $"Author: [{note.User}](https://retroachievements.org/user/{note.User})",
        //             ];
        //         }
        //     }
        // }

        public GetCodeNotes? GetCodeNotes()
        {
            return this.codeNotes;
        }

        // private void Load()
        // {
        //     var classes = this.GetClassData();
        //     for (int i = 0; i < this.functionDefinitions.Length; i++)
        //     {
        //         FunctionDefinition fn = this.functionDefinitions[i];
        //         string comment = string.Join("\n", fn.CommentDoc);
        //         this.comments[fn.Key] = NewHoverData(fn.Key, -1, "", comment, fn.URL, fn.Args);
        //         this.keywordKinds[fn.Key] = CompletionItemKind.Function;
        //     }
        //     if (text != null && text != "")
        //     {
        //         foreach (Match ItemMatch in Regex.Matches(text, @"\/\/\s*#ID\s*=\s*(\d+)"))
        //         {
        //             string gameIDStr = ItemMatch.Groups.Values.ElementAt(1).ToString();
        //             try
        //             {
        //                 int gameID = int.Parse(gameIDStr);
        //                 if (gameID > 0)
        //                 {
        //                     this.gameID = gameID;
        //                 }
        //             }
        //             catch (FormatException)
        //             {
        //                 this.gameID = 0; // reset the game id
        //             }
        //         }
        //         foreach (Match ItemMatch in Regex.Matches(text, @"(\w+)\s*="))
        //         {
        //             string varName = ItemMatch.Groups.Values.ElementAt(1).ToString();
        //             this.keywordKinds[varName] = CompletionItemKind.Variable;
        //         }
        //         foreach (Match ItemMatch in Regex.Matches(text, @"(\bfunction\b)[\t ]*([a-zA-Z][\w]*)[\t ]*\(([^\(\)]*)\)")) // keep in sync with syntax file rascript.tmLanguage.json #function-definitions regex
        //         {
        //             string className = DetectClass(ItemMatch.Index, classes);
        //             string funcName = ItemMatch.Groups.Values.ElementAt(2).ToString();
        //             Position pos = this._textPositions.GetPosition(ItemMatch.Index);
        //             functionLocations[funcName] = pos;
        //             this.keywordKinds[funcName] = CompletionItemKind.Function;
        //             string[] args = ItemMatch.Groups.Values.ElementAt(3).ToString().Split(",").Select(s => s.Trim()).ToArray();
        //             string comment = this.GetCommentText(pos);
        //             this.comments[funcName] = NewHoverData(funcName, ItemMatch.Index, className, comment, null, args);
        //         }
        //     }
        // }

        public bool InCommentBounds(int index)
        {
            for (int i = 0; i < this.commentBounds.Length; i++)
            {
                CommentBounds bound = this.commentBounds[i];
                if (index >= bound.Start && index <= bound.End)
                {
                    return true;
                }
            }
            return false;
        }

        public string DetectClass(int funcPos) {
            foreach (var data in this.classes)
            {
                if (funcPos >= data.Value.Start && funcPos <= data.Value.End)
                {
                    return data.Key;
                }
            }
            return "";
        }

        private Dictionary<string, ClassScope> GetClassData()
        {
            Dictionary<string, ClassScope> classes = new Dictionary<string, ClassScope>();
            foreach (Match ItemMatch in Regex.Matches(this._text, @"(\bclass\b)[\t ]*([a-zA-Z_][\w]*)")) // keep in sync with syntax file rascript.tmLanguage.json #function-definitions regex
            {
                // dont parse if its in a comment
                if (this.InCommentBounds(ItemMatch.Index))
                {
                    continue;
                }
                int postClassNameInd = ItemMatch.Index + ItemMatch.Groups.Values.ElementAt(0).Length;
                int ind = postClassNameInd;
                Stack<int> stack = new Stack<int>();
                string strippedText = ""; // this is used to determine the implicit arguments to a class constructor
                while (ind < this._text.Length)
                {
                    // anything other than white space or open curly brace is an error and we just wont parse this class
                    if (this._text[ind] != ' ' && this._text[ind] != '\n' && this._text[ind] != '\r' && this._text[ind] != '\t' && this._text[ind] != '{')
                    {
                        break;
                    }
                    if (this._text[ind] == '{')
                    {
                        // get the position of the opening curly brace
                        stack.Push(ind);
                        break;
                    }
                    ind++;
                }
                if (stack.Count == 1)
                {
                    // if we have a curly brace scope, start parsing to find the end of the scope
                    ind = stack.Peek() + 1; // next char after our first open curly brace
                    while (ind < this._text.Length)
                    {
                        if (this._text[ind] == '}')
                        {
                            stack.Pop();
                        }
                        else if (this._text[ind] == '{')
                        {
                            stack.Push(ind);
                        }
                        else
                        {
                            if (stack.Count == 1)
                            {
                                // if the code is at the first level of the class (not in a function) append it to our stripped class
                                strippedText = strippedText + this._text[ind];
                            }
                        }
                        if (stack.Count == 0)
                        {
                            // we have found our end position of the scope, break out
                            break;
                        }
                        ind++;
                    }
                    List<string> args = new List<string>();
                    foreach (Match ItemMatch2 in Regex.Matches(strippedText, @"([a-zA-Z_][\w]*)[\t ]*="))
                    {
                        args.Add(ItemMatch2.Groups.Values.ElementAt(1).ToString());
                    }
                    ClassScope scope = new ClassScope()
                    {
                        Start = ItemMatch.Index,
                        End = ind,
                        Functions = new Dictionary<string, FunctionDefinition>(),
                        ConstructorArgs = args.ToArray()
                    };
                    classes.Add(ItemMatch.Groups.Values.ElementAt(2).ToString(), scope);
                }
            }
            return classes;
        }

        public int CountArgsAt(int offset)
        {
            int count = 0;
            offset++; // move one over, the end offset should be at the character at the end of the function name
            if (this._text[offset] == '(')
            {
                offset++;
                while (offset < this._text.Length)
                {
                    if (this._text[offset] == ')')
                    {
                        break;
                    }
                    if (count == 0)
                    {
                        count = 1;
                    }
                    else
                    {
                        if (this._text[offset] == ',')
                        {
                            count++;
                        }
                    }
                    offset++;
                }
            }
            return count;
        }

        private CommentBounds[] GetCommentBoundsList()
        {
            List<CommentBounds> commentBounds = new List<CommentBounds>();
            bool inComment = false;
            int tempStart = 0;
            if (this._text.Length < 2)
            {
                return commentBounds.ToArray();
            }
            for (int i = 1; i < this._text.Length; i++)
            {
                if (inComment)
                {
                    if (this._text[i] == '\n' || this._text[i] == '\r')
                    {
                        inComment = false;
                        CommentBounds commentBound = new CommentBounds()
                        {
                            Start = tempStart,
                            End = i - 1,
                            Type = "Line",
                            Raw = this._text[tempStart..i]
                        };
                        commentBounds.Add(commentBound);
                    }
                }
                else
                {
                    if (this._text[i - 1] == '/' && this._text[i] == '/')
                    {
                        inComment = true;
                        tempStart = i - 1;
                    }
                }
                if (i == this._text.Length - 1 && inComment)
                {
                    inComment = false;
                    CommentBounds commentBound = new CommentBounds()
                    {
                        Start = tempStart,
                        End = i,
                        Type = "Line",
                        Raw = this._text[tempStart..]
                    };
                    commentBounds.Add(commentBound);
                }
            }
            // parse different comment types seperately incase they are mixed together,
            // the bounds between these two could overlap technically

            // get bounds of block comments
            inComment = false;
            tempStart = 0;
            for (int i = 1; i < this._text.Length; i++)
            {
                if (inComment)
                {
                    if (this._text[i - 1] == '*' && this._text[i] == '/') // end
                    {
                        inComment = false;
                        CommentBounds commentBound = new CommentBounds
                        {
                            Start = tempStart,
                            End = i - 1,
                            Type = "Block",
                            Raw = this._text[tempStart..i]
                        };
                        commentBounds.Add(commentBound);
                    }
                }
                else
                {
                    if (this._text[i - 1] == '/' && this._text[i] == '*') // start
                    {
                        inComment = true;
                        tempStart = i - 1;
                    }
                }
                if (i == this._text.Length - 1 && inComment)
                {
                    inComment = false;
                    CommentBounds commentBound = new CommentBounds()
                    {
                        Start = tempStart,
                        End = i,
                        Type = "Block",
                        Raw = this._text[tempStart..]
                    };
                    commentBounds.Add(commentBound);
                }
            }
            return commentBounds.ToArray();
        }

        private string GetCommentText(Position pos)
        {
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
                    offset = offset + 1;
                }
            }
            string finalComment = untrimmedComment;
            if (blockCommentStarStyle)
            {
                finalComment = comment;
            }
            return finalComment;
        }

        private HoverData NewHoverText(string key, int index, string type, string className, string text, string? docUrl, string[] args)
        {
            string argStr = string.Join(", ", args);
            string[] commentLines = Regex.Split(text, @"\r?\n");
            List<string> lines = new List<string>();
            string prefix = "function ";
            if (className != "")
            {
                prefix = $"// class {className}\nfunction ";
            }
            lines.Add($"```rascript\n{prefix}{key}({argStr})\n```");
            if (type == "class")
            {
                string fnLine = lines[0];
                lines.Clear();
                lines.Add($"```rascript\nclass {key}\n```");
                lines.Add(fnLine);
            }
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
            return new HoverData
            {
                Key = key,
                Index = index,
                ClassName = className,
                Args = args,
                Lines = lines.ToArray()
            };
        }

        private string[] NewHoverData(string key, int index, string className, string text, string? docUrl, string[] args)
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

        public Func<ClassFunction, bool> ClassFilter(bool global, bool usingThis, string className)
        {
            return (el) =>
            {
                if (global)
                {
                    return el.ClassName == "";
                }
                else if (usingThis)
                {
                    return el.ClassName == className;
                }
                return el.ClassName != "";
            };
        }

        public WordLocation GetWordAtPosition(Position pos)
        {
            var lines = this._text.Split('\n');
            var line = lines[pos.Line];
            var index = Convert.ToInt32(pos.Character);
            var leftIndex = Convert.ToInt32(pos.Character);
            var rightIndex = Convert.ToInt32(pos.Character);

            if (index >= line.Length)
            {
                return new WordLocation
                {
                    Word = "",
                    Start = pos,
                    End = pos
                };
            }
            var initialChar = line[Convert.ToInt32(pos.Character)];
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
                            leftIndex = i;
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
                            rightIndex = i;
                            continue;
                        }
                        break;
                    }
                }
            }
            return new WordLocation
            {
                Word = word.ToString(),
                Start = new Position
                {
                    Line = pos.Line,
                    Character = leftIndex,
                },
                End = new Position
                {
                    Line = pos.Line,
                    Character = rightIndex,
                },
            };
        }

        public int GetOffsetAt(Position pos)
        {
            var lines = this._text.Split('\n');
            var line = lines[pos.Line];
            var index = Convert.ToInt32(pos.Character);
            var leftInd = Convert.ToInt32(pos.Character);
            var rightInd = Convert.ToInt32(pos.Character);

            if (index >= line.Length)
            {
                return -1;
            }
            var realLines = new List<string>();
            for (int i = 0; i < pos.Line; i++)
            {
                realLines.Add(lines[i]);
            }
            var partialString = line.Substring(0, index);
            realLines.Add(partialString);
            return String.Join("\n", realLines.ToArray()).Length;
        }

        public WordScope GetScope(Position pos)
        {
            bool global = true;
            bool usingThis = false;
            int offset = this.GetOffsetAt(pos) - 1;

            while (global && offset >= 0)
            {
                if (this._text[offset] != ' ' && this._text[offset] != '\t' && this._text[offset] != '.')
                {
                    break;
                }
                if (this._text[offset] == '.')
                {
                    // in here means the previous non whitespace character next to the word hovered over is a dot which is the class attribute accessor operator
                    global = false;
                    if (offset - 4 >= 0)
                    {
                        if (this._text[offset - 4] == 't' && this._text[offset - 3] == 'h' && this._text[offset - 2] == 'i' && this._text[offset - 1] == 's')
                        {
                            usingThis = true;
                        }
                    }
                    break;
                }
                offset--;
            }
            return new WordScope
            {
                Global = global,
                UsingThis = usingThis
            };
        }

        public WordType GetWordType(WordLocation location)
        {
            bool fn = false;
            bool cls = false;
            int startOffset = this.GetOffsetAt(location.Start);
            int endOffset = this.GetOffsetAt(location.End);

            // check for function
            if (endOffset+1 <= this._text.Length && this._text[endOffset+1] == '(')
            {
                fn = true;
            }
            int offset = startOffset - 1;
            while (offset >= 0)
            {
                // start searching for the previous word to be 'class'
                if (this._text[offset] != ' ' && this._text[offset] != '\t' && this._text[offset] != 's')
                {
                    break;
                }
                if (this._text[offset] == 's')
                {
                    if (offset - 4 >= 0)
                    {
                        if (this._text[offset - 4] == 'c' && this._text[offset - 3] == 'l' && this._text[offset - 2] == 'a' && this._text[offset - 1] == 's')
                        {
                            cls = true;
                        }
                    }
                    break;
                }
                offset--;
            }
            return new WordType
            {
                Function = fn,
                Class = cls,
            };
        }

        public static bool IsWordLetter(char c)
        {
            return char.IsLetterOrDigit(c) || c == '_';
        }

        // public Position? GetLinkLocation(string word)
        // {
        //     if (this.functionLocations.ContainsKey(word))
        //     {
        //         return this.functionLocations[word];
        //     }
        //     return null;
        // }

        // public string[]? GetHoverText(string word)
        // {
        //     if (this.comments.ContainsKey(word))
        //     {
        //         return this.comments[word];
        //     }
        //     return null;
        // }

        // public string[] GetKeywords()
        // {
        //     return this.keywords.ToArray();
        // }

        // public CompletionItemKind? GetKeywordCompletionItemKind(string keyword)
        // {
        //     if (this.keywordKinds.ContainsKey(keyword))
        //     {
        //         return this.keywordKinds[keyword];
        //     }
        //     return null;
        // }

        public List<HoverData>? GetHoverData(string className)
        {
            if (this.words.ContainsKey(className))
            {
                return this.words[className];
            }
            return null;
        }

        public List<ClassFunction>? GetClassFunctionDefinitions(string className)
        {
            if (this.functionDefinitions.ContainsKey(className))
            {
                return this.functionDefinitions[className];
            }
            return null;
        }

        public int GetGameID()
        {
            return this.gameID;
        }
    }
}