using System;

namespace AZCL
{
    /// <summary>
    /// Copy methods extension for generic arrays T[].
    /// </summary>
    public static class ArrayCopyExtensions
    {
        /// <summary>
        /// Creates a copy of this array, or returns null if source is null.
        /// </summary>
        public static T[] Copy<T>(this T[] source)
        {
            if (source == null)
                return null;
            int length = source.Length;
            T[] copy = new T[length];
            System.Array.Copy(source, copy, length);
            return copy;
        }

        /// <summary>
        /// Copies a subarray of this array.
        /// </summary><remarks>
        /// The term subarray is derived from the term substring (and is exactly the same thing except the elements need not be characters).
        /// </remarks><returns>
        /// A new array equivalent to the subarray that begins at <paramref name="startIndex"/> in the source array,
        /// or an empty array if <paramref name="startIndex"/> is equals to the length of the source array.
        /// </returns>
        /// <param name="source">Source array.</param>
        /// <param name="startIndex">Inclusive start index of the source. Elements before this index will not be copied.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="startIndex"/> is less than zero or greater than the length of the source.
        /// </exception>
        public static T[] Copy<T>(this T[] source, int startIndex)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (unchecked((uint)startIndex > (uint)source.Length))
                throw new ArgumentOutOfRangeException(nameof(startIndex));

            int length = source.Length - startIndex;
            T[] copy = new T[length];
            if (length != 0)
                System.Array.Copy(source, startIndex, copy, 0, length);
            return copy;
        }

        /// <summary>
        /// Copies a subarray of this array.
        /// </summary><remarks>
        /// The term subarray is derived from the term substring (and is exactly the same thing except the elements need not be characters).
        /// </remarks><returns>
        /// A new array equivalent to the subarray containing the specified number of elements copied from the source array starting at the specified index,
        /// or an empty array if <paramref name="startIndex"/> is equal to the length of the source array and <paramref name="length"/> is zero.
        /// </returns>
        /// <param name="source">Source array.</param>
        /// <param name="startIndex">Inclusive start index of the source. Elements before this index will not be copied.</param>
        /// <param name="length">The number of elements in the subarray.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="startIndex"/> is less than zero or greater than the length of the source.<br/>
        /// Also thrown if <paramref name="length"/> is less than zero or greater than source.Length - <paramref name="startIndex"/>.
        /// </exception>
        public static T[] Copy<T>(this T[] source, int startIndex, int length)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (unchecked((uint)startIndex > (uint)source.Length))
                throw new ArgumentOutOfRangeException(nameof(startIndex));
            if (unchecked((uint)length > (uint)(source.Length - startIndex)))
                throw new ArgumentOutOfRangeException(nameof(length));

            T[] copy = new T[length];
            if (length != 0)
                System.Array.Copy(source, startIndex, copy, 0, length);
            return copy;
        }

        /// <summary>
        /// Copies a subarray of this array.
        /// </summary><remarks>
        /// The term subarray is derived from the term substring (and is exactly the same thing except the elements need not be characters).
        /// </remarks><returns>
        /// A new array equivalent to the subarray of length <paramref name="endIndexExclusive"/> - <paramref name="startIndex"/> elements
        /// which are copied from the source array starting at the <paramref name="startIndex"/> position.
        /// </returns>
        /// <param name="source">Source array.</param>
        /// <param name="startIndex">Inclusive start index of the source. Elements before this index will not be copied.</param>
        /// <param name="endIndexExclusive">Exclusive end index of the source. Elements from this index onward will not be copied.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="startIndex"/> is less than zero or greater than the length of the source.<br/>
        /// Also thrown if <paramref name="endIndexExclusive"/> is less than <paramref name="startIndex"/> or greater than source.Length.
        /// </exception>
        public static T[] CopyRange<T>(this T[] source, int startIndex, int endIndexExclusive)
        {
            int length;
            unchecked
            {
                if (source == null)
                    throw new ArgumentNullException(nameof(source));
                if ((uint)startIndex > (uint)source.Length)
                    throw new ArgumentOutOfRangeException(nameof(startIndex));
                length = endIndexExclusive - startIndex; // <-- can overflow iff endIndexExclusive is out of range, but no worries:
                if ((uint)length > (uint)(source.Length - startIndex)) // <-- this condition is always true on overflow.
                    throw new ArgumentOutOfRangeException(nameof(endIndexExclusive));
            }

            T[] copy = new T[length];
            if (length != 0)
                System.Array.Copy(source, startIndex, copy, 0, length);
            return copy;
        }

        /// <summary>
        /// Creates a copy of this array of the specified length.
        /// </summary><remarks>
        /// If <paramref name="length"/> of the copy is greater than that of the <paramref name="source"/>,
        /// then the new added elements of the copy will simply be default(<typeparamref name="T"/>).
        /// </remarks>
        /// <param name="source">Source array.</param>
        /// <param name="length">Desired length of the copy; can be same, less, or more than the length of the source.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="length"/> is less than zero.
        /// </exception>
        public static T[] CopyAndResize<T>(this T[] source, int length)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (length < 0)
                throw new ArgumentOutOfRangeException(nameof(length));

            T[] copy = new T[length];
            length = Math.Min(length, source.Length); // <-- avoid going out of range during copy
            System.Array.Copy(source, copy, length);
            return copy;
        }

        /*
        /// <summary>
        /// Creates a copy of this array of the specified length, excluding source elements before the specified start index.
        /// </summary><remarks>
        /// If <paramref name="startIndex"/> + <paramref name="length"/> is greater than the length of the <paramref name="source"/>,
        /// then the new added elements of the copy will simply be default(<typeparamref name="T"/>).
        /// </remarks>
        /// <param name="source">Source array.</param>
        /// <param name="length">Desired length of the copy; can be same, less, or more than the length of the source.</param>
        /// <param name="startIndex">Inclusive start index of the source. Elements before this index will not be copied.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="length"/> is less than zero,
        /// or <paramref name="startIndex"/> is larger than the length of the <paramref name="source"/>.
        /// </exception>
        public static T[] CopyAndResize<T>(this T[] source, int length, int startIndex)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (unchecked((uint)startIndex > (uint)source.Length))
                throw new ArgumentOutOfRangeException(nameof(startIndex));
            if (length < 0)
                throw new ArgumentOutOfRangeException(nameof(length));

            T[] copy = new T[length];
            length = Math.Min(length, source.Length - startIndex); // <-- avoid going out of range during copy
            System.Array.Copy(source, startIndex, copy, 0, length);
            return copy;
        }
        */

        /// <summary>
        /// Creates a copy of this array which includes one additional value inserted at the specified index.
        /// </summary><returns>
        /// A new array of length + 1 which is a copy of the source array with the <paramref name="value"/> argument
        /// inserted at the specified <paramref name="index"/>.
        /// </returns>
        /// <param name="source">Source array.</param>
        /// <param name="value">New element to insert into the copy.</param>
        /// <param name="index">Index of the value inserted into the copy.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="index"/> is less than zero or larger than the length of the <paramref name="source"/>.
        /// </exception>
        public static T[] CopyAndInsert<T>(this T[] source, T value, int index)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (unchecked((uint)index > (uint)source.Length))
                throw new ArgumentOutOfRangeException(nameof(index));

            T[] copy = new T[source.Length + 1];
            if (index != 0)
                System.Array.Copy(source, copy, index);
            copy[index] = value;
            if (index != source.Length)
                System.Array.Copy(source, index, copy, index + 1, source.Length - index);
            return copy;
        }

        /// <summary>
        /// Creates a copy of this array which includes one additional appended value.
        /// </summary><returns>
        /// A new array of length + 1 which is a copy of the source array with the specified <paramref name="value"/> appended.
        /// </returns>
        /// <param name="source">Source array.</param>
        /// <param name="value">New element to have appended to the end of the copy.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> is null.
        /// </exception>
        public static T[] CopyAndAppend<T>(this T[] source, T value)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            T[] copy = new T[source.Length + 1];
            System.Array.Copy(source, copy, source.Length);
            copy[source.Length] = value;
            return copy;
        }

        /// <summary>
        /// Creates a copy of this array which includes one additional appended value.
        /// </summary><remarks>
        /// This overload is for use with large structs values to avoid copying the value argument on method call.
        /// No changes are made to the value.
        /// </remarks><returns>
        /// A new array of length + 1 which is a copy of the source array with the specified <paramref name="value"/> appended.
        /// </returns>
        /// <param name="source">Source array.</param>
        /// <param name="value">New element to have appended to the end of the copy.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> is null.
        /// </exception>
        public static T[] CopyAndAppend<T>(this T[] source, ref T value) where T : struct
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            T[] copy = new T[source.Length + 1];
            System.Array.Copy(source, copy, source.Length);
            copy[source.Length] = value;
            return copy;
        }

        /// <summary>
        /// Creates a copy of this array that contains one less element by excludes the value at index <paramref name="index"/>.
        /// </summary><returns>
        /// A new array of length - 1 which is a copy of the source array except that the value found at the specified source <paramref name="index"/> has been excluded.
        /// </returns>
        /// <param name="source">Source array.</param>
        /// <param name="index">Index of element to exclude from the copy.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="index"/> is less than zero or greater than or equal to the length of the source.
        /// </exception>
        public static T[] CopyExcluding<T>(this T[] source, int index)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (unchecked((uint)index >= (uint)source.Length))
                throw new ArgumentOutOfRangeException(nameof(index));

            T[] copy = new T[source.Length - 1];
            if (index != 0)
                System.Array.Copy(source, copy, index);
            if (index != copy.Length)
                System.Array.Copy(source, index + 1, copy, index, copy.Length - index);
            return copy;
        }
    }
}
