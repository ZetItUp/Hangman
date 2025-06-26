using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman.Screens
{
    public static class ScreenManager
    {
        public enum ScreenID
        {
            MainMenu,
            GameScreen
        }

        private static ScreenID currentScreen = ScreenID.MainMenu;
        private static readonly Dictionary<ScreenID, Screen> screens = [];

        public static void Initialize()
        {
            AddScreen(ScreenID.MainMenu, new MainMenu());
            AddScreen(ScreenID.GameScreen, new GameScreen());
        }

        public static Screen GetScreen(ScreenID screenID)
        {
            if (screens.TryGetValue(screenID, out var screen))
            {
                return screen;
            }

            throw new KeyNotFoundException($"Screen with ID {screenID} not found.");
        }

        public static void SetScreen(ScreenID screenID)
        {
            if (screens.ContainsKey(screenID))
            {
                currentScreen = screenID;
                screens[currentScreen].JustEnteredScreen = true;
                screens[currentScreen].Reset();
            }
            else
            {
                throw new KeyNotFoundException($"Screen with ID {screenID} not found.");
            }
        }

        public static void Update()
        {
            switch (currentScreen)
            {
                case ScreenID.MainMenu:
                    screens[ScreenID.MainMenu].Update();
                    break;
                case ScreenID.GameScreen:
                    screens[ScreenID.GameScreen].Update();
                    break;
            }
        }

        
        private static void AddScreen(ScreenID screenID, Screen screen)
        {
            screens.Add(screenID, screen);
        }
    }
}
