using System;
using Xunit;

namespace BinarySearchTests
{
    public class UnitTest1
    {
        [Theory]
        [InlineData(2,2)]
        [InlineData(10,6)]
        [InlineData(-5,0)]
        public void CanFindIndex(int search, int index)
        {
            int[] array = new int[] { -5, 0, 2, 3, 5, 7, 10 };
            int result = BinarySearch.Program.BinarySearch(array, search);
            Assert.Equal(index, result);
        }

        [Fact]
        public void NumberNotFoundReturnsNegativeOne()
        {
            int[] array = new int[] { -5, 0, 2, 3, 5, 7, 10 };
            int result = BinarySearch.Program.BinarySearch(array, 1);
            Assert.Equal(-1, result);
        }

        [Fact]
        public void SearchingArrayLengthZeroReturnsNegativeOne()
        {
            int[] array = new int[0];
            int result = BinarySearch.Program.BinarySearch(array, 0);
            Assert.Equal(-1, result);
        }
    }
}
