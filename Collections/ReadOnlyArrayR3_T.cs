using System;
using System.Collections.Generic;

namespace AZCL.Collections
{
    /// <summary>
    /// A read-only struct wrapper for generic arrays.
    /// </summary><remarks>
    /// Instances of this struct are valid even when default initialized. See <see cref="ReadOnlyArrayR2{T}.IsAbsent"/>.
    /// <para/>
    /// This struct contains only a single field: a reference to the backing array.
    /// Thus its size will match that of a reference meaning that instances can be passed around as arguments without performance penalty.
    /// </remarks>
    /// <typeparam name="T">The type of the elements of the array.</typeparam>
    public partial struct ReadOnlyArrayR3<T> : IEquatable<ReadOnlyArrayR3<T>>, IEquatable<Array>, IEnumerable<T>
    {
        private readonly T[,,] array;

        /// <summary>
        /// Creates a ReadOnlyArray wrapper for an array.
        /// </summary><remarks>
        /// If the array argument is null, the backing array of the ReadOnlyArray will simply be absent.
        /// </remarks>
        public static implicit operator ReadOnlyArrayR3<T>(T[,,] array)
        {
            return array == null ? new ReadOnlyArrayR3<T>() : new ReadOnlyArrayR3<T>(array);
        }

        /// <summary>
        /// Creates a ReadOnlyArray wrapper for an array.
        /// </summary><remarks>
        /// If a backing array is absent, the backing array of the ReadOnlyArray will simply be absent too.
        /// </remarks>
        public static implicit operator ReadOnlyArrayR3<T>(ArrayR3<T> array)
        {
            return new ReadOnlyArrayR3<T>(array);
        }

        /// <summary>
        /// Creates a ReadOnlyArrayR3 wrapper for a rank 3 array.
        /// </summary>
        /// <param name="array">The array to wrap.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="array"/> is null.
        /// </exception>
        public ReadOnlyArrayR3(T[,,] array)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            this.array = array;
        }

        /// <summary>
        /// Creates a ReadOnlyArrayR3 wrapper from an ArrayR3 wrapper.
        /// </summary><remarks>
        /// If a backing array is absent, the backing array of the ReadOnlyArray will simply be absent too.
        /// </remarks>
        /// <param name="array">A wrapped array to re-wrap as readonly.</param>
        public ReadOnlyArrayR3(ArrayR3<T> array)
        {
            this.array = array.Array;
            //if (this.array == null)
            //    throw new ArgumentException(paramName: nameof(array), message: ERR.ARRAY_ARG_ABSENT);
        }

        // used in various casts / ctors / extensions
        internal T[,,] Array
        {
            get { return array; }
        }

        /// <summary>
        /// Gets the value at the specified enumeration index in the wrapped backing array.
        /// </summary>
        /// <inheritdoc cref="CalculateIndexes(int, out int, out int, out int)" select="remarks"/>
        /// <param name="index">An enumeration index. See <see cref="CalculateIndexes(int, out int, out int, out int)"/> for more info.</param>
        /// <exception cref="IndexOutOfRangeException">
        /// Thrown if <paramref name="index"/> is less than zero or greater than or equal to the <see cref="Length"/> of the array.
        /// (Note especially that if the backing array is absent (null) the <see cref="Length"/> property will be zero.)
        /// </exception>
        /// <seealso cref="CalculateIndexes(int, out int, out int, out int)"/>
        public T this[int index]
        {
            get
            {
                int x, y, z;
                CalculateIndexes(index, out x, out y, out z);
                return array[x, y, z];
            }
        }

        /// <summary>
        /// Gets the value at the specified position in the wrapped backing array.
        /// </summary>
        /// <exception cref="IndexOutOfRangeException">
        /// Thrown if any of the indexes are less than zero, or greater than the upper bound for the corresponding dimension.
        /// </exception>
        public T this[int x, int y, int z]
        {
            get
            {
                if (array == null)
                    throw new IndexOutOfRangeException(ERR.BACKING_ARRAY_ABSENT);
                return array[x, y, z];
            }
        }

        /// <summary>
        /// Given a one-dimensional enumeration index, calculates the corresponding x, y, and z item indexes.
        /// </summary><remarks>
        /// When enumerating over multi-dimensional arrays elements are traversed such that the indices of the
        /// rightmost dimension are increased first, then the next left dimension, and so on to the left.
        /// <br/>(See C# Language Specification - Version 4.0 paragraph 8.8.4)<br/>
        /// In other words x = index / (<see cref="LengthY"/> * <see cref="LengthZ"/>),
        /// temp = index % (<see cref="LengthY"/> * <see cref="LengthZ"/>),
        /// y = temp / <see cref="LengthZ"/>, z = temp % <see cref="LengthZ"/>.
        /// </remarks>
        /// <param name="index">An enumeration index to calculate item indexes for.</param>
        /// <param name="x">Resulting x index.</param>
        /// <param name="y">Resulting y index.</param>
        /// <param name="z">Resulting z index.</param>
        /// <exception cref="IndexOutOfRangeException">
        /// Thrown if <paramref name="index"/> is less than zero or greater than or equal to the <see cref="Length"/> of the array.
        /// (Note especially that if the backing array <see cref="IsAbsent"/> the <see cref="Length"/> property will be zero.)
        /// </exception>
        /// <seealso cref="GetValue1D(int)"/>
        public void CalculateIndexes(int index, out int x, out int y, out int z)
        {
            if (unchecked((uint)index >= (uint)Length))
                throw array == null ? new IndexOutOfRangeException(ERR.BACKING_ARRAY_ABSENT) : new IndexOutOfRangeException();

            int leny = array.GetLength(1); // we know array is non null and that all dimensions are non-zero after the above check^
            int lenz = array.GetLength(2);
            int lenm = leny * lenz;
            int m;

            x = index / lenm;     // Fast DivRem (div-part 1)
            m = index - x * lenm; // Fast DivRem (mod-part 1)
            y = m / lenz;     // Fast DivRem (div-part 2)
            z = m - y * lenz; // Fast DivRem (mod-part 2)

            // IL doesn't have a DivRem instruction because IL doesn't support instructions with two return values.
            // Thus the above is the fastest way to DivRem in .Net (and it's the way .Net Core does it) because as of
            // yet the Jitter doesn't optimize when it sees % and / used together. (There is a petition for it though.)
        }

        /// <summary>
        /// Given a one-dimensional enumeration index, tries to calculate the corresponding x, y, and z item indexes.
        /// </summary><returns>
        /// False if the <paramref name="index"/> was out of bounds (or the backing array is absent); otherwise true.
        /// </returns>
        /// <inheritdoc cref="CalculateIndexes(int, out int, out int, out int)" select="remarks"/>
        /// <param name="index">An enumeration index to calculate item indexes for.</param>
        /// <param name="x">Resulting x index.</param>
        /// <param name="y">Resulting y index.</param>
        /// <param name="z">Resulting z index.</param>
        public bool TryCalculateIndexes(int index, out int x, out int y, out int z)
        {
            if (unchecked((uint)index >= (uint)Length))
            {
                x = y = z = 0;
                return false;
            }

            int leny = array.GetLength(1); // we know array is non null and that all dimensions are non-zero after the above check^
            int lenz = array.GetLength(2);
            int lenm = leny * lenz;
            int m;

            x = index / lenm;     // Fast DivRem (div-part 1)
            m = index - x * lenm; // Fast DivRem (mod-part 1)
            y = m / lenz;     // Fast DivRem (div-part 2)
            z = m - y * lenz; // Fast DivRem (mod-part 2)

            // IL doesn't have a DivRem instruction because IL doesn't support instructions with two return values.
            // Thus the above is the fastest way to DivRem in .Net (and it's the way .Net Core does it) because as of
            // yet the Jitter doesn't optimize when it sees % and / used together. (There is a petition for it though.)

            return true;
        }

        /// <summary>
        /// Creates a copy of the backing array.
        /// </summary><returns>
        /// A copy of the backing array, or null if the backing array is absent.
        /// </returns>
        public T[,,] CopyBacking()
        {
            if (array == null)
                return null;
            T[,,] copy = ArrayHelper.New(array);
            System.Array.Copy(array, copy, array.Length);
            return copy;
        }

        /* Method CopyTo absent because it's for single dimensional arrays only as part of the ICollection interface. */

        /// <summary>
        /// Indicates whether this instance and a specified object are considered equivalent.
        /// </summary><returns>
        /// True if <paramref name="obj"/> is a <see cref="ReadOnlyArrayR3{T}"/> of the same <typeparamref name="T"/> and
        /// their backing arrays are reference equal (or both absent), or if <paramref name="obj"/> is non-null and
        /// reference equal to the backing array of this instance; otherwise false.
        /// </returns>
        /// <param name="obj">Another object to compare against.</param>
        public override bool Equals(object obj)
        {
            return obj != null && (object.ReferenceEquals(this.array, obj) || obj is ReadOnlyArrayR3<T> && Equals((ReadOnlyArrayR3<T>)obj));
        }

        /// <summary>
        /// Indicates whether this and another instance are considered equivalent.
        /// </summary><returns>
        /// True if both instances share the same backing array, or if both backing arrays are absent; otherwise false.
        /// </returns>
        /// <param name="other">Another instance to compare against.</param>
        public bool Equals(ReadOnlyArrayR3<T> other)
        {
            return object.ReferenceEquals(this.array, other.array);
        }

        /// <summary>
        /// Indicates whether the backing array of this wrapper and a specified array are reference equal.
        /// </summary><returns>
        /// True if this wrapper has a backing array (backing array is not absent) and it is the same
        /// array as the specified <paramref name="other"/> argument; otherwise false.
        /// </returns>
        /// <param name="other">Array to compare against the backing array of this wrapper.</param>
        public bool Equals(Array other)
        {
            return array != null & object.ReferenceEquals(this.array, other);
        }

        /// <summary>
        /// Get an enumerator that can iterate through the collection.
        /// </summary><remarks>
        /// This is the method that a foreach statement would call. (The C# compiler employs duck-typing for foreach loops.)
        /// </remarks>
        public ArrayR3<T>.Enumerator GetEnumerator()
        {
            return new ArrayR3<T>.Enumerator(array);
        }
        
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return array == null ? Empty<T>.GetEnumerator() : new ArrayR3<T>.Enumerator(array);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return array?.GetEnumerator() ?? Empty<T>.GetEnumerator();
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return array == null ? 0 : array.GetHashCode();
        }

        /// <summary>
        /// Gets a 32-bit integer that represents the number of elements in the specified dimension of the Array.
        /// </summary><returns>
        /// A 32-bit integer that represents the number of elements in the specified dimension.
        /// </returns>
        /// <param name="dimension">A zero-based dimension of the Array whose length needs to be determined.</param>
        /// <exception cref="IndexOutOfRangeException">
        /// Thrown if <paramref name="dimension"/> is less than zero or greater or equal to the rank of the array. (Rank == 3).
        /// </exception>
        public int GetLength(int dimension)
        {
            if (array == null)
                throw new IndexOutOfRangeException(ERR.BACKING_ARRAY_ABSENT);

            return array.GetLength(dimension);
        }

        /// <summary>
        /// Gets the value at the specified position in the wrapped backing array.
        /// </summary>
        /// <param name="x">First index of the element to get.</param>
        /// <param name="y">Second index of the element to get.</param>
        /// <param name="z">Third index of the element to get.</param>
        /// <exception cref="IndexOutOfRangeException">
        /// Thrown if any of the indexes are less than zero, or greater than the upper bound for the corresponding dimension.
        /// </exception>
        public T GetValue(int x, int y, int z)
        {
            return this[x, y, z];
        }

        /// <summary>
        /// Gets the value at the specified enumeration index in the wrapped backing array.
        /// </summary>
        /// <inheritdoc cref="CalculateIndexes(int, out int, out int, out int)" select="remarks"/>
        /// <param name="index">An enumeration index. See <see cref="CalculateIndexes(int, out int, out int, out int)"/> for more info.</param>
        /// <exception cref="IndexOutOfRangeException">
        /// Thrown if <paramref name="index"/> is less than zero or greater than or equal to the <see cref="Length"/> of the array.
        /// (Note especially that if the backing array is absent (null) the <see cref="Length"/> property will be zero.)
        /// </exception>
        /// <seealso cref="CalculateIndexes(int, out int, out int, out int)"/>
        public T GetValue1D(int index)
        {
            int x, y, z;
            CalculateIndexes(index, out x, out y, out z);
            return array[x, y, z];
        }

        /// <summary>
        /// True if this instance was default initialized / constructed (backing array is absent).
        /// </summary><remarks>
        /// Default instances are still valid and behave as if the backing was an empty array.
        /// </remarks>
        public bool IsAbsent
        {
            get { return array == null; }
        }

        /// <summary>
        /// Gets a 32-bit integer that represents the total number of elements in all
        /// the dimensions of the wrapped backing array.
        /// </summary><remarks>
        /// Will be zero if this wrapper was default constructed / backing array is absent.
        /// </remarks>
        public int Length
        {
            get { return array == null ? 0 : array.Length; }
        }

        /// <summary>
        /// Gets a 32-bit integer that represents the number of elements in the first dimension of the Array.
        /// </summary><remarks>
        /// Will be zero if this wrapper was default constructed / backing array is absent.<br/>
        /// This is the same as calling <see cref="GetLength(int)"/> with 0 as argument for the dimension parameter.
        /// </remarks>
        public int LengthX
        {
            get { return array == null ? 0 : array.GetLength(0); }
        }

        /// <summary>
        /// Gets a 32-bit integer that represents the number of elements in the second dimension of the Array.
        /// </summary><remarks>
        /// Will be zero if this wrapper was default constructed / backing array is absent.<br/>
        /// This is the same as calling <see cref="GetLength(int)"/> with 1 as argument for the dimension parameter.
        /// </remarks>
        public int LengthY
        {
            get { return array == null ? 0 : array.GetLength(1); }
        }

        /// <summary>
        /// Gets a 32-bit integer that represents the number of elements in the second dimension of the Array.
        /// </summary><remarks>
        /// Will be zero if this wrapper was default constructed / backing array is absent.<br/>
        /// This is the same as calling <see cref="GetLength(int)"/> with 2 as argument for the dimension parameter.
        /// </remarks>
        public int LengthZ
        {
            get { return array == null ? 0 : array.GetLength(2); }
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return array == null ? "<ReadOnlyArrayR3:{}>" : ("<ReadOnlyArrayR3:" + array.ToString() + ">");
        }
    }
}
