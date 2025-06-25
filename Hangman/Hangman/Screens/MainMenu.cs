using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HangmanUtils;

namespace Hangman.Screens
{
    public class MainMenu : Screen
    {
        private const ConsoleColor WindowBackground = ConsoleColor.DarkBlue;
        private const ConsoleColor WindowForeground = ConsoleColor.Gray;

        private string[] options = new string[]
        {
            "1. Start Game",
            "2. Exit"
        };

        public MainMenu()
        {
            Title = "Hangman 2025";
            Description = "This game was made in C#";
        }

        public override void Update()
        {
            int width = Console.WindowWidth;
            int height = Console.WindowHeight;

            if (PrevWindowHeight != height || PrevWindowWidth != width)
            {
                DrawMenu();

                PrevWindowWidth = width;
                PrevWindowHeight = height;
            }

            if (FirstDraw)
            {
                DrawMenu();
            }

            if (Console.KeyAvailable)
            {
                char choice = Console.ReadKey(true).KeyChar;

                switch (choice)
                {
                    case '1':
                        ScreenManager.SetScreen(ScreenManager.ScreenID.GameScreen);
                        break;
                    case '2':
                        ExitGame();
                        break;
                }
            }

            base.Update();
        }

        private void DrawMenu()
        {
            Console.Clear();
            Console.Write("\n\n");
            DrawDoubleBox(10, 5, Console.WindowWidth - 20, 10, WindowForeground, WindowBackground);
            Console.SetCursorPosition(0, 7);
            ConsoleExt.WriteCentered(Title, ConsoleColor.Yellow, WindowBackground, Console.WindowWidth);
            ConsoleExt.WriteCentered(Description, WindowForeground, WindowBackground, Console.WindowWidth);
            Console.SetCursorPosition(0, 9);
            ConsoleExt.WriteCentered("-= Main Menu =-", WindowForeground, ConsoleColor.Black, Console.WindowWidth);
            Console.SetCursorPosition(0, Console.WindowHeight - 1);
        }
    }
}
