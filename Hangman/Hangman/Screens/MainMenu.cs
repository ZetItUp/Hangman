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
            Title = "Hangman";
            Description = "This game was made in C#";
        }

        public override void Update()
        {
            if (JustEnteredScreen)
            {
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
            Console.SetCursorPosition(0, 0);

            Console.Write("\n\n");
            DrawDoubleBox(10, 3, Console.WindowWidth - 20, 7, WindowForeground, WindowBackground);
            Console.SetCursorPosition(0, 5);
            ConsoleExt.WriteCentered(Title, ConsoleColor.Yellow, WindowBackground, Console.WindowWidth);
            ConsoleExt.WriteCentered(Description, WindowForeground, WindowBackground, Console.WindowWidth);
            Console.WriteLine();
            ConsoleExt.WriteCentered("Created by ZetItUp, 2025", ConsoleColor.DarkCyan, WindowBackground, Console.WindowWidth);
            Console.SetCursorPosition(0, 12);
            DrawBox(Console.WindowWidth / 2 - 14, 12, 27, 6, WindowForeground);
            Console.SetCursorPosition(0, 12);
            ConsoleExt.WriteCentered(" Main Menu \n", WindowForeground, ConsoleColor.Black, Console.WindowWidth);
            ConsoleExt.WriteCentered(options[0], WindowForeground, ConsoleColor.Black, Console.WindowWidth);
            ConsoleExt.WriteCentered(options[1], WindowForeground, ConsoleColor.Black, Console.WindowWidth);
            Console.SetCursorPosition(0, Console.WindowHeight - 1);
        }
    }
}
