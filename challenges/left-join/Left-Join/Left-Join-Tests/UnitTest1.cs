using Hashtables.Classes;
using Left_Join;
using System.Collections.Generic;
using Xunit;

namespace Left_Join_Tests
{
    public class UnitTest1
    {
        [Fact]
        public void CanJoin()
        {
            Hashtable<List<string>> left = new Hashtable<List<string>>();
            Hashtable<List<string>> right = new Hashtable<List<string>>();

            string key = "big";
            List<string> values = new List<string>
            {
                "huge"
            };
            left.Add(key, values);

            key = "big";
            values = new List<string>
            {
                "tiny"
            };
            right.Add(key, values);

            Program.LeftJoin(left, right);

            List<string> result = left.Get("big");
            List<string> expected = new List<string>();
            expected.AddRange(new string[] { "huge", "tiny" });

            Assert.Equal(expected, result);
        }

        [Fact]
        public void CanIgnoreNonMatchingKeysInRightTable()
        {
            Hashtable<List<string>> left = new Hashtable<List<string>>();
            Hashtable<List<string>> right = new Hashtable<List<string>>();

            string key = "big";
            List<string> values = new List<string>();
            values.Add("huge");
            left.Add(key, values);

            key = "fast";
            values = new List<string>();
            values.Add("speedy");
            right.Add(key, values);

            Program.LeftJoin(left, right);

            Assert.False(left.Contains("fast"));
        }
    }
}
