using System;
using System.Collections.Generic;
using System.Linq;
using ImportedLinq;
using Xunit;

namespace ImportedLinqTest
{
    public class MinByTest
    {
        [Fact]
        public void MinBy0()
        {
            IReadOnlyCollection<string> actual = new[] {"C#", "F#"}.MinBy(it => it.Length);
            Assert.Equal(new[] {"C#", "F#"}, actual);
        }

        [Fact]
        public void MinBy1()
        {
            IReadOnlyCollection<string> actual = new[] {"Java", "Scala", "Kotlin", "Groovy"}.MinBy(it => it.Length);
            Assert.Equal(new[] {"Java"}, actual);
        }

        [Fact]
        public void ThrowInvalidOperationException()
        {
            Assert.Throws<InvalidOperationException>(() => Enumerable.Empty<int>().MinBy(x => x));
        }

        [Fact]
        public void ThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => default(IEnumerable<int>).MinBy(it => it));
            Assert.Throws<ArgumentNullException>(() => new[] {1}.MinBy(default(Func<int, int>)));

            Assert.Throws<ArgumentNullException>(() =>
            {
                default(IEnumerable<int>).MinBy(it => it, Comparer<int>.Default);
            });
            Assert.Throws<ArgumentNullException>(() =>
            {
                new[] {1}.MinBy(default(Func<int, int>), Comparer<int>.Default);
            });

            Assert.Throws<ArgumentNullException>(() => { new[] {0}.MinBy(it => it, default); });
        }
    }
}