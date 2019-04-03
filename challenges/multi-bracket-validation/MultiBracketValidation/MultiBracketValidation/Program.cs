using System;
using StacksAndQueues.Classes;

namespace MultiBracketValidation
{
    public class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool ValidateBrackets(string input)
        {
            char[] chars = input.ToCharArray();
            Stack<char> brackets = new Stack<char>();
            char bracket;
            
            for (int i = 0; i < chars.Length; i++)
            {
                if (chars[i] == '{' || chars[i] == '(' || chars[i] == '[')
                {
                    brackets.Push(chars[i]);
                }
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
            Console.WriteLine("Hello World!");
        }
    }
}
