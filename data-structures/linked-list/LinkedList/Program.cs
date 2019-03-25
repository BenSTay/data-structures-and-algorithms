using System;
using LinkedList.Classes;

namespace LinkedList
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            LinkList list = new LinkList();
            for (int i = 0; i < 10; i++)
            {
                list.Insert(i + 1);
            }
            Console.WriteLine(list.Print());
        }
    }
}
