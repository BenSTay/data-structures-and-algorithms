using System;
using Xunit;
using LinkedList.Classes;

namespace LinkedListTests
{
    public class UnitTest1
    {
        [Fact]
        public void CanInstantiateLinkList()
        {
            LinkList list = new LinkList();
            Assert.NotNull(list);
        }

        [Fact]
        public void CanInsert()
        {
            LinkList list = new LinkList();
            list.Insert(100);
            Assert.Equal(100, list.Head.Value);
        }

        [Fact]
        public void HeadNodeIsFirstNode()
        {
            LinkList list = new LinkList();
            list.Insert(10);
            list.Insert(5);
            Assert.Equal(10, list.Head.Next.Value);
        }

        [Fact]
        public void CanInsertMultiple()
        {
            LinkList list = new LinkList();
            list.Insert(1);
            list.Insert(2);
            Assert.NotNull(list.Head.Next);
        }

        [Fact]
        public void FoundValueReturnsTrue()
        {
            LinkList list = new LinkList();
            list.Insert(1);
            Assert.True(list.Includes(1));
        }

        [Fact]
        public void ValueNotFoundReturnsFalse()
        {
            LinkList list = new LinkList();
            list.Insert(1);
            Assert.False(list.Includes(2));
        }

        [Fact]
        public void CanReturnValues()
        {
            LinkList list = new LinkList();
            list.Insert(2);
            list.Insert(1);
            Assert.Equal("1 -> 2", list.Print());
        }

        [Fact]
        public void CanAppend()
        {
            LinkList list = new LinkList();
            list.Insert(1);
            list.Append(2);
            Assert.Equal(2, list.Head.Next.Value);
        }

        [Fact]
        public void CanAppendMultiple()
        {
            LinkList list = new LinkList();
            list.Insert(1);
            for (int i = 2; i <= 5; i++)
            {
                list.Append(i);
            }
            Assert.Equal("1 -> 2 -> 3 -> 4 -> 5", list.Print());
        }

        [Fact]
        public void CanInsertBeforeMiddle()
        {
            LinkList list = new LinkList();
            for (int i = 1; i <= 5; i++)
            {
                list.Append(i);
            }
            list.InsertBefore(3, 100);
            Assert.Equal("1 -> 2 -> 100 -> 3 -> 4 -> 5", list.Print());
        }

        [Fact]
        public void CanInsertBeforeHead()
        {
            LinkList list = new LinkList();
            for (int i = 1; i <= 5; i++)
            {
                list.Append(i);
            }
            list.InsertBefore(1, 100);
            Assert.Equal(100, list.Head.Value);
        }

        [Fact]
        public void CanInsertAfterMiddle()
        {
            LinkList list = new LinkList();
            for (int i = 1; i <= 5; i++)
            {
                list.Append(i);
            }
            list.InsertAfter(3, 100);
            Assert.Equal("1 -> 2 -> 3 -> 100 -> 4 -> 5", list.Print());
        }

        [Fact]
        public void CanInsertAfterLastNode()
        {
            LinkList list = new LinkList();
            for (int i = 1; i <= 5; i++)
            {
                list.Append(i);
            }
            list.InsertAfter(5, 100);
            Assert.Equal("1 -> 2 -> 3 -> 4 -> 5 -> 100", list.Print());
        }

        [Fact]
        public void KGreaterThanListLengthThrowsException()
        {
            LinkList list = new LinkList();
            for (int i = 1; i <= 5; i++)
            {
                list.Append(i);
            }
            Assert.Throws<ArgumentOutOfRangeException>(() => list.KthFromEnd(6));
        }

        [Fact]
        public void KEqualsListLengthGetsHeadsValue()
        {
            LinkList list = new LinkList();
            for (int i = 1; i <= 5; i++)
            {
                list.Append(i);
            }
            int result = list.KthFromEnd(4);
            Assert.Equal(list.Head.Value, result);
        }

        [Fact]
        public void KIsNegativeThrowsException()
        {
            LinkList list = new LinkList();
            for (int i = 1; i <= 5; i++)
            {
                list.Append(i);
            }
            Assert.Throws<ArgumentOutOfRangeException>(() => list.KthFromEnd(-1));
        }

        [Fact]
        public void KthFromEndWorksOnListWithSingleNode()
        {
            LinkList list = new LinkList();
            list.Insert(100);
            int result = list.KthFromEnd(0);
            Assert.Equal(list.Head.Value, result);
        }

        [Fact]
        public void KthFromElementHappyPath()
        {
            LinkList list = new LinkList();
            for (int i = 1; i <= 5; i++)
            {
                list.Append(i);
            }
            int result = list.KthFromEnd(2);
            Assert.Equal(3, result);
        }
    }
}
