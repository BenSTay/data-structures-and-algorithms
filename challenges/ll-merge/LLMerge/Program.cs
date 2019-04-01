using System;
using LinkedList.Classes;

namespace LLMerge
{
    public class Program
    {
        public static LinkList MergeLists(LinkList list1, LinkList list2)
        {
            list1.Current = list1.Head;
            list2.Current = list2.Head;

            while (list1.Current != null && list2.Current != null)
            {
                list2.Current = list2.Current.Next;
                list2.Head.Next = list1.Current.Next;
                list1.Current.Next = list2.Head;
                list1.Current = list2.Head.Next;
                list2.Head = list2.Current;
            }

            if (list1.Head is null) return list2;
            else if (list2.Head != null)
            {
                list2.Head = null;
                list2.Current = null;
            }
            return list1;
        }

        static void Main(string[] args)
        {
            LinkList list1 = new LinkList();
            LinkList list2 = new LinkList();

            for (int i = 0; i < 6; i++)
            {
                if (i % 2 == 0) list1.Append(i);
                else list2.Append(i);
            }

            Console.WriteLine($"List 1: {list1.Print()}");
            Console.WriteLine($"List 2: {list2.Print()}");

            LinkList merged = MergeLists(list1, list2);

            Console.WriteLine($"Merged: {merged.Print()}");
        }
    }
}
