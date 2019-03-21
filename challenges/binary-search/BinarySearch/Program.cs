using System;

namespace BinarySearch
{
    public class Program
    {
        /// <summary>
        /// Uses a binary search algorithm to find the index of a given number in an array.
        /// </summary>
        /// <param name="array">The array being searched.</param>
        /// <param name="num">The number being looked for.</param>
        /// <returns>The index of the number in the array (-1 if the number is not found).</returns>
        public static int BinarySearch(int[] array, int num)
        {
            int start = 0, end = array.Length - 1, index;

            while (end >= start)
            {
                index = (start + end) / 2;
                if (num > array[index]) start = index + 1;
                else if (num < array[index]) end = index - 1;
                else return index;
            }

            return -1;
        }


        static void Main(string[] args)
        {
            string input;
            int num;

            // Initializes and displays the test array in the console.
            int[] array = new int[] { -9, -8, -6, -5, -3, -2, 0, 1, 3, 4, 6, 7, 9 };
            Console.WriteLine($"Array: {{ {string.Join(", ", array)} }}");

            // Prompts the user to enter a number until a valid number is entered.
            do
            {
                Console.Write("Enter a number to see its index in the array: ");
                input = Console.ReadLine();
            } while (!Int32.TryParse(input, out num));

            // Performs binary search using the given number and displays the result
            int result = BinarySearch(array, num);
            Console.WriteLine((result == -1 ? "Number not found" : $"Index: {result}"));
        }
    }
}
