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
    }
}
