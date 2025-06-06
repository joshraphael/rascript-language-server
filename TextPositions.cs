using OmniSharp.Extensions.LanguageServer.Protocol.Models;

namespace RAScriptLanguageServer
{

    public class TextPositions
    {
        readonly long[] lineStartPositions;
        public TextPositions(string text)
        {
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }
            lineStartPositions = CalculateLineStartPositions(text);
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

        long[] CalculateLineStartPositions(string text)
        {
            if (text == null)
                throw new ArgumentNullException(nameof(text));

            List<long> lineStarts = new List<long>();

            int currentPosition = 0;
            int currentLineStart = 0;
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

                            break;
                        }
                }
            }
            lineStarts.Add(currentLineStart);

            return lineStarts.ToArray();
        }
    }
}