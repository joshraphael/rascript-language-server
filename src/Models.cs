namespace RAScriptLanguageServer
{
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
}