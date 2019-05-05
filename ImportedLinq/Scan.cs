using System;
using System.Collections.Generic;

namespace ImportedLinq
{
    public static partial class EnumerableEx
    {
        /// <summary>
        ///     Generates a accumulated values sequence with scanning the source sequence and applying the accumulator function.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TAccumulate">The type of the accumulator value.</typeparam>
        /// <param name="source">A sequence to scan over.</param>
        /// <param name="seed">The initial accumulator value.</param>
        /// <param name="accumulator"> An accumulator function to be invoked on each element. </param>
        /// <exception cref="System.ArgumentNullException">Thrown when source or accumulator is null.</exception>
        /// <returns>Sequence with each intermediate accumulated value.</returns>
        /// <remarks>
        ///     This method is implemented by using deferred execution. The immediate return value is an object that stores all the information that is required to perform the action. The query represented by this method is not executed until the object is enumerated either by calling its GetEnumerator method directly or by using foreach in Visual C# or For Each in Visual Basic.
        /// </remarks>
        public static IEnumerable<TAccumulate> Scan<TSource, TAccumulate>(
            this IEnumerable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> accumulator)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (accumulator == null)
            {
                throw new ArgumentNullException(nameof(accumulator));
            }

            return Impl(source, seed, accumulator);

            IEnumerable<TAccumulate> Impl(IEnumerable<TSource> sourceImpl,
                TAccumulate seedImpl,
                Func<TAccumulate, TSource, TAccumulate> accumulatorImpl)
            {
                TAccumulate acc = seedImpl;

                foreach (TSource element in sourceImpl)
                {
                    acc = accumulatorImpl(acc, element);
                    yield return acc;
                }
            }
        }

        /// <summary>
        ///     Generates a accumulated values sequence with scanning the source sequence and applying the accumulator function.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">A sequence to scan over.</param>
        /// <param name="accumulator"> An accumulator function to be invoked on each element. </param>
        /// <exception cref="System.ArgumentNullException">Thrown when source or accumulator is null.</exception>
        /// <returns>Sequence with each intermediate accumulated value.</returns>
        /// <remarks>
        ///     This method is implemented by using deferred execution. The immediate return value is an object that stores all the information that is required to perform the action. The query represented by this method is not executed until the object is enumerated either by calling its GetEnumerator method directly or by using foreach in Visual C# or For Each in Visual Basic.
        /// </remarks>
        public static IEnumerable<TSource> Scan<TSource>(
            this IEnumerable<TSource> source, Func<TSource, TSource, TSource> accumulator)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (accumulator == null)
            {
                throw new ArgumentNullException(nameof(accumulator));
            }

            return Impl(source, accumulator);

            IEnumerable<TSource> Impl(IEnumerable<TSource> sourceImpl, Func<TSource, TSource, TSource> accumulatorImpl)
            {
                bool hasSeed = false;
                TSource acc = default;

                foreach (TSource element in sourceImpl)
                {
                    if (hasSeed)
                    {
                        acc = accumulatorImpl(acc, element);
                        yield return acc;
                    }
                    else
                    {
                        hasSeed = true;
                        acc = element;
                    }
                }
            }
        }
    }
}