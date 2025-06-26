/*
 * Hangman Game
 * 
 * By ZetItUp, 2025
 */
namespace Hangman.Screens
{
    // ScreenManager class manages different screens in the Hangman game
    public static class ScreenManager
    {
        // Enum to define the different screens available in the game
        public enum ScreenID
        {
            MainMenu,
            GameScreen
        }

        // Current screen being displayed
        private static ScreenID currentScreen = ScreenID.MainMenu;
        // Dictionary to hold the screens with their IDs
        private static readonly Dictionary<ScreenID, Screen> screens = [];

        // Initialize the screen manager by adding the main menu and game screen
        // This method should be called at the start of the game
        public static void Initialize()
        {
            AddScreen(ScreenID.MainMenu, new MainMenu());
            AddScreen(ScreenID.GameScreen, new GameScreen());
        }

        /// <summary>
        /// Get a Screen by its ID.
        /// </summary>
        /// <param name="screenID">The ID of the Screen</param>
        /// <returns>Screen with the matching ID</returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public static Screen GetScreen(ScreenID screenID)
        {
            // Check if the screen exists in the dictionary and return it
            // out var screen is a pattern matching feature in C# that checks if the key exists and assigns the value to the variable
            if (screens.TryGetValue(screenID, out var screen))
            {
                return screen;
            }

            // If the screen does not exist, throw an exception with a descriptive message
            throw new KeyNotFoundException($"Screen with ID {screenID} not found.");
        }

        /// <summary>
        /// Sets the screen to the specified ScreenID.
        /// </summary>
        /// <param name="screenID">Screen ID</param>
        /// <exception cref="KeyNotFoundException"></exception>
        public static void SetScreen(ScreenID screenID)
        {
            // Check if the screen exists in the dictionary
            if (screens.ContainsKey(screenID))
            {
                // If the screen exists, set it as the current screen
                currentScreen = screenID;
                // Reset the screen state
                screens[currentScreen].JustEnteredScreen = true;
                screens[currentScreen].Reset();
            }
            else
            {
                // If the screen does not exist, throw an exception with a descriptive message
                throw new KeyNotFoundException($"Screen with ID {screenID} not found.");
            }
        }

        /// <summary>
        /// Updates the current screen.
        /// </summary>
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

        /// <summary>
        /// Add a screen to the ScreenManager.
        /// </summary>
        /// <param name="screenID">Screen ID</param>
        /// <param name="screen">Screen object</param>
        private static void AddScreen(ScreenID screenID, Screen screen)
        {
            screens.Add(screenID, screen);
        }
    }
}
