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
        public GameScreen()
        {
            Title = "Hangman";
            Description = "Guess the word before you run out of attempts!";
            currentWord = GenerateNewWord();
        }

        public override void Update()
        {
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
            }


            if (updateWord)
            {
                updateWord = false;
                DrawWord();
                DrawGuessedLetters();
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
                        }
                    }
                    else
                    {
                        Console.SetCursorPosition(1, Console.WindowHeight - 9);
                        ConsoleExt.WriteCentered($"                                  ", ConsoleColor.Gray, ConsoleColor.Black, Console.WindowWidth);
                        Console.SetCursorPosition(0, Console.WindowHeight - 9);
                        ConsoleExt.WriteCentered($"You already guessed: {guessedLetter}", ConsoleColor.Yellow, ConsoleColor.Black, Console.WindowWidth);
                    }
                }
                else if (key == ConsoleKey.Escape)
                {
                    ScreenManager.SetScreen(ScreenManager.ScreenID.MainMenu);
                    Reset();
                }
            }

            base.Update();
        }

        public override void Reset()
        {
            base.Reset();
            currentWord = GenerateNewWord();
            updateWord = true;
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
