using System;
using System.Collections.Generic;

namespace ImportedLinq
{
    public static partial class EnumerableEx
    {
        /// <summary>
        /// Creates a Dictionary<TKey, int> from an source sequence according to a specified key selector function and exist counts in source sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by keySelector.</typeparam>
        /// <param name="source">A sequence to create count dictionary: Dictionary<TKey, int>.</param>
        /// <param name="keySelector">A function to extract a key from each element.</param>
        /// <exception cref="System.ArgumentNullException">Thrown when source or keySelector is null.</exception>
        /// <returns>A Dictionary<TKey, int> that contains keys and its exist counts in source.</returns>
        public static IReadOnlyDictionary<TKey, int> CountBy<TSource, TKey>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector
        ) => source.CountBy(keySelector, null);

        /// <summary>
        /// Creates a Dictionary<TKey, int> from an source sequence according to a specified key selector function, equality comparer and exist counts in source sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by keySelector.</typeparam>
        /// <param name="source">A sequence to create count dictionary: Dictionary<TKey, int>.</param>
        /// <param name="keySelector">A function to extract a key from each element.</param>
        /// <param name="comparer">An IEqualityComparer<T> to compare keys.</param>
        /// <exception cref="System.ArgumentNullException">Thrown when source or keySelector is null.</exception>
        /// <returns>A Dictionary<TKey, int> that contains keys and its exist counts in source.</returns>
        public static IReadOnlyDictionary<TKey, int> CountBy<TSource, TKey>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            IEqualityComparer<TKey> comparer)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (keySelector == null)
            {
                throw new ArgumentNullException(nameof(keySelector));
            }

            Dictionary<TKey, int> result = new Dictionary<TKey, int>(comparer);

            int nullCount = 0;

            foreach (TSource it in source)
            {
                TKey key = keySelector(it);

                if (key == null)
                {
                    nullCount++;
                }
                else if (result.ContainsKey(key))
                {
                    result[key]++;
                }
                else
                {
                    result[key] = 1;
                }
            }

            if (nullCount > 0)
            {
                return new NullableKeyDictionary<TKey, int>(result, nullCount);
            }
            else
            {
                return result;
            }
        }
    }
}