using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AZCL.Collections
{
    /// <summary>
    /// Static extension class that implements most static System.Array methods, but for ReadOnlyArrays.
    /// </summary>
    public static class ReadOnlyArray
    {
        /// <summary>
        /// Creates a <see cref="ReadOnlyArray{T}"/> for an array.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the array.</typeparam>
        /// <param name="array">The array to wrap.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="array"/> is null.
        /// </exception>
        public static ReadOnlyArray<T> Create<T>(T[] array)
        {
            return new ReadOnlyArray<T>(array);
        }

        /* Static methods from the System.Array class - wrapped as extension methods for ReadOnlyArray. */
        /* ******************************************************************************************** */

        /// <inheritdoc cref="Array.AsReadOnly{T}(T[])"/>
        public static ReadOnlyCollection<T> AsReadOnlyColletion<T>(this ReadOnlyArray<T> array)
        {
            return array.Source == null ? Empty<T>.ReadOnlyCollection : new ReadOnlyCollection<T>(array.Source);
        }

        /// <inheritdoc cref="Array.BinarySearch{T}(T[], T)"/>
        public static int BinarySearch<T>(this ReadOnlyArray<T> array, T value)
        {
            return array.Source == null ? -1 : Array.BinarySearch<T>(array.Source, value);
        }

        /// <inheritdoc cref="Array.BinarySearch{T}(T[], T, IComparer{T})"/>
        public static int BinarySearch<T>(this ReadOnlyArray<T> array, T value, IComparer<T> comparer)
        {
            return array.Source == null ? -1 : Array.BinarySearch<T>(array.Source, value, comparer);
        }

        /// <inheritdoc cref="Array.BinarySearch{T}(T[], int, int, T)"/>
        public static int BinarySearch<T>(this ReadOnlyArray<T> array, int index, int length, T value)
        {
            return array.Source == null ? -1 : Array.BinarySearch<T>(array.Source, index, length, value);
        }

        /// <inheritdoc cref="Array.BinarySearch{T}(T[], int, int, T, IComparer{T})"/>
        public static int BinarySearch<T>(this ReadOnlyArray<T> array, int index, int length, T value, IComparer<T> comparer)
        {
            return array.Source == null ? -1 : Array.BinarySearch<T>(array.Source, index, length, value, comparer);
        }

        /// <inheritdoc cref="Array.Exists{T}(T[], Predicate{T})"/>
        public static bool Exists<T>(this ReadOnlyArray<T> array, Predicate<T> match)
        {
            return array.Source == null ? false : Array.Exists<T>(array.Source, match);
        }

        /// <inheritdoc cref="Array.TrueForAll{T}(T[], Predicate{T})"/>
        public static bool TrueForAll<T>(this ReadOnlyArray<T> array, Predicate<T> match)
        {
            return array.Source == null ? false : Array.TrueForAll<T>(array.Source, match);
        }

        /// <inheritdoc cref="Array.Find{T}(T[], Predicate{T})"/>
        public static T Find<T>(this ReadOnlyArray<T> array, Predicate<T> match)
        {
            return array.Source == null ? default(T) : Array.Find<T>(array.Source, match);
        }

        /// <summary>
        /// Retrieves all the elements that match the conditions defined by the specified predicate as an ArrayIterator.
        /// </summary>
        /// <param name="array">Array to search in.</param>
        /// <param name="match">The System.Predicate&lt;T&gt; that defined the conditions of the elements to search for.</param>
        /// <returns>
        /// An <see cref="ArrayEnumerator{T}"/> wrapping an array containing all the elements that matched the conditions defined
        /// by the specified predicate, if found; otherwise a default constructed <see cref="ArrayEnumerator{T}"/> (wrapping null).
        /// </returns>
        public static ArrayEnumerator<T> FindAll<T>(this ReadOnlyArray<T> array, Predicate<T> match)
        {
            if (array.Source == null)
                return default(ArrayEnumerator<T>);
            var arr = Array.FindAll<T>(array.Source, match);
            return arr.Length == 0 ? default(ArrayEnumerator<T>) : arr;
        }

        /// <inheritdoc cref="Array.FindLast{T}(T[], Predicate{T})"/>
        public static T FindLast<T>(this ReadOnlyArray<T> array, Predicate<T> match)
        {
            return array.Source == null ? default(T) : Array.FindLast<T>(array.Source, match);
        }

        /// <inheritdoc cref="Array.FindIndex{T}(T[], Predicate{T})"/>
        public static int FindIndex<T>(this ReadOnlyArray<T> array, Predicate<T> match)
        {
            return array.Source == null ? -1 : Array.FindIndex<T>(array.Source, match);
        }

        /// <inheritdoc cref="Array.FindIndex{T}(T[], int, Predicate{T})"/>
        public static int FindIndex<T>(this ReadOnlyArray<T> array, int startIndex, Predicate<T> match)
        {
            return array.Source == null ? -1 : Array.FindIndex<T>(array.Source, startIndex, match);
        }

        /// <inheritdoc cref="Array.FindIndex{T}(T[], int, int, Predicate{T})"/>
        public static int FindIndex<T>(this ReadOnlyArray<T> array, int startIndex, int count, Predicate<T> match)
        {
            return array.Source == null ? -1 : Array.FindIndex<T>(array.Source, startIndex, count, match);
        }

        /// <inheritdoc cref="Array.FindLast{T}(T[], Predicate{T})"/>
        public static int FindLastIndex<T>(this ReadOnlyArray<T> array, Predicate<T> match)
        {
            return array.Source == null ? -1 : Array.FindLastIndex<T>(array.Source, match);
        }

        /// <inheritdoc cref="Array.FindLastIndex{T}(T[], int, Predicate{T})"/>
        public static int FindLastIndex<T>(this ReadOnlyArray<T> array, int startIndex, Predicate<T> match)
        {
            return array.Source == null ? -1 : Array.FindLastIndex<T>(array.Source, startIndex, match);
        }

        /// <inheritdoc cref="Array.FindLastIndex{T}(T[], int, int, Predicate{T})"/>
        public static int FindLastIndex<T>(this ReadOnlyArray<T> array, int startIndex, int count, Predicate<T> match)
        {
            return array.Source == null ? -1 : Array.FindLastIndex<T>(array.Source, startIndex, count, match);
        }

        /// <inheritdoc cref="Array.IndexOf{T}(T[], T)"/>
        public static int IndexOf<T>(this ReadOnlyArray<T> array, T value)
        {
            return array.Source == null ? -1 : Array.IndexOf<T>(array.Source, value);
        }

        /// <inheritdoc cref="Array.IndexOf{T}(T[], T, int)"/>
        public static int IndexOf<T>(this ReadOnlyArray<T> array, T value, int startIndex)
        {
            return array.Source == null ? -1 : Array.IndexOf<T>(array.Source, value, startIndex);
        }

        /// <inheritdoc cref="Array.IndexOf{T}(T[], T, int, int)"/>
        public static int IndexOf<T>(this ReadOnlyArray<T> array, T value, int startIndex, int count)
        {
            return array.Source == null ? -1 : Array.IndexOf<T>(array.Source, value, startIndex, count);
        }

        /// <inheritdoc cref="Array.LastIndexOf{T}(T[], T)"/>
        public static int LastIndexOf<T>(this ReadOnlyArray<T> array, T value)
        {
            return array.Source == null ? -1 : Array.LastIndexOf<T>(array.Source, value);
        }

        /// <inheritdoc cref="Array.LastIndexOf{T}(T[], T, int)"/>
        public static int LastIndexOf<T>(this ReadOnlyArray<T> array, T value, int startIndex)
        {
            return array.Source == null ? -1 : Array.LastIndexOf<T>(array.Source, value, startIndex);
        }

        /// <inheritdoc cref="Array.LastIndexOf{T}(T[], T, int, int)"/>
        public static int LastIndexOf<T>(this ReadOnlyArray<T> array, T value, int startIndex, int count)
        {
            return array.Source == null ? -1 : Array.LastIndexOf<T>(array.Source, value, startIndex, count);
        }
    }
}
