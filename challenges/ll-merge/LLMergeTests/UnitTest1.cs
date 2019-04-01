using System;
using Xunit;
using LinkedList.Classes;
using LLMerge;

namespace LLMergeTests
{
    public class UnitTest1
    {
        [Fact]
        public void MergeListsWorks()
        {
            LinkList list1 = new LinkList();
            LinkList list2 = new LinkList();

            for (int i = 0; i < 6; i++)
            {
                if (i % 2 == 0) list1.Append(i);
                else list2.Append(i);
            }

            LinkList merged = Program.MergeLists(list1, list2);
            string expected = "0 -> 1 -> 2 -> 3 -> 4 -> 5";
            Assert.Equal(expected, merged.Print());
        }

        [Fact]
        public void MergeListReturnsEmptyListWhenBothListsAreEmpty()
        {
            LinkList list1 = new LinkList();
            LinkList list2 = new LinkList();

            LinkList merged = Program.MergeLists(list1, list2);
            Assert.Null(merged.Head);
        }

        [Fact]
        public void MergeListReturnsSecondListIfFirstIsEmpty()
        {
            LinkList list1 = new LinkList();
            LinkList list2 = new LinkList();

            for (int i = 0; i < 6; i++)
            {
                if (i % 2 == 1) list2.Append(i);
            }

            LinkList merged = Program.MergeLists(list1, list2);

            Assert.Equal(list2.Print(), merged.Print());
        }

        [Fact]
        public void MergeListWorksOnListsOfDifferentSizes()
        {
            LinkList list1 = new LinkList();
            LinkList list2 = new LinkList();

            for (int i = 0; i < 6; i++)
            {
                if (i % 2 == 1) list2.Append(i);
                else list1.Append(i);
            }
            list1.Append(6);
            list1.Append(7);

            LinkList merged = Program.MergeLists(list1, list2);

            string expected = "0 -> 1 -> 2 -> 3 -> 4 -> 5 -> 6 -> 7";
            Assert.Equal(expected, merged.Print());
        }

        [Fact]
        public void MergeListDestroysList2WhenEvenWhenList2IsLonger()
        {
            LinkList list1 = new LinkList();
            LinkList list2 = new LinkList();

            for (int i = 0; i < 6; i++)
            {
                if (i % 2 == 1) list2.Append(i);
                else list1.Append(i);
            }
            list2.Append(6);
            list2.Append(7);

            LinkList merged = Program.MergeLists(list1, list2);

            Assert.Null(list2.Head);
        }
    }
}
