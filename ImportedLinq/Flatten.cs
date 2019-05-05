using System;
using System.Collections.Generic;
using System.Linq;

namespace ImportedLinq
{
    public static partial class EnumerableEx
    {
        /// <summary>
        ///     Converts sequence of sequence to sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of the sequence.</typeparam>
        /// <param name="source">A sequence of values to flatten.</param>
        /// <returns>Flattened sequence.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when source is null.</exception>
        /// <remarks>
        ///     This method is implemented by using deferred execution. The immediate return value is an object that stores all the information that is required to perform the action. The query represented by this method is not executed until the object is enumerated either by calling its GetEnumerator method directly or by using foreach in Visual C# or For Each in Visual Basic.
        /// </remarks>
        public static IEnumerable<TSource> Flatten<TSource>(this IEnumerable<IEnumerable<TSource>> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.SelectMany(it => it);
        }
    }
}