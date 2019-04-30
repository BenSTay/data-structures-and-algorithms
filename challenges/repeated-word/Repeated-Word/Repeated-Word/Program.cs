using System;
using System.Text.RegularExpressions;
using Hashtables.Classes;

namespace Repeated_Word
{
    public class Program
    {
        /// <summary>
        /// Finds and returns the first repeated word in a string.
        /// </summary>
        /// <param name="input">The string being searched.</param>
        /// <returns>The first repeated word.</returns>
        public static string RepeatedWord(string input)
        {
            if (input is null) return null;

            // Splits the input string on spaces & punctuation, keeping dashes & hyphens.
            Regex regex = new Regex(@"(?:(?!['-])\W)+");
            string[] words = regex.Split(input.ToLower());

            Hashtable<int> hashtable = new Hashtable<int>();

            for (int i = 0; i < words.Length; i++)
            {
                if (hashtable.Contains(words[i])) return words[i];
                else hashtable.Add(words[i], i);
            }

            return null;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
