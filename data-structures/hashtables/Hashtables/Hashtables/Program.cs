using System;
using Hashtables.Classes;

namespace Hashtables
{
    class Program
    {
        static void Main(string[] args)
        {
            Hashtable<int> table = new Hashtable<int>();
            table.Add("apple", 21);
            table.Add("Apple", 22);
            table.Print();
        }
    }
}
