using System;
using System.Collections.Generic;

namespace ImportedLinq
{
    public static partial class EnumerableEx
    {
        /// <summary>
        ///     Generates a sequence of buffers over the source sequence with specified length.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of the sequence.</typeparam>
        /// <param name="source">A sequence of values to flatten.</param>
        /// <param name="count">The number of elements for allocated buffers.</param>
        /// <exception cref="System.ArgumentNullException">Thrown when source is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Thrown when count is less than or equal to 0.</exception>
        /// <returns>Buffered Sequence that containing source sequence elements.</returns>
        /// <remarks>
        ///     This method is implemented by using deferred execution. The immediate return value is an object that stores all the information that is required to perform the action. The query represented by this method is not executed until the object is enumerated either by calling its GetEnumerator method directly or by using foreach in Visual C# or For Each in Visual Basic.
        /// </remarks>
        public static IEnumerable<IReadOnlyList<TSource>> Buffer<TSource>(this IEnumerable<TSource> source, int count)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (count <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            return BufferImpl(source, count);
        }

        /// <summary>
        ///     Generates a sequence of buffers over the source sequence, with specified length, possible overlap or skip.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of the sequence.</typeparam>
        /// <param name="source">A sequence of values to flatten.</param>
        /// <param name="count">The number of elements for allocated buffers.</param>
        /// <param name="skip">The number of elements to skip  between the start of consecutive buffers.</param>
        /// <exception cref="System.ArgumentNullException">Thrown when source is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Thrown when count or skip is less than or equal to 0.</exception>
        /// <returns>Buffered Sequence that containing source sequence elements.</returns>
        /// <remarks>
        ///     This method is implemented by using deferred execution. The immediate return value is an object that stores all the information that is required to perform the action. The query represented by this method is not executed until the object is enumerated either by calling its GetEnumerator method directly or by using foreach in Visual C# or For Each in Visual Basic.
        /// </remarks>
        public static IEnumerable<IReadOnlyList<TSource>> Buffer<TSource>(
            this IEnumerable<TSource> source, int count, int skip)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (count <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            if (skip <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(skip));
            }

            if (count == skip)
            {
                return BufferImpl(source, count);
            }
            else if (count < skip)
            {
                return BufferSkip(source, count, skip);
            }
            else
            {
                return BufferOverlap(source, count, skip);
            }
        }

        private static IEnumerable<IReadOnlyList<TSource>> BufferImpl<TSource>(IEnumerable<TSource> source, int count)
        {
            List<TSource> buffer = null;

            foreach (TSource element in source)
            {
                if (buffer == null)
                {
                    buffer = new List<TSource>();
                }

                buffer.Add(element);

                if (buffer.Count == count)
                {
                    yield return buffer;
                    buffer = null;
                }
            }

            if (buffer != null)
            {
                yield return buffer;
            }
        }

        private static IEnumerable<IReadOnlyList<TSource>> BufferOverlap<TSource>(
            IEnumerable<TSource> source, int count, int skip)
        {
            Queue<List<TSource>> buffers = new Queue<List<TSource>>();

            int index = 0;
            foreach (TSource element in source)
            {
                if (index % skip == 0)
                {
                    buffers.Enqueue(new List<TSource>(count));
                }

                foreach (List<TSource> buffer in buffers)
                {
                    buffer.Add(element);
                }

                if (buffers.Count > 0 && buffers.Peek().Count == count)
                {
                    yield return buffers.Dequeue();
                }

                index++;
            }

            while (buffers.Count > 0)
            {
                yield return buffers.Dequeue();
            }
        }


        private static IEnumerable<IReadOnlyList<TSource>> BufferSkip<TSource>(
            IEnumerable<TSource> source, int count, int skip)
        {
            List<TSource> buffer = null;

            int index = 0;

            foreach (TSource element in source)
            {
                if (index == 0)
                {
                    buffer = new List<TSource>();
                }

                buffer?.Add(element);

                index++;

                if (index == count)
                {
                    yield return buffer;
                    buffer = null;
                }

                if (index == skip)
                {
                    index = 0;
                }
            }

            if (buffer != null)
            {
                yield return buffer;
            }
        }
    }
}