using OmniSharp.Extensions.LanguageServer.Protocol.Models;
using OmniSharp.Extensions.LanguageServer.Protocol.Server;
using OmniSharp.Extensions.LanguageServer.Protocol.Window;
using System.Text;

namespace RAScriptLanguageServer
{

    public class TextPositions
    {
        private readonly ILanguageServerFacade _router;
        readonly long[] lineStartPositions;
        readonly List<string> lines;
        public TextPositions(ILanguageServerFacade router, string text)
        {
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }
            _router = router;
            List<long> lineStarts = new List<long>();
            this.lines = new List<string>();
            int currentPosition = 0;
            int currentLineStart = 0;
            StringBuilder line = new StringBuilder();
            while (currentPosition < text.Length)
            {
                char currentChar = text[currentPosition];
                currentPosition++;

                switch (currentChar)
                {
                    case '\r':
                        {
                            if (currentPosition < text.Length && text[currentPosition] == '\n')
                                currentPosition++;

                            goto case '\n';
                        }
                    case '\n':
                        {
                            lineStarts.Add(currentLineStart);
                            currentLineStart = currentPosition;
                            this.lines.Add(line.ToString());
                            line = new StringBuilder();
                            break;
                        }
                    default:
                        {
                            line.Append(currentChar);
                            break;
                        }
                }
            }
            lineStarts.Add(currentLineStart);
            this.lines.Add(line.ToString());
            this.lineStartPositions = lineStarts.ToArray();
        }

        public Position GetPosition(long absolutePosition)
        {
            int targetLine = Array.BinarySearch(lineStartPositions, absolutePosition);
            if (targetLine < 0)
                targetLine = ~targetLine - 1; // No match, so BinarySearch returns 2's complement of the following line index.

            // Internally, we're 0-based, but lines and columns are (by convention) 1-based.
            return new Position(
                targetLine,
                Convert.ToInt32(absolutePosition - lineStartPositions[targetLine])
            );
        }

        public string? GetLineAt(int line)
        {
            if (line < 0 || line >= this.lines.Count)
            {
                return null;
            }
            return this.lines[line];
        }

        public List<string> GetLines()
        {
            return this.lines;
        }
    }
}