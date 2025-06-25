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

        bool updateWord = true;
        public GameScreen()
        {
            Title = "Hangman Game";
            Description = "Guess the word before you run out of attempts!";
            currentWord = GenerateNewWord();
        }

        public override void Update()
        {
            if(FirstDraw)
            {
                DrawGame();
            }


            if (updateWord)
            {
                updateWord = false;
                DrawWord();
            }

            if(Console.KeyAvailable)
            {
                var key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.Escape:
                        ScreenManager.SetScreen(ScreenManager.ScreenID.MainMenu);
                        Reset();
                        break;
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
            DrawBox(2, 5, Console.WindowWidth - 4, 3, ConsoleColor.White, ConsoleColor.Black);
            Console.SetCursorPosition(3, 6);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Current Word: ");
            for(int i = 0; i < currentWord.Length; i++)
            {
                if (char.IsLetter(currentWord[i]))
                {
                    Console.Write("_ ");
                }
                else
                {
                    Console.Write(currentWord[i] + " ");
                }
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
        }
    }
}
