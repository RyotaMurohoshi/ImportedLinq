using System;
using System.Collections.Generic;
using System.Linq;
using ImportedLinq;
using ImportedLinqTest.Helpers;
using Xunit;

namespace ImportedLinqTest
{
    public class PartitionTest
    {
        [Fact]
        public void Partition0()
        {
            (IReadOnlyCollection<int> True, IReadOnlyCollection<int> False) actual
                = new[] {3, 1, 4, 1, 5, 9, 2}.Partition(it => it % 2 == 0);

            Assert.Equal(new[] {4, 2}, actual.True);
            Assert.Equal(new[] {3, 1, 1, 5, 9}, actual.False);
        }

        [Fact]
        public void Partition1()
        {
            (IReadOnlyCollection<int> True, IReadOnlyCollection<int> False) actual
                = Enumerable.Range(0, 1000).Partition(it => it % 2 == 0);

            Assert.Equal(Enumerable.Range(0, 500).Select(it => it * 2), actual.True);
            Assert.Equal(Enumerable.Range(0, 500).Select(it => it * 2 + 1), actual.False);
        }

        [Fact]
        public void PartitionEmpty()
        {
            (IReadOnlyCollection<int> True, IReadOnlyCollection<int> False) actual
                = Enumerable.Empty<int>().Partition(it => it % 2 == 0);

            Assert.Equal(Enumerable.Empty<int>(), actual.True);
            Assert.Equal(Enumerable.Empty<int>(), actual.False);
        }


        [Fact]
        public void PartitionTrueEmpty()
        {
            (IReadOnlyCollection<int> True, IReadOnlyCollection<int> False) actual
                = new[] {1, 3, 5, 7, 9}.Partition(it => it % 2 == 0);

            Assert.Equal(Enumerable.Empty<int>(), actual.True);
            Assert.Equal(new[] {1, 3, 5, 7, 9}, actual.False);
        }

        [Fact]
        public void PartitionFalseEmpty()
        {
            (IReadOnlyCollection<int> True, IReadOnlyCollection<int> False) actual
                = new[] {0, 2, 4, 6, 8}.Partition(it => it % 2 == 0);

            Assert.Equal(new[] {0, 2, 4, 6, 8}, actual.True);
            Assert.Equal(Enumerable.Empty<int>(), actual.False);
        }

        [Fact]
        public void ThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => { default(IEnumerable<int>).Partition(it => it % 2 == 0); });
            Assert.Throws<ArgumentNullException>(() => { new[] {0}.Partition(default); });
        }

        [Fact]
        public void ThrowException0()
        {
            Assert.Throws<Exception>(() => { new ThrowExceptionEnumerable<int>().Partition(it => it % 2 == 0); });
        }

        [Fact]
        public void ThrowException1()
        {
            Assert.Throws<Exception>(() =>
            {
                new ThrowExceptionEnumerable<int>(3, 1, 4, 1, 5, 9, 2).Partition(it => it % 2 == 0);
            });
        }
    }
}