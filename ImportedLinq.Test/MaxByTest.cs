using System;
using System.Collections.Generic;
using System.Linq;
using ImportedLinq;
using Xunit;

namespace ImportedLinqTest
{
    public class MaxByTest
    {
        [Fact]
        public void MinBy0()
        {
            IReadOnlyCollection<string> actual = new[] {"C#", "F#"}.MaxBy(it => it.Length);
            Assert.Equal(new[] {"C#", "F#"}, actual);

        }

        [Fact]
        public void MinBy1()
        {
            IReadOnlyCollection<string> actual = new[] {"Java", "Scala", "Kotlin", "Groovy"}.MaxBy(it => it.Length);
            Assert.Equal(new[] {"Kotlin", "Groovy"}, actual);

        }

        [Fact]
        public void ThrowInvalidOperationException()
        {
            Assert.Throws<InvalidOperationException>(() => Enumerable.Empty<int>().MaxBy(x => x));
        }

        [Fact]
        public void ThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => default(IEnumerable<int>).MaxBy(it => it));
            Assert.Throws<ArgumentNullException>(() => new[] {1}.MaxBy(default(Func<int, int>)));

            Assert.Throws<ArgumentNullException>(() =>
            {
                default(IEnumerable<int>).MaxBy(it => it, Comparer<int>.Default);
            });
            Assert.Throws<ArgumentNullException>(() =>
            {
                new[] {1}.MaxBy(default(Func<int, int>), Comparer<int>.Default);
            });

            Assert.Throws<ArgumentNullException>(() => { new[] {0}.MaxBy(it => it, default); });
        }
    }
}