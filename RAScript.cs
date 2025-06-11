using System.Text;
using OmniSharp.Extensions.LanguageServer.Protocol.Models;

namespace RAScriptLanguageServer
{
    public class RAScript
    {
        private readonly string documentUri;
        private readonly StringBuilder document;
        private readonly Parser parser;
        public RAScript(string documentUri, StringBuilder document, Parser parser)
        {
            this.documentUri = documentUri;
            this.document = document;
            this.parser = parser;
        }

        public string GetDocumentText()
        {
            return document.ToString();
        }

        public Parser GetParser()
        {
            return parser;
        }
    }
}