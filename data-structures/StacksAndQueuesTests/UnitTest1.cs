using System;
using Xunit;
using StacksAndQueues.Classes;

namespace StacksAndQueuesTests
{
    public class UnitTest1
    {
        [Fact]
        public void CanPushToStack()
        {
            Stack<int> stack = new Stack<int>();
            stack.Push(1);
            Assert.NotNull(stack.Top);
        }

        [Fact]
        public void CanPushMultipleToStack()
        {
            Stack<int> stack = new Stack<int>();
            stack.Push(1);
            stack.Push(2);
            Assert.NotNull(stack.Top.Next);
        }

        [Fact]
        public void CanPopFromStack()
        {
            Stack<int> stack = new Stack<int>();
            stack.Push(1);
            int result = stack.Pop();
            Assert.Equal(1, result);
        }

        [Fact]
        public void CanEmptyStack()
        {
            Stack<int> stack = new Stack<int>();
            stack.Push(1);
            stack.Pop();
            Assert.Null(stack.Top);
        }

        [Fact]
        public void CanPeekOnStack()
        {
            Stack<int> stack = new Stack<int>();
            stack.Push(1);
            int result = stack.Peek();
            Assert.Equal(1, result);
        }

        [Fact]
        public void CanInstantiateStack()
        {
            Stack<int> stack = new Stack<int>();
            Assert.NotNull(stack);
        }

        [Fact]
        public void CanEnqueueToQueue()
        {
            Queue<string> queue = new Queue<string>();
            queue.Enqueue("cat");
            Assert.True(queue.Front == queue.Rear && queue.Front != null);
        }

        [Fact]
        public void CanEnqueueMultipleToQueue()
        {
            Queue<string> queue = new Queue<string>();
            queue.Enqueue("cat");
            queue.Enqueue("dog");
            Assert.True(queue.Front != queue.Rear && queue.Rear.Value == "dog");
        }

        [Fact]
        public void CanDequeueFromQueue()
        {
            Queue<string> queue = new Queue<string>();
            queue.Enqueue("cat");
            queue.Enqueue("dog");
            string result = queue.Dequeue();
            Assert.True(queue.Front.Value == "dog" && result == "cat");
        }

        [Fact]
        public void CanPeekOnQueue()
        {
            Queue<string> queue = new Queue<string>();
            queue.Enqueue("cat");
            queue.Enqueue("dog");
            string result = queue.Peek();
            Assert.True(queue.Front.Value == "cat" && result == "cat");
        }

        [Fact]
        public void CanEmptyQueue()
        {
            Queue<string> queue = new Queue<string>();
            queue.Enqueue("cat");
            string result = queue.Dequeue();
            Assert.True(queue.Front == queue.Rear && queue.Front == null);
        }

        [Fact]
        public void CanInstantiateQueue()
        {
            Queue<string> queue = new Queue<string>();
            Assert.NotNull(queue);
        }
    }
}
