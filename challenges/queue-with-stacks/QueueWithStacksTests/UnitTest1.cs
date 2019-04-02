using System;
using Xunit;
using QueueWithStacks.Classes;
using StacksAndQueues.Classes;

namespace QueueWithStacksTests
{
    public class UnitTest1
    {
        [Fact]
        public void PseudoQueueWorksLikeRealQueue()
        {
            PseudoQueue<int> pseudo = new PseudoQueue<int>();
            Queue<int> queue = new Queue<int>();

            for (int i = 0; i < 6; i++)
            {
                pseudo.Enqueue(i);
                queue.Enqueue(i);
            }

            bool isEqual;
            do
            {
                isEqual = pseudo.Dequeue() == queue.Dequeue();
            } while (queue.Front != null && isEqual);

            Assert.True(isEqual);
        }

        [Fact]
        public void CannotDequeueFromEmptyPseudoQueue()
        {
            PseudoQueue<int> pseudo = new PseudoQueue<int>();
            Assert.Throws<InvalidOperationException>(() => pseudo.Dequeue());
        }
    }
}
