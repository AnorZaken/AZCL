using System;

namespace AZCL
{
    /// <summary>
    /// Extends [generic] array types with various methods.
    /// </summary>
    public static partial class ArrayExtensions
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
        /// Creates a copy of this array, or returns null if source is null.
        /// </summary>
        public static T[,] Copy<T>(this T[,] source)
        {
            if (source == null)
                return null;

            var copy = ArrayHelper.New(source);
            System.Array.Copy(source, copy, source.Length);
            return copy;
        }

        /// <summary>
        /// Creates a copy of this array, or returns null if source is null.
        /// </summary>
        public static T[,,] Copy<T>(this T[,,] source)
        {
            if (source == null)
                return null;

            var copy = ArrayHelper.New(source);
            System.Array.Copy(source, copy, source.Length);
            return copy;
        }

        /// <summary>
        /// Creates a copy of this jagged array of the specified depth, or returns null if source is null.
        /// </summary><remarks>
        /// Use the <paramref name="copyDepth"/> parameter to control the "shallowness" / depth of copying to perform.
        /// <br/>
        /// A value of zero means the inner arrays of the returned array will simply be shallow copied references to the
        /// inner arrays of the <paramref name="source"/>, i.e. Object.ReferenceEquals(result[x], source[x]) == true.
        /// <br/>
        /// A value of one means that one level of inner arrays will be copied, i.e. Object.ReferenceEquals(result[x], source[x]) == false.
        /// <br/>
        /// The default value for <paramref name="copyDepth"/> is always to copy all inner arrays.
        /// Increasing <paramref name="copyDepth"/> further in an attempt to turn this copy method into a cloning method is not supported.
        /// </remarks><returns>
        /// A new jagged array that is a copy of the jagged <paramref name="source"/> array in the following way:<br/>
        /// If <paramref name="copyDepth"/> equals zero then all inner arrays of the returned array are references to inner arrays in the <paramref name="source"/> array.
        /// If <paramref name="copyDepth"/> equals one then all inner arrays of the returned array will be copies of the inner arrays of the <paramref name="source"/> array.
        /// </returns>
        /// <param name="source">Source array.</param>
        /// <param name="copyDepth">Determines the depth of copying. Can not exceed the default value, which means to copy all inner arrays.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="copyDepth"/> is less than zero or greater than or equal to the number of (jagged) dimensions of the <paramref name="source"/> array.
        /// </exception>
        public static T[][] Copy<T>(this T[][] source, int copyDepth = 1)
        {
            if (source == null)
                return null;
            if (unchecked((uint)copyDepth > 1u))
                throw new ArgumentOutOfRangeException(nameof(copyDepth));

            int len = source.Length;
            var copy = new T[len][];
            if (copyDepth == 0)
            {
                System.Array.Copy(source, copy, len);
            }
            else
            {
                for (int x = 0; x < len; ++x)
                    copy[x] = source[x].Copy();
            }
            return copy;
        }

        /// <summary>
        /// Creates a copy of this jagged array of the specified depth, or returns null if source is null.
        /// </summary><remarks>
        /// Use the <paramref name="copyDepth"/> parameter to control the "shallowness" / depth of copying to perform.
        /// <br/>
        /// A value of zero means the inner arrays of the returned array will simply be shallow copied references to the
        /// inner arrays of the <paramref name="source"/>, i.e. Object.ReferenceEquals(result[x], source[x]) == true.
        /// <br/>
        /// A value of one means that one level of inner arrays will be copied, i.e. Object.ReferenceEquals(result[x], source[x]) == false,
        /// however beyond that the copying is shallow, i.e. Object.ReferenceEquals(result[x][y], source[x][y]) == true.
        /// <br/>
        /// A value of two means that two levels of inner arrays will be copied, i.e. Object.ReferenceEquals(result[x], source[x]) == false
        /// and Object.ReferenceEquals(result[x][y], source[x][y]) == false.
        /// <br/>
        /// The default value for <paramref name="copyDepth"/> is always to copy all inner arrays.
        /// Increasing <paramref name="copyDepth"/> further in an attempt to turn this copy method into a cloning method is not supported.
        /// </remarks><returns>
        /// A new jagged array that is a copy of the jagged <paramref name="source"/> array in the following way:<br/>
        /// If <paramref name="copyDepth"/> equals zero then all inner arrays of the returned array are references to inner arrays in the <paramref name="source"/> array.<br/>
        /// If <paramref name="copyDepth"/> equals one then all first level inner arrays of the returned array will be copies of the first level inner arrays of the <paramref name="source"/> array,
        /// however those copies will contain references to second level arrays in the <paramref name="source"/> array.<br/>
        /// If <paramref name="copyDepth"/> equals two then all inner arrays of the returned array will be copies of the inner arrays of the <paramref name="source"/> array (all levels).
        /// </returns>
        /// <param name="source">Source array.</param>
        /// <param name="copyDepth">Determines the depth of copying. Can not exceed the default value, which means to copy all inner arrays.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="copyDepth"/> is less than zero or greater than or equal to the number of (jagged) dimensions of the <paramref name="source"/> array.
        /// </exception>
        public static T[][][] Copy<T>(this T[][][] source, int copyDepth = 2)
        {
            if (source == null)
                return null;
            if (unchecked((uint)copyDepth > 2u))
                throw new ArgumentOutOfRangeException(nameof(copyDepth));

            int len = source.Length;
            var copy = new T[len][][];
            if (copyDepth == 0)
            {
                System.Array.Copy(source, copy, len);
            }
            else
            {
                for (int x = 0; x < len; ++x)
                    copy[x] = Copy(source[x], copyDepth - 1);
            }
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
        public static T[] CopyRange<T>(this T[] source, int startIndex)
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
        public static T[] CopyRange<T>(this T[] source, int startIndex, int length)
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

        /*
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
        */

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
        /// or <paramref name="startIndex"/> is greater than the length of the <paramref name="source"/>.
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
        /// Thrown if <paramref name="index"/> is less than zero or greater than the length of the <paramref name="source"/>.
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
        /// Creates a copy of this array which includes one additional value inserted at the specified index.
        /// </summary><remarks>
        /// This overload is for use with large structs values to avoid copying the value argument on method call.
        /// No changes are made to the value.
        /// </remarks><returns>
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
        /// Thrown if <paramref name="index"/> is less than zero or greater than the length of the <paramref name="source"/>.
        /// </exception>
        public static T[] CopyAndInsert<T>(this T[] source, ref T value, int index) where T : struct
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
        /// Creates a copy of this jagged array which includes one additional inner array inserted at the specified index.
        /// </summary><remarks>
        /// Use the <paramref name="copyDepth"/> parameter to control the "shallowness" / depth of copying to perform.
        /// <br/>
        /// A value of zero means the inner arrays of the returned array will simply be shallow copied references to the inner
        /// arrays of the source, i.e. Object.ReferenceEquals(result[x], source[x]) == true for x &lt; <paramref name="index"/>
        /// and Object.ReferenceEquals(result[x + 1], source[x]) == true for x &gt;= <paramref name="index"/>.
        /// <br/>
        /// A value of one means that one level of inner arrays will be copied, i.e. Object.ReferenceEquals(result[x], source[x]) == false.
        /// <br/>
        /// The default value for <paramref name="copyDepth"/> is always to copy all inner arrays.
        /// Increasing <paramref name="copyDepth"/> further in an attempt to turn this copy method into a cloning method is not supported.
        /// </remarks><returns>
        /// A new jagged array of length + 1 with the <paramref name="arrayToInsert"/> inserted at the specified <paramref name="index"/>.
        /// As for all other inner arrays of the returned array one of the following applies:<br/>
        /// If <paramref name="copyDepth"/> equals zero then they are references to inner arrays in the <paramref name="source"/> array.<br/>
        /// If <paramref name="copyDepth"/> equals one then they will be copies of the inner arrays of the <paramref name="source"/> array.
        /// </returns>
        /// <param name="source">Source array.</param>
        /// <param name="arrayToInsert">New inner array to insert into the copy.</param>
        /// <param name="index">(Outer-) Index where the specified inner array will be inserted into the copy.</param>
        /// <param name="copyDepth">Determines the depth of copying. Can not exceed the default value, which is to copy all inner arrays.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="index"/> is less than zero or greater than the length of the (outer) <paramref name="source"/> array,
        /// or if <paramref name="copyDepth"/> is less than zero or greater than or equal to the number of (jagged) dimensions of the <paramref name="source"/> array.
        /// </exception>
        public static T[][] CopyAndInsert<T>(this T[][] source, T[] arrayToInsert, int index, int copyDepth = 1)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (unchecked((uint)index > (uint)source.Length))
                throw new ArgumentOutOfRangeException(nameof(index));
            if (unchecked((uint)copyDepth > 1u))
                throw new ArgumentOutOfRangeException(nameof(copyDepth));

            T[][] copy = new T[source.Length + 1][];
            if (copyDepth == 0)
            {
                if (index != 0)
                    System.Array.Copy(source, copy, index);
                copy[index] = arrayToInsert;
                if (index != source.Length)
                    System.Array.Copy(source, index, copy, index + 1, source.Length - index);
            }
            else // depth: 1
            {
                for (int i = 0; i < index; ++i)
                    copy[i] = source[i].Copy();
                copy[index] = arrayToInsert;
                for (int i = index; i < source.Length; ++i)
                    copy[i + 1] = source[i].Copy();
            }
            return copy;
        }

        /// <summary>
        /// Creates a copy of this jagged array which includes one additional inner array inserted at the specified index.
        /// </summary><remarks>
        /// Use the <paramref name="copyDepth"/> parameter to control the "shallowness" / depth of copying to perform.
        /// <br/>
        /// A value of zero means the inner arrays of the returned array will simply be shallow copied references to the inner
        /// arrays of the source, i.e. Object.ReferenceEquals(result[x], source[x]) == true for x &lt; <paramref name="index"/>
        /// and Object.ReferenceEquals(result[x + 1], source[x]) == true for x &gt;= <paramref name="index"/>.
        /// <br/>
        /// A value of one means that one level of inner arrays will be copied, i.e. Object.ReferenceEquals(result[x], source[x]) == false,
        /// however beyond that the copying is shallow, i.e. Object.ReferenceEquals(result[x][y], source[x][y]) == true for x &lt; index
        /// and Object.ReferenceEquals(result[x + 1][y], source[x][y]) == true for x &gt;= <paramref name="index"/>.
        /// <br/>
        /// A value of two means that two levels of inner arrays will be copied, i.e. Object.ReferenceEquals(result[x], source[x]) == false
        /// and Object.ReferenceEquals(result[x][y], source[x][y]) == false.
        /// <br/>
        /// The default value for <paramref name="copyDepth"/> is always to copy all inner arrays.
        /// Increasing <paramref name="copyDepth"/> further in an attempt to turn this copy method into a cloning method is not supported.
        /// </remarks><returns>
        /// A new jagged array of length + 1 with the <paramref name="arrayToInsert"/> inserted at the specified <paramref name="index"/>.<br/>
        /// If <paramref name="copyDepth"/> equals zero then all other inner arrays of the returned array are references to inner arrays in the <paramref name="source"/> array.<br/>
        /// If <paramref name="copyDepth"/> equals one then all other first level inner arrays of the returned array will be copies of the first level inner arrays of the <paramref name="source"/> array,
        /// however those copies will contain references to second level inner arrays in the <paramref name="source"/> array.<br/>
        /// If <paramref name="copyDepth"/> equals two then all other inner arrays of the returned array will be copies of the inner arrays of the <paramref name="source"/> array (all levels).
        /// </returns>
        /// <param name="source">Source array.</param>
        /// <param name="arrayToInsert">New inner array to insert into the copy.</param>
        /// <param name="index">(Outer-) Index where the specified inner array will be inserted into the copy.</param>
        /// <param name="copyDepth">Determines the depth of copying. Can not exceed the default value, which is to copy all inner arrays.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="index"/> is less than zero or greater than the length of the (outer) <paramref name="source"/> array,
        /// or if <paramref name="copyDepth"/> is less than zero or greater than or equal to the number of (jagged) dimensions of the <paramref name="source"/> array.
        /// </exception>
        public static T[][][] CopyAndInsert<T>(this T[][][] source, T[][] arrayToInsert, int index, int copyDepth = 2)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (unchecked((uint)index > (uint)source.Length))
                throw new ArgumentOutOfRangeException(nameof(index));
            if (unchecked((uint)copyDepth > 2u))
                throw new ArgumentOutOfRangeException(nameof(copyDepth));

            T[][][] copy = new T[source.Length + 1][][];
            if (copyDepth == 0)
            {
                if (index != 0)
                    System.Array.Copy(source, copy, index);
                copy[index] = arrayToInsert;
                if (index != source.Length)
                    System.Array.Copy(source, index, copy, index + 1, source.Length - index);
            }
            else
            {
                for (int i = 0; i < index; ++i)
                    copy[i] = source[i].Copy(copyDepth - 1);
                copy[index] = arrayToInsert;
                for (int i = index; i < source.Length; ++i)
                    copy[i + 1] = source[i].Copy(copyDepth - 1);
            }
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
        /// Creates a copy of this jagged array which includes one additional appended inner array.
        /// </summary><remarks>
        /// Use the <paramref name="copyDepth"/> parameter to control the "shallowness" / depth of copying to perform.
        /// <br/>
        /// A value of zero means the inner arrays of the returned array will simply be shallow copied references to the inner
        /// arrays of the source, i.e. Object.ReferenceEquals(result[x], source[x]) == true.
        /// <br/>
        /// A value of one means that one level of inner arrays will be copied, i.e. Object.ReferenceEquals(result[x], source[x]) == false.
        /// <br/>
        /// The default value for <paramref name="copyDepth"/> is always to copy all inner arrays.
        /// Increasing <paramref name="copyDepth"/> further in an attempt to turn this copy method into a cloning method is not supported.
        /// </remarks><returns>
        /// A new jagged array of length + 1 with the <paramref name="arrayToAppend"/> appended at the end.
        /// As for all other inner arrays of the returned array one of the following applies:<br/>
        /// If <paramref name="copyDepth"/> equals zero then they are references to inner arrays in the <paramref name="source"/> array.<br/>
        /// If <paramref name="copyDepth"/> equals one then they will be copies of the inner arrays of the <paramref name="source"/> array.
        /// </returns>
        /// <param name="source">Source array.</param>
        /// <param name="arrayToAppend">New inner array to have appended to the end of the copy.</param>
        /// <param name="copyDepth">Determines the depth of copying. Can not exceed the default value, which is to copy all inner arrays.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="copyDepth"/> is less than zero or greater than or equal to the number of (jagged) dimensions of the <paramref name="source"/> array.
        /// </exception>
        public static T[][] CopyAndAppend<T>(this T[][] source, T[] arrayToAppend, int copyDepth = 1)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return CopyAndInsert(source, arrayToAppend, source.Length, copyDepth);
        }

        /// <summary>
        /// Creates a copy of this jagged array which includes one additional appended inner array.
        /// </summary><remarks>
        /// Use the <paramref name="copyDepth"/> parameter to control the "shallowness" / depth of copying to perform.
        /// <br/>
        /// A value of zero means the inner arrays of the returned array will simply be shallow copied references to the inner
        /// arrays of the source, i.e. Object.ReferenceEquals(result[x], source[x]) == true.
        /// <br/>
        /// A value of one means that one level of inner arrays will be copied, i.e. Object.ReferenceEquals(result[x], source[x]) == false,
        /// however beyond that the copying is shallow, i.e. Object.ReferenceEquals(result[x][y], source[x][y]) == true.
        /// <br/>
        /// A value of two means that two levels of inner arrays will be copied, i.e. Object.ReferenceEquals(result[x], source[x]) == false
        /// and Object.ReferenceEquals(result[x][y], source[x][y]) == false.
        /// <br/>
        /// The default value for <paramref name="copyDepth"/> is always to copy all inner arrays.
        /// Increasing <paramref name="copyDepth"/> further in an attempt to turn this copy method into a cloning method is not supported.
        /// </remarks><returns>
        /// A new jagged array of length + 1 with the <paramref name="arrayToAppend"/> appended at the end.
        /// As for other inner arrays of the returned array one of the following applies:<br/>
        /// If <paramref name="copyDepth"/> equals zero then they are references to inner arrays in the <paramref name="source"/> array.<br/>
        /// If <paramref name="copyDepth"/> equals one then the first level of inner arrays will be copies of the first level inner arrays of the <paramref name="source"/> array,
        /// however those copies will contain references to second level inner arrays in the <paramref name="source"/> array.<br/>
        /// If <paramref name="copyDepth"/> equals two then all inner arrays of the returned array will be copies of the inner arrays of the <paramref name="source"/> array (all levels).
        /// </returns>
        /// <param name="source">Source array.</param>
        /// <param name="arrayToAppend">New inner array to have appended to the end of the copy.</param>
        /// <param name="copyDepth">Determines the depth of copying. Can not exceed the default value, which is to copy all inner arrays.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="copyDepth"/> is less than zero or greater than or equal to the number of (jagged) dimensions of the <paramref name="source"/> array.
        /// </exception>
        public static T[][][] CopyAndAppend<T>(this T[][][] source, T[][] arrayToAppend, int copyDepth = 2)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return CopyAndInsert(source, arrayToAppend, source.Length, copyDepth);
        }

        /// <summary>
        /// Creates a copy of this array that contains one less element by excluding the value at index <paramref name="index"/>.
        /// </summary><returns>
        /// A new array of length - 1 which is a copy of the source array except that the specified element has been excluded.
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

        /// <summary>
        /// Creates a copy of this jagged array but with the element found at <paramref name="source"/>[<paramref name="excludeX"/>][<paramref name="excludeY"/>] excluded.
        /// </summary><remarks>
        /// Use the <paramref name="copyDepth"/> parameter to control the "shallowness" / depth of copying to perform.
        /// <br/>
        /// A value of zero means the inner arrays of the returned array will simply be shallow copied references to the inner
        /// arrays of the source*, i.e. Object.ReferenceEquals(result[x], source[x]) == true, x != <paramref name="excludeX"/>.
        /// <br/>
        /// A value of one means that one level of inner arrays will be copied, i.e. Object.ReferenceEquals(result[x], source[x]) == false.
        /// <br/>
        /// The default value for <paramref name="copyDepth"/> is always to copy all inner arrays.
        /// Increasing <paramref name="copyDepth"/> further in an attempt to turn this copy method into a cloning method is not supported.
        /// <br/>
        /// <i>*Of course all inner arrays along the specified index path down to the element to exclude will always have to be copied
        /// regardless of what argument was supplied to the <paramref name="copyDepth"/> parameter. This is because a change to an inner
        /// array "Inner" for excluding a value means "Inner" has to be replaced with a copy (with one less element), this in turn means
        /// that the parent array of "Inner" has to be copied too since one of its elements need replacing (original "Inner" to new one
        /// element shorter "Inner"). This bubbles up until we reach the top parent (the <paramref name="source"/> argument).</i>
        /// </remarks>
        /// <param name="source">Source array.</param>
        /// <param name="excludeX">First index of element to exclude from the copy.</param>
        /// <param name="excludeY">Second index of element to exclude from the copy.</param>
        /// <param name="copyDepth">Determines the depth of copying. Can not exceed the default value, which is to copy all inner arrays.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="excludeX"/> is less than zero or greater than or equal to the (outer) length of the <paramref name="source"/> array.<br/>
        /// Thrown if <paramref name="excludeY"/> is less than zero or greater than or equal to the length of the inner array found at <paramref name="source"/>[<paramref name="excludeX"/>].<br/>
        /// Thrown if <paramref name="copyDepth"/> is less than zero or greater than or equal to the number of (jagged) dimensions of the <paramref name="source"/> array.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if the inner array found at <paramref name="source"/>[<paramref name="excludeX"/>] is null.
        /// </exception>
        public static T[][] CopyExcluding<T>(this T[][] source, int excludeX, int excludeY, int copyDepth = 1)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (unchecked((uint)excludeX >= (uint)source.Length))
                throw new ArgumentOutOfRangeException(nameof(excludeX));
            var inner = source[excludeX];
            if (inner == null)
                throw new ArgumentException(paramName: nameof(source), message: ERR.EXCLUDE_INNER);
            if (unchecked((uint)excludeY >= (uint)inner.Length))
                throw new ArgumentOutOfRangeException(nameof(excludeY));
            if (unchecked((uint)copyDepth > 1u))
                throw new ArgumentOutOfRangeException(nameof(copyDepth));

            var copy = new T[source.Length][];

            if (copyDepth == 0)
            {
                if (excludeX != 0)
                    System.Array.Copy(source, copy, excludeX);
                copy[excludeX] = CopyExcluding(source[excludeX], excludeY);
                if (excludeX != copy.Length)
                    System.Array.Copy(source, excludeX + 1, copy, excludeX, copy.Length - excludeX);
            }
            else
            {
                for (int i = 0; i < excludeX; ++i)
                    copy[i] = Copy(source[i]);
                copy[excludeX] = CopyExcluding(source[excludeX], excludeY);
                for (int i = excludeX + 1; i < copy.Length; ++i)
                    copy[i] = Copy(source[i]);
            }

            return copy;
        }

        /* IMPORTANT NOTE - regarding CopyExcluding overloads
         * -----------------------------------------------------------------------------------------------
         * The overload below MUST be included!
         * This is because overload resolution with all int arguments, the last of which is optional,
         * is otherwise very likely to result in a call to the wrong / unintended method.
         * 
         * The following happens if the below overload does NOT exist:
         * Having a T[][][] array; and calling array.CopyExcluding(x, y, z) would not result in a call
         * to CopyExcluding(T[][][], x, y, z, copyDepth=2) as one might reasonably expect, but rather
         * it will become a call to CopyExcluding(T[][], x, y, copyDepth=z).
         * Following the principle of least surprise this is unacceptable!
         * This is what C# designer call "a pit of despair": A mistake that is easy to make, isn't caught
         * at compile time, possibly not even at runtime either until potentially something somewhere
         * else in the code blows up, and you are stuck in a debugging nightmare. A dark place.
         * 
         * So there are three options:
         * A. Have different method names. This will pollute intellisense since they no longer overload.
         * B. Make the copyDepth parameter non-optional. This isn't desirable either since the copyDepth
         *    parameter is a bit advanced. Having it default to the most likely choice is a big plus.
         * C. Include the below overload since it will be preferred in the trouble case described above.
         * 
         * Clearly option C is the best choice.
         * A cautionary tale as to why it can be dangerous to combine overloads and optional parameters!
         */

        /// <summary>
        /// Creates a copy of this jagged array but with the element found at <paramref name="source"/>[<paramref name="excludeX"/>][<paramref name="excludeY"/>] excluded.
        /// </summary><remarks>
        /// This is the same as calling <see cref="CopyExcluding{T}(T[][][], int, int, int, int)"/> with a default "copyExcluding = 2" argument,
        /// which in essence means that every inner array will be copied (as opposed to referenced).
        /// See the remarks on the <see cref="CopyExcluding{T}(T[][][], int, int, int, int)"/> method for more info.
        /// </remarks>
        /// <param name="source">Source array.</param>
        /// <param name="excludeX">First index of element to exclude from the copy.</param>
        /// <param name="excludeY">Second index of element to exclude from the copy.</param>
        /// <param name="excludeZ">Third index of element to exclude from the copy.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="excludeX"/> is less than zero or greater than or equal to the (outer) length of the <paramref name="source"/> array.<br/>
        /// Thrown if <paramref name="excludeY"/> is less than zero or greater than or equal to the length of the inner array found at <paramref name="source"/>[<paramref name="excludeX"/>].<br/>
        /// Thrown if <paramref name="excludeZ"/> is less than zero or greater than or equal to the length of the inner array found at <paramref name="source"/>[<paramref name="excludeX"/>][<paramref name="excludeY"/>].
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if the inner array found at <paramref name="source"/>[<paramref name="excludeX"/>] or <paramref name="source"/>[<paramref name="excludeX"/>][<paramref name="excludeY"/>] is null.
        /// </exception>
        public static T[][][] CopyExcluding<T>(this T[][][] source, int excludeX, int excludeY, int excludeZ)
        {
            return CopyExcluding(source, excludeX, excludeY, excludeZ, copyDepth: 2);
        }

        /// <summary>
        /// Creates a copy of this jagged array but with the element found at <paramref name="source"/>[<paramref name="excludeX"/>][<paramref name="excludeY"/>] excluded.
        /// </summary><remarks>
        /// Use the <paramref name="copyDepth"/> parameter to control the "shallowness" / depth of copying to perform.
        /// <br/>
        /// A value of zero means the inner arrays of the returned array will simply be shallow copied references to the inner
        /// arrays of the source*, i.e. Object.ReferenceEquals(result[x], source[x]) == true, x != <paramref name="excludeX"/>.
        /// <br/>
        /// A value of one means that one level of inner arrays will be copied, i.e. Object.ReferenceEquals(result[x], source[x]) == false.
        /// <br/>
        /// The default value for <paramref name="copyDepth"/> is always to copy all inner arrays.
        /// Increasing <paramref name="copyDepth"/> further in an attempt to turn this copy method into a cloning method is not supported.
        /// <br/>
        /// <i>*Of course all inner arrays along the specified index path down to the element to exclude will always have to be copied
        /// regardless of what argument was supplied to the <paramref name="copyDepth"/> parameter. This is because a change to an inner
        /// array "Inner" for excluding a value means "Inner" has to be replaced with a copy (with one less element), this in turn means
        /// that the parent array of "Inner" has to be copied too since one of its elements need replacing (original "Inner" to new one
        /// element shorter "Inner"). This bubbles up until we reach the top parent (the <paramref name="source"/> argument).</i>
        /// </remarks>
        /// <param name="source">Source array.</param>
        /// <param name="excludeX">First index of element to exclude from the copy.</param>
        /// <param name="excludeY">Second index of element to exclude from the copy.</param>
        /// <param name="excludeZ">Third index of element to exclude from the copy.</param>
        /// <param name="copyDepth">Determines the depth of copying. Can not exceed the default value, which is to copy all inner arrays.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="excludeX"/> is less than zero or greater than or equal to the (outer) length of the <paramref name="source"/> array.<br/>
        /// Thrown if <paramref name="excludeY"/> is less than zero or greater than or equal to the length of the inner array found at <paramref name="source"/>[<paramref name="excludeX"/>].<br/>
        /// Thrown if <paramref name="excludeZ"/> is less than zero or greater than or equal to the length of the inner array found at <paramref name="source"/>[<paramref name="excludeX"/>][<paramref name="excludeY"/>].<br/>
        /// Thrown if <paramref name="copyDepth"/> is less than zero or greater than or equal to the number of (jagged) dimensions of the <paramref name="source"/> array.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if the inner array found at <paramref name="source"/>[<paramref name="excludeX"/>] or <paramref name="source"/>[<paramref name="excludeX"/>][<paramref name="excludeY"/>] is null.
        /// </exception>
        public static T[][][] CopyExcluding<T>(this T[][][] source, int excludeX, int excludeY, int excludeZ, int copyDepth = 2)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (unchecked((uint)excludeX >= (uint)source.Length))
                throw new ArgumentOutOfRangeException(nameof(excludeX));
            var inner = source[excludeX];
            if (inner == null)
                throw new ArgumentException(paramName: nameof(source), message: ERR.EXCLUDE_INNER);
            if (unchecked((uint)excludeY >= (uint)inner.Length))
                throw new ArgumentOutOfRangeException(nameof(excludeY));
            var innermost = inner[excludeY];
            if (innermost == null)
                throw new ArgumentException(paramName: nameof(source), message: ERR.EXCLUDE_INNER);
            if (unchecked((uint)excludeZ >= (uint)innermost.Length))
                throw new ArgumentOutOfRangeException(nameof(excludeY));
            if (unchecked((uint)copyDepth > 2u))
                throw new ArgumentOutOfRangeException(nameof(copyDepth));

            var copy = new T[source.Length][][];

            if (copyDepth == 0)
            {
                if (excludeX != 0)
                    System.Array.Copy(source, copy, excludeX);
                copy[excludeX] = CopyExcluding(source[excludeX], excludeY, excludeZ, 0);
                if (excludeX != copy.Length)
                    System.Array.Copy(source, excludeX + 1, copy, excludeX, copy.Length - excludeX);
            }
            else
            {
                for (int i = 0; i < excludeX; ++i)
                    copy[i] = Copy(source[i], copyDepth - 1);
                copy[excludeX] = CopyExcluding(source[excludeX], excludeY, excludeZ, copyDepth - 1);
                for (int i = excludeX + 1; i < copy.Length; ++i)
                    copy[i] = Copy(source[i], copyDepth - 1);
            }

            return copy;
        }

        
        /* Code that illustrates the overloading-with-jagged-arrays-and-optional-parameters issue:
         * Now it should work as expected, but if CopyExcluding(T[][][],int,int,int) is removed then
         * the call at B would call the same method as the call at A: CopyExcluding(T[][],int,int,int). */
        // -----------------------------------------------------------------------------------------
        /* private static void OverloadResolution()
        {
            var a = new int[1][][];
            var b = a.CopyExcluding(0);
            var c = a.CopyExcluding(0, 0); //    <--- A
            var d = a.CopyExcluding(0, 0, 0); // <--- B
            var e = a.CopyExcluding(0, 0, 0, 0);
            var f = new int[1][];
            var g = f.CopyExcluding(0);
            var h = f.CopyExcluding(0, 0);
            var i = f.CopyExcluding(0, 0, 0);
        }
        */
    }
}
