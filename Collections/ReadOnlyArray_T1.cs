using System;
using System.Collections.Generic;

namespace AZCL.Collections
{
    /// <summary>
    /// A read-only struct wrapper for generic arrays.
    /// </summary><remarks>
    /// Instances of this struct are valid even when default initialized. See <see cref="ReadOnlyArray{T}.IsDefault"/>.
    /// <para/>
    /// This struct contains only a single field: a reference to the source array.
    /// Thus its size will match that of a reference meaning that instances can be passed around as arguments without performance penalty.
    /// </remarks>
    /// <typeparam name="T">The type of the elements of the array.</typeparam>
    public partial struct ReadOnlyArray<T> : IEquatable<ReadOnlyArray<T>>, IEquatable<T[]>, IEnumerable<T>
    {
        private const string
               ERR_SOURCE_NULL = "Source array is null.";

        private readonly T[] array;

        /// <summary>
        /// Creates a ReadOnlyArray wrapper for an array.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="array"/> is null.
        /// </exception>
        public static implicit operator ReadOnlyArray<T>(T[] array)
        {
            return new ReadOnlyArray<T>(array);
        }
        
        /// <summary>
        /// Creates a ReadOnlyArray wrapper for an array.
        /// </summary>
        /// <param name="array">The array to wrap.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="array"/> is null.
        /// </exception>
        public ReadOnlyArray(T[] array)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            this.array = array;
        }

        // used by ReadOnlyArrayEnumerator ctor and ReadOnlyArray extensions
        internal T[] Source
        {
            get { return array; }
        }

        /// <summary>
        /// True if this instance was default initialized / constructed (source array is null).
        /// </summary><remarks>
        /// Default instances are still valid and behave as if the source was an empty array.
        /// </remarks>
        public bool IsDefault
        {
            get { return array == null; }
        }

        /// <summary>
        /// Creates a copy of the source array.
        /// </summary>
        /// <returns>A copy of the source array, or null if the source array is null.</returns>
        public T[] CopySource()
        {
            if (array == null)
                return null;
            T[] copy = new T[array.Length];
            Array.Copy(array, copy, array.Length);
            return copy;
        }

        /// <summary>
        /// Gets a 32-bit integer that represents the total number of elements in all
        /// the dimensions of the wrapped source array.
        /// </summary><remarks>
        /// Will be zero if the ReadOnlyArray was default constructed.
        /// </remarks>
        public int Length
        {
            get { return array == null ? 0 : array.Length; }
        }

        /// <summary>
        /// Gets the value at the specified position in the wrapped source array.
        /// </summary>
        /// <exception cref="IndexOutOfRangeException">
        /// Thrown if the index is less than zero, or larger than or equal to <see cref="Length"/>.
        /// </exception>
        public T this[int index]
        {
            get
            {
                if (array == null)
                    throw new IndexOutOfRangeException(ERR_SOURCE_NULL);
                return array[index];
            }
        }

        /// <summary>
        /// Copies all the elements of the current one-dimensional ReadOnlyArray to the
        /// specified one-dimensional System.Array starting at the specified destination
        /// System.Array index.
        /// </summary><remarks>
        /// This method does nothing if the ReadOnlyArray was default constructed.
        /// <para/>
        /// See System.Array.CopyTo for more information.
        /// </remarks>
        /// <param name="array">
        /// The one-dimensional System.Array that is the destination
        /// of the elements copied from the current ReadOnlyArray.
        /// </param>
        /// <param name="index">
        /// A 32-bit integer that represents the index in <paramref name="array"/> at
        /// which copying begins.
        /// </param>
        public void CopyTo(Array array, int index)
        {
            if (this.array != null)
                this.array.CopyTo(array, index);
        }
        
        /// <summary>
        /// Indicates whether this instance and a specified object are considered equivalent.
        /// </summary>
        /// <param name="obj">Another object to compare to.</param>
        /// <returns>
        /// True if <paramref name="obj"/> is a <see cref="ReadOnlyArray{T}"/> of the same <typeparamref name="T"/> and their source arrays are
        /// reference equal, or if <paramref name="obj"/> is non-null and reference equal to the source array of this instance; otherwise false.
        /// </returns>
        public override bool Equals(object obj)
        {
            return obj != null && (object.ReferenceEquals(this.array, obj) || obj is ReadOnlyArray<T> && Equals((ReadOnlyArray<T>)obj));
        }

        /// <inheritdoc/>
        public bool Equals(ReadOnlyArray<T> other)
        {
            return object.ReferenceEquals(this.array, other.array);
        }
        /// <inheritdoc/>
        public bool Equals(T[] other) // returns true for null if source array is also null.
        {
            return object.ReferenceEquals(this.array, other);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary><remarks>
        /// This is the method that a foreach statement would call on a ReadOnlyArray value. (The C# compiler employs duck-typing hacks for foreach loops!)
        /// </remarks>
        public Enumerator GetEnumerator()
        {
            return new Enumerator(array);
        }

        /// <inheritdoc/>
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            IEnumerable<T> e = array ?? Empty<T>.Array;
            return e.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return (array ?? Empty<T>.Array).GetEnumerator();
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return array == null ? 0 : array.GetHashCode();
        }

        /// <summary>
        /// Gets the value at the specified position in the wrapped source array.
        /// </summary>
        /// <exception cref="IndexOutOfRangeException">
        /// Thrown if the index is less than zero, or larger than or equal to <see cref="Length"/>.
        /// </exception>
        public T GetValue(int index)
        {
            return this[index];
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return array == null ? "<ReadOnlyArray:{}>" : ("<ReadOnlyArray:" + array.ToString() + ">");
        }

        
        /*
        internal ReadOnlyArrayEnumerator<T> GetReadOnlyArrayEnumerator(int start, int length)
        {
            if (array == null)
            {
                if ((start | length) == 0)
                    return default(ReadOnlyArrayEnumerator<T>);
                else
                    throw new ArgumentOutOfRangeException(start == 0 ? nameof(length) : nameof(start));
            }

            return new ReadOnlyArrayEnumerator<T>(start, array, length);
        }
        */
    }
}
