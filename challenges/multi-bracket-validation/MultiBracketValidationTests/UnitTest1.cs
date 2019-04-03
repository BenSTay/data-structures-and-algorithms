using System;
using Xunit;
using MultiBracketValidation;

namespace MultiBracketValidationTests
{
    public class UnitTest1
    {
        [Theory]
        [InlineData("(){}[]")]
        [InlineData("({[]})")]
        [InlineData("({}[])")]
        public void HappyPath(string teststring)
        {
            Assert.True(Program.ValidateBrackets(teststring));
        }

        [Theory]
        [InlineData("([)]")]
        [InlineData("{{{}}}}")]
        [InlineData("[[[[]]]")]
        public void UnhappyPath(string teststring)
        {
            Assert.False(Program.ValidateBrackets(teststring));
        }

        [Fact]
        public void NoBracketsReturnsTrue()
        {
            Assert.True(Program.ValidateBrackets("asparagus"));
        }

    }
}
