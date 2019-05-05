using System;
using System.Collections;
using System.Collections.Generic;

namespace ImportedLinqTest.Helpers
{
    public class ThrowExceptionEnumerable<T> : IEnumerable<T>
    {
        private readonly IEnumerable<T> source;

        public ThrowExceptionEnumerable(params T[] source)
        {
            this.source = source;
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (T element in source)
            {
                yield return element;
            }

            throw new Exception();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}