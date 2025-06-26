using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hangman.Utils;
using HangmanUtils;

namespace Hangman.Screens
{
    public class GameScreen : Screen
    {
        string currentWord = string.Empty;
        string guessedLetters = string.Empty;

        bool updateWord = true;

        int triesLeft = 6;

        public GameScreen()
        {
            Title = "Hangman";
            Description = "Guess the word before you run out of attempts!";
            currentWord = GenerateNewWord();
        }

        public override void Update()
        {
            if (JustEnteredScreen)
            {
                Reset();
                JustEnteredScreen = false;

                while (Console.KeyAvailable)
                {
                    Console.ReadKey(true);
                }

                return;
            }

            int width = Console.WindowWidth;
            int height = Console.WindowHeight;

            if (PrevWindowHeight != height || PrevWindowWidth != width)
            {
                DrawGame();
                DrawWord();

                PrevWindowWidth = width;
                PrevWindowHeight = height;
            }

            if (FirstDraw)
            {
                DrawGame();
                DrawGuessedLetters();
            }


            if (updateWord)
            {
                updateWord = false;
                DrawWord();

                if (CheckVictory())
                {
                    DrawVictory();
                    return;
                }
            }

            if(Console.KeyAvailable)
            {
                var keyInfo = Console.ReadKey(true);
                var key = keyInfo.Key;
                
                if(key >= ConsoleKey.A && key <= ConsoleKey.Z)
                {
                    char guessedLetter = char.ToLower(keyInfo.KeyChar);
                    
                    if(!guessedLetters.Contains(guessedLetter))
                    {
                        guessedLetters += guessedLetter;
                        updateWord = true;
                        DrawGuessedLetters();

                        Console.SetCursorPosition(1, Console.WindowHeight - 9);
                        ConsoleExt.WriteCentered($"                                  ", ConsoleColor.Gray, ConsoleColor.Black, Console.WindowWidth);

                        if (currentWord.ToLower().Contains(guessedLetter))
                        {
                            Console.SetCursorPosition(0, Console.WindowHeight - 9);
                            ConsoleExt.WriteCentered($"Good guess: {guessedLetter}", ConsoleColor.Green, ConsoleColor.Black, Console.WindowWidth);
                        }
                        else
                        {
                            Console.SetCursorPosition(0, Console.WindowHeight - 9);
                            ConsoleExt.WriteCentered($"Wrong guess: {guessedLetter}", ConsoleColor.Red, ConsoleColor.Black, Console.WindowWidth);
                            triesLeft--;
                        }
                    }
                    else
                    {
                        Console.SetCursorPosition(1, Console.WindowHeight - 9);
                        ConsoleExt.WriteCentered($"                                  ", ConsoleColor.Gray, ConsoleColor.Black, Console.WindowWidth);
                        Console.SetCursorPosition(0, Console.WindowHeight - 9);
                        ConsoleExt.WriteCentered($"You already guessed: {guessedLetter}", ConsoleColor.Yellow, ConsoleColor.Black, Console.WindowWidth);
                    }

                    ShowTriesLeft();
                }
                else if (key == ConsoleKey.Escape)
                {
                    ScreenManager.SetScreen(ScreenManager.ScreenID.MainMenu);
                    Reset();
                }
            }

            if(triesLeft <= 0)
            {
                DrawGameOver();

                ScreenManager.SetScreen(ScreenManager.ScreenID.MainMenu);
            }

            base.Update();
        }

        public override void Reset()
        {
            base.Reset();
            currentWord = GenerateNewWord();
            updateWord = true;
            guessedLetters = string.Empty;
            triesLeft = 6;
        }

        private void ShowTriesLeft()
        {
            Console.SetCursorPosition(3, Console.WindowHeight - 10);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"Tries Left:");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($" {triesLeft}");
        }

        private string GenerateNewWord()
        {
            var word = WordLoader.LoadRandomWord();

            if (string.IsNullOrEmpty(word))
            {
                Console.WriteLine("No words available to play.");
                return string.Empty;
            }

            return word;
        }

        private void DrawWord()
        {
            DrawBox(2, 6, Console.WindowWidth - 4, 3, ConsoleColor.Yellow, ConsoleColor.Black);
            Console.SetCursorPosition(4, 7);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Current Word: ");
            Console.ForegroundColor = ConsoleColor.Green;

            for (int i = 0; i < currentWord.Length; i++)
            {
                if( guessedLetters.Contains(currentWord[i].ToString().ToLower()))
                {
                    Console.Write($"{currentWord[i].ToString().ToUpper()} ");
                }
                else
                {
                    Console.Write("_ ");
                }
            }
        }

        private bool CheckVictory()
        {
            foreach (char c in currentWord)
            {
                if (!guessedLetters.Contains(c.ToString().ToLower()))
                {
                    return false; // Not all letters guessed
                }
            }
            return true; // All letters guessed
        }

        private void DrawVictory()
        {             
            DrawBox(2, Console.WindowHeight / 2 - 2, Console.WindowWidth - 4, 5, ConsoleColor.Yellow, ConsoleColor.DarkGreen);
            Console.SetCursorPosition(4, Console.WindowHeight / 2 - 2);
            ConsoleExt.WriteCentered(" Y O U   W I N ", ConsoleColor.Yellow, ConsoleColor.DarkGreen, Console.WindowWidth - 4);
            ConsoleExt.WriteCentered($"The word was: {currentWord.ToUpper()}", ConsoleColor.White, ConsoleColor.DarkGreen, Console.WindowWidth - 4);
            Console.WriteLine();
            ConsoleExt.WriteCentered("Press ANY key to return to the main menu.", ConsoleColor.White, ConsoleColor.DarkGreen, Console.WindowWidth - 4);
            Console.ResetColor();
            Console.ReadKey(true);

            while (Console.KeyAvailable)
            {
                Console.ReadKey(true);
            }

            ScreenManager.SetScreen(ScreenManager.ScreenID.MainMenu);
            JustEnteredScreen = true;
        }

        private void DrawGameOver()
        {
            DrawBox(2, Console.WindowHeight / 2 - 2, Console.WindowWidth - 4, 5, ConsoleColor.Yellow, ConsoleColor.DarkRed);
            Console.SetCursorPosition(4, Console.WindowHeight / 2  - 2);
            ConsoleExt.WriteCentered(" G A M E   O V E R ", ConsoleColor.Yellow, ConsoleColor.DarkRed, Console.WindowWidth - 4);
            ConsoleExt.WriteCentered($"The word was: {currentWord.ToUpper()}", ConsoleColor.White, ConsoleColor.DarkRed, Console.WindowWidth - 4);
            Console.WriteLine();
            ConsoleExt.WriteCentered("Press ANY key to return to the main menu.", ConsoleColor.White, ConsoleColor.DarkRed, Console.WindowWidth - 4);
            Console.ResetColor();
            Console.ReadKey(true);

            while (Console.KeyAvailable)
            {
                Console.ReadKey(true);
            }

            ScreenManager.SetScreen(ScreenManager.ScreenID.MainMenu);
            JustEnteredScreen = true;
        }

        private void DrawGuessedLetters()
        {
            DrawBox(2, Console.WindowHeight - 8, Console.WindowWidth - 4, 3, ConsoleColor.Red, ConsoleColor.Black);
            Console.SetCursorPosition(4, Console.WindowHeight - 7);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Guessed Letters: ");
            Console.ForegroundColor = ConsoleColor.White;

            for (int i = 0; i < guessedLetters.Length; i++)
            {
                Console.Write($"{guessedLetters[i].ToString().ToUpper()} ");
            }
        }

        private void DrawGame()
        {
            Console.Clear();
            DrawDoubleBox(0, 0, Console.WindowWidth, Console.WindowHeight, ConsoleColor.White, ConsoleColor.Black);
            Console.SetCursorPosition(2, 3);
            Console.ForegroundColor = ConsoleColor.Gray;
            ConsoleExt.WriteCentered(Title, ConsoleColor.Yellow, ConsoleColor.Black, Console.WindowWidth);
            ConsoleExt.WriteCentered(Description, ConsoleColor.Gray, ConsoleColor.Black, Console.WindowWidth);

            Console.SetCursorPosition(0, Console.WindowHeight - 2);
            ConsoleExt.WriteCentered("Press ESC to return to the main menu.", ConsoleColor.Cyan, ConsoleColor.Black, Console.WindowWidth);
        }
    }
}
