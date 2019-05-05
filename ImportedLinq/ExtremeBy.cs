using System;
using System.Collections.Generic;

namespace ImportedLinq
{
    public static partial class EnumerableEx
    {
        private enum Order
        {
            Normal,
            Reverse
        }

        private static IReadOnlyCollection<TSource> ExtremeBy<TSource, TKey>(IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector, IComparer<TKey> compare, Order order)
        {
            List<TSource> result = new List<TSource>();

            using (IEnumerator<TSource> enumerator = source.GetEnumerator())
            {
                if (!enumerator.MoveNext())
                {
                    throw new InvalidOperationException("Source sequence is empty.");
                }

                TSource current = enumerator.Current;
                TKey currentKey = keySelector(current);

                result.Add(current);

                while (enumerator.MoveNext())
                {
                    current = enumerator.Current;
                    TKey key = keySelector(current);

                    int compared = compare.Compare(key, currentKey) * (order == Order.Reverse ? -1 : +1);

                    if (compared == 0) // add max/min value
                    {
                        result.Add(current);
                    }
                    else if (compared > 0) // update and reset max/min value
                    {
                        result = new List<TSource>
                        {
                            current
                        };
                        currentKey = key;
                    }
                }
            }

            return result;
        }
    }
}