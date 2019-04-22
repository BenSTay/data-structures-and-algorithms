using System;

namespace Insertion
{
    public class Program
    {
        /// <summary>
        /// Sorts an array of ints using Insertion Sort.
        /// </summary>
        /// <param name="arr">The unsorted array.</param>
        /// <returns>The sorted array.</returns>
        public static int[] InsertionSort(int[] arr)
        {
            int temp;
            for (int i = 0; i < arr.Length; i++)
            {
                temp = arr[i];
                for (int j = i - 1; j >= 0; j--)
                {
                    if (arr[j] > temp)
                    {
                        arr[j + 1] = arr[j];
                        arr[j] = temp;
                    }
                    else break;
                }
            }
            return arr;
        }

        private static void Main(string[] args)
        {
            int[] ints = new int[] { 1, 3, 0, 2, 5, 4 };
            Console.WriteLine($"Unsorted: {string.Join(", ", ints)}");
            InsertionSort(ints);
            Console.WriteLine($"Sorted: {string.Join(", ", ints)}");
        }
    }
}