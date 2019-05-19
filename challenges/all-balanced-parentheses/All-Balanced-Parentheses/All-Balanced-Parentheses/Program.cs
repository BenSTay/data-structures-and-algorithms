using System;
using System.Collections.Generic;

namespace All_Balanced_Parentheses
{
    public class Program
    {
        /// <summary>
        /// Generates an array containing every possible balanced combination of parentheses for a given number of pairs of parentheses.
        /// </summary>
        /// <param name="n">The number of parentheses pairs.</param>
        /// <returns>An array of strings</returns>
        public static string[] GenerateBalancedParenthesesCombinations(int n)
        {
            if (n < 1) throw new ArgumentException("n must be greater than zero!");

            string parens;
            Queue<string> parenQueue = new Queue<string>();
            parenQueue.Enqueue("()");

            while (parenQueue.Peek().Length / 2 < n)
            {
                parens = parenQueue.Dequeue();
                if (!parenQueue.Contains($"(){parens}")) parenQueue.Enqueue($"(){parens}");
                if (!parenQueue.Contains($"{parens}()")) parenQueue.Enqueue($"{parens}()");
                parenQueue.Enqueue($"({parens})");
            }

            string[] result = new string[parenQueue.Count];
            int i = 0;

            while (parenQueue.TryDequeue(out parens))
            {
                result[i] = parens;
                i++;
            }

            return result;
        }
        static void Main(string[] args)
        {
            for (int i = 1; i < 100; i++)
            {
                Console.WriteLine($"n: {i}, combinations: {GenerateBalancedParenthesesCombinations(i).Length}");
            }

            Console.WriteLine(string.Join(", ", GenerateBalancedParenthesesCombinations(4)));
            
        }
    }
}
