using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AZCL.Collections
{
    // This could be greatly expanded in .net4+

    /// <summary>
    /// Provides a static cache of various empty (read-only) collection instances.
    /// </summary>
    /// <typeparam name="T">Element type of the collections.</typeparam>
    public static class Empty<T>
    {
        /// <summary>
        /// Empty (zero length) <typeparamref name="T"/>[] array.
        /// </summary>
        public static readonly T[] Array = new T[0];

        /// <summary>
        /// Empty (zero length) <typeparamref name="T"/>[,] array.
        /// </summary>
        public static readonly T[,] ArrayR2 = new T[0,0];

        /// <summary>
        /// Empty (zero length) <typeparamref name="T"/>[,,] array.
        /// </summary>
        public static readonly T[,,] ArrayR3 = new T[0,0,0];

        /// <summary>
        /// Empty (read-only) ICollection&lt;<typeparamref name="T"/>&gt;.
        /// </summary>
        public static ICollection<T> ICollection
            => Array;

        /// <summary>
        /// Empty IEnumerable&lt;<typeparamref name="T"/>&gt;.
        /// </summary>
        public static IEnumerable<T> IEnumerable
            => Array; // System.SZArrayHelper caches enumerators for empty arrays.
        
        /// <summary>
        /// Get an empty IEnumerator&lt;<typeparamref name="T"/>&gt;.
        /// </summary>
        public static IEnumerator<T> GetEnumerator()
            => IEnumerable.GetEnumerator();

        /// <summary>
        /// Empty IList&lt;<typeparamref name="T"/>&gt;.
        /// </summary>
        public static IList<T> IList
            => Array;

        /// <summary>
        /// Empty System.Collections.ObjectMode.ReadOnlyCollection&lt;<typeparamref name="T"/>&gt;.
        /// </summary>
        public static ReadOnlyCollection<T> ReadOnlyCollection = new ReadOnlyCollection<T>(Array); // contains one IList and one Object reference (the latter is syncRoot).
    }
}
