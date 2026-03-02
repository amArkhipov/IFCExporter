using System.Text;

namespace IFCExporter.RevitCommand
{
    public class KirillicEncoder : IEncoder
    {
        public string EncodeToEscapeSequence(string text)
        {
            StringBuilder builder = new StringBuilder();
            for (var i = 0; i < text.Length;)
            {
                if (IsKirillicSymbol(text[i])) 
                {
                    builder.Append(@"\X2\");
                    while (i < text.Length && IsKirillicSymbol(text[i]))
                    {
                        ushort codePoint = (ushort)text[i];
                        // Форматируем число в четырехзначный HEX
                        string hexCode = codePoint.ToString("X4");
                        builder.Append(hexCode);
                        i++;
                    }
                    builder.Append(@"\X0\");
                }
                else
                {
                    builder.Append(text[i]);
                    i++;
                }
            }
            return builder.ToString();
        }
        private bool IsKirillicSymbol(char ch)
        {
            // Диапазоны для кириллического диапазона Unicode
            return (ch >= '\u0400' && ch <= '\u04FF');
        }
    }
}
