using System;
using System.Collections.Generic;
using System.Linq;
using ImportedLinq;
using ImportedLinqTest.Helpers;
using Xunit;

namespace ImportedLinqTest
{
    public class WithIndexTest
    {
        [Fact]
        public void WithIndexEmpty()
        {
            IEnumerable<(String Element, int Index)> actual = Enumerable.Empty<string>().WithIndex();
            IEnumerable<(String Element, int Index)> expected = Enumerable.Empty<(String Element, int Index)>();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WithIndex()
        {
            IEnumerable<(String Element, int Index)> actual = new[] {"a", "b", "c", "d", "e"}.WithIndex();
            IEnumerable<(String Element, int Index)> expected = new[]
            {
                ValueTuple.Create("a", 0),
                ValueTuple.Create("b", 1),
                ValueTuple.Create("c", 2),
                ValueTuple.Create("d", 3),
                ValueTuple.Create("e", 4),
            };

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WithIndexAccessTupleMember()
        {
            (String Element, int Index)[] actual = new[] {"a", "b", "c", "d", "e"}.WithIndex().ToArray();

            Assert.Equal(actual[0].Element, "a");
            Assert.Equal(actual[1].Element, "b");
            Assert.Equal(actual[2].Element, "c");
            Assert.Equal(actual[3].Element, "d");
            Assert.Equal(actual[4].Element, "e");

            Assert.Equal(actual[0].Index, 0);
            Assert.Equal(actual[1].Index, 1);
            Assert.Equal(actual[2].Index, 2);
            Assert.Equal(actual[3].Index, 3);
            Assert.Equal(actual[4].Index, 4);
        }

        [Fact]
        public void ThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => default(IEnumerable<string>).WithIndex());
        }

        [Fact]
        public void ThrowException()
        {
            IEnumerable<string> source = new ThrowExceptionEnumerable<string>("a", "b", "c", "d");

            Assert.Throws<Exception>(() =>
            {
                foreach ((string element, int index) in source.WithIndex())
                {
                }
            });
        }

        [Fact]
        public void NotThrowException()
        {
            IEnumerable<string> source = new ThrowExceptionEnumerable<string>("a", "b", "c", "d");

            foreach ((string element, int index) in source.WithIndex().Take(4))
            {
            }
        }
    }
}