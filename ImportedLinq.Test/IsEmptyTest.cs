using System;
using System.Collections.Generic;
using System.Linq;
using ImportedLinq;
using ImportedLinqTest.Helpers;
using Xunit;

namespace ImportedLinqTest
{
    public class IsEmptyTest
    {
        [Fact]
        public void ThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => default(IEnumerable<int>).IsEmpty());
        }

        [Fact]
        public void IsEmptyEmpty()
        {
            Assert.True(Enumerable.Empty<int>().IsEmpty());
        }

        [Fact]
        public void IsEmptyNonEmpty()
        {
            Assert.False(new[] {1}.IsEmpty());
        }

        [Fact]
        public void ThrowExceptionWhenIsEmpty()
        {
            // throw Exception on 1st element
            IEnumerable<int> source = new ThrowExceptionEnumerable<int>();

            Assert.Throws<Exception>(() =>
            {
                bool isEmpty = source.IsEmpty();
            });
        }

        [Fact]
        public void NotThrowExceptionWhenIsEmpty()
        {
            // throw Exception on 5th element
            IEnumerable<int> source = new ThrowExceptionEnumerable<int>(0, 1, 2, 3);

            bool isEmpty = source.IsEmpty();
        }
    }
}