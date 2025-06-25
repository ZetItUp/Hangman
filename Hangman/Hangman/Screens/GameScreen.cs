using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman.Screens
{
    public class GameScreen : Screen
    {
        public GameScreen()
        {
            Title = "Hangman Game";
            Description = "Guess the word before you run out of attempts!";
        }

        public override void Update()
        {
            if(FirstDraw)
            {
                DrawGame();
            }

            if(Console.KeyAvailable)
            {
                var key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.Escape:
                        ScreenManager.SetScreen(ScreenManager.ScreenID.MainMenu);
                        break;
                }
            }


            base.Update();
        }

        private void DrawGame()
        {
            Console.Clear();
            DrawDoubleBox(0, 0, Console.WindowWidth, Console.WindowHeight, ConsoleColor.DarkBlue, ConsoleColor.Black);
            Console.SetCursorPosition(2, 1);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(Title);
            Console.SetCursorPosition(2, 3);
            Console.WriteLine(Description);
        }
    }
}
