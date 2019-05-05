using System;
using System.Collections.Generic;
using System.Linq;
using ImportedLinq;
using ImportedLinqTest.Helpers;
using Xunit;

namespace ImportedLinqTest
{
    public class CountByTest
    {
        [Fact]
        public void CountBy0()
        {
            IReadOnlyDictionary<int, int> actual = new[] {3, 1, 4, 1, 5, 9, 2}.CountBy(n => n);

            IReadOnlyDictionary<int, int> expected = new Dictionary<int, int>
            {
                {1, 2},
                {2, 1},
                {3, 1},
                {4, 1},
                {5, 1},
                {9, 1},
            };

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CountBy1()
        {
            IReadOnlyDictionary<char, int> actual = "HelloWorld".CountBy(c => c);

            IReadOnlyDictionary<char, int> expected = new Dictionary<char, int>
            {
                {'H', 1},
                {'e', 1},
                {'l', 3},
                {'o', 2},
                {'W', 1},
                {'r', 1},
                {'d', 1},
            };

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CountBy2()
        {
            IReadOnlyDictionary<int, int> actual = Enumerable.Range(0, 1000).CountBy(c => c % 2);

            IReadOnlyDictionary<int, int> expected = new Dictionary<int, int>
            {
                {0, 500},
                {1, 500},
            };

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CountByWithComparer()
        {
            IReadOnlyDictionary<string, int> actual =
                new[] {"A", "a", "A", "b", "B", "c"}.CountBy(c => c, StringComparer.OrdinalIgnoreCase);

            IReadOnlyDictionary<string, int> expected = new Dictionary<string, int>
            {
                {"A", 3},
                {"b", 2},
                {"c", 1},
            };

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CountByWithNullKey()
        {
            IReadOnlyDictionary<string, int> actual = new[]
                {
                    "a", null, "a", "a", "a", null, "b", "b", null, "c"
                }
                .CountBy(s => s);

            List<KeyValuePair<string, int>> expected = new List<KeyValuePair<string, int>>
            {
                new KeyValuePair<string, int>("a", 4),
                new KeyValuePair<string, int>("b", 2),
                new KeyValuePair<string, int>("c", 1),
                new KeyValuePair<string, int>(null, 3),
            };

            List<string> keys = new List<string>
            {
                "a",
                "b",
                "c",
                null,
            };

            List<int> values = new List<int>
            {
                4,
                2,
                1,
                3,
            };

            Assert.Equal(expected, actual);
            Assert.Equal(keys, actual.Keys);
            Assert.Equal(values, actual.Values);
            Assert.Equal(4, actual.Count);
            Assert.True(actual.ContainsKey(null));
            Assert.True(actual.ContainsKey("a"));
            Assert.True(actual.ContainsKey("b"));
            Assert.True(actual.ContainsKey("c"));
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => default(IEnumerable<int>).CountBy(n => n));
            Assert.Throws<ArgumentNullException>(() => new[] {1}.CountBy(default(Func<int, int>)));
        }

        [Fact]
        public void ThrowException()
        {
            Assert.Throws<Exception>(() => { new ThrowExceptionEnumerable<int>(0, 1, 2, 3, 4).CountBy(it => it); });
        }
    }
}