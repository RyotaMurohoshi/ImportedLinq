using System;
using System.Collections.Generic;
using System.Linq;
using ImportedLinq;
using ImportedLinqTest.Helpers;
using Xunit;

namespace ImportedLinqTest
{
    public class FlattenTest
    {
        [Fact]
        public void FlattenArrayOfArray()
        {
            IEnumerable<int> actual = new[]
            {
                new[] {0, 1, 2, 3},
                new[] {4, 5},
                new[] {6, 7, 8},
                new[] {9},
            }.Flatten();

            IEnumerable<int> expected = new[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FlattenListOfList()
        {
            IEnumerable<int> actual = new List<List<int>>
            {
                new List<int> {0, 1, 2, 3},
                new List<int> {4, 5},
                new List<int> {6, 7, 8},
                new List<int> {9},
            }.Flatten();

            IEnumerable<int> expected = new[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FlattenArrayOfList()
        {
            IEnumerable<int> actual = new[]
            {
                new List<int> {0, 1, 2, 3},
                new List<int> {4, 5},
                new List<int> {6, 7, 8},
                new List<int> {9},
            }.Flatten();

            IEnumerable<int> expected = new[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FlattenArrayOfMixed()
        {
            IEnumerable<int> actual = new IEnumerable<int>[]
            {
                new[] {0, 1, 2, 3},
                new List<int> {4, 5},
                new List<int> {6, 7, 8},
                new[] {9},
            }.Flatten();

            IEnumerable<int> expected = new[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FlattenEmpty()
        {
            IEnumerable<int> actual = Enumerable.Empty<IEnumerable<int>>().Flatten();


            IEnumerable<int> expected = Enumerable.Empty<int>();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => { default(IEnumerable<IEnumerable<int>>).Flatten(); });
        }

        [Fact]
        public void ThrowExceptionWhenGetEnumeratorExpected0()
        {
            Assert.Throws<Exception>(() =>
            {
                foreach (int e in new ThrowExceptionEnumerable<IEnumerable<int>>().Flatten())
                {
                }
            });
        }

        [Fact]
        public void ThrowExceptionWhenGetEnumeratorExpected1()
        {
            IEnumerable<IEnumerable<int>> source = new ThrowExceptionEnumerable<IEnumerable<int>>(new[] {0, 1, 2});

            Assert.Throws<Exception>(() =>
            {
                foreach (int e in source.Flatten())
                {
                }
            });
        }

        [Fact]
        public void ThrowExceptionWhenGetEnumeratorExpected2()
        {
            IEnumerable<IEnumerable<int>> source = new ThrowExceptionEnumerable<IEnumerable<int>>(
                new[] {0, 1, 2},
                new[] {3, 4, 5, 6}
            );

            Assert.Throws<Exception>(() =>
            {
                foreach (int e in source.Flatten())
                {
                }
            });
        }

        [Fact]
        public void ThrowExceptionWhenGetEnumeratorExpected3()
        {
            IEnumerable<IEnumerable<int>> source = new List<IEnumerable<int>>
            {
                new[] {0, 1, 2},
                new[] {3, 4, 5, 6},
                new ThrowExceptionEnumerable<int>(7, 8, 9)
            };

            Assert.Throws<Exception>(() =>
            {
                foreach (int e in source.Flatten())
                {
                }
            });
        }


        [Fact]
        public void NotThrowExceptionUntilIterate()
        {
            new ThrowExceptionEnumerable<IEnumerable<int>>().Flatten();

            // not throw Exception
        }

        [Fact]
        public void NotThrowExceptionUntilIterateIt0()
        {
            IEnumerable<IEnumerable<int>> source = new ThrowExceptionEnumerable<IEnumerable<int>>();

            foreach (int e in source.Flatten().Take(0))
            {
            }
        }


        [Fact]
        public void NotThrowExceptionUntilIterateIt1()
        {
            IEnumerable<IEnumerable<int>> source = new ThrowExceptionEnumerable<IEnumerable<int>>(new[] {0, 1, 2});

            foreach (int e in source.Flatten().Take(3))
            {
            }
        }

        [Fact]
        public void NotThrowExceptionUntilIterateIt2()
        {
            IEnumerable<IEnumerable<int>> source = new ThrowExceptionEnumerable<IEnumerable<int>>(
                new[] {0, 1, 2},
                new[] {3, 4, 5, 6}
            );

            foreach (int e in source.Flatten().Take(7))
            {
            }
        }

        [Fact]
        public void NotThrowExceptionUntilIterateIt3()
        {
            IEnumerable<IEnumerable<int>> source = new List<IEnumerable<int>>
            {
                new[] {0, 1, 2},
                new[] {3, 4, 5, 6},
                new ThrowExceptionEnumerable<int>(7, 8, 9)
            };


            foreach (int e in source.Flatten().Take(10))
            {
            }
        }
    }
}