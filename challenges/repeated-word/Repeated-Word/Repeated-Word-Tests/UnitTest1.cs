using System;
using Xunit;
using Repeated_Word;

namespace Repeated_Word_Tests
{
    public class UnitTest1
    {
        [Fact]
        public void CanFindRepeatedWord()
        {
            string input = "Once upon a time in a galaxy far, far away...";
            string result = Program.RepeatedWord(input);
            Assert.Equal("a", result);
        }

        [Fact]
        public void RepeatedWordReturnsNullIfNoRepeatsFound()
        {
            string input = "You thought your first kiss would be with JoJo but it was me, Dio!";
            Assert.Null(Program.RepeatedWord(input));
        }

        [Fact]
        public void RepeatedWordReturnsNullIfNullInput()
        {
            Assert.Null(Program.RepeatedWord(null));
        }
    }
}
