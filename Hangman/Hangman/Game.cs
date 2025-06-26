/*
 * Hangman Game
 * 
 * By ZetItUp, 2025
 */
using Hangman.Screens;

namespace Hangman
{
    // The game class manages the game loop
    public class Game
    {
        private bool running = true;

        public Game()
        {
            // Initialize the screen manager and add screens
            ScreenManager.Initialize();
        }

        public void Run()
        {
            // Get the main menu screen
            var screen = ScreenManager.GetScreen(ScreenManager.ScreenID.MainMenu);

            // Subscribe to the ExitGameEvent to stop the game loop
            screen.ExitGameEvent += () =>
            {
                running = false;
            };

            // Game loop, if running == false, the loop will stop
            while (running)
            {
                // Update the current screen
                ScreenManager.Update();

                // Sleep for a short time to prevent high CPU usage
                Thread.Sleep(10);
            }
        }
    }
}
