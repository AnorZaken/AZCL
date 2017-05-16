using System;

namespace AZCL.Collections
{
    /// <summary>
    /// Static helper class for creating array enumerators.
    /// </summary><remarks>
    /// Iter stands for Iterator, and although this certainly isn't an iterator, there is some resemblance in the
    /// sense that iterators are used to (automatically) generate enumerators, and this class provides methods to
    /// create enumerators (with automatic type inference). Hence the name choice.
    /// </remarks>
    public static class Iter
    {
        /// <inheritdoc cref="ArrayEnumerator{T}.ArrayEnumerator(T[])"/>
        public static ArrayEnumerator<T> Create<T>(T[] array)
        {
            return new ArrayEnumerator<T>(array);
        }

        /// <inheritdoc cref="ArrayEnumerator{T}.ArrayEnumerator(T[], int)"/>
        public static ArrayEnumerator<T> Create<T>(T[] array, int length)
        {
            return new ArrayEnumerator<T>(array, length);
        }

        /// <inheritdoc cref="ArrayEnumerator{T}.ArrayEnumerator(int, T[])"/>
        public static ArrayEnumerator<T> Create<T>(int start, T[] array)
        {
            return new ArrayEnumerator<T>(start, array);
        }

        /// <inheritdoc cref="ArrayEnumerator{T}.ArrayEnumerator(int, T[], int)"/>
        public static ArrayEnumerator<T> Create<T>(int start, T[] array, int length)
        {
            return new ArrayEnumerator<T>(start, array, length);
        }

        // ----

        /// <inheritdoc cref="ReadOnlyArrayEnumerator{T}.ReadOnlyArrayEnumerator(T[])"/>
        public static ReadOnlyArrayEnumerator<T> CreateReadOnly<T>(T[] array)
        {
            return new ReadOnlyArrayEnumerator<T>(array);
        }

        /// <inheritdoc cref="ReadOnlyArrayEnumerator{T}.ReadOnlyArrayEnumerator(T[], int)"/>
        public static ReadOnlyArrayEnumerator<T> CreateReadOnly<T>(T[] array, int length)
        {
            return new ReadOnlyArrayEnumerator<T>(array, length);
        }

        /// <inheritdoc cref="ReadOnlyArrayEnumerator{T}.ReadOnlyArrayEnumerator(int, T[])"/>
        public static ReadOnlyArrayEnumerator<T> CreateReadOnly<T>(int start, T[] array)
        {
            return new ReadOnlyArrayEnumerator<T>(start, array);
        }

        /// <inheritdoc cref="ReadOnlyArrayEnumerator{T}.ReadOnlyArrayEnumerator(int, T[], int)"/>
        public static ReadOnlyArrayEnumerator<T> CreateReadOnly<T>(int start, T[] array, int length)
        {
            return new ReadOnlyArrayEnumerator<T>(start, array, length);
        }

        // ----

        /// <inheritdoc cref="ReadOnlyArrayEnumerator{T}.ReadOnlyArrayEnumerator(ReadOnlyArray{T})"/>
        public static ReadOnlyArrayEnumerator<T> CreateReadOnly<T>(ReadOnlyArray<T> array)
        {
            return new ReadOnlyArrayEnumerator<T>(array);
        }

        /// <inheritdoc cref="ReadOnlyArrayEnumerator{T}.ReadOnlyArrayEnumerator(ReadOnlyArray{T}, int)"/>
        public static ReadOnlyArrayEnumerator<T> CreateReadOnly<T>(ReadOnlyArray<T> array, int length)
        {
            return new ReadOnlyArrayEnumerator<T>(array, length);
        }

        /// <inheritdoc cref="ReadOnlyArrayEnumerator{T}.ReadOnlyArrayEnumerator(int, ReadOnlyArray{T})"/>
        public static ReadOnlyArrayEnumerator<T> CreateReadOnly<T>(int start, ReadOnlyArray<T> array)
        {
            return new ReadOnlyArrayEnumerator<T>(start, array);
        }

        /// <inheritdoc cref="ReadOnlyArrayEnumerator{T}.ReadOnlyArrayEnumerator(int, ReadOnlyArray{T}, int)"/>
        public static ReadOnlyArrayEnumerator<T> CreateReadOnly<T>(int start, ReadOnlyArray<T> array, int length)
        {
            return new ReadOnlyArrayEnumerator<T>(start, array, length);
        }
    }
}
