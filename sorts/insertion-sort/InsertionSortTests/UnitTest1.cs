using System;
using Xunit;
using Insertion;

namespace InsertionSortTests
{
    public class UnitTest1
    {
        [Fact]
        public void CanSortRandom()
        {
            int[] test = new int[] { 230, 54, 137, 46, 67, 64, 80, 116 };
            int[] expected = new int[] { 46, 54, 64, 67, 80, 116, 137, 230 };
            int[] result = Program.InsertionSort(test);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void CanSortReverse()
        {
            int[] test = new int[] { 7, 6, 5, 4, 3, 2, 1, 0 };
            int[] expected = new int[] { 0, 1, 2, 3, 4, 5, 6, 7 };
            int[] result = Program.InsertionSort(test);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void CanSortSorted()
        {
            int[] test = new int[] { 2, 4, 6, 8, 10, 12, 14, 16 };
            int[] expected = new int[] { 2, 4, 6, 8, 10, 12, 14, 16 };
            int[] result = Program.InsertionSort(test);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void CanSortEmpty()
        {
            int[] test = new int[0];
            int[] result = Program.InsertionSort(test);
            Assert.Empty(result);
        }

        [Fact]
        public void CanSortSingle()
        {
            int[] test = new int[] { 1 };
            int[] expected = new int[] { 1 };
            int[] result = Program.InsertionSort(test);
            Assert.Equal(expected, result);
        }
    }
}
