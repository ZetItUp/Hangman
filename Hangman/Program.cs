/*
 * Hangman Game
 * 
 * By ZetItUp, 2025
 */
// Include Hangman namespace to access game functionality
using Hangman;

// Disable the cursor for a cleaner UI
Console.CursorVisible = false;

// Initialize the game and run it
Game game = new Game();
game.Run();
