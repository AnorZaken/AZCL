using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AZCL.Collections
{
    // This could be greatly expanded in .net4+

    /// <summary>
    /// Static helper class for caching various empty (read-only) collection instances.
    /// </summary>
    /// <typeparam name="T">Element type of the collections.</typeparam>
    public static class Empty<T>
    {
        /// <summary>
        /// Empty (zero length) T[] array.
        /// </summary>
        public static readonly T[] Array = new T[0];

        /// <summary>
        /// Empty (read-only) ICollection&lt;T&gt;.
        /// </summary>
        public static ICollection<T> ICollection
        {
            get { return Array; }
        }

        /// <summary>
        /// Empty IEnumerable&lt;T&gt;.
        /// </summary>
        public static IEnumerable<T> IEnumerable
        {
            get { return Array; } // System.SZArrayHelper caches enumerators for empty arrays.
        }

        /// <summary>
        /// Empty IList&lt;T&gt;.
        /// </summary>
        public static IList<T> IList
        {
            get { return Array; }
        }

        /// <summary>
        /// Empty System.Collections.ObjectMode.ReadOnlyCollection&lt;T&gt;.
        /// </summary>
        public static ReadOnlyCollection<T> ReadOnlyCollection = new ReadOnlyCollection<T>(Array); // contains one IList and one Object reference (the latter is syncRoot).
    }
}
