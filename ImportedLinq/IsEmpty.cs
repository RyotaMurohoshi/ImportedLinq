using System;
using System.Collections.Generic;
using System.Linq;

namespace ImportedLinq
{
    public static partial class EnumerableEx
    {
        /// <summary>
        ///     Determines whether sequence is empty or not.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of the sequence.</typeparam>
        /// <param name="source">A sequence of values to determine whether sequence is empty.</param>
        /// <returns>True if the sequence is empty. False otherwise.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when source is null.</exception>
        public static bool IsEmpty<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return !source.Any();
        }
    }
}
