using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MitchJourn_e.Classes
{
    internal class RandomWord
    {
        string[] rawData;
        int wordCount;

        /// <summary>
        /// Gets a random word from the SDWords.txt file.
        /// The list of words are ones that have been used to train Stable Diffusion.
        /// </summary>
        public RandomWord()
        {
            rawData = File.ReadAllLines("SDWords.txt");
            wordCount = rawData.Length;
        }

        /// <summary>
        /// Returns 1 word used in Stable Diffusion training
        /// </summary>
        /// <returns>string word</returns>
        public string GetWord()
        {
            Random random = new Random();
            string randomWord = rawData[random.Next(0, wordCount - 1)];

            return randomWord;
        }
    }
}
