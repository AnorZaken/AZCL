using System;
using System.Collections.Generic;

namespace AZCL.Collections
{
    /// <summary>
    /// A read-only struct wrapper for generic arrays.
    /// </summary><remarks>
    /// <para>
    /// Default initialized instances of this struct and will behave as if wrapping an empty array.
    /// See <see cref="ReadOnlyArray{T}.IsAbsent"/>.
    /// </para>
    /// <inheritdoc cref="Array2{T}" select="para[@id='wrapperSize']"/>
    /// </remarks>
    /// <typeparam name="T">The type of the elements of the array.</typeparam>
    public partial struct ReadOnlyArray<T> : IEquatable<ReadOnlyArray<T>>, IEquatable<Array>, IEnumerable<T>
    {
        private readonly T[] array;

        /// <summary>
        /// Creates a ReadOnlyArray wrapper for an array.
        /// </summary><remarks>
        /// If the array argument is null, the backing array of the ReadOnlyArray will simply be absent.
        /// </remarks>
        public static implicit operator ReadOnlyArray<T>(T[] array)
            => array == null ? new ReadOnlyArray<T>() : new ReadOnlyArray<T>(array);
        
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

        // used in various casts / ctors / extensions
        internal T[] ArrayRaw => array;

        // used in various casts / ctors / extensions
        internal T[] Array
            => array ?? Empty<T>.Array;

        /// <summary>
        /// Gets the value at the specified position in the wrapped backing array.
        /// </summary>
        /// <exception cref="IndexOutOfRangeException">
        /// Thrown if the index is less than zero, or larger than or equal to <see cref="Length"/>.
        /// </exception>
        public T this[int index]
        {
            get
            {
                if (array == null)
                    throw new IndexOutOfRangeException(ERR.BACKING_ARRAY_ABSENT);
                return array[index];
            }
        }

        /// <summary>
        /// Creates a copy of the backing array.
        /// </summary><returns>
        /// A copy of the backing array, or null if the backing array is absent.
        /// </returns>
        public T[] CopyBacking()
        {
            if (array == null)
                return null;
            T[] copy = new T[array.Length];
            System.Array.Copy(array, copy, array.Length);
            return copy;
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
        /// </summary><returns>
        /// True if <paramref name="obj"/> is a <see cref="ReadOnlyArray{T}"/> of the same <typeparamref name="T"/>
        /// and their backing arrays are reference equal, or if <paramref name="obj"/> is non-null and reference
        /// equal to the backing array of this instance; otherwise false.
        /// </returns>
        /// <param name="obj">Another object to compare against.</param>
        public override bool Equals(object obj)
        {
            return obj != null && (ReferenceEquals(this.array, obj) || obj is ReadOnlyArray<T> && Equals((ReadOnlyArray<T>)obj));
        }

        /// <summary>
        /// Indicates whether this and another instance are considered equivalent.
        /// </summary><returns>
        /// True if both instances share the same backing array, or if both backing arrays are absent; otherwise false.
        /// </returns>
        /// <param name="other">Another instance to compare against.</param>
        public bool Equals(ReadOnlyArray<T> other)
            => ReferenceEquals(this.array, other.array);
        
        /// <summary>
        /// Indicates whether the backing array of this wrapper and a specified array are reference equal.
        /// </summary><returns>
        /// True if this wrapper has a backing array (backing array is not absent) and it is the same
        /// array as the specified <paramref name="other"/> argument; otherwise false.
        /// </returns>
        /// <param name="other">Array to compare against the backing array of this wrapper.</param>
        public bool Equals(Array other)
            => array != null & ReferenceEquals(this.array, other);
        
        /// <summary>
        /// Get an enumerator that can iterate through the collection.
        /// </summary><remarks>
        /// This is the method that a foreach statement would call. (The C# compiler employs duck-typing for foreach loops.)
        /// </remarks>
        public Enumerator GetEnumerator()
            => new Enumerator(array);
        
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            IEnumerable<T> e = array ?? Empty<T>.Array;
            return e.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            => (array ?? Empty<T>.Array).GetEnumerator();
        
        /// <inheritdoc/>
        public override int GetHashCode()
            => array == null ? 0 : array.GetHashCode();
        
        /// <summary>
        /// Gets the value at the specified position in the wrapped backing array.
        /// </summary>
        /// <exception cref="IndexOutOfRangeException">
        /// Thrown if the index is less than zero, or larger than or equal to <see cref="Length"/>.
        /// </exception>
        public T GetValue(int index)
            => this[index];

        /// <summary>
        /// True if this instance was default initialized / constructed (backing array is absent).
        /// </summary><remarks>
        /// Default instances are still valid and behave as if the backing was an empty array.
        /// </remarks>
        public bool IsAbsent
            => array == null;

        /// <summary>
        /// Gets a 32-bit integer that represents the total number of elements in all
        /// the dimensions of the wrapped backing array.
        /// </summary><remarks>
        /// Will be zero if this wrapper was default constructed / backing array is absent.
        /// </remarks>
        public int Length
            => array == null ? 0 : array.Length;

        /// <inheritdoc/>
        public override string ToString()
            => array == null ? "<ReadOnlyArray:{}>" : ("<ReadOnlyArray:" + array.ToString() + ">");

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
