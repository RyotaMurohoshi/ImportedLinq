using System;
using System.Collections.Generic;
using System.Linq;
using ImportedLinq;
using ImportedLinqTest.Helpers;
using Xunit;

namespace ImportedLinqTest
{
    public class BufferTest
    {
        [Fact]
        public void ThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => default(IEnumerable<int>).Buffer(5));
            Assert.Throws<ArgumentNullException>(() => default(IEnumerable<int>).Buffer(5, 5));
            Assert.Throws<ArgumentNullException>(() => default(IEnumerable<int>).Buffer(5, 4));
            Assert.Throws<ArgumentNullException>(() => default(IEnumerable<int>).Buffer(5, 6));
        }

        [Fact]
        public void ThrowArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new[] {3, 1, 4, 1, 5, 9, 2}.Buffer(0));
            Assert.Throws<ArgumentOutOfRangeException>(() => new[] {3, 1, 4, 1, 5, 9, 2}.Buffer(-1));
            Assert.Throws<ArgumentOutOfRangeException>(() => new[] {3, 1, 4, 1, 5, 9, 2}.Buffer(0, 4));
            Assert.Throws<ArgumentOutOfRangeException>(() => new[] {3, 1, 4, 1, 5, 9, 2}.Buffer(-1, 4));
            Assert.Throws<ArgumentOutOfRangeException>(() => new[] {3, 1, 4, 1, 5, 9, 2}.Buffer(4, 0));
            Assert.Throws<ArgumentOutOfRangeException>(() => new[] {3, 1, 4, 1, 5, 9, 2}.Buffer(4, -1));
            Assert.Throws<ArgumentOutOfRangeException>(() => new[] {3, 1, 4, 1, 5, 9, 2}.Buffer(0, 0));
        }

        [Fact]
        public void BufferImpl0()
        {
            IEnumerable<IReadOnlyList<int>> actual = Enumerable.Range(0, 10).Buffer(3);
            IEnumerable<IReadOnlyList<int>> expected = new[]
            {
                new[] {0, 1, 2},
                new[] {3, 4, 5},
                new[] {6, 7, 8},
                new[] {9}
            };

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void BufferImpl1()
        {
            IEnumerable<IReadOnlyList<int>> actual = Enumerable.Range(0, 9).Buffer(3);
            IEnumerable<IReadOnlyList<int>> expected = new[]
            {
                new[] {0, 1, 2},
                new[] {3, 4, 5},
                new[] {6, 7, 8},
            };

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void BufferImpl2()
        {
            IEnumerable<IReadOnlyList<int>> actual = Enumerable.Range(0, 10).Buffer(3, 3);
            IEnumerable<IReadOnlyList<int>> expected = new[]
            {
                new[] {0, 1, 2},
                new[] {3, 4, 5},
                new[] {6, 7, 8},
                new[] {9}
            };

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void BufferImpl3()
        {
            IEnumerable<IReadOnlyList<int>> actual = Enumerable.Range(0, 9).Buffer(3, 3);
            IEnumerable<IReadOnlyList<int>> expected = new[]
            {
                new[] {0, 1, 2},
                new[] {3, 4, 5},
                new[] {6, 7, 8},
            };

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void BufferOverlap()
        {
            IEnumerable<IReadOnlyList<int>> actual = Enumerable.Range(0, 9).Buffer(3, 2);
            IEnumerable<IReadOnlyList<int>> expected = new[]
            {
                new[] {0, 1, 2},
                new[] {2, 3, 4},
                new[] {4, 5, 6},
                new[] {6, 7, 8},
                new[] {8},
            };

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void BufferSkip()
        {
            IEnumerable<IReadOnlyList<int>> actual = Enumerable.Range(0, 9).Buffer(3, 4);
            IEnumerable<IReadOnlyList<int>> expected = new[]
            {
                new[] {0, 1, 2},
                new[] {4, 5, 6},
                new[] {8},
            };

            Assert.Equal(expected, actual);
        }


        [Fact]
        public void NotThrowExceptionImpl()
        {
            IEnumerable<IReadOnlyList<int>> actual = new ThrowExceptionEnumerable<int>(0, 1, 2, 3, 4).Buffer(2).Take(2);

            foreach (IReadOnlyList<int> e in actual)
            {
            }
        }

        [Fact]
        public void NotThrowExceptionSkip()
        {
            IEnumerable<IReadOnlyList<int>> actual = new ThrowExceptionEnumerable<int>(0, 1, 2, 3, 4).Buffer(2, 1)
                .Take(4);

            foreach (IReadOnlyList<int> e in actual)
            {
            }
        }

        [Fact]
        public void NotThrowExceptionOverlap()
        {
            IEnumerable<IReadOnlyList<int>> actual = new ThrowExceptionEnumerable<int>(0, 1, 2, 3, 4).Buffer(2, 3)
                .Take(1);

            foreach (IReadOnlyList<int> e in actual)
            {
            }
        }

        [Fact]
        public void ThrowExceptionImpl()
        {
            IEnumerable<IReadOnlyList<int>> actual = new ThrowExceptionEnumerable<int>(0, 1, 2, 3, 4).Buffer(2);

            Assert.Throws<Exception>(() =>
            {
                foreach (IReadOnlyList<int> e in actual)
                {
                }
            });
        }

        [Fact]
        public void ThrowExceptionSkip()
        {
            IEnumerable<IReadOnlyList<int>> actual = new ThrowExceptionEnumerable<int>(0, 1, 2, 3, 4).Buffer(2, 1);

            Assert.Throws<Exception>(() =>
            {
                foreach (IReadOnlyList<int> e in actual)
                {
                }
            });
        }

        [Fact]
        public void ThrowExceptionOverlap()
        {
            IEnumerable<IReadOnlyList<int>> actual = new ThrowExceptionEnumerable<int>(0, 1, 2, 3, 4).Buffer(2, 3);

            Assert.Throws<Exception>(() =>
            {
                foreach (IReadOnlyList<int> e in actual)
                {
                }
            });
        }
    }
}