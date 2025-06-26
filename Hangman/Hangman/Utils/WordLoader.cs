/*
 * Hangman Game
 * 
 * By ZetItUp, 2025
 */
namespace Hangman.Utils
{
    // Static class for loading random words from a file
    public static class WordLoader
    {
        // Random number generator for selecting a random word
        private static readonly Random rng = new Random();

        /// <summary>
        /// Load Random Word from a specified file.
        /// </summary>
        /// <param name="filePath">Full path to a textfile containing words</param>
        /// <returns>A random word from the file</returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="Exception"></exception>
        public static string LoadRandomWord(string filePath = "words.txt")
        {
            // Validate the file path
            if (string.IsNullOrEmpty(filePath))
            {
                //  hrow an exception if the file path is null or empty
                throw new ArgumentException("File path cannot be null or empty.", nameof(filePath));
            }

            // Try to read the file and select a random word
            try
            {
                // Read all lines from the file, filter out empty lines and lines with spaces
                var words = File.ReadAllLines(filePath).Where(line => !string.IsNullOrWhiteSpace(line) && !line.Contains(" ")).ToList();

                // If no valid words are found, return an empty string
                if (words.Count == 0)
                {
                    return string.Empty; 
                }

                // Select a random index from the list of words
                int randomIndex = rng.Next(words.Count);

                // Return the word at the random index, trimmed of whitespace
                return words[randomIndex].Trim();
            }
            catch (Exception ex)
            {
                // If an error occurs while reading the file, throw an exception with the error message
                throw new Exception($"Error loading words from file: {ex.Message}", ex);
            }
        }
    }
}
