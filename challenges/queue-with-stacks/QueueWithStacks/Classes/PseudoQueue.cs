using System;
using System.Text;
using StacksAndQueues.Classes;

namespace QueueWithStacks.Classes
{
    public class PseudoQueue<T>
    {
        private Stack<T> InStack { get; set; }
        private Stack<T> OutStack { get; set; }

        public PseudoQueue()
        {
            InStack = new Stack<T>();
            OutStack = new Stack<T>();
        }

        private void MoveNodes(Stack<T> origin, Stack<T> destination)
        {
            while (origin.Top != null)
            {
                destination.Push(origin.Pop());
            }
        }

        public void Enqueue(T value)
        {
            MoveNodes(OutStack, InStack);
            InStack.Push(value);
        }

        public T Dequeue()
        {
            MoveNodes(InStack, OutStack);
            if (OutStack.Top is null) throw new InvalidOperationException("Cannot dequeue from empty queue!");
            else return OutStack.Pop();
        }
    }
}
