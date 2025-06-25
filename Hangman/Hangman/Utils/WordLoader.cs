using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman.Utils
{
    public static class WordLoader
    {
        private static readonly Random rng = new Random();

        public static string LoadRandomWord(string filePath = "words.txt")
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentException("File path cannot be null or empty.", nameof(filePath));
            }

            try
            {
                var words = File.ReadAllLines(filePath).Where(line => !string.IsNullOrWhiteSpace(line) && !line.Contains(" ")).ToList();

                if (words.Count == 0)
                {
                    return string.Empty; 
                }

                int randomIndex = rng.Next(words.Count);

                return words[randomIndex].Trim();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error loading words from file: {ex.Message}", ex);
            }
        }
    }
}
