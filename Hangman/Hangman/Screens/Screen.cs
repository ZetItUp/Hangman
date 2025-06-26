/*
 * Hangman Game
 * 
 * By ZetItUp, 2025
 */
namespace Hangman.Screens
{
    // Base class for all screens in the Hangman game
    public class Screen
    {
        // Event to signal when the game should exit
        public event Action ExitGameEvent;
        // Default title and description for the screen
        public string Title { get; set; } = "<Title>";
        public string Description { get; set; } = "<Description>";

        // Flags and variables to manage screen state
        protected bool FirstDraw = true;                             // Indicates if this is the first draw of the screen
        protected int PrevWindowWidth = Console.WindowWidth;         // Previous window width to detect changes
        protected int PrevWindowHeight = Console.WindowHeight;       // Previous window height to detect changes
        public bool JustEnteredScreen = true;                        // Indicates if the screen was just entered

        public Screen()
        {

        }

        // Resets the screen state, called when entering the screen
        public virtual void Reset()
        {
            // Reset the flags and previous window dimensions
            FirstDraw = true;
            JustEnteredScreen = true;
            PrevWindowWidth = Console.WindowWidth;
            PrevWindowHeight = Console.WindowHeight;
        }

        // Method to exit the game, triggers the ExitGameEvent
        protected void ExitGame()
        {
            // Invoke the exit game event to notify subscribers that the event has occurred
            // Make sure to check if there are any subscribers before invoking
            ExitGameEvent?.Invoke();
        }

        // Updates the screen state, called in the game loop
        public virtual void Update()
        {
            // If this is the first draw, set the flag to false
            if (FirstDraw)
            {
                FirstDraw = false;
            }
        }

        /// <summary>
        /// Draws a box with double lines around it.
        /// </summary>
        /// <param name="x">X Position</param>
        /// <param name="y">Y Postion</param>
        /// <param name="width">Box Width</param>
        /// <param name="height">Box Height</param>
        /// <param name="fgColor">Foreground Color</param>
        /// <param name="bgColor">Background Color</param>
        protected void DrawDoubleBox(int x, int y, int width, int height, ConsoleColor fgColor = ConsoleColor.Gray, ConsoleColor bgColor = ConsoleColor.Black)
        {
            // Validate the dimensions of the box, if they are less than 2, return without drawing
            if (width < 2 || height < 2)
            {
                return;
            }

            // Ensure the box fits within the console window dimensions
            // If the x or y position is negative, set it to 0
            if (x < 0)
            {
                x = 0;
            }
            // If the y position is negative, set it to 0
            if (y < 0)
            {
                y = 0;
            }
            // If the box exceeds the console window width or height, adjust the width and height accordingly
            // by subtracting the x and y positions from the console window dimensions
            if (x + width > Console.WindowWidth)
            {
                width = Console.WindowWidth - x;
            }
            if (y + height > Console.WindowHeight)
            {
                height = Console.WindowHeight - y;
            }

            // Set the cursor position to the top-left corner of the box
            Console.SetCursorPosition(x, y);
            // Set the foreground and background colors for the box
            Console.ForegroundColor = fgColor;
            Console.BackgroundColor = bgColor;

            // Draw the top border of the box
            Console.Write("╔" + new string('═', width - 2) + "╗");

            // Draw the sides of the box
            for (int i = 1; i < height - 1; i++)
            {
                // Set the cursor position for each row of the box, add the current row index to the y position
                Console.SetCursorPosition(x, y + i);
                // Draw the left and right sides of the box with spaces in between
                Console.Write("║" + new string(' ', width - 2) + "║");
            }

            // Draw the bottom border of the box
            Console.SetCursorPosition(x, y + height - 1);
            Console.Write("╚" + new string('═', width - 2) + "╝");
        }

        /// <summary>
        /// Draws a box with single lines around it.
        /// </summary>
        /// <param name="x">X Position</param>
        /// <param name="y">Y Postion</param>
        /// <param name="width">Box Width</param>
        /// <param name="height">Box Height</param>
        /// <param name="fgColor">Foreground Color</param>
        /// <param name="bgColor">Background Color</param>
        protected void DrawBox(int x, int y, int width, int height, ConsoleColor fgColor = ConsoleColor.Gray, ConsoleColor bgColor = ConsoleColor.Black)
        {
            // Validate the dimensions of the box, if they are less than 2, return without drawing
            if (width < 2 || height < 2)
            {
                return;
            }

            // Ensure the box fits within the console window dimensions
            // If the x or y position is negative, set it to 0
            if (x < 0)
            {
                x = 0;
            }
            // If the y position is negative, set it to 0
            if (y < 0)
            {
                y = 0;
            }
            // If the box exceeds the console window width or height, adjust the width and height accordingly
            // by subtracting the x and y positions from the console window dimensions
            if (x + width > Console.WindowWidth)
            {
                width = Console.WindowWidth - x;
            }
            if (y + height > Console.WindowHeight)
            {
                height = Console.WindowHeight - y;
            }

            // Set the cursor position to the top-left corner of the box
            Console.SetCursorPosition(x, y);
            // Set the foreground and background colors for the box
            Console.ForegroundColor = fgColor;
            Console.BackgroundColor = bgColor;

            // Draw the top border of the box
            Console.Write("┌" + new string('─', width - 2) + "┐");

            // Draw the sides of the box
            for (int i = 1; i < height - 1; i++)
            {
                // Set the cursor position for each row of the box, add the current row index to the y position
                Console.SetCursorPosition(x, y + i);
                // Draw the left and right sides of the box with spaces in between
                Console.Write("│" + new string(' ', width - 2) + "│");
            }

            // Draw the bottom border of the box
            Console.SetCursorPosition(x, y + height - 1);
            Console.Write("└" + new string('─', width - 2) + "┘");
        }
    }
}
