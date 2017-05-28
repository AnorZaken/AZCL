using System;

namespace AZCL.Collections
{
    /// <summary>
    /// Static helper class for creating array enumerators.
    /// </summary><remarks>
    /// Iter stands for Iterator, and although this certainly isn't an iterator, there is some resemblance in the
    /// sense that iterators are used to (automatically) generate enumerators, and this class provides methods to
    /// create enumerators (with automatic type inference). Hence the name.
    /// </remarks>
    public static class Iter
    {
        /// <summary>
        /// Creates an enumerator (of <typeparamref name="T"/>) for a rank 2 array.
        /// </summary>
        public static ArrayR2<T>.Enumerator Create<T>(T[,] array)
        {
            return new ArrayR2<T>.Enumerator(array);
        }

        // ----

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

        /// <inheritdoc cref="ArrayEnumeratorReadOnly{T}.ArrayEnumeratorReadOnly(T[])"/>
        public static ArrayEnumeratorReadOnly<T> CreateReadOnly<T>(T[] array)
        {
            return new ArrayEnumeratorReadOnly<T>(array);
        }

        /// <inheritdoc cref="ArrayEnumeratorReadOnly{T}.ArrayEnumeratorReadOnly(T[], int)"/>
        public static ArrayEnumeratorReadOnly<T> CreateReadOnly<T>(T[] array, int length)
        {
            return new ArrayEnumeratorReadOnly<T>(array, length);
        }

        /// <inheritdoc cref="ArrayEnumeratorReadOnly{T}.ArrayEnumeratorReadOnly(int, T[])"/>
        public static ArrayEnumeratorReadOnly<T> CreateReadOnly<T>(int start, T[] array)
        {
            return new ArrayEnumeratorReadOnly<T>(start, array);
        }

        /// <inheritdoc cref="ArrayEnumeratorReadOnly{T}.ArrayEnumeratorReadOnly(int, T[], int)"/>
        public static ArrayEnumeratorReadOnly<T> CreateReadOnly<T>(int start, T[] array, int length)
        {
            return new ArrayEnumeratorReadOnly<T>(start, array, length);
        }

        // ----

        /// <inheritdoc cref="ArrayEnumeratorReadOnly{T}.ArrayEnumeratorReadOnly(ReadOnlyArray{T})"/>
        public static ArrayEnumeratorReadOnly<T> CreateReadOnly<T>(ReadOnlyArray<T> array)
        {
            return new ArrayEnumeratorReadOnly<T>(array);
        }

        /// <inheritdoc cref="ArrayEnumeratorReadOnly{T}.ArrayEnumeratorReadOnly(ReadOnlyArray{T}, int)"/>
        public static ArrayEnumeratorReadOnly<T> CreateReadOnly<T>(ReadOnlyArray<T> array, int length)
        {
            return new ArrayEnumeratorReadOnly<T>(array, length);
        }

        /// <inheritdoc cref="ArrayEnumeratorReadOnly{T}.ArrayEnumeratorReadOnly(int, ReadOnlyArray{T})"/>
        public static ArrayEnumeratorReadOnly<T> CreateReadOnly<T>(int start, ReadOnlyArray<T> array)
        {
            return new ArrayEnumeratorReadOnly<T>(start, array);
        }

        /// <inheritdoc cref="ArrayEnumeratorReadOnly{T}.ArrayEnumeratorReadOnly(int, ReadOnlyArray{T}, int)"/>
        public static ArrayEnumeratorReadOnly<T> CreateReadOnly<T>(int start, ReadOnlyArray<T> array, int length)
        {
            return new ArrayEnumeratorReadOnly<T>(start, array, length);
        }
    }
}
