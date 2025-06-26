/*
 * Hangman Game
 * 
 * By ZetItUp, 2025
 */
namespace Hangman.Utils
{
    // Static class for console extensions
    public static class ConsoleExt
    {
        /// <summary>
        /// Get the center position for a given text based on the console width.
        /// </summary>
        /// <param name="text">The text that is being printed</param>
        /// <param name="consoleWidth">Width of the Console window</param>
        /// <returns>The X position which would be the center position to start writing the text</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static int GetCenterPosition(string text, int consoleWidth = 80)
        {
            // Validate the console width
            if (consoleWidth <= 0)
            {
                // Throw an exception if the console width is not greater than zero
                throw new ArgumentOutOfRangeException(nameof(consoleWidth), "Console width must be greater than zero.");
            }

            // If the text is null or empty, return 0 as the center position
            if (string.IsNullOrEmpty(text))
            {
                return 0;
            }

            // Calculate the center position based on the text length and console width
            int textLength = text.Length;
            // Calculate the center position by subtracting the text length from the console width and dividing by 2
            int centerPosition = (consoleWidth - textLength) / 2;

            // Return the center position, ensuring it is not negative
            return Math.Max(centerPosition, 0);
        }

        /// <summary>
        /// Write a text centered in the console with specified foreground and background colors.
        /// </summary>
        /// <param name="text">Text to print</param>
        /// <param name="fgColor">Foreground Color</param>
        /// <param name="bgColor">Background Color</param>
        /// <param name="consoleWidth">Width of the Console Window</param>
        public static void WriteCentered(string text, ConsoleColor fgColor = ConsoleColor.Gray, ConsoleColor bgColor = ConsoleColor.Black, int consoleWidth = 80)
        {
            // Get the center position for the text
            int centerPosition = GetCenterPosition(text, consoleWidth);

            // Set the console colors and write the text at the center position
            Console.ForegroundColor = fgColor;
            Console.BackgroundColor = bgColor;
            Console.SetCursorPosition(centerPosition, Console.CursorTop);
            Console.WriteLine(text);

            // Reset the console colors to default
            Console.ResetColor();
        }
    }
}
