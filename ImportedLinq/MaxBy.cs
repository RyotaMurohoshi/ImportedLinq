using System;
using System.Collections.Generic;

namespace ImportedLinq
{
    public static partial class EnumerableEx
    {
        /// <summary>
        ///     Finds the elements that have maximum key value in the sequence by using the argument comparer.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by keySelector.</typeparam>
        /// <param name="source">A sequence of elements to determine the maximum values of.</param>
        /// <param name="keySelector">A function to extract a key from each element.</param>
        /// <exception cref="System.ArgumentNullException">Thrown when source or keySelector is null.</exception>
        /// <exception cref="System.InvalidOperationException">Thrown when sequence is empty.</exception>
        /// <returns>Collection with the elements that have maximum key value in the sequence.</returns>
        public static IReadOnlyCollection<TSource> MaxBy<TSource, TKey>(
            this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (keySelector == null)
            {
                throw new ArgumentNullException(nameof(keySelector));
            }

            return ExtremeBy(source, keySelector, Comparer<TKey>.Default, Order.Normal);
        }

        /// <summary>
        ///     Finds the elements that have maximum key value in the sequence by using the argument comparer.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by keySelector.</typeparam>
        /// <param name="source">A sequence of elements to determine the maximum values of.</param>
        /// <param name="keySelector">A function to extract a key from each element.</param>
        /// <param name="comparer">A comparer to compare key value.</param>
        /// <exception cref="System.ArgumentNullException">Thrown when source, keySelector or comparer is null.</exception>
        /// <exception cref="System.InvalidOperationException">Thrown when sequence is empty.</exception>
        /// <returns>Collection with the elements that have maximum key value in the sequence.</returns>
        public static IReadOnlyCollection<TSource> MaxBy<TSource, TKey>(
            this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (keySelector == null)
            {
                throw new ArgumentNullException(nameof(keySelector));
            }

            if (comparer == null)
            {
                throw new ArgumentNullException(nameof(comparer));
            }

            return ExtremeBy(source, keySelector, comparer, Order.Normal);
        }
    }
}