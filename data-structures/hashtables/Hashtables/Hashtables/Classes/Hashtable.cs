using System;
using System.Collections.Generic;
using System.Text;

namespace Hashtables.Classes
{
    public class Hashtable<T>
    {
        public KeyValuePair<string, T>[] Table { get; set; }

        /// <summary>
        /// Creates a Hashtable with the default table size of 16.
        /// </summary>
        public Hashtable()
        {
            Table = new KeyValuePair<string, T>[16];
        }

        /// <summary>
        /// Creates a Hashtable with the given table size.
        /// </summary>
        /// <param name="size">The starting size of the table.</param>
        /// <exception cref="ArgumentOutOfRangeException">If the given size is less than one.</exception>
        public Hashtable(int size)
        {
            if (size < 1) throw new ArgumentOutOfRangeException("Table must be of size greater than zero!");
            Table = new KeyValuePair<string, T>[size];
        }

        /// <summary>
        /// Doubles the size of the table.
        /// </summary>
        public void Resize()
        {
            KeyValuePair<string,T>[] oldTable = Table;
            Table = new KeyValuePair<string, T>[oldTable.Length * 2];

            for (int i = 0; i < oldTable.Length; i++)
            {
                if (oldTable[i].Key != null)
                {
                    KeyValuePair<string, T> kvp = oldTable[i];
                    Add(kvp.Key, kvp.Value);
                }
            }
        }

        /// <summary>
        /// Adds a new key/value pair to the table.
        /// </summary>
        /// <param name="key">The key of the key/value pair being added.</param>
        /// <param name="value">The value of the key/value pair being added.</param>
        /// <exception cref="ArgumentException">If the table already contains the given key.</exception>
        public void Add(string key, T value)
        {
            if (Contains(key)) throw new ArgumentException("Key already exists!");
            int hash;
            do
            {
                hash = Hash(key);
                if (Table[hash].Key != null) Resize();
                else break;
            } while (true);

            Table[hash] = new KeyValuePair<string, T>(key, value);
        }

        /// <summary>
        /// Gets the value associated with a given key.
        /// </summary>
        /// <param name="key">The key associated with the value.</param>
        /// <returns>The value associated with the key.</returns>
        /// <exception cref="KeyNotFoundException">If the table does not contain the given key.</exception>
        public T Get(string key)
        {
            if (Contains(key)) return Table[Hash(key)].Value;
            else throw new KeyNotFoundException();
        }

        /// <summary>
        /// Searches the table for a given key.
        /// </summary>
        /// <param name="key">The key being searched for.</param>
        /// <returns>A boolean representing if the key was found.</returns>
        public bool Contains(string key)
        {
            return Table[Hash(key)].Key == key;
        }

        /// <summary>
        /// Generates a number using the given key.
        /// </summary>
        /// <param name="key">The key being used to generate the hashed value.</param>
        /// <returns>The hashed value.</returns>
        public int Hash(string key)
        {
            char[] chars = key.ToCharArray();
            int sum = 0;
            for (int i = 0; i < key.Length; i++)
            {
                sum += (i + 1) * (int)Math.Pow(chars[i], 2);
            }
            return Math.Abs(sum * 4021) % Table.Length;
        }

        public void Print()
        {
            for (int i = 0; i < Table.Length; i++)
            {
                Console.WriteLine($"{i}: Key = {(Table[i].Key is null ? "null" : Table[i].Key)}, Value = {Table[i].Value}");
            }
        }
    }
}
