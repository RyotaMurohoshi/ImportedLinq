using System;
using System.Collections.Generic;

namespace ImportedLinq
{
    public static partial class EnumerableEx
    {
        /// <summary>
        /// Partitions a sequence to Value(True, False) with a predicate, True is satisfied and False is not.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">A sequence to partition.</param>
        /// <param name="predicate">A function to partition elements.</param>
        /// <exception cref="System.ArgumentNullException">Thrown when source or predicate is null.</exception>
        /// <returns>
        /// A ValueTuple(True, False), True is sequence elements that are satisfied  the  predicate and False is sequence elements that are not satisfied the predicate.
        /// </returns>
        public static (IReadOnlyCollection<TSource> True, IReadOnlyCollection<TSource> False) Partition<TSource>(
            this IEnumerable<TSource> source, Func<TSource, bool> predicate
        )
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            List<TSource> trueList = new List<TSource>();
            List<TSource> falseList = new List<TSource>();

            foreach (TSource element in source)
            {
                if (predicate(element))
                {
                    trueList.Add(element);
                }
                else
                {
                    falseList.Add(element);
                }
            }

            return ValueTuple.Create(trueList, falseList);
        }
    }
}