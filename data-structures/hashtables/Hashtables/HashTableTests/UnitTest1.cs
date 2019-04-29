using System;
using Xunit;
using Hashtables.Classes;
using System.Collections.Generic;

namespace HashTableTests
{
    public class UnitTest1
    {
        [Fact]
        public void CanAddKeyValuePair()
        {
            Hashtable<int> hashtable = new Hashtable<int>();
            hashtable.Add("potato", 1024);
            bool valueExists = false;
            for (int i = 0; i < hashtable.Table.Length; i++)
            {
                if (hashtable.Table[i].Value == 1024)
                {
                    valueExists = true;
                    break;
                }
            }
            Assert.True(valueExists);
        }

        [Fact]
        public void CanGetValueByKey()
        {
            Hashtable<int> hashtable = new Hashtable<int>();
            hashtable.Add("potato", 1024);
            int result = hashtable.Get("potato");
            Assert.Equal(1024, result);
        }

        [Fact]
        public void ExceptionIsThrownWhenKeyIsNotFound()
        {
            Hashtable<int> hashtable = new Hashtable<int>();
            Assert.Throws<KeyNotFoundException>(() => hashtable.Get("potato"));
        }

        [Fact]
        public void CanHandleCollision()
        {
            Hashtable<int> hashtable = new Hashtable<int>(1);
            hashtable.Add("potato", 1024);
            hashtable.Add("tomato", 768);
            Assert.True(hashtable.Contains("potato") && hashtable.Contains("tomato"));
        }

        [Fact]
        public void CanResizeTable()
        {
            Hashtable<int> hashtable = new Hashtable<int>(1);
            hashtable.Resize();
            Assert.Equal(2, hashtable.Table.Length);
        }

        [Fact]
        public void CanHashKey()
        {
            Hashtable<int> hashtable = new Hashtable<int>(1024);
            int hash = hashtable.Hash("supercalifragilisticexpialidocious");
            Assert.True(hash >= 0 && hash < 1024);
        }
    }
}
