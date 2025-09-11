using OmniSharp.Extensions.LanguageServer.Protocol.Models;

namespace RAScriptLanguageServer
{
    public class ClassScope
    {
        public required int Start { get; set; }
        public required int End { get; set; }
        public required Dictionary<string, FunctionDefinition> Functions { get; set; }
        public required string[] ConstructorArgs { get; set; }
    }
    public class CommentBounds
    {
        public required int Start { get; set; }
        public required int End { get; set; }
        public required string Type { get; set; }
        public required string Raw { get; set; }
    }
    public class FunctionDefinition
    {
        public required string Key { get; set; }
        public required string URL { get; set; }
        public required string[] Args { get; set; }
        public required string[] CommentDoc { get; set; }
    }

    public class ClassFunction
    {
        public required string ClassName { get; set; }
        public required string Name { get; set; }
        public required Position Pos { get; set; }
        public required string[] Args { get; set; }
    }

    public class HoverData
    {
        public required string Key { get; set; }
        public required int Index { get; set; }
        public required string ClassName { get; set; }
        public required string[] Args { get; set; }
        public required string[] Lines { get; set; }
    }
}