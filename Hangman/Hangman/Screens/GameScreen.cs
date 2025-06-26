/*
 * Hangman Game
 * 
 * By ZetItUp, 2025
 */
using Hangman.Utils;

namespace Hangman.Screens
{
    // GameScreen class represents the game screen of the Hangman game
    public class GameScreen : Screen
    {
        // Variable to hold the current word to guess
        string currentWord = string.Empty;
        // Variable to hold the guessed letters
        string guessedLetters = string.Empty;

        // Flag to indicate if the word needs to be updated and redrawn
        bool updateWord = true;

        // Constants for the maximum number of tries allowed, change this to increase or decrease difficulty
        const int MAX_TRIES = 6;

        // Number of tries left for the player
        int triesLeft = MAX_TRIES;

        public GameScreen()
        {
            // Set the title and description of the game screen
            Title = "Hangman";
            Description = "Guess the word before you run out of attempts!";
            // Initialize the game by generating a new word
            currentWord = GenerateNewWord();
        }

        /// <summary>
        /// Update the game screen.
        /// </summary>
        public override void Update()
        {
            // If the screen was just entered, reset the game state and clear the input buffer
            if (JustEnteredScreen)
            {
                // Reset the game state
                Reset();
                JustEnteredScreen = false;

                // Clear any key presses in the input buffer to avoid processing them
                while (Console.KeyAvailable)
                {
                    Console.ReadKey(true);
                }

                // Return early to avoid doing any game logic on the first update
                return;
            }

            // Get the current console window dimensions
            int width = Console.WindowWidth;
            int height = Console.WindowHeight;

            // If the window dimensions have changed, redraw the game and word
            if (PrevWindowHeight != height || PrevWindowWidth != width)
            {
                // Draw the game with the new dimensions
                DrawGame();
                // Draw the guessed letters with the new dimensions
                DrawWord();

                // Set previous window dimensions to the current ones
                PrevWindowWidth = width;
                PrevWindowHeight = height;
            }

            // If this is the first draw of the screen, draw the game and guessed letters
            if (FirstDraw)
            {
                DrawGame();
                DrawGuessedLetters();
            }

            // If the word needs to be updated and redrawn
            if (updateWord)
            {
                // Set flag to false
                updateWord = false;
                // Draw the current word with the guessed letters
                DrawWord();

                // Check if player has guessed all letters in the word
                if (CheckVictory())
                {
                    // Draw the victory screen
                    DrawVictory();
                    // Return to stop any other game logic from executing
                    return;
                }
            }

            // If a key is available to read
            if (Console.KeyAvailable)
            {
                // Read the key input
                var keyInfo = Console.ReadKey(true);
                // Get the key pressed
                var key = keyInfo.Key;

                // Check if the key is a letter from A to Z
                if (key >= ConsoleKey.A && key <= ConsoleKey.Z)
                {
                    // Get the letter pressed and convert it to lowercase
                    char guessedLetter = char.ToLower(keyInfo.KeyChar);

                    // If guessedLetters does not contain the guessed letter
                    if (!guessedLetters.Contains(guessedLetter))
                    {
                        // Add the guessed letter to the guessedLetters string
                        guessedLetters += guessedLetter;
                        // Set the flag to update the word
                        updateWord = true;
                        // Draw the guessed letters
                        DrawGuessedLetters();

                        // Clear the line with the messages which shows whether the guess was correct or not
                        // This is done due to the console not clearing the line properly after doing a Console.Clear()
                        Console.SetCursorPosition(1, Console.WindowHeight - 9);
                        ConsoleExt.WriteCentered($"                                  ", ConsoleColor.Gray, ConsoleColor.Black, Console.WindowWidth);

                        // Check if the guessed letter, in lower-case, is in the current word
                        if (currentWord.ToLower().Contains(guessedLetter))
                        {
                            // If the guess is correct, display a success message with green color and black background
                            Console.SetCursorPosition(0, Console.WindowHeight - 9);
                            ConsoleExt.WriteCentered($"Good guess: {guessedLetter}", ConsoleColor.Green, ConsoleColor.Black, Console.WindowWidth);
                        }
                        else
                        {
                            // If the guess is incorrect, display a failure message with red color and black background
                            Console.SetCursorPosition(0, Console.WindowHeight - 9);
                            ConsoleExt.WriteCentered($"Wrong guess: {guessedLetter}", ConsoleColor.Red, ConsoleColor.Black, Console.WindowWidth);

                            // Decrease the number of tries left
                            triesLeft--;
                        }
                    }
                    else
                    {
                        // Clear the line with the messages which shows whether the guess was correct or not
                        // This is done due to the console not clearing the line properly after doing a Console.Clear()
                        Console.SetCursorPosition(1, Console.WindowHeight - 9);
                        ConsoleExt.WriteCentered($"                                  ", ConsoleColor.Gray, ConsoleColor.Black, Console.WindowWidth);
                        // If the letter has already been guessed, display a message with yellow color and black background
                        Console.SetCursorPosition(0, Console.WindowHeight - 9);
                        ConsoleExt.WriteCentered($"You already guessed: {guessedLetter}", ConsoleColor.Yellow, ConsoleColor.Black, Console.WindowWidth);
                    }

                    // Show the number of tries left
                    ShowTriesLeft();
                }
                // Else check if the key is Escape
                else if (key == ConsoleKey.Escape)
                {
                    // If Escape is pressed, change to the main menu screen
                    ScreenManager.SetScreen(ScreenManager.ScreenID.MainMenu);
                    // Reset the game state
                    Reset();
                }
            }

            // Check if the player has run out of tries
            if (triesLeft <= 0)
            {
                // Draw the game over screen
                DrawGameOver();

                // Change to the main menu screen
                ScreenManager.SetScreen(ScreenManager.ScreenID.MainMenu);
            }

            // Update the base screen class
            base.Update();
        }

        /// <summary>
        /// Reset the game state.
        /// </summary>
        public override void Reset()
        {
            // Reset the base screen state
            base.Reset();
            // Generate a new word for the game
            currentWord = GenerateNewWord();
            // Set the flag to update the word
            updateWord = true;
            // Reset the guessed letters and tries left
            guessedLetters = string.Empty;
            triesLeft = MAX_TRIES;
        }

        /// <summary>
        /// Show the number of tries left.
        /// </summary>
        private void ShowTriesLeft()
        {
            // Set the cursor to a specific position on the screen
            Console.SetCursorPosition(3, Console.WindowHeight - 10);
            // Draw first half in yellow color
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"Tries Left:");
            // Draw the number of tries left in white color
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($" {triesLeft}");
        }

        /// <summary>
        /// Generate a new random word for the game.
        /// </summary>
        /// <returns>A string containing the new word</returns>
        private string GenerateNewWord()
        {
            // Get a random word from the WordLoader utility class
            var word = WordLoader.LoadRandomWord();

            // If the word is null or empty, display a message and return an empty string
            if (string.IsNullOrEmpty(word))
            {
                // Show a message indicating no words are available, this shouldn't happen if the words.txt file is present and contains words
                Console.WriteLine("No words available to play.");

                // Return an empty string to indicate no word was generated
                return string.Empty;
            }

            // Return the generated word, trimmed of whitespace
            return word;
        }

        /// <summary>
        /// Draw the current word to guess on the screen.
        /// </summary>
        private void DrawWord()
        {
            // Draw a box around the word display area
            DrawBox(2, 6, Console.WindowWidth - 4, 3, ConsoleColor.Yellow, ConsoleColor.Black);
            Console.SetCursorPosition(4, 7);
            // Draw the text Current Word in yellow color
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Current Word: ");
            // Set the foreground color to green for the letters
            Console.ForegroundColor = ConsoleColor.Green;

            // Loop through each character in the current word
            for (int i = 0; i < currentWord.Length; i++)
            {
                // If the guessed letters contain the current character, display it in uppercase and a space after it
                if ( guessedLetters.Contains(currentWord[i].ToString().ToLower()))
                {
                    Console.Write($"{currentWord[i].ToString().ToUpper()} ");
                }
                // If the guessed letters do not contain the current character, display an underscore with a space
                else
                {
                    Console.Write("_ ");
                }
            }
        }

        /// <summary>
        /// Check if the player has guessed all letters in the current word.
        /// </summary>
        /// <returns>True if all letters are guessed, False otherwise</returns>
        private bool CheckVictory()
        {
            // Loop through each character in the current word
            foreach (char c in currentWord)
            {
                // If the guessed letters do not contain the current character (in lowercase), return false
                if (!guessedLetters.Contains(c.ToString().ToLower()))
                {
                    return false; // Not all letters guessed
                }
            }

            // If all characters in the current word are found in the guessed letters, return true
            return true; // All letters guessed
        }

        /// <summary>
        /// Draw the victory screen when the player wins.
        /// </summary>
        private void DrawVictory()
        {
            // Draw a box around the victory message in green background and yellow border
            DrawBox(2, Console.WindowHeight / 2 - 2, Console.WindowWidth - 4, 5, ConsoleColor.Yellow, ConsoleColor.DarkGreen);
            Console.SetCursorPosition(4, Console.WindowHeight / 2 - 2);
            // Show the YOU WIN message in the top center of the box
            ConsoleExt.WriteCentered(" Y O U   W I N ", ConsoleColor.Yellow, ConsoleColor.DarkGreen, Console.WindowWidth - 4);
            // Show the current word in uppercase in the center of the box
            ConsoleExt.WriteCentered($"The word was: {currentWord.ToUpper()}", ConsoleColor.White, ConsoleColor.DarkGreen, Console.WindowWidth - 4);
            Console.WriteLine();
            // Show a message to press any key to return to the main menu
            ConsoleExt.WriteCentered("Press ANY key to return to the main menu.", ConsoleColor.White, ConsoleColor.DarkGreen, Console.WindowWidth - 4);

            // Reset the console colors to default
            Console.ResetColor();
            // Wait for a key press
            Console.ReadKey(true);

            // Flush the input buffer to ensure no keys are left pressed
            while (Console.KeyAvailable)
            {
                Console.ReadKey(true);
            }

            // Change the screen to the main menu
            ScreenManager.SetScreen(ScreenManager.ScreenID.MainMenu);
            JustEnteredScreen = true;
        }

        /// <summary>
        /// Draw the game over screen when the player loses.
        /// </summary>
        private void DrawGameOver()
        {
            // Draw a box around the game over message in red background and yellow border
            DrawBox(2, Console.WindowHeight / 2 - 2, Console.WindowWidth - 4, 5, ConsoleColor.Yellow, ConsoleColor.DarkRed);
            Console.SetCursorPosition(4, Console.WindowHeight / 2  - 2);
            // Show the GAME OVER message in the top center of the box
            ConsoleExt.WriteCentered(" G A M E   O V E R ", ConsoleColor.Yellow, ConsoleColor.DarkRed, Console.WindowWidth - 4);
            // Show the current word in uppercase in the center of the box
            ConsoleExt.WriteCentered($"The word was: {currentWord.ToUpper()}", ConsoleColor.White, ConsoleColor.DarkRed, Console.WindowWidth - 4);
            Console.WriteLine();
            // Show a message to press any key to return to the main menu
            ConsoleExt.WriteCentered("Press ANY key to return to the main menu.", ConsoleColor.White, ConsoleColor.DarkRed, Console.WindowWidth - 4);
            // Reset the console colors to default
            Console.ResetColor();
            // Wait for a key press
            Console.ReadKey(true);

            // Flush the input buffer to ensure no keys are left pressed
            while (Console.KeyAvailable)
            {
                Console.ReadKey(true);
            }

            // Change the screen to the main menu
            ScreenManager.SetScreen(ScreenManager.ScreenID.MainMenu);
        }

        /// <summary>
        ///  Draw the guessed letters on the screen.
        /// </summary>
        private void DrawGuessedLetters()
        {
            // Draw a box around the guessed letters area
            DrawBox(2, Console.WindowHeight - 8, Console.WindowWidth - 4, 3, ConsoleColor.Red, ConsoleColor.Black);
            Console.SetCursorPosition(4, Console.WindowHeight - 7);
            // Set the foreground color to red for the Guessed Letters label
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Guessed Letters: ");
            // Set the foreground color to white for the actual letters guessed
            Console.ForegroundColor = ConsoleColor.White;

            // Loop through each character in the guessed letters string
            for (int i = 0; i < guessedLetters.Length; i++)
            {
                // Print each guessed letter in uppercase followed by a space
                Console.Write($"{guessedLetters[i].ToString().ToUpper()} ");
            }
        }

        /// <summary>
        /// Draw the game screen with a double box around it.
        /// </summary>
        private void DrawGame()
        {
            // Clear the console and draw a double box around the game screen
            Console.Clear();
            DrawDoubleBox(0, 0, Console.WindowWidth, Console.WindowHeight, ConsoleColor.White, ConsoleColor.Black);
            Console.SetCursorPosition(2, 3);
            Console.ForegroundColor = ConsoleColor.Gray;
            // Write the title and description centered in the console
            ConsoleExt.WriteCentered(Title, ConsoleColor.Yellow, ConsoleColor.Black, Console.WindowWidth);
            ConsoleExt.WriteCentered(Description, ConsoleColor.Gray, ConsoleColor.Black, Console.WindowWidth);

            // Set the cursor position to the bottom left corner of the game area
            Console.SetCursorPosition(0, Console.WindowHeight - 2);
            // Write Press ESC to return to the main menu at the bottom of the screen in cyan color
            ConsoleExt.WriteCentered("Press ESC to return to the main menu.", ConsoleColor.Cyan, ConsoleColor.Black, Console.WindowWidth);
        }
    }
}
