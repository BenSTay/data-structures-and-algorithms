using System;
using StacksAndQueues.Classes;

namespace MultiBracketValidation
{
    public class Program
    {
        /// <summary>
        /// Checks if a string contains any unclosed brackets.
        /// </summary>
        /// <param name="input">The string being verified.</param>
        /// <returns>A boolean representing if the string was valid.</returns>
        public static bool ValidateBrackets(string input)
        {
            char[] chars = input.ToCharArray();
            Stack<char> brackets = new Stack<char>();
            char bracket;
            
            // Iterate through each character in the input string
            for (int i = 0; i < chars.Length; i++)
            {
                // If the character is a opening bracket, push it to the brackets stack
                if (chars[i] == '{' || chars[i] == '(' || chars[i] == '[')
                {
                    brackets.Push(chars[i]);
                }

                /* If the character is a closing bracket, check if any opening brackets have been detected previously,
                and if the previous opening bracket matches the style of the closing bracket. If either of those
                conditions are false, false is returned. */
                else if (chars[i] == '}' || chars[i] == ')' || chars[i] == ']')
                {
                    try
                    {
                        bracket = brackets.Pop();
                    }

                    catch
                    {
                        return false;
                    }

                    if (bracket == '{' && chars[i] != '}') return false;
                    if (bracket == '(' && chars[i] != ')') return false;
                    if (bracket == '[' && chars[i] != ']') return false;
                }
            }

            // Returns true if all open brackets have been closed.
            try
            {
                brackets.Pop();
            }

            catch
            {
                return true;
            }

            return false;
        }

        static void Main(string[] args)
        {
            Console.Write("Enter a string to check: ");
            string input = Console.ReadLine();
            Console.WriteLine($"The string is{(ValidateBrackets(input) ? "" : " not")} valid.");
        }
    }
}
