using System;
using Xunit;
using All_Balanced_Parentheses;

namespace Paren_Tests
{
    public class UnitTest1
    {
        [Fact]
        public void CanGetAllPossibleCombos()
        {
            string[] result = Program.GenerateBalancedParenthesesCombinations(3);
            string[] expected = new string[] { "()()()", "(()())", "()(())", "(())()", "((()))" };

            Assert.Equal(expected, result);
        }

        [Fact]
        public void CantGetCombinationsWhenNIsLessThan1()
        {
            Assert.Throws<ArgumentException>(() => Program.GenerateBalancedParenthesesCombinations(-1));
        }

        [Fact]
        public void NoImbalancedParentheses()
        {
            string imbalanced = "())(())(";

            Assert.DoesNotContain(imbalanced, Program.GenerateBalancedParenthesesCombinations(4));
        }
    }
}
