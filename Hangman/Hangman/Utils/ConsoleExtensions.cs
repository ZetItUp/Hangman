using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanUtils
{
    public static class ConsoleExt
    {
        public static int GetCenterPosition(string text, int consoleWidth = 80)
        {
            if (consoleWidth <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(consoleWidth), "Console width must be greater than zero.");
            }

            if (string.IsNullOrEmpty(text))
            {
                return 0;
            }

            int textLength = text.Length;
            int centerPosition = (consoleWidth - textLength) / 2;

            return Math.Max(centerPosition, 0);
        }

        public static void WriteCentered(string text, ConsoleColor fgColor = ConsoleColor.Gray, ConsoleColor bgColor = ConsoleColor.Black, int consoleWidth = 80)
        {
            int centerPosition = GetCenterPosition(text, consoleWidth);

            Console.ForegroundColor = fgColor;
            Console.BackgroundColor = bgColor;
            Console.SetCursorPosition(centerPosition, Console.CursorTop);
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }
}
