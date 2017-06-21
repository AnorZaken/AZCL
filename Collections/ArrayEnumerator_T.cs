//#define READ_ONLY
// ArrayEnumerator and ArrayEnumeratorReadOnly are near identical, so for the sake of less developer errors it's the same source with a toggled define.

using System;
using System.Collections.Generic;

namespace AZCL.Collections
{
#if READ_ONLY
    /// <summary>
    /// An read-only array enumerator implemented as a struct, with extended functionality such as indexer, moving backwards, index and length, and optional source range restriction.
    /// </summary><remarks>
    /// Only the array and its elements are read-only, i.e. if <typeparamref name="T"/> is a reference type then the references stored in the array can't be changed,
    /// but there is of course nothing preventing a call to any mutating method exposed on the referenced objects.
    /// <para/>
    /// Instances of this struct are valid even when default initialized. See <see cref="ArrayEnumeratorReadOnly{T}.IsAbsent"/>.
    /// <para/>
    /// The size of the struct is one reference (the source array) + three Int32 values.
    /// </remarks>
    /// <typeparam name="T">Array element type.</typeparam>
    public struct ArrayEnumeratorReadOnly<T>
#else
    /// <summary>
    /// An array enumerator implemented as a struct, with extended functionality such as moving backwards, retrieving index and length, and optional source range restriction.
    /// </summary><remarks>
    /// Instances of this struct are valid even when default initialized. See <see cref="ArrayEnumerator{T}.IsAbsent"/>.
    /// <para/>
    /// The size of the struct is one reference (the source array) + three Int32 values.
    /// </remarks>
    /// <typeparam name="T">Array element type.</typeparam>
    public struct ArrayEnumerator<T>
#endif
        : IEnumerator<T>
    {
        // cast operators:
#if READ_ONLY
        /// <summary>
        /// Creates an <see cref="ArrayEnumeratorReadOnly{T}"/> from an array.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="array"/> is null.
        /// </exception>
        public static implicit operator ArrayEnumeratorReadOnly<T>(T[] array)
            => new ArrayEnumeratorReadOnly<T>(array);

        /// <summary>
        /// Creates an <see cref="ArrayEnumeratorReadOnly{T}"/> from a <see cref="ReadOnlyArray{T}"/>.
        /// </summary><remarks>
        /// This operator does not throw, even if the <see cref="ReadOnlyArray{T}"/> argument was default initialized.
        /// </remarks>
        public static implicit operator ArrayEnumeratorReadOnly<T>(ReadOnlyArray<T> array)
            => new ArrayEnumeratorReadOnly<T>(array);
#else
        /// <summary>
        /// Creates an <see cref="ArrayEnumerator{T}"/> from an array.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="array"/> is null.
        /// </exception>
        public static implicit operator ArrayEnumerator<T>(T[] array)
            => new ArrayEnumerator<T>(array);

        /// <summary>
        /// Creates an <see cref="ArrayEnumeratorReadOnly{T}"/> from an <see cref="ArrayEnumerator{T}"/>.
        /// </summary><remarks>
        /// This cast retains the current position of the <paramref name="mutable"/> enumerator (such as it is at cast time)!
        /// </remarks>
        public static implicit operator ArrayEnumeratorReadOnly<T>(ArrayEnumerator<T> mutable)
            => new ArrayEnumeratorReadOnly<T>(mutable.array, mutable.startInclusive, mutable.endInclusive, mutable.index);
#endif

        // The T[] ctors are identical except for "a ArrayEnumeratorReadOnly" vs "an ArrayEnumerator" in their summaries,
        // however ArrayEnumeratorReadOnly has an additional set of (public) ctors for construction from ReadOnlyArrays,
        // as well as one additional (internal) ctor used for some casts, including the one above.
        #region --- Ctor ---
#if READ_ONLY
        /// <summary>
        /// Creates a <see cref="ArrayEnumeratorReadOnly{T}"/> from an array.
        /// </summary>
        /// <param name="array">Array to create enumerator for.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="array"/> is null.
        /// </exception>
        public ArrayEnumeratorReadOnly(T[] array)
            : this(array, 0, array == null ? 0 : array.Length - 1)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
        }

        /// <summary>
        /// Creates a <see cref="ArrayEnumeratorReadOnly{T}"/> covering the early part of an array.
        /// </summary>
        /// <param name="array">Array to create enumerator for.</param>
        /// <param name="length">Number of elements the enumerator should cover.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="array"/> is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="length"/> is less than zero or larger than the length of the array.
        /// </exception>
        public ArrayEnumeratorReadOnly(T[] array, int length)
            : this(array, 0, length - 1)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            if (unchecked((uint)length > (uint)array.Length))
                throw new ArgumentOutOfRangeException(nameof(length));
        }

        /// <summary>
        /// Creates a <see cref="ArrayEnumeratorReadOnly{T}"/> covering the latter part of an array.
        /// </summary>
        /// <param name="array">Array to create enumerator for.</param>
        /// <param name="start">Index from which the iteration should begin (inclusive).</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="array"/> is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="start"/> is less than zero or larger than the length of the array.
        /// </exception>
        public ArrayEnumeratorReadOnly(int start, T[] array)
            : this(array, start, array == null ? 0 : array.Length - 1)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            if (unchecked((uint)start > (uint)array.Length))
                throw new ArgumentOutOfRangeException(nameof(start));
        }

        /// <summary>
        /// Creates a <see cref="ArrayEnumeratorReadOnly{T}"/> covering a specified range of an array.
        /// </summary>
        /// <param name="array">Array to create enumerator for.</param>
        /// <param name="start">Index from which the iteration should begin (inclusive).</param>
        /// <param name="length">Number of elements the enumerator should cover.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="array"/> is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if either <paramref name="start"/>, <paramref name="length"/> or both, is less than zero or larger than the length of the array.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="start"/> + <paramref name="length"/> exceeds the length of the array.
        /// </exception>
        public ArrayEnumeratorReadOnly(int start, T[] array, int length)
            : this(array, start, start + length - 1)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            if ((start | length) < 0 | unchecked((uint)(start + length) > (uint)array.Length))
            {
                if (unchecked((uint)start > (uint)array.Length))
                    throw new ArgumentOutOfRangeException(nameof(start));
                if (unchecked((uint)length > (uint)array.Length))
                    throw new ArgumentOutOfRangeException(nameof(length));

                throw new ArgumentException(ERR.START_PLUS_LENGTH);
            }
        }

        /// <summary>
        /// Creates a <see cref="ArrayEnumeratorReadOnly{T}"/> from a <see cref="ReadOnlyArray{T}"/>.
        /// </summary>
        /// <param name="array">Array to create enumerator for.</param>
        public ArrayEnumeratorReadOnly(ReadOnlyArray<T> array)
            : this(array.Array, 0, array.Length - 1)
        { }

        /// <summary>
        /// Creates a <see cref="ArrayEnumeratorReadOnly{T}"/> covering the early part of a <see cref="ReadOnlyArray{T}"/>.
        /// </summary>
        /// <param name="array">Array to create enumerator for.</param>
        /// <param name="length">Number of elements the enumerator should cover.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="length"/> is less than zero or larger than the length of the array.
        /// </exception>
        public ArrayEnumeratorReadOnly(ReadOnlyArray<T> array, int length)
            : this(array.Array, 0, length - 1)
        {
            if (unchecked((uint)length > (uint)array.Length))
                throw new ArgumentOutOfRangeException(nameof(length));
        }

        /// <summary>
        /// Creates a <see cref="ArrayEnumeratorReadOnly{T}"/> covering the latter part of a <see cref="ReadOnlyArray{T}"/>.
        /// </summary>
        /// <param name="array">Array to create enumerator for.</param>
        /// <param name="start">Index from which the iteration should begin (inclusive).</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="start"/> is less than zero or larger than the length of the array.
        /// </exception>
        public ArrayEnumeratorReadOnly(int start, ReadOnlyArray<T> array)
            : this(array.Array, start, array.Length - 1)
        {
            if (unchecked((uint)start > (uint)array.Length))
                throw new ArgumentOutOfRangeException(nameof(start));
        }

        /// <summary>
        /// Creates a <see cref="ArrayEnumeratorReadOnly{T}"/> covering a specified range of a <see cref="ReadOnlyArray{T}"/>.
        /// </summary>
        /// <param name="array">Array to create enumerator for.</param>
        /// <param name="start">Index from which the iteration should begin (inclusive).</param>
        /// <param name="length">Number of elements the enumerator should cover.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if either <paramref name="start"/>, <paramref name="length"/> or both, is less than zero or larger than the length of the array.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="start"/> + <paramref name="length"/> exceeds the length of the array.
        /// </exception>
        public ArrayEnumeratorReadOnly(int start, ReadOnlyArray<T> array, int length)
            : this(array.Array, start, start + length - 1)
        {
            if ((start | length) < 0 | unchecked((uint)(start + length) > (uint)array.Length))
            {
                if (unchecked((uint)start > (uint)array.Length))
                    throw new ArgumentOutOfRangeException(nameof(start));
                if (unchecked((uint)length > (uint)array.Length))
                    throw new ArgumentOutOfRangeException(nameof(length));

                throw new ArgumentException(ERR.START_PLUS_LENGTH);
            }
        }

        private ArrayEnumeratorReadOnly(T[] array, int startInclusive, int endInclusive)
        {
            this.array = array;
            this.startInclusive = startInclusive;
            this.endInclusive = endInclusive;
            this.index = -1;
        }

        // Ctor used to preserve the current position when casting from ArrayEnumerator or ReadOnlyArray.Enumerator to ArrayEnumeratorReadOnly.
        internal ArrayEnumeratorReadOnly(T[] array, int startInclusive, int endInclusive, int index)
        {
            this.array = array;
            this.startInclusive = startInclusive;
            this.endInclusive = endInclusive;
            this.index = index;
        }
#else
        /// <summary>
        /// Creates an <see cref="ArrayEnumerator{T}"/> from an array.
        /// </summary>
        /// <param name="array">Array to create enumerator for.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="array"/> is null.
        /// </exception>
        public ArrayEnumerator(T[] array)
            : this(array, 0, array == null ? 0 : array.Length - 1)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
        }

        /// <summary>
        /// Creates an <see cref="ArrayEnumerator{T}"/> covering the early part of an array.
        /// </summary>
        /// <param name="array">Array to create enumerator for.</param>
        /// <param name="length">Number of elements the enumerator should cover.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="array"/> is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="length"/> is less than zero or larger than the length of the array.
        /// </exception>
        public ArrayEnumerator(T[] array, int length)
            : this(array, 0, length - 1)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            if (unchecked((uint)length > (uint)array.Length))
                throw new ArgumentOutOfRangeException(nameof(length));
        }

        /// <summary>
        /// Creates an <see cref="ArrayEnumerator{T}"/> covering the latter part of an array.
        /// </summary>
        /// <param name="array">Array to create enumerator for.</param>
        /// <param name="start">Index from which the iteration should begin (inclusive).</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="array"/> is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="start"/> is less than zero or larger than the length of the array.
        /// </exception>
        public ArrayEnumerator(int start, T[] array)
            : this(array, start, array == null ? 0 : array.Length - 1)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            if (unchecked((uint)start > (uint)array.Length))
                throw new ArgumentOutOfRangeException(nameof(start));
        }

        /// <summary>
        /// Creates an <see cref="ArrayEnumerator{T}"/> covering a specified range of an array.
        /// </summary>
        /// <param name="array">Array to create enumerator for.</param>
        /// <param name="start">Index from which the iteration should begin (inclusive).</param>
        /// <param name="length">Number of elements the enumerator should cover.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="array"/> is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if either <paramref name="start"/>, <paramref name="length"/> or both, is less than zero or larger than the length of the array.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="start"/> + <paramref name="length"/> exceeds the length of the array.
        /// </exception>
        public ArrayEnumerator(int start, T[] array, int length)
            : this(array, start, start + length - 1)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            if ((start | length) < 0 | unchecked((uint)(start + length) > (uint)array.Length))
            {
                if (unchecked((uint)start > (uint)array.Length))
                    throw new ArgumentOutOfRangeException(nameof(start));
                if (unchecked((uint)length > (uint)array.Length))
                    throw new ArgumentOutOfRangeException(nameof(length));

                throw new ArgumentException(ERR.START_PLUS_LENGTH);
            }
        }

        private ArrayEnumerator(T[] array, int startInclusive, int endInclusive)
        {
            this.array = array;
            this.startInclusive = startInclusive;
            this.endInclusive = endInclusive;
            this.index = -1;
        }
#endif
        #endregion

        private readonly T[] array;
        private readonly int startInclusive, endInclusive;
        private int index;

#if !READ_ONLY
        /// <summary>
        /// Reference to the source array (or null if default constructed).
        /// </summary>
        public T[] Source
            => array;

        /// <summary>
        /// The current index relative to the source, i.e. <see cref="StartIndex"/> + <see cref="CurrentIndex"/>.
        /// </summary><remarks>
        /// This value will be -1 if the enumerator was default constructed.
        /// </remarks>
        public int CurrentSourceIndex
            => array == null ? -1 : index;

        /// <summary>
        /// The enumerators start index relative to the source.
        /// </summary><remarks>
        /// This is a readonly value. It will be zero by default unless a different value was specified in the constructor.
        /// </remarks>
        public int StartIndex
            => startInclusive;
#endif
        /// <summary>
        /// The current element index of the enumerator, relative to the source start index (optionally specified in the constructor).
        /// </summary><remarks>
        /// This will be an unspecified negative if the current position of the enumerator is before the first or after the last element in its range.
        /// The same is true if the source is null.
        /// </remarks>
        public int CurrentIndex
            => array == null ? -1 : index - startInclusive;

        /// <summary>
        /// True if this enumerator instance was default initialized / constructed (source is null).
        /// </summary><remarks>
        /// Default instances are still valid and as is thus expected they behave as empty enumerators.
        /// </remarks>
        public bool IsAbsent
            => array == null;

        /// <summary>
        /// The length of the element range of the enumerator.
        /// </summary><remarks>
        /// This is always less than or equal to the length of the source array, or zero if the source array is null.
        /// </remarks>
        public int Length
            => array == null ? 0 : LengthFast;

        private int LengthFast
            => endInclusive - startInclusive + 1;

        /// <summary>
        /// Creates an array copy of the source elements covered by the enumerators range.
        /// </summary>
        /// <returns>An array copy of the source elements covered by the enumerators range, or null if the source is null.</returns>
        public T[] Copy()
        {
            if (array == null)
                return null;
            T[] copy = new T[LengthFast];
            Array.Copy(array, startInclusive, copy, 0, LengthFast);
            return copy;
        }

#if !READ_ONLY
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
#endif

        /// <summary>
        /// Element indexer relative to the enumerator's source start index.
        /// </summary>
        /// <param name="i">A positive index less than <see cref="Length"/>.</param>
        /// <returns>The i-th element in the range of elements covered by this enumerator.</returns>
        /// <exception cref="IndexOutOfRangeException">
        /// Thrown if the index is negative or greater than or equal to <see cref="Length"/>.
        /// </exception>
        public T this[int i]
        {
            get
            {
                int a = startInclusive + i;
                if (i < 0 | unchecked((uint)a > (uint)endInclusive))
                    throw new IndexOutOfRangeException();
                return array[a];
            }
#if !READ_ONLY
            set
            {
                int a = startInclusive + i;
                if (i < 0 | unchecked((uint)a > (uint)endInclusive))
                    throw new IndexOutOfRangeException();
                array[a] = value;
            }
#endif
        }

#if READ_ONLY
        /// <summary>
        /// Gets the element in the source array at the current position of the enumerator.
        /// </summary>
        public T Current
        {
            get
            {
                if (index == -1 | array == null)
                    return default(T);
                return array[index];
            }
        }
#else
        /// <summary>
        /// Gets or sets the element in the source array at the current position of the enumerator.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this property is used as a setter and the enumerator is positioned before the first or after the last element in its source range.
        /// Also thrown if this property is used as a setter and the source array is null.
        /// </exception>
        public T Current
        {
            get
            {
                if (index == -1 | array == null)
                    return default(T);
                return array[index];
            }
            set
            {
                if (array == null)
                    throw new InvalidOperationException(ERR.BACKING_ARRAY_ABSENT);
                if (index == -1)
                    throw new InvalidOperationException(ERR.CURRENT_INVALID);
                array[index] = value;
            }
        }
#endif

        object System.Collections.IEnumerator.Current
        {
            get
            {
                if (array == null)
                    throw new InvalidOperationException(ERR.BACKING_ARRAY_ABSENT);
                if (index == -1)
                    throw new InvalidOperationException(ERR.CURRENT_INVALID);

                return array[index];
            }
        }

        /// <summary>
        /// Advances the enumerator to the next element of the source (within the enumerators range).
        /// </summary>
        /// <returns>
        /// True if the enumerator was successfully advanced to the next element;
        /// False if the enumerator has passed the end of its range.
        /// </returns>
        public bool MoveNext()
        {
            if (index < endInclusive)
            {
                if (index == -1) //this happens iff this is the first method called (since construction/reset)
                {
                    if (LengthFast == 0)
                        return false;
                    index = startInclusive;
                    return true;
                }
                index = index + 1;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Retreats the enumerator to the previous element of the source (within the enumerators range).
        /// </summary><remarks>
        /// Calling MovePrev() before any call to <see cref="MoveNext()"/>, since the instantiation of the enumerator
        /// or after a <see cref="Reset()"/> call, will move the enumerator to the last element in its range.
        /// </remarks>
        /// <returns>
        /// True if the enumerator was successfully retreated to the previous element;
        /// False if the enumerator has passed the beginning of its range.
        /// </returns>
        public bool MovePrev()
        {
            if (index > startInclusive)
            {
                index = index - 1;
                return true;
            }
            else if (index == -1) //this happens iff this is the first method called (since construction/reset)
            {
                if (LengthFast == 0)
                    return false;
                index = endInclusive;
                return true;
            }
            return false;
        }

        /// <inheritdoc/>
        public void Reset()
        {
            index = -1;
        }

        void IDisposable.Dispose()
        { }
    }
}
