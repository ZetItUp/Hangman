using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hangman.Screens;

namespace Hangman
{

    public class Game
    {
        private bool running = true;

        public Game()
        {
            ScreenManager.Initialize();
        }

        public void Run()
        {
            var screen = ScreenManager.GetScreen(ScreenManager.ScreenID.MainMenu);

            screen.ExitGameEvent += () =>
            {
                running = false;
            };


            while (running)
            {
                ScreenManager.Update();
                Thread.Sleep(10);
            }
        }
    }
}
