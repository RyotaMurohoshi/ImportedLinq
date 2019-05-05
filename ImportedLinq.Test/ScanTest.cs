using System;
using System.Collections.Generic;
using System.Linq;
using ImportedLinq;
using ImportedLinqTest.Helpers;
using Xunit;

namespace ImportedLinqTest
{
    public class ScanTest
    {
        [Fact]
        public void ScanWithoutSeed()
        {
            IEnumerable<int> actual = Enumerable.Range(0, 5).Scan((acc, element) => acc + element);
            Assert.Equal(new[] {1, 3, 6, 10}, actual);
        }

        [Fact]
        public void ScanWithSeed()
        {
            IEnumerable<int> actual = Enumerable.Range(0, 5).Scan(0, (acc, element) => acc + element);
            Assert.Equal(new[] {0, 1, 3, 6, 10}, actual);
        }

        [Fact]
        public void EmptyScanWithoutSeed()
        {
            IEnumerable<int> actual = Enumerable.Empty<int>().Scan((acc, element) => acc + element);
            Assert.Equal(Enumerable.Empty<int>(), actual);
        }

        [Fact]
        public void EmptyScanWithSeed()
        {
            IEnumerable<int> actual = Enumerable.Empty<int>().Scan(0, (acc, element) => acc + element);
            Assert.Equal(Enumerable.Empty<int>(), actual);
        }

        [Fact]
        public void ThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => default(IEnumerable<int>).Scan((x, y) => x + y));
            Assert.Throws<ArgumentNullException>(() => new[] {1}.Scan(default));

            Assert.Throws<ArgumentNullException>(() => default(IEnumerable<int>).Scan(1, (x, y) => x * y));
            Assert.Throws<ArgumentNullException>(() => new[] {1}.Scan(0, null));
        }

        [Fact]
        public void ThrowExceptionWithoutSeed()
        {
            IEnumerable<int> source = new ThrowExceptionEnumerable<int>(0, 1, 2);

            Assert.Throws<Exception>(() =>
            {
                foreach (int e in source.Scan((acc, element) => acc + element))
                {
                }
            });
        }

        [Fact]
        public void ThrowExceptionWithSeed()
        {
            IEnumerable<int> source = new ThrowExceptionEnumerable<int>(0, 1, 2);

            Assert.Throws<Exception>(() =>
            {
                foreach (int e in source.Scan(0, (acc, element) => acc + element))
                {
                }
            });
        }

        [Fact]
        public void NotThrowExceptionWithoutSeed0()
        {
            new ThrowExceptionEnumerable<int>().Scan((acc, element) => acc + element);
        }

        [Fact]
        public void NotThrowExceptionWithoutSeed1()
        {
            IEnumerable<int> source = new ThrowExceptionEnumerable<int>(0, 1, 2);

            foreach (int e in source.Scan((acc, element) => acc + element).Take(2))
            {
            }
        }

        [Fact]
        public void NotThrowExceptionWithSeed0()
        {
            new ThrowExceptionEnumerable<int>().Scan(0, (acc, element) => acc + element);
        }

        [Fact]
        public void NotThrowExceptionWithSeed1()
        {
            IEnumerable<int> source = new ThrowExceptionEnumerable<int>(0, 1, 2);

            foreach (int e in source.Scan(0, (acc, element) => acc + element).Take(3))
            {
            }
        }
    }
}