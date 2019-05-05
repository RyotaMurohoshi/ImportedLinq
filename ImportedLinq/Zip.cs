using System;
using System.Collections.Generic;
using System.Linq;

namespace ImportedLinq
{
    public static partial class EnumerableEx
    {
        /// <summary>
        ///     Creates ValueTuple(First, Second) to the corresponding elements of two sequences, produces a sequence of the ValueTuple(First, Second).
        /// </summary>
        /// <typeparam name="TFirst">The type of the elements of the first input sequence.</typeparam>
        /// <typeparam name="TSecond">The type of the elements of the second input sequence.</typeparam>
        /// <param name="first">The first sequence to merge.</param>
        /// <param name="second">The second sequence to merge.</param>
        /// <returns>Sequence that contains ValueTuple(First, Second).</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when first or second is null.</exception>
        /// <remarks>
        ///     This method is implemented by using deferred execution. The immediate return value is an object that stores all the information that is required to perform the action. The query represented by this method is not executed until the object is enumerated either by calling its GetEnumerator method directly or by using foreach in Visual C# or For Each in Visual Basic.
        ///
        ///     The method merges each element of the first sequence with an element that has the same index in the second sequence. If the sequences do not have the same number of elements, the method merges sequences until it reaches the end of one of them. For example, if one sequence has three elements and the other one has four, the result sequence will have only three elements.
        /// </remarks>
        public static IEnumerable<(TFirst First, TSecond Second)> Zip<TFirst, TSecond>(
            this IEnumerable<TFirst> first, IEnumerable<TSecond> second
        )
        {
            if (first == null)
            {
                throw new ArgumentNullException(nameof(first));
            }

            if (second == null)
            {
                throw new ArgumentNullException(nameof(second));
            }

            return first.Zip(second, ValueTuple.Create);
        }
    }
}