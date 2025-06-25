using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman.Screens
{
    public class Screen
    {
        public event Action ExitGameEvent;
        public string Title { get; set; } = "<Title>";
        public string Description { get; set; } = "<Description>";

        protected bool FirstDraw = true;
        protected int PrevWindowWidth = Console.WindowWidth;
        protected int PrevWindowHeight = Console.WindowHeight;

        public Screen()
        {

        }

        public virtual void Reset()
        {
            FirstDraw = true;
            PrevWindowWidth = Console.WindowWidth;
            PrevWindowHeight = Console.WindowHeight;
        }

        protected void ExitGame()
        {
            ExitGameEvent?.Invoke();
        }

        public virtual void Update()
        {
            if (FirstDraw)
            {
                FirstDraw = false;
            }
        }

        protected void DrawDoubleBox(int x, int y, int width, int height, ConsoleColor fgColor = ConsoleColor.Gray, ConsoleColor bgColor = ConsoleColor.Black)
        {
            if(width < 2 || height < 2)
            {
                return;
            }

            if (x < 0)
            {
                x = 0;
            }
            if (y < 0)
            {
                y = 0;
            }
            if (x + width > Console.WindowWidth)
            {
                width = Console.WindowWidth - x;
            }
            if (y + height > Console.WindowHeight)
            {
                height = Console.WindowHeight - y;
            }

            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = fgColor;
            Console.BackgroundColor = bgColor;

            Console.Write("╔" + new string('═', width - 2) + "╗");

            for (int i = 1; i < height - 1; i++)
            {
                Console.SetCursorPosition(x, y + i);
                Console.Write("║" + new string(' ', width - 2) + "║");
            }

            Console.SetCursorPosition(x, y + height - 1);
            Console.Write("╚" + new string('═', width - 2) + "╝");
        }

        protected void DrawBox(int x, int y, int width, int height, ConsoleColor fgColor = ConsoleColor.Gray, ConsoleColor bgColor = ConsoleColor.Black)
        {
            if (width < 2 || height < 2)
            {
                return;
            }

            if(x < 0)
            {
                x = 0;
            }
            if(y < 0)
            {
                y = 0;
            }
            if (x + width > Console.WindowWidth)
            {
                width = Console.WindowWidth - x;
            }
            if (y + height > Console.WindowHeight)
            {
                height = Console.WindowHeight - y;
            }

            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = fgColor;
            Console.BackgroundColor = bgColor;

            Console.Write("┌" + new string('─', width - 2) + "┐");

            for (int i = 1; i < height - 1; i++)
            {
                Console.SetCursorPosition(x, y + i);
                Console.Write("│" + new string(' ', width - 2) + "│");
            }

            Console.SetCursorPosition(x, y + height - 1);
            Console.Write("└" + new string('─', width - 2) + "┘");
        }
    }
}
