using System;
using System.Collections.Generic;
using System.Linq;
using ImportedLinq;
using ImportedLinqTest.Helpers;
using Xunit;

namespace ImportedLinqTest
{
    public class ZipTest
    {
        [Fact]
        public void ZipEmptyFirst()
        {
            IEnumerable<int> first = Enumerable.Empty<int>();
            IEnumerable<string> second = new[] {"a", "b", "c"};

            IEnumerable<(int First, string Second)> actual = first.Zip(second);
            IEnumerable<(int First, string Second)> expected = Enumerable.Empty<(int First, string Second)>();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ZipEmptySecond()
        {
            IEnumerable<int> first = new[] {0, 1, 2, 3};
            IEnumerable<string> second = Enumerable.Empty<string>();

            IEnumerable<(int First, string Second)> actual = first.Zip(second);
            IEnumerable<(int First, string Second)> expected = Enumerable.Empty<(int First, string Second)>();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ZipEmptyBoth()
        {
            IEnumerable<int> first = Enumerable.Empty<int>();
            IEnumerable<string> second = Enumerable.Empty<string>();

            IEnumerable<(int First, string Second)> actual = first.Zip(second);
            IEnumerable<(int First, string Second)> expected = Enumerable.Empty<(int First, string Second)>();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ZipSameLength()
        {
            IEnumerable<int> first = new[] {0, 1, 2, 3};
            IEnumerable<string> second = new[] {"a", "b", "c", "d"};

            IEnumerable<(int First, string Second)> actual = first.Zip(second);
            IEnumerable<(int First, string Second)> expected = new[]
            {
                ValueTuple.Create(0, "a"),
                ValueTuple.Create(1, "b"),
                ValueTuple.Create(2, "c"),
                ValueTuple.Create(3, "d")
            }.AsEnumerable();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ZipFirstLonger()
        {
            IEnumerable<int> first = new[] {0, 1, 2, 3};
            IEnumerable<string> second = new[] {"a", "b", "c"};

            IEnumerable<(int First, string Second)> actual = first.Zip(second);
            IEnumerable<(int First, string Second)> expected = new[]
            {
                ValueTuple.Create(0, "a"),
                ValueTuple.Create(1, "b"),
                ValueTuple.Create(2, "c"),
            }.AsEnumerable();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ZipSecondLonger()
        {
            IEnumerable<int> first = new[] {0, 1, 2};
            IEnumerable<string> second = new[] {"a", "b", "c", "d"};

            IEnumerable<(int First, string Second)> actual = first.Zip(second);
            IEnumerable<(int First, string Second)> expected = new[]
            {
                ValueTuple.Create(0, "a"),
                ValueTuple.Create(1, "b"),
                ValueTuple.Create(2, "c"),
            }.AsEnumerable();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ZipAccessTupleMember()
        {
            IEnumerable<int> first = new[] {0, 1, 2};
            IEnumerable<string> second = new[] {"a", "b", "c"};

            IReadOnlyList<(int First, string Second)> result = first.Zip(second).ToList();

            Assert.Equal(result[0].First, 0);
            Assert.Equal(result[1].First, 1);
            Assert.Equal(result[2].First, 2);
            Assert.Equal(result[0].Second, "a");
            Assert.Equal(result[1].Second, "b");
            Assert.Equal(result[2].Second, "c");
        }

        [Fact]
        public void ThrowArgumentNullExceptionFirst()
        {
            IEnumerable<int> first = default(IEnumerable<int>);
            IEnumerable<string> second = new[] {"a", "b", "c"};

            Assert.Throws<ArgumentNullException>(() => first.Zip(second));
        }

        [Fact]
        public void ThrowArgumentNullExceptionSecond()
        {
            IEnumerable<int> first = new[] {0, 1, 2, 3};
            IEnumerable<string> second = default(IEnumerable<string>);

            Assert.Throws<ArgumentNullException>(() => first.Zip(second));
        }

        [Fact]
        public void ThrowArgumentNullExceptionBoth()
        {
            IEnumerable<int> first = default;
            IEnumerable<string> second = default(IEnumerable<string>);

            Assert.Throws<ArgumentNullException>(() => first.Zip(second));
        }

        [Fact]
        public void ThrowExceptionFirstSameLength()
        {
            IEnumerable<int> first = new ThrowExceptionEnumerable<int>(0, 1, 2, 3);
            IEnumerable<string> second = new[] {"a", "b", "c", "d"};

            Assert.Throws<Exception>(() =>
            {
                foreach ((int fst, string snd) in first.Zip(second))
                {
                }
            });
        }

        [Fact]
        public void ThrowExceptionFirstLonger()
        {
            IEnumerable<int> first = new ThrowExceptionEnumerable<int>(0, 1, 2, 3);
            IEnumerable<string> second = new[] {"a", "b", "c"};

            foreach ((int fst, string snd) in first.Zip(second))
            {
            }
        }

        [Fact]
        public void ThrowExceptionFirstShorter()
        {
            IEnumerable<int> first = new ThrowExceptionEnumerable<int>(0, 1, 2, 3);
            IEnumerable<string> second = new[] {"a", "b", "c", "d", "e"};

            Assert.Throws<Exception>(() =>
            {
                foreach ((int fst, string snd) in first.Zip(second))
                {
                }
            });
        }

        [Fact]
        public void ThrowExceptionSecondSameLength()
        {
            IEnumerable<int> first = new[] {0, 1, 2, 3};
            IEnumerable<string> second = new ThrowExceptionEnumerable<string>("a", "b", "c", "d");

            foreach ((int fst, string snd) in first.Zip(second))
            {
            }
        }

        [Fact]
        public void ThrowExceptionSecondLonger()
        {
            IEnumerable<int> first = new[] {0, 1, 2, 3};
            IEnumerable<string> second = new ThrowExceptionEnumerable<string>("a", "b", "c", "d", "e");

            foreach ((int fst, string snd) in first.Zip(second))
            {
            }
        }

        [Fact]
        public void ThrowExceptionSecondShorter()
        {
            IEnumerable<int> first = new[] {0, 1, 2, 3};
            IEnumerable<string> second = new ThrowExceptionEnumerable<string>("a", "b", "c");

            Assert.Throws<Exception>(() =>
            {
                foreach ((int fst, string snd) in first.Zip(second))
                {
                }
            });
        }
    }
}