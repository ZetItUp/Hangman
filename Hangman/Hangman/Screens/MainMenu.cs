/*
 * Hangman Game
 * 
 * By ZetItUp, 2025
 */
using Hangman.Utils;

namespace Hangman.Screens
{
    // MainMenu class represents the main menu screen of the Hangman game
    public class MainMenu : Screen // Inherits from the base Screen class
    {
        // Constants for the window background and foreground colors
        private const ConsoleColor WindowBackground = ConsoleColor.DarkBlue;
        private const ConsoleColor WindowForeground = ConsoleColor.Gray;

        // String array to hold the menu options
        private string[] options = new string[]
        {
            "1. Start Game",
            "2. Exit"
        };

        public MainMenu()
        {
            // Set the title and description of the main menu
            Title = "Hangman";
            Description = "This game was made in C#";
        }

        /// <summary>
        /// Update the main menu screen.
        /// </summary>
        public override void Update()
        {
            // If the screen was just entered, flush the input buffer and return
            if (JustEnteredScreen)
            {
                // Set the flag to false to indicate that the screen has been entered
                // and properly handled.
                JustEnteredScreen = false;

                // Clear any key presses in the input buffer to avoid processing them
                while (Console.KeyAvailable)
                {
                    Console.ReadKey(true);
                }

                return;
            }

            // Get the current console window dimensions
            int width = Console.WindowWidth;
            int height = Console.WindowHeight;

            // If the window dimensions have changed, redraw the menu
            if (PrevWindowHeight != height || PrevWindowWidth != width)
            {
                // Draw the menu with the new dimensions
                DrawMenu();

                // Update the previous window dimensions to the current ones
                PrevWindowWidth = width;
                PrevWindowHeight = height;
            }

            // If this is the first draw of the screen, draw the menu
            if (FirstDraw)
            {
                DrawMenu();
            }

            // Check if a key is available to read
            if (Console.KeyAvailable)
            {
                // Read the key input from the user
                char choice = Console.ReadKey(true).KeyChar;

                // Check the user's choice and perform the corresponding action
                switch (choice)
                {
                    case '1':
                        // If the user chooses to start the game, set the screen to the game screen
                        ScreenManager.SetScreen(ScreenManager.ScreenID.GameScreen);
                        break;
                    case '2':
                        // If the user chooses to exit, call the ExitGame method
                        ExitGame();
                        break;
                    default:
                        return; // If the input is not recognized, do nothing
                }
            }

            // Call the base Update method, from Screen.cs, to handle any additional updates
            base.Update();
        }

        /// <summary>
        /// Draw the main menu screen.
        /// </summary>
        private void DrawMenu()
        {
            // Clear the console and set the cursor position to the top left corner, 2 lines down
            Console.Clear();
            Console.SetCursorPosition(0, 2);

            // Draw a double box around the main menu area
            DrawDoubleBox(10, 3, Console.WindowWidth - 20, 7, WindowForeground, WindowBackground);

            // Set the cursor position to the center of the screen
            Console.SetCursorPosition(0, 5);
            // Write the title and description centered in the console with specified colors
            ConsoleExt.WriteCentered(Title, ConsoleColor.Yellow, WindowBackground, Console.WindowWidth);
            ConsoleExt.WriteCentered(Description, WindowForeground, WindowBackground, Console.WindowWidth);
            Console.WriteLine();
            ConsoleExt.WriteCentered("Created by ZetItUp, 2025", ConsoleColor.DarkCyan, WindowBackground, Console.WindowWidth);

            // Set the cursor position to somewhere below the title and description, 12 lines down
            Console.SetCursorPosition(0, 12);
            // Draw a bow below the title and description box
            DrawBox(Console.WindowWidth / 2 - 14, 12, 27, 6, WindowForeground);
            // Reset the cursor position to the same line the box started at
            Console.SetCursorPosition(0, 12);
            // Write Main Menu on the same line as the top of the box, centered
            ConsoleExt.WriteCentered(" Main Menu \n", WindowForeground, ConsoleColor.Black, Console.WindowWidth);
            // Write the menu options centered in the box
            ConsoleExt.WriteCentered(options[0], WindowForeground, ConsoleColor.Black, Console.WindowWidth);
            ConsoleExt.WriteCentered(options[1], WindowForeground, ConsoleColor.Black, Console.WindowWidth);
            // Jump to the bottom om the console window
            Console.SetCursorPosition(0, Console.WindowHeight - 1);
        }
    }
}
