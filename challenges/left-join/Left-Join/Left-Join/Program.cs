﻿using System;
using System.Collections.Generic;
using Hashtables.Classes;

namespace Left_Join
{
    public class Program
    {
        /// <summary>
        /// Performs a left join on two hashtables.
        /// </summary>
        /// <param name="left">The left hashtable.</param>
        /// <param name="right">The right hashtable.</param>
        /// <returns>The joined hashtable.</returns>
        public static Hashtable<List<string>> LeftJoin
            (Hashtable<List<string>> left, Hashtable<List<string>> right)
        {
            for (int i = 0; i < left.Table.Length; i++)
            {
                string key = left.Table[i].Key;
                if (key != null)
                {
                    if (right.Contains(key))
                    {
                        left.Get(key).AddRange(right.Get(key));
                    }
                }
            }
            return left;
        }

        static void Main(string[] args)
        {
            Hashtable<List<string>> left = new Hashtable<List<string>>();
            Hashtable<List<string>> right = new Hashtable<List<string>>();

            string[] leftKeys = new string[]
            {
                "big", "hot", "fast"
            };

            string[] leftValues = new string[]
            {
                "huge", "sweltering", "speedy"
            };

            for (int i = 0; i < leftKeys.Length; i++)
            {
                string key = leftKeys[i];
                List<string> values = new List<string>();
                values.Add(leftValues[i]);
                left.Add(key, values);
            }

            string[] rightKeys = new string[]
            {
                "big", "smart", "fast"
            };

            string[] rightValues = new string[]
            {
                "tiny", "dumb", "slow"
            };

            for (int i = 0; i < rightKeys.Length; i++)
            {
                string key = rightKeys[i];
                List<string> values = new List<string>();
                values.Add(rightValues[i]);
                right.Add(key, values);
            }

            Console.WriteLine("Left Table");
            left.Print();

            Console.WriteLine("\nRight Table");
            right.Print();

            Console.WriteLine("\nJoined Table");
            LeftJoin(left, right).Print();

        }
    }
}
