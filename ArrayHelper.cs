using System;
using System.Collections.Generic;
using AZCL.Collections;

namespace AZCL
{
    /// <summary>
    /// Various array related methods and extensions.
    /// </summary>
    public static partial class ArrayHelper
    {
        /// <summary>
        /// Updates the <paramref name="array"/> reference with a copy that has the specified <paramref name="value"/> appended.
        /// </summary><remarks>
        /// If the array reference is null then a new single element array containing the <paramref name="value"/>
        /// argument is created and assigned to it.
        /// </remarks>
        /// <param name="array">The array to replace with a new array that has one new element appended.</param>
        /// <param name="value">The element to append to the end of the new array.</param>
        public static void AppendOrCreate<T>(ref T[] array, T value)
        {
            array = array == null ? new T[] { value } : ArrayExtensions.CopyAndAppend(array, value);
        }

        /// <summary>
        /// Wraps a multi-rank array in an <see cref="Array2{T}"/> wrapper which implements IEnumerable&lt;<typeparamref name="T"/>&gt; for use with Linq and foreach loops.
        /// </summary><remarks>
        /// Unfortunately multi-rank arrays in C# only implements IEnumerable and not IEnumerable&lt;<typeparamref name="T"/>&gt;.
        /// To solve this AZCL implements its own enumerators for multi-rank arrays (up to rank 10) and provides specialized wrappers (up to rank 3) for use with Linq and in foreach loops.
        /// <br/>
        /// Example: given an array <c>T[,] arr;</c> for some type <c>T</c> it can be used as <c>foreach(T elem in arr.AsLinqable())</c>".
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="array"/> is null.
        /// </exception>
        public static Array2<T> AsLinqable<T>(this T[,] array)
            => new Array2<T>(array);
        
        /// <summary>
        /// Wraps a multi-rank array in an <see cref="Array3{T}"/> wrapper which implements IEnumerable&lt;<typeparamref name="T"/>&gt; for use with Linq and foreach loops.
        /// </summary>
        /// <inheritdoc cref="AsLinqable{T}(T[,])" select="remarks"/>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="array"/> is null.
        /// </exception>
        public static Array3<T> AsLinqable<T>(this T[,,] array)
            => new Array3<T>(array);
        
        // ---

        /// <summary>
        /// Converts a range of elements from an array of one type to an array of another type.
        /// </summary><remarks>
        /// If <paramref name="startIndex"/> is zero then this is the same as calling
        /// System.Array.ConvertAll&lt;<typeparamref name="TInput"/>, <typeparamref name="TOutput"/>&gt;.
        /// </remarks><returns>
        /// An array of length <paramref name="input"/>.Length - <paramref name="startIndex"/> and type <typeparamref name="TOutput"/>
        /// containing the resulting converted elements.
        /// </returns>
        /// <typeparam name="TInput">The type of the elements of the <paramref name="input"/> array.</typeparam>
        /// <typeparam name="TOutput">The type of the elements of the output array.</typeparam>
        /// <param name="input">Input array containing elements to convert.</param>
        /// <param name="converter">A System.Converter&lt;TInput, TOutput&gt; used to converts elements from one type to another type.</param>
        /// <param name="startIndex">Starting index in the <paramref name="input"/> array.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if any of the arguments are null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="startIndex"/> is negative or greater than the length of the <paramref name="input"/> array.
        /// </exception>
        public static TOutput[] Convert<TInput, TOutput>(TInput[] input, Converter<TInput, TOutput> converter, int startIndex = 0)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));
            if (converter == null)
                throw new ArgumentNullException(nameof(converter));
            if (unchecked((uint)startIndex > (uint)input.Length))
                throw new ArgumentOutOfRangeException(nameof(startIndex));

            var output = new TOutput[input.Length - startIndex];
            for (int i = 0; i < output.Length; ++i)
                output[i] = converter(input[i + startIndex]);
            return output;
        }

        /// <summary>
        /// Converts a range of elements from an array of one type to an array of another type.
        /// </summary><returns>
        /// An array of specified <paramref name="length"/> and type <typeparamref name="TOutput"/> containing the resulting converted elements.
        /// </returns>
        /// <typeparam name="TInput">The type of the elements of the <paramref name="input"/> array.</typeparam>
        /// <typeparam name="TOutput">The type of the elements of the output array.</typeparam>
        /// <param name="input">Input array containing elements to convert.</param>
        /// <param name="converter">A System.Converter&lt;TInput, TOutput&gt; used to converts elements from one type to another type.</param>
        /// <param name="startIndex">Starting index in the <paramref name="input"/> array.</param>
        /// <param name="length">Number of elements to convert.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if any of the arguments are null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="startIndex"/> is negative or greater than the length of the <paramref name="input"/> array,
        /// or if <paramref name="length"/> is negative or greater than <paramref name="input"/>.Length - <paramref name="startIndex"/>.
        /// </exception>
        public static TOutput[] Convert<TInput, TOutput>(TInput[] input, Converter<TInput, TOutput> converter, int startIndex, int length)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));
            if (converter == null)
                throw new ArgumentNullException(nameof(converter));
            if (unchecked((uint)startIndex > (uint)input.Length))
                throw new ArgumentOutOfRangeException(nameof(startIndex));
            if (unchecked((uint)length > (uint)(input.Length - startIndex)))
                throw new ArgumentOutOfRangeException(nameof(length));

            var output = new TOutput[length];
            for (int i = 0; i < output.Length; ++i)
                output[i] = converter(input[i + startIndex]);
            return output;
        }

        // ---

        /// <summary>
        /// Creates a jagged array filled with arrays.
        /// </summary><remarks>
        /// For example if <paramref name="length"/> is 2 and <paramref name="innerLength"/> is 3,
        /// then a T[][] array containing two T[] arrays with room for three T elements each is created.
        /// </remarks>
        /// <param name="length">Length of the T[][] array - i.e. the number of T[] arrays to create.</param>
        /// <param name="innerLength">Length that all inner T[] arrays should have.</param>
        /// <exception cref="OverflowException">
        /// Thrown if any of the length arguments are negative (or exceed the maximum supported array length).
        /// </exception>
        public static T[][] CreateJagged<T>(int length, int innerLength)
        {
            T[][] array = new T[length][];
            CreateJaggedInner(array, innerLength);
            return array;
        }

        /// <summary>
        /// Creates a jagged array filled with arrays.
        /// </summary><remarks>
        /// For example if <paramref name="length"/> is 2 and <paramref name="innerLength"/> is 3,
        /// then a T[][] array containing two T[] arrays with room for three T elements each is created.
        /// </remarks>
        /// <param name="array">The created jagged array.</param>
        /// <param name="length">Length of the T[][] array - i.e. the number of T[] arrays to create.</param>
        /// <param name="innerLength">Length that all inner T[] arrays should have.</param>
        /// <exception cref="OverflowException">
        /// Thrown if any of the length arguments are negative (or exceed the maximum supported array length).
        /// </exception>
        public static void CreateJagged<T>(out T[][] array, int length, int innerLength)
        {
            array = CreateJagged<T>(length, innerLength);
        }

        /// <summary>
        /// Takes an empty jagged T[][] array and fills it with inner T[] arrays of the specified length.
        /// </summary>
        /// <param name="array">Empty jagged array to populate with inner arrays.</param>
        /// <param name="innerLength">Length that all inner T[] arrays should have.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array argument is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if any element is non-null, i.e. if any inner array already exist.
        /// </exception>
        /// <exception cref="OverflowException">
        /// Thrown if the <paramref name="innerLength"/> argument is negative (or exceed the maximum supported array length).
        /// </exception>
        public static void CreateJaggedInner<T>(this T[][] array, int innerLength)
        {
            NullCheck(array);
            for (int i = 0; i < array.Length; ++i)
            {
                if (array[i] != null)
                    throw new ArgumentException(paramName: nameof(array), message: ERR.CREATE_INNER);

                array[i] = new T[innerLength];
            }
        }

        /// <summary>
        /// Creates a jagged T[][][] array filled with T[][] arrays that are filled with T[] arrays.
        /// </summary>
        /// <param name="length">Length of the T[][][] array - i.e. the number of T[][] arrays to create.</param>
        /// <param name="secondLength">Length of the T[][] arrays - i.e. the number of T[] arrays to create in each T[][] array.</param>
        /// <param name="innermostLength">Length that all (innermost) T[] arrays should have.</param>
        /// <exception cref="OverflowException">
        /// Thrown if any of the length arguments are negative (or exceed the maximum supported array length).
        /// </exception>
        public static T[][][] CreateJagged<T>(int length, int secondLength, int innermostLength)
        {
            T[][][] array = new T[length][][];
            CreateJaggedInner(array, secondLength, innermostLength);
            return array;
        }

        /// <summary>
        /// Creates a jagged T[][][] array filled with T[][] arrays that are filled with T[] arrays.
        /// </summary>
        /// <param name="array">The created jagged array.</param>
        /// <param name="length">Length of the T[][][] array - i.e. the number of T[][] arrays to create.</param>
        /// <param name="secondLength">Length of the T[][] arrays - i.e. the number of T[] arrays to create in each T[][] array.</param>
        /// <param name="innermostLength">Length that all (innermost) T[] arrays should have.</param>
        /// <exception cref="OverflowException">
        /// Thrown if any of the length arguments are negative (or exceed the maximum supported array length).
        /// </exception>
        public static void CreateJagged<T>(out T[][][] array, int length, int secondLength, int innermostLength)
        {
            array = CreateJagged<T>(length, secondLength, innermostLength);
        }

        /// <summary>
        /// Takes an empty jagged T[][][] array and fills it with T[][] arrays which in turn are filled with T[] arrays.
        /// </summary>
        /// <param name="array">Empty jagged array to populate with inner arrays.</param>
        /// <param name="secondLength">Length that all inner T[][] arrays should have.</param>
        /// <param name="innermostLength">Length that all inner T[] arrays should have.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array argument is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if any element is non-null, i.e. if any inner array already exist.
        /// </exception>
        /// <exception cref="OverflowException">
        /// Thrown if any of the length arguments are negative (or exceed the maximum supported array length).
        /// </exception>
        public static void CreateJaggedInner<T>(this T[][][] array, int secondLength, int innermostLength)
        {
            NullCheck(array);
            for (int i = 0; i < array.Length; ++i)
            {
                if (array[i] != null)
                    throw new ArgumentException(paramName: nameof(array), message: ERR.CREATE_INNER);

                CreateJagged(out array[i], secondLength, innermostLength);
            }
        }

        // ---

        /// <summary>
        /// Get the lengths of all the dimensions of a multi-rank array.
        /// </summary>
        /// <param name="array">The array to get lengths for.</param>
        /// <returns>
        /// An int[] array containing the lengths of all the dimensions of the array, in left to right index order.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array is null.
        /// </exception>
        public static int[] GetLengths(Array array)
            => GetLengths_Internal(NullCheck(array));

        /// <summary>
        /// Get the lengths of the x and y dimensions of a rank 2 array.
        /// </summary>
        /// <param name="array">The array to get lengths for.</param>
        /// <returns>
        /// An int tuple containing the lengths of the dimensions of the array.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array is null.
        /// </exception>
        public static Tuples.Int2 GetLenghtsTuple<T>(T[,] array)
            => new Tuples.Int2(NullCheck(array).LengthX(), array.LengthY());

        /// <summary>
        /// Get the lengths of the x, y, and z dimensions of a rank 3 array.
        /// </summary>
        /// <param name="array">The array to get lengths for.</param>
        /// <returns>
        /// An int tuple containing the lengths of the dimensions of the array.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array is null.
        /// </exception>
        public static Tuples.Int3 GetLenghtsTuple<T>(T[,,] array)
            => new Tuples.Int3(NullCheck(array).LengthX(), array.LengthY(), array.LengthZ());

        /// <summary>
        /// Get the lengths of the w, x, y, and z dimensions of a rank 4 array.
        /// </summary>
        /// <param name="array">The array to get lengths for.</param>
        /// <returns>
        /// An int tuple containing the lengths of the dimensions of the array.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array is null.
        /// </exception>
        public static Tuples.Int4 GetLenghtsTuple<T>(T[,,,] array)
            => new Tuples.Int4(NullCheck(array).LengthW(), array.LengthX(), array.LengthY(), array.LengthZ());

        // ---

        /// <summary>
        /// Get the lower bounds of all the dimensions of a multi-rank array.
        /// </summary><remarks>
        /// An empty dimension has a lower bound of zero.
        /// </remarks>
        /// <param name="array">The array to get bounds for.</param>
        /// <returns>
        /// An int[] array containing the lower bounds of all the dimensions of the array, in left to right index order.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array is null.
        /// </exception>
        public static int[] GetLowerBounds(Array array)
            => GetLowerBounds_Internal(NullCheck(array));

        /// <summary>
        /// Get the lower bounds of the x and y dimensions of a rank 2 array.
        /// </summary><remarks>
        /// An empty dimension has a lower bound of zero.
        /// </remarks>
        /// <param name="array">The array to get bounds for.</param>
        /// <returns>
        /// An int tuple containing the lower bounds of the dimensions of the array.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array is null.
        /// </exception>
        public static Tuples.Int2 GetLowerBoundsTuple<T>(T[,] array)
            => new Tuples.Int2(NullCheck(array).GetLowerBound(0), array.GetLowerBound(1));

        /// <summary>
        /// Get the lower bounds of the x, y, and z dimensions of a rank 3 array.
        /// </summary>
        /// <inheritdoc cref="GetLowerBoundsTuple{T}(T[,])" select="remarks|returns"/>
        /// <param name="array">The array to get bounds for.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array is null.
        /// </exception>
        public static Tuples.Int3 GetLowerBoundsTuple<T>(T[,,] array)
            => new Tuples.Int3(NullCheck(array).GetLowerBound(0), array.GetLowerBound(1), array.GetLowerBound(2));

        /// <summary>
        /// Get the lower bounds of the w, x, y, and z dimensions of a rank 4 array.
        /// </summary>
        /// <inheritdoc cref="GetLowerBoundsTuple{T}(T[,])" select="remarks|returns"/>
        /// <param name="array">The array to get bounds for.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array is null.
        /// </exception>
        public static Tuples.Int4 GetLowerBoundsTuple<T>(T[,,,] array)
            => new Tuples.Int4(NullCheck(array).GetLowerBound(0), array.GetLowerBound(1), array.GetLowerBound(2), array.GetLowerBound(3));

        // ---

        /// <summary>
        /// Get the upper bounds of all the dimensions of a multi-rank array.
        /// </summary><remarks>
        /// An empty dimension has an upper bound of -1.
        /// </remarks>
        /// <param name="array">The array to get bounds for.</param>
        /// <returns>
        /// An int[] array containing the upper bounds of all the dimensions of the array, in left to right index order.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array is null.
        /// </exception>
        public static int[] GetUpperBounds(Array array)
            => GetUpperBounds_Internal(NullCheck(array));

        /// <summary>
        /// Get the upper bounds of the x and y dimensions of a rank 2 array.
        /// </summary><remarks>
        /// An empty dimension has an upper bound of -1.
        /// </remarks>
        /// <param name="array">The array to get bounds for.</param>
        /// <returns>
        /// An int tuple containing the upper bounds of the dimensions of the array.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array is null.
        /// </exception>
        public static Tuples.Int2 GetUpperBoundsTuple<T>(T[,] array)
            => new Tuples.Int2(NullCheck(array).GetUpperBound(0), array.GetUpperBound(1));

        /// <summary>
        /// Get the upper bounds of the x, y, and z dimensions of a rank 3 array.
        /// </summary>
        /// <inheritdoc cref="GetUpperBoundsTuple{T}(T[,])" select="remarks|returns"/>
        /// <param name="array">The array to get bounds for.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array is null.
        /// </exception>
        public static Tuples.Int3 GetUpperBoundsTuple<T>(T[,,] array)
            => new Tuples.Int3(NullCheck(array).GetUpperBound(0), array.GetUpperBound(1), array.GetUpperBound(2));

        /// <summary>
        /// Get the upper bounds of the w, x, y, and z dimensions of a rank 4 array.
        /// </summary>
        /// <inheritdoc cref="GetUpperBoundsTuple{T}(T[,])" select="remarks|returns"/>
        /// <param name="array">The array to get bounds for.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array is null.
        /// </exception>
        public static Tuples.Int4 GetUpperBoundsTuple<T>(T[,,,] array)
            => new Tuples.Int4(NullCheck(array).GetUpperBound(0), array.GetUpperBound(1), array.GetUpperBound(2), array.GetUpperBound(3));

        // ---

        /// <summary>
        /// Tests if an array is null or of zero length.
        /// </summary><returns>
        /// True if the array is null or empty, otherwise false.
        /// </returns>
        public static bool IsNullOrEmpty(Array array)
            => array == null || array.Length == 0;

        /// <summary>
        /// Returns whether the specified array is zero based, i.e. whether the lower bound is zero for all its dimensions.
        /// <para/>(The <see cref="ArrayHelper"/> class only supports zero bound arrays.)
        /// </summary>
        /// <returns>
        /// True if all dimensions are zero bound; otherwise false.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array is null.
        /// </exception>
        public static bool IsZeroBound(Array array)
        {
            for (int r = 0, rank = NullCheck(array).Rank; r < rank; ++r)
                if (array.GetLowerBound(r) != 0) // Note: GetLowerBound returns 0 even for empty dimensions (GetUpperBound returns -1).
                    return false;

            return true;
        }

        /* (...overloaded for efficient unrolling - these will be the most common cases anyway.) */
        /// <summary>
        /// Returns whether the specified array is zero based, i.e. whether the lower bound is zero for all its dimensions.
        /// <para/>(The <see cref="ArrayHelper"/> class only supports zero bound arrays.)
        /// </summary>
        /// <returns>
        /// True if all dimensions are zero bound; otherwise false.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array is null.
        /// </exception>
        public static bool IsZeroBound<T>(T[] array)
            => 0 == array.GetLowerBound(0); // Note: GetLowerBound returns 0 even for empty dimensions (GetUpperBound returns -1).

        /// <summary>
        /// Returns whether the specified array is zero based, i.e. whether the lower bound is zero for all its dimensions.
        /// <para/>(The <see cref="ArrayHelper"/> class only supports zero bound arrays.)
        /// </summary>
        /// <returns>
        /// True if all dimensions are zero bound; otherwise false.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array is null.
        /// </exception>
        public static bool IsZeroBound<T>(T[,] array)
            => 0 == (array.GetLowerBound(0) | array.GetLowerBound(1));

        /// <summary>
        /// Returns whether the specified array is zero based, i.e. whether the lower bound is zero for all its dimensions.
        /// <para/>(The <see cref="ArrayHelper"/> class only supports zero bound arrays.)
        /// </summary>
        /// <returns>
        /// True if all dimensions are zero bound; otherwise false.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array is null.
        /// </exception>
        public static bool IsZeroBound<T>(T[,,] array)
            => 0 == (array.GetLowerBound(0) | array.GetLowerBound(1) | array.GetLowerBound(2));

        /// <summary>
        /// Returns whether the specified array is zero based, i.e. whether the lower bound is zero for all its dimensions.
        /// <para/>(The <see cref="ArrayHelper"/> class only supports zero bound arrays.)
        /// </summary>
        /// <returns>
        /// True if all dimensions are zero bound; otherwise false.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array is null.
        /// </exception>
        public static bool IsZeroBound<T>(T[,,,] array)
            => 0 == (array.GetLowerBound(0) | array.GetLowerBound(1) | array.GetLowerBound(2) | array.GetLowerBound(3));

        /// <summary>
        /// Gets a 32-bit integer that represents the total number of elements in all
        /// the dimensions of the array, or 0 if the array is null.
        /// </summary>
        public static int LengthOrZero<T>(Array array)
            => array == null ? 0 : array.Length;

        // ---
        
        /// <summary>
        /// Fills an array with new instances of T.
        /// </summary><remarks>
        /// This will overwrite all existing elements in the array.
        /// <para/>
        /// For large arrays with value type elements it's highly recommended to use the <see cref="Clear{T}(T[])"/> method instead.
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array argument is null.
        /// </exception>
        /// <seealso cref="Clear{T}(T[])"/>
        public static void Populate<T>(this T[] array) where T : new()
        {
            NullCheck(array);
            for (int i = 0; i < array.Length; ++i)
                array[i] = new T();
        }

        /// <summary>
        /// Fills an array with new instances of T.
        /// </summary><remarks>
        /// This will overwrite all existing elements in the array.
        /// <para/>
        /// For large arrays with value type elements it's highly recommended to use the <see cref="Clear{T}(T[,])"/> method instead.
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array argument is null.
        /// </exception>
        /// <seealso cref="Clear{T}(T[,])"/>
        public static void Populate<T>(this T[,] array) where T : new()
        {
            NullCheck(array);
            int lenx = array.GetLength(0);
            int leny = array.GetLength(1);
            for (int x = 0; x < lenx; ++x)
                for (int y = 0; y < leny; ++y)
                    array[x, y] = new T();
        }

        /// <summary>
        /// Fills an array with new instances of T.
        /// </summary><remarks>
        /// This will overwrite all existing elements in the array.
        /// <para/>
        /// For large arrays with value type elements it's highly recommended to use the <see cref="Clear{T}(T[,,])"/> method instead.
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array argument is null.
        /// </exception>
        /// <seealso cref="Clear{T}(T[,,])"/>
        public static void Populate<T>(this T[,,] array) where T : new()
        {
            NullCheck(array);
            int lenx = array.GetLength(0);
            int leny = array.GetLength(1);
            int lenz = array.GetLength(2);
            for (int x = 0; x < lenx; ++x)
                for (int y = 0; y < leny; ++y)
                    for (int z = 0; z < lenz; ++z)
                        array[x, y, z] = new T();
        }

        /// <summary>
        /// Fills an array with new instances of T.
        /// </summary><remarks>
        /// This will overwrite all existing elements in the array.
        /// <para/>
        /// For large arrays with value type elements it's highly recommended to use the <see cref="Clear{T}(T[,,])"/> method instead.
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array argument is null.
        /// </exception>
        /// <seealso cref="Clear{T}(T[,,])"/>
        public static void Populate<T>(this T[,,,] array) where T : new()
        {
            NullCheck(array);
            int lenw = array.GetLength(0);
            int lenx = array.GetLength(1);
            int leny = array.GetLength(2);
            int lenz = array.GetLength(3);
            for (int w = 0; w < lenw; ++w)
                for (int x = 0; x < lenx; ++x)
                    for (int y = 0; y < leny; ++y)
                        for (int z = 0; z < lenz; ++z)
                            array[w, x, y, z] = new T();
        }

        /// <summary>
        /// Fills the inner arrays of a jagged array with new instances of T.
        /// </summary><remarks>
        /// This will overwrite all existing elements in the inner arrays.
        /// <para/>
        /// For large arrays with value type elements it's highly recommended to use the <see cref="ClearInner{T}(T[][])"/> method instead.
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array argument is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if any inner array is null.
        /// </exception>
        /// <seealso cref="ClearInner{T}(T[][])"/>
        public static void Populate<T>(this T[][] array) where T : new()
        {
            NullCheck(array);
            for (int i = 0; i < array.Length; ++i)
            {
                var arr = array[i];
                if (arr == null)
                    throw new ArgumentException(paramName: nameof(array), message: ERR.POPULATE_INNER);
                Populate(arr);
            }
        }

        /// <summary>
        /// Fills the innermost arrays of a jagged array with new instances of T.
        /// </summary><remarks>
        /// This will overwrite all existing elements in the innermost arrays.
        /// <para/>
        /// For large arrays with value type elements it's highly recommended to use the <see cref="ClearInner{T}(T[][][])"/> method instead.
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array argument is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if any inner array is null.
        /// </exception>
        /// <seealso cref="ClearInner{T}(T[][][])"/>
        public static void Populate<T>(this T[][][] array) where T : new()
        {
            NullCheck(array);
            for (int i = 0; i < array.Length; ++i)
            {
                var arr = array[i];
                if (arr == null)
                    throw new ArgumentException(paramName: nameof(array), message: ERR.POPULATE_INNER);
                Populate(arr);
            }
        }

        /// <summary>
        /// Fills an array with a single repeated value.
        /// </summary><remarks>
        /// This will overwrite all existing elements in the array.
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array argument is null.
        /// </exception>
        public static void Populate<T>(this T[] array, T value)
        {
            const int HALFLIMIT = 100;
            const int THRESHOLD = HALFLIMIT * 2; // Decide which fill-method to use (based on array length)

            int len = NullCheck(array).Length;
            if (len < THRESHOLD) // few elements: for-loops are faster
            {
                for (int i = 0; i < len; ++i)
                    array[i] = value;
            }
            else // many elements: use ArrayCopy chunking
            {
                for (int i = 0; i < HALFLIMIT; ++i) // set an initial bunch of values
                    array[i] = value;
                RepeatRange_Impl(array, HALFLIMIT); // copy that range onto all remaining elements
            }
        }

        /// <summary>
        /// Fills an array with a single repeated value.
        /// </summary><remarks>
        /// This will overwrite all existing elements in the array.
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array argument is null.
        /// </exception>
        public static void Populate<T>(this T[,] array, T value)
        {
            const int HALFLIMIT = 90;
            const int THRESHOLD = HALFLIMIT * 2; // Decide which fill-method to use (based on array length)
            
            if (NullCheck(array).Length < THRESHOLD) // few elements: for-loops are faster
            {
                int lenx = array.GetLength(0);
                int leny = array.GetLength(1);
                for (int x = 0; x < lenx; ++x)
                    for (int y = 0; y < leny; ++y)
                        array[x, y] = value;
            }
            else // many elements: use ArrayCopy chunking
            {
                SetRange_ForLoop(array, ref value, HALFLIMIT); // set an initial bunch of values (with loops)
                RepeatRange_Impl(array, HALFLIMIT); // copy that range onto all remaining elements
            }
        }

        /// <summary>
        /// Fills an array with a single repeated value.
        /// </summary><remarks>
        /// This will overwrite all existing elements in the array.
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array argument is null.
        /// </exception>
        public static void Populate<T>(this T[,,] array, T value)
        {
            const int HALFLIMIT = 90;
            const int THRESHOLD = HALFLIMIT * 2; // Decide which fill-method to use (based on array length)
            
            if (NullCheck(array).Length < THRESHOLD) // few elements: for-loops are faster
            {
                int lenx = array.GetLength(0);
                int leny = array.GetLength(1);
                int lenz = array.GetLength(2);
                for (int x = 0; x < lenx; ++x)
                    for (int y = 0; y < leny; ++y)
                        for (int z = 0; z < lenz; ++z)
                            array[x, y, z] = value;
            }
            else // many elements: use ArrayCopy chunking
            {
                SetRange_ForLoop(array, ref value, HALFLIMIT); // set an initial bunch of values (with loops)
                RepeatRange_Impl(array, HALFLIMIT); // copy that range onto all remaining elements
            }
        }

        /// <summary>
        /// Fills an array with a single repeated value.
        /// </summary><remarks>
        /// This will overwrite all existing elements in the array.
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array argument is null.
        /// </exception>
        public static void Populate<T>(this T[,,,] array, T value)
        {
            const int HALFLIMIT = 90;
            const int THRESHOLD = HALFLIMIT * 2; // Decide which fill-method to use (based on array length)

            if (NullCheck(array).Length < THRESHOLD) // few elements: for-loops are faster
            {
                int lenw = array.GetLength(0);
                int lenx = array.GetLength(1);
                int leny = array.GetLength(2);
                int lenz = array.GetLength(3);
                for (int w = 0; w < lenw; ++w)
                    for (int x = 0; x < lenx; ++x)
                        for (int y = 0; y < leny; ++y)
                            for (int z = 0; z < lenz; ++z)
                                array[w, x, y, z] = value;
            }
            else // many elements: use ArrayCopy chunking
            {
                SetRange_ForLoop(array, ref value, HALFLIMIT); // set an initial bunch of values (with loops)
                RepeatRange_Impl(array, HALFLIMIT); // copy that range onto all remaining elements
            }
        }

        /// <summary>
        /// Fills the inner arrays of a jagged array with a single repeated value.
        /// </summary><remarks>
        /// This will overwrite all existing elements in the inner arrays.
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array argument is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if any inner array is null.
        /// </exception>
        public static void Populate<T>(this T[][] array, T value)
        {
            NullCheck(array);

            int i;
            T[] copysource;
            // find an inner array that has a non-zero length
            if (!Populate_FindNonEmptyInner(array, out copysource, out i))
                return; // <-- array is T[0][] so there is nothing to populate

            // populate the inner array assigned to 'copysource'
            Populate(copysource, value);

            // populate all remaining inner arrays using 'copysource'
            Populate_RepeatCopy(array, ref copysource, i + 1);
        }

        /// <summary>
        /// Fills the innermost arrays of a jagged array with a single repeated value.
        /// </summary><remarks>
        /// This will overwrite all existing elements in the innermost arrays.
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array argument is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if any inner array is null.
        /// </exception>
        public static void Populate<T>(this T[][][] array, T value)
        {
            NullCheck(array);

            int x, y;
            T[] copysource;
            // find an inner array that has a non-zero length
            if (!Populate_FindNonEmptyInner(array, out copysource, out x, out y))
                return; // <-- array is T[0][] so there is nothing to populate

            // populate the inner array assigned to 'copysource'
            Populate(copysource, value);

            // populate all remaining inner arrays using 'copysource'
            Populate_RepeatCopy(array, ref copysource, x, y + 1);
        }

        /// <summary>
        /// Fills the array using a System.Func&lt;T&gt;.
        /// </summary><remarks>
        /// The System.Func&lt;T&gt; will be called once per element in the array, starting at the first element and progressing in order.
        /// </remarks>
        /// <param name="array">Array to populate.</param>
        /// <param name="factoryFunc">A delegate that is used as a factory to populate the array.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if any of the arguments are null.
        /// </exception>
        public static void Populate<T>(this T[] array, System.Func<T> factoryFunc)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            if (factoryFunc == null)
                throw new ArgumentNullException(nameof(factoryFunc));

            for (int i = 0; i < array.Length; ++i)
                array[i] = factoryFunc();
        }

        /// <summary>
        /// Fills the array using a System.Func&lt;T&gt;.
        /// </summary><remarks>
        /// The System.Func&lt;T&gt; will be called once per element in the array, starting at [0,0] and progressing in [x,y] order.
        /// </remarks>
        /// <param name="array">Array to populate.</param>
        /// <param name="factoryFunc">A delegate that is used as a factory to populate the array.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if any of the arguments are null.
        /// </exception>
        public static void Populate<T>(this T[,] array, System.Func<T> factoryFunc)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            if (factoryFunc == null)
                throw new ArgumentNullException(nameof(factoryFunc));

            int lenx = array.GetLength(0);
            int leny = array.GetLength(1);
            for (int x = 0; x < lenx; ++x)
                for (int y = 0; y < leny; ++y)
                    array[x, y] = factoryFunc();
        }

        /// <summary>
        /// Fills the array using a System.Func&lt;T&gt;.
        /// </summary><remarks>
        /// The System.Func&lt;T&gt; will be called once per element in the array, starting at [0,0,0] and progressing in [x,y,z] order.
        /// </remarks>
        /// <param name="array">Array to populate.</param>
        /// <param name="factoryFunc">A delegate that is used as a factory to populate the array.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if any of the arguments are null.
        /// </exception>
        public static void Populate<T>(this T[,,] array, System.Func<T> factoryFunc)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            if (factoryFunc == null)
                throw new ArgumentNullException(nameof(factoryFunc));

            int lenx = array.GetLength(0);
            int leny = array.GetLength(1);
            int lenz = array.GetLength(2);
            for (int x = 0; x < lenx; ++x)
                for (int y = 0; y < leny; ++y)
                    for (int z = 0; z < lenz; ++z)
                        array[x, y, z] = factoryFunc();
        }

        /// <summary>
        /// Fills the array using a System.Func&lt;T&gt;.
        /// </summary><remarks>
        /// The System.Func&lt;T&gt; will be called once per element in the array, starting at [0,0,0,0] and progressing in [w,x,y,z] order.
        /// </remarks>
        /// <param name="array">Array to populate.</param>
        /// <param name="factoryFunc">A delegate that is used as a factory to populate the array.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if any of the arguments are null.
        /// </exception>
        public static void Populate<T>(this T[,,,] array, System.Func<T> factoryFunc)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            if (factoryFunc == null)
                throw new ArgumentNullException(nameof(factoryFunc));

            int lenw = array.GetLength(0);
            int lenx = array.GetLength(1);
            int leny = array.GetLength(2);
            int lenz = array.GetLength(3);
            for (int w = 0; w < lenw; ++w)
                for (int x = 0; x < lenx; ++x)
                    for (int y = 0; y < leny; ++y)
                        for (int z = 0; z < lenz; ++z)
                            array[w, x, y, z] = factoryFunc();
        }

        /// <summary>
        /// Fills the inner arrays using a System.Func&lt;T&gt;.
        /// </summary><remarks>
        /// The System.Func&lt;T&gt; will be called once per element of each inner array,
        /// starting at the first element of the first inner array and progressing in [x][y] order.
        /// </remarks>
        /// <param name="array">Array to populate.</param>
        /// <param name="factoryFunc">A delegate that is used as a factory to populate the inner arrays.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if any of the arguments are null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if any inner array is null.
        /// </exception>
        public static void Populate<T>(this T[][] array, System.Func<T> factoryFunc)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            if (factoryFunc == null)
                throw new ArgumentNullException(nameof(factoryFunc));

            for (int i = 0; i < array.Length; ++i)
            {
                T[] arr = array[i];
                if (arr == null)
                    throw new ArgumentException(paramName: nameof(array), message: ERR.POPULATE_INNER);

                Populate(arr, factoryFunc);
            }
        }

        /// <summary>
        /// Fills the innermost arrays using a System.Func&lt;T&gt;.
        /// </summary><remarks>
        /// The System.Func&lt;T&gt; will be called once per element of each innermost array,
        /// starting at the first element of the first innermost array of the first inner array and progressing in [x][y][z] order.
        /// </remarks>
        /// <param name="array">Array to populate.</param>
        /// <param name="factoryFunc">A delegate that is used as a factory to populate the innermost arrays.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if any of the arguments are null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if any inner array is null.
        /// </exception>
        public static void Populate<T>(this T[][][] array, System.Func<T> factoryFunc)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            if (factoryFunc == null)
                throw new ArgumentNullException(nameof(factoryFunc));

            for (int i = 0; i < array.Length; ++i)
            {
                T[][] arr = array[i];
                if (arr == null)
                    throw new ArgumentException(paramName: nameof(array), message: ERR.POPULATE_INNER);

                Populate(arr, factoryFunc);
            }
        }

        /// <summary>
        /// Fills the array using a System.Func&lt;int, T&gt; that takes the array index as argument.
        /// </summary><remarks>
        /// The System.Func&lt;int, T&gt; will be called once per element in the array, with the index of
        /// the element to populate as argument, starting at the first element and progressing in order.
        /// </remarks>
        /// <param name="array">Array to populate.</param>
        /// <param name="factoryFunc">A delegate that is used as a factory to populate the array.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if any of the arguments are null.
        /// </exception>
        public static void Populate<T>(this T[] array, System.Func<int, T> factoryFunc)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            if (factoryFunc == null)
                throw new ArgumentNullException(nameof(factoryFunc));

            for (int i = 0; i < array.Length; ++i)
                array[i] = factoryFunc(i);
        }

        /// <summary>
        /// Fills the array using a System.Func&lt;int, int, T&gt; that takes the array indexes as arguments.
        /// </summary><remarks>
        /// The System.Func&lt;int, int, T&gt; will be called once per element in the array, progressing in [x,y] order.
        /// The <paramref name="factoryFunc"/> arguments will be supplied in the same order, i.e <paramref name="factoryFunc"/>(x, y).
        /// </remarks>
        /// <param name="array">Array to populate.</param>
        /// <param name="factoryFunc">A delegate that is used as a factory to populate the array.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if any of the arguments are null.
        /// </exception>
        public static void Populate<T>(this T[,] array, System.Func<int, int, T> factoryFunc)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            if (factoryFunc == null)
                throw new ArgumentNullException(nameof(factoryFunc));

            int lenx = array.GetLength(0);
            int leny = array.GetLength(1);
            for (int x = 0; x < lenx; ++x)
                for (int y = 0; y < leny; ++y)
                    array[x, y] = factoryFunc(x, y);
        }

        /// <summary>
        /// Fills the array using a System.Func&lt;int, int, int, T&gt; that takes the array indexes as arguments.
        /// </summary><remarks>
        /// The System.Func&lt;int, int, int, T&gt; will be called once per element in the array, progressing in [x,y,z] order.
        /// The <paramref name="factoryFunc"/> arguments will be supplied in the same order, i.e <paramref name="factoryFunc"/>(x, y, z).
        /// </remarks>
        /// <param name="array">Array to populate.</param>
        /// <param name="factoryFunc">A delegate that is used as a factory to populate the array.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if any of the arguments are null.
        /// </exception>
        public static void Populate<T>(this T[,,] array, System.Func<int, int, int, T> factoryFunc)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            if (factoryFunc == null)
                throw new ArgumentNullException(nameof(factoryFunc));

            int lenx = array.GetLength(0);
            int leny = array.GetLength(1);
            int lenz = array.GetLength(2);
            for (int x = 0; x < lenx; ++x)
                for (int y = 0; y < leny; ++y)
                    for (int z = 0; z < lenz; ++z)
                        array[x, y, z] = factoryFunc(x, y, z);
        }

        /// <summary>
        /// Fills the array using a System.Func&lt;int, int, int, int, T&gt; that takes the array indexes as arguments.
        /// </summary><remarks>
        /// The System.Func&lt;int, int, int, int, T&gt; will be called once per element in the array, progressing in [w,x,y,z] order.
        /// The <paramref name="factoryFunc"/> arguments will be supplied in the same order, i.e <paramref name="factoryFunc"/>(w, x, y, z).
        /// </remarks>
        /// <param name="array">Array to populate.</param>
        /// <param name="factoryFunc">A delegate that is used as a factory to populate the array.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if any of the arguments are null.
        /// </exception>
        public static void Populate<T>(this T[,,,] array, System.Func<int, int, int, int, T> factoryFunc)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            if (factoryFunc == null)
                throw new ArgumentNullException(nameof(factoryFunc));

            int lenw = array.GetLength(0);
            int lenx = array.GetLength(1);
            int leny = array.GetLength(2);
            int lenz = array.GetLength(3);
            for (int w = 0; w < lenw; ++w)
                for (int x = 0; x < lenx; ++x)
                    for (int y = 0; y < leny; ++y)
                        for (int z = 0; z < lenz; ++z)
                            array[w, x, y, z] = factoryFunc(w, x, y, z);
        }

        /// <summary>
        /// Fills the inner arrays using a System.Func&lt;int, int, T&gt; that takes the array indexes as arguments.
        /// </summary><remarks>
        /// The System.Func&lt;int, int, T&gt; will be called once per element of each inner array,
        /// starting at the first element of the first inner array and progressing in [x][y] order.
        /// The <paramref name="factoryFunc"/> arguments will be supplied in the same order, i.e <paramref name="factoryFunc"/>(x, y).
        /// </remarks>
        /// <param name="array">Array to populate.</param>
        /// <param name="factoryFunc">A delegate that is used as a factory to populate the inner arrays.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if any of the arguments are null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if any inner array is null.
        /// </exception>
        public static void Populate<T>(this T[][] array, System.Func<int, int, T> factoryFunc)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            if (factoryFunc == null)
                throw new ArgumentNullException(nameof(factoryFunc));

            for (int x = 0; x < array.Length; ++x)
            {
                T[] arr1 = array[x];
                if (arr1 == null)
                    throw new ArgumentException(paramName: nameof(array), message: ERR.POPULATE_INNER);

                for (int y = 0; y < arr1.Length; ++y)
                    arr1[y] = factoryFunc(x, y);
            }
        }

        /// <summary>
        /// Fills the innermost arrays using a System.Func&lt;int, int, int, T&gt; that takes the array indexes as arguments.
        /// </summary><remarks>
        /// The System.Func&lt;int, int, int, T&gt; will be called once per element of each innermost array,
        /// starting at the first element of the first innermost array of the first inner array and progressing in [x][y][z] order.
        /// The <paramref name="factoryFunc"/> arguments will be supplied in the same order, i.e <paramref name="factoryFunc"/>(x, y, z).
        /// </remarks>
        /// <param name="array">Array to populate.</param>
        /// <param name="factoryFunc">A delegate that is used as a factory to populate the innermost arrays.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if any of the arguments are null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if any inner array is null.
        /// </exception>
        public static void Populate<T>(this T[][][] array, System.Func<int, int, int, T> factoryFunc)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            if (factoryFunc == null)
                throw new ArgumentNullException(nameof(factoryFunc));

            for (int x = 0; x < array.Length; ++x)
            {
                T[][] arr2 = array[x];
                if (arr2 == null)
                    throw new ArgumentException(paramName: nameof(array), message: ERR.POPULATE_INNER);

                for (int y = 0; y < arr2.Length; ++y)
                {
                    T[] arr1 = arr2[y];
                    if (arr1 == null)
                        throw new ArgumentException(paramName: nameof(array), message: ERR.POPULATE_INNER);

                    for (int z = 0; z < arr1.Length; ++z)
                        arr1[z] = factoryFunc(x, y, z);
                }
            }
        }

        // ---

        //public static void Slice<T>(T[,] input, out T[][] output); // <-- TODO !

        // -----

        // (the point is to try to avoid calling this slow method if possible)
        internal static IEnumerable<TResult> CastIter<TResult>(System.Collections.IEnumerable source)
        {
            AZAssert.NotNullInternal(source, nameof(source));

            foreach (var element in source)
                yield return (TResult)element;
        }

        internal static bool ExistsNull<T>(T[] array) where T : class
        {
            AZAssert.NotNullInternal(array, nameof(array));

            for (int i = 0; i < array.Length; ++i)
                if (array[i] == null)
                    return true;
            return false;
        }

        internal static int[] GetLengths_Internal(Array array)
        {
            AZAssert.NotNullInternal(array, nameof(array));

            var lenghts = new int[array.Rank];
            for (int i = 0; i < lenghts.Length; ++i)
                lenghts[i] = array.GetLength(i);
            return lenghts;
        }

        internal static int[] GetLowerBounds_Internal(Array array)
        {
            AZAssert.NotNullInternal(array, nameof(array));

            var bounds = new int[array.Rank];
            for (int i = 0; i < bounds.Length; ++i)
                bounds[i] = array.GetLowerBound(i);
            return bounds;
        }

        // no null-check!
        internal static Tuples.Int2 GetLowerBoundsTuple_Internal<T>(T[,] array)
            => new Tuples.Int2(array.GetLowerBound(0), array.GetLowerBound(1));

        // no null-check!
        internal static Tuples.Int3 GetLowerBoundsTuple_Internal<T>(T[,,] array)
            => new Tuples.Int3(array.GetLowerBound(0), array.GetLowerBound(1), array.GetLowerBound(2));

        // no null-check!
        internal static Tuples.Int4 GetLowerBoundsTuple_Internal<T>(T[,,,] array)
            => new Tuples.Int4(array.GetLowerBound(0), array.GetLowerBound(1), array.GetLowerBound(2), array.GetLowerBound(3));

        // ! resulting indexes are NOT guaranteed to be valid for use in an indexer! (i.e. if one of the lengths are 0) !
        internal static int[] GetUpperBounds_Internal(Array array)
        {
            AZAssert.NotNullInternal(array, nameof(array));

            var bounds = new int[array.Rank];
            for (int i = 0; i < bounds.Length; ++i)
                bounds[i] = array.GetUpperBound(i);
            return bounds;
        }

        // no null-check!
        internal static Tuples.Int2 GetUpperBoundsTuple_Internal<T>(T[,] array)
            => new Tuples.Int2(array.GetUpperBound(0), array.GetUpperBound(1));

        // no null-check!
        internal static Tuples.Int3 GetUpperBoundsTuple_Internal<T>(T[,,] array)
            => new Tuples.Int3(array.GetUpperBound(0), array.GetUpperBound(1), array.GetUpperBound(2));

        // no null-check!
        internal static Tuples.Int4 GetUpperBoundsTuple_Internal<T>(T[,,,] array)
            => new Tuples.Int4(array.GetUpperBound(0), array.GetUpperBound(1), array.GetUpperBound(2), array.GetUpperBound(3));

        // ---

        // treats null as an empty array!
        internal static T Last<T>(T[,] arrayOrNull)
        {
            if (arrayOrNull == null)
                throw new InvalidOperationException(ERR.SOURCE_EMPTY);
            int bx = arrayOrNull.GetUpperBound(0);
            int by = arrayOrNull.GetUpperBound(1);
            if ((bx | by) == -1) // at least one dimension is empty (This assumes zero-bound!!)
                throw new InvalidOperationException(ERR.SOURCE_EMPTY);
            return arrayOrNull[bx, by];
        }

        // treats null as an empty array!
        internal static T Last<T>(T[,,] arrayOrNull)
        {
            if (arrayOrNull == null)
                throw new InvalidOperationException(ERR.SOURCE_EMPTY);
            int bx = arrayOrNull.GetUpperBound(0);
            int by = arrayOrNull.GetUpperBound(1);
            int bz = arrayOrNull.GetUpperBound(2);
            if ((bx | by | bz) == -1) // at least one dimension is empty (This assumes zero-bound!!)
                throw new InvalidOperationException(ERR.SOURCE_EMPTY);
            return arrayOrNull[bx, by, bz];
        }

        // treats null as an empty array!
        internal static T Last<T>(T[,,,] arrayOrNull)
        {
            if (arrayOrNull == null)
                throw new InvalidOperationException(ERR.SOURCE_EMPTY);
            int bw = arrayOrNull.GetUpperBound(0);
            int bx = arrayOrNull.GetUpperBound(1);
            int by = arrayOrNull.GetUpperBound(2);
            int bz = arrayOrNull.GetUpperBound(3);
            if ((bw | bx | by | bz) == -1) // at least one dimension is empty (This assumes zero-bound!!)
                throw new InvalidOperationException(ERR.SOURCE_EMPTY);
            return arrayOrNull[bw, bx, by, bz];
        }

        // treats null as an empty array!
        internal static T LastOrDefault<T>(T[,] arrayOrNull)
        {
            if (arrayOrNull != null)
            {
                int bx = arrayOrNull.GetUpperBound(0);
                int by = arrayOrNull.GetUpperBound(1);
                if ((bx | by) != -1) // no dimension is empty (This assumes zero-bound!!)
                    return arrayOrNull[bx, by];
            }
            return default(T);
        }

        // treats null as an empty array!
        internal static T LastOrDefault<T>(T[,,] arrayOrNull)
        {
            if (arrayOrNull != null)
            {
                int bx = arrayOrNull.GetUpperBound(0);
                int by = arrayOrNull.GetUpperBound(1);
                int bz = arrayOrNull.GetUpperBound(2);
                if ((bx | by | bz) != -1) // no dimension is empty (This assumes zero-bound!!)
                    return arrayOrNull[bx, by, bz];
            }
            return default(T);
        }

        // treats null as an empty array!
        internal static T LastOrDefault<T>(T[,,,] arrayOrNull)
        {
            if (arrayOrNull != null)
            {
                int bw = arrayOrNull.GetUpperBound(0);
                int bx = arrayOrNull.GetUpperBound(1);
                int by = arrayOrNull.GetUpperBound(2);
                int bz = arrayOrNull.GetUpperBound(3);
                if ((bw | bx | by | bz) != -1) // no dimension is empty (This assumes zero-bound!!)
                    return arrayOrNull[bw, bx, by, bz];
            }
            return default(T);
        }

        // creates a new array with same dimensions as source
        internal static T[,] New<T>(T[,] source)
        {
            AZAssert.NotNullInternal(source, nameof(source));
            return new T[source.GetLength(0), source.GetLength(1)];
        }

        // creates a new array with same dimensions as source
        internal static T[,,] New<T>(T[,,] source)
        {
            AZAssert.NotNullInternal(source, nameof(source));
            return new T[source.GetLength(0), source.GetLength(1), source.GetLength(2)];
        }

        // creates a new array with same shape as source
        internal static T[][] New<T>(T[][] source, int instancingDepth = 1)
        {
            AZAssert.NotNullInternal(source, nameof(source));

            if (unchecked((uint)instancingDepth > 1u))
                throw new ArgumentOutOfRangeException(nameof(instancingDepth));

            var arr = new T[source.Length][];
            if (instancingDepth != 0)
            {
                for (int i = 0; i < arr.Length; ++i)
                    arr[i] = new T[source[i].Length];
            }
            return arr;
        }

        // creates a new array with same shape as source
        internal static T[][][] New<T>(T[][][] source, int instancingDepth = 1)
        {
            AZAssert.NotNullInternal(source, nameof(source));

            if (unchecked((uint)instancingDepth > 1u))
                throw new ArgumentOutOfRangeException(nameof(instancingDepth));

            var arr = new T[source.Length][][];
            if (instancingDepth != 0)
            {
                for (int i = 0; i < arr.Length; ++i)
                    arr[i] = New(source[i], instancingDepth - 1);
            }
            return arr;
        }

        // treats null as an empty array!
        internal static IEnumerable<T> Reverse<T>(T[,] arrayOrNull)
        {
            if (arrayOrNull != null)
            {
                var max = GetUpperBoundsTuple_Internal(arrayOrNull);
                var min = GetLowerBoundsTuple_Internal(arrayOrNull);
                for (int x = max.x; x >= min.x; --x)
                    for (int y = max.y; y >= min.y; --y)
                        yield return arrayOrNull[x, y];
            }
        }

        // treats null as an empty array!
        internal static IEnumerable<T> Reverse<T>(T[,,] arrayOrNull)
        {
            if (arrayOrNull != null)
            {
                var max = GetUpperBoundsTuple_Internal(arrayOrNull);
                var min = GetLowerBoundsTuple_Internal(arrayOrNull);
                for (int x = max.x; x >= min.x; --x)
                    for (int y = max.y; y >= min.y; --y)
                        for (int z = max.z; z >= min.z; --z)
                            yield return arrayOrNull[x, y, z];
            }
        }

        // treats null as an empty array!
        internal static IEnumerable<T> Reverse<T>(T[,,,] arrayOrNull)
        {
            if (arrayOrNull != null)
            {
                var max = GetUpperBoundsTuple_Internal(arrayOrNull);
                var min = GetLowerBoundsTuple_Internal(arrayOrNull);
                for (int w = max.w; w >= min.w; --w)
                    for (int x = max.x; x >= min.x; --x)
                        for (int y = max.y; y >= min.y; --y)
                            for (int z = max.z; z >= min.z; --z)
                                yield return arrayOrNull[w, x, y, z];
            }
        }

        internal static IEnumerable<TSource> Skip<TSource>(TSource[,] arrayOrNull, int count)
        {
            var enumerator = new Array2<TSource>.Enumerator(arrayOrNull, count < 0 ? 0 : count);
            while (enumerator.MoveNext())
                yield return enumerator.Current;
        }

        internal static IEnumerable<TSource> Skip<TSource>(TSource[,,] arrayOrNull, int count)
        {
            var enumerator = new Array3<TSource>.Enumerator(arrayOrNull, count < 0 ? 0 : count);
            while (enumerator.MoveNext())
                yield return enumerator.Current;
        }

        // treats null as an empty array!
        internal static T[] ToArray<T>(T[,] arrayOrNull)
        {
            int i;
            if (arrayOrNull == null || (i = arrayOrNull.Length) == 0)
                return Empty<T>.Array;

            var arr = new T[i];
            i = 0;

            var max = GetUpperBoundsTuple_Internal(arrayOrNull);
            var min = GetLowerBoundsTuple_Internal(arrayOrNull);
            for (int x = min.x; x <= max.x; ++x)
                for (int y = min.y; y <= max.y; ++y)
                    arr[i++] = arrayOrNull[x, y];

            return arr;
        }

        // treats null as an empty array!
        internal static T[] ToArray<T>(T[,,] arrayOrNull)
        {
            int i;
            if (arrayOrNull == null || (i = arrayOrNull.Length) == 0)
                return Empty<T>.Array;

            var arr = new T[i];
            i = 0;

            var max = GetUpperBoundsTuple_Internal(arrayOrNull);
            var min = GetLowerBoundsTuple_Internal(arrayOrNull);
            for (int x = min.x; x <= max.x; ++x)
                for (int y = min.y; y <= max.y; ++y)
                    for (int z = min.z; z <= max.z; ++z)
                        arr[i++] = arrayOrNull[x, y, z];

            return arr;
        }

        // treats null as an empty array!
        internal static T[] ToArray<T>(T[,,,] arrayOrNull)
        {
            int i;
            if (arrayOrNull == null || (i = arrayOrNull.Length) == 0)
                return Empty<T>.Array;

            var arr = new T[i];
            i = 0;

            var max = GetUpperBoundsTuple_Internal(arrayOrNull);
            var min = GetLowerBoundsTuple_Internal(arrayOrNull);
            for (int w = min.w; w <= max.w; ++w)
                for (int x = min.x; x <= max.x; ++x)
                    for (int y = min.y; y <= max.y; ++y)
                        for (int z = min.z; z <= max.z; ++z)
                            arr[i++] = arrayOrNull[w, x, y, z];

            return arr;
        }

        // treats null as an empty array!
        internal static List<T> ToList<T>(T[,] arrayOrNull)
        {
            int len;
            if (arrayOrNull == null || (len = arrayOrNull.Length) == 0)
                return new List<T>(0);

            var list = new List<T>(len);

            var max = GetUpperBoundsTuple_Internal(arrayOrNull);
            var min = GetLowerBoundsTuple_Internal(arrayOrNull);
            for (int x = min.x; x <= max.x; ++x)
                for (int y = min.y; y <= max.y; ++y)
                    list.Add(arrayOrNull[x, y]);

            return list;
        }

        // treats null as an empty array!
        internal static List<T> ToList<T>(T[,,] arrayOrNull)
        {
            int len;
            if (arrayOrNull == null || (len = arrayOrNull.Length) == 0)
                return new List<T>(0);

            var list = new List<T>(len);

            var max = GetUpperBoundsTuple_Internal(arrayOrNull);
            var min = GetLowerBoundsTuple_Internal(arrayOrNull);
            for (int x = min.x; x <= max.x; ++x)
                for (int y = min.y; y <= max.y; ++y)
                    for (int z = min.z; z <= max.z; ++z)
                        list.Add(arrayOrNull[x, y, z]);

            return list;
        }

        // treats null as an empty array!
        internal static List<T> ToList<T>(T[,,,] arrayOrNull)
        {
            int len;
            if (arrayOrNull == null || (len = arrayOrNull.Length) == 0)
                return new List<T>(0);

            var list = new List<T>(len);

            var max = GetUpperBoundsTuple_Internal(arrayOrNull);
            var min = GetLowerBoundsTuple_Internal(arrayOrNull);
            for (int w = min.w; w <= max.w; ++w)
                for (int x = min.x; x <= max.x; ++x)
                    for (int y = min.y; y <= max.y; ++y)
                        for (int z = min.z; z <= max.z; ++z)
                            list.Add(arrayOrNull[w, x, y, z]);

            return list;
        }

        // -----

        private static void ChunkCopy_Impl<T>(ref T[] source, T[] target)
        {
            AZAssert.NotEmptyInternal(source, nameof(source));
            AZAssert.NotNullInternal(target, nameof(target));

            int rem = target.Length; // (elements remaining)
            int has = source.Length; // (source has available)
            if (has >= rem)
            {
                Array.Copy(source, target, rem);
            }
            else
            {
                Array.Copy(source, target, has); // copy as many elements as source has
                RepeatRange_Impl(target, has); // copy as much as target has onto itself repeatedly until all remaining elements copied to
                source = target; // make target the new source (since it's larger)
            }
        }

        // for use in expressions (where the argument is called "array")
        private static T NullCheck<T>(T array)
        {
            if (array == null) throw new ArgumentNullException(nameof(array));
            return array;
        }
        
        private static void RepeatRange_Impl(Array array, int has) // (array 'has' available)
        {
            AZAssert.NotNullInternal(array, nameof(array));
            AZAssert.BoundsInternal(array, has - 1, nameof(has), false); // -1 because has is a count AND is not allowed to be zero!

            int rem = array.Length - has; // (elements remaining)
            while (rem > has)
            {
                System.Array.Copy(array, 0, array, has, has); // copy as much as the array has
                rem -= has;
                has <<= 1; // 'has' doubles each pass
            }
            System.Array.Copy(array, 0, array, has, rem); // copy final elements (less than 'has')
        }
        
        private static void SetRange_ForLoop<T>(T[,] array, ref T value, int count, int x = 0, int y = 0)
        {
            AZAssert.NotEmptyInternal(array, nameof(array));
            AZAssert.BoundsInternal(array, x, y, count);

            int leny = array.GetLength(1);
            for (int i = 0; i < count; ++i, ++y)
            {
                if (y == leny)
                {
                    y = 0;
                    ++x;
                }
                array[x, y] = value;
            }
        }

        private static void SetRange_ForLoop<T>(T[,,] array, ref T value, int count, int x = 0, int y = 0, int z = 0)
        {
            AZAssert.NotEmptyInternal(array, nameof(array));
            AZAssert.BoundsInternal(array, x, y, z, count);

            int leny = array.GetLength(1);
            int lenz = array.GetLength(2);
            for (int i = 0; i < count; ++i, ++z)
            {
                if (z == lenz)
                {
                    z = 0;
                    if (++y == leny)
                    {
                        y = 0;
                        ++x;
                    }
                }
                array[x, y, z] = value;
            }
        }

        private static void SetRange_ForLoop<T>(T[,,,] array, ref T value, int count, int w = 0, int x = 0, int y = 0, int z = 0)
        {
            AZAssert.NotEmptyInternal(array, nameof(array));
            AZAssert.BoundsInternal(array, w, x, y, z, count);

            int lenx = array.GetLength(1);
            int leny = array.GetLength(2);
            int lenz = array.GetLength(3);
            for (int i = 0; i < count; ++i, ++z)
            {
                if (z == lenz)
                {
                    z = 0;
                    if (++y == leny)
                    {
                        y = 0;
                        if (++x == lenx)
                        {
                            x = 0;
                            ++w;
                        }
                    }
                }
                array[w, x, y, z] = value;
            }
        }

        private static bool Populate_FindNonEmptyInner<T>(T[][] array, out T[] inner, out int i) // 'i' will be the index of 'inner'
        {
            AZAssert.NotNullInternal(array, nameof(array));

            for (i = 0; i < array.Length; ++i)
            {
                inner = array[i];

                if (inner == null)
                    throw new ArgumentException(paramName: nameof(array), message: ERR.POPULATE_INNER);

                if (inner.Length != 0)
                    return true;
            }

            inner = null;
            return false;
        }
        
        private static bool Populate_FindNonEmptyInner<T>(T[][][] array, out T[] innermost, out int x, out int y) // 'x' and 'y' will be the indexes of 'innermost'
        {
            AZAssert.NotNullInternal(array, nameof(array));

            for (x = 0; x < array.Length; ++x)
            {
                T[][] inner = array[x];

                if (inner == null)
                    throw new ArgumentException(paramName: nameof(array), message: ERR.POPULATE_INNER);

                Populate_FindNonEmptyInner(inner, out innermost, out y);
            }

            y = 0;
            innermost = null;
            return false;
        }
        
        private static void Populate_RepeatCopy<T>(T[][] array, ref T[] copysource, int i) // 'i' should be the index of the first inner target
        {
            AZAssert.NotNullInternal(array, nameof(array));

            // use Array.Copy and 'copysource' to populate all remaining inner arrays
            for (; i < array.Length; ++i)
            {
                T[] copytarget = array[i];

                if (copytarget == null)
                    throw new ArgumentException(paramName: nameof(array), message: ERR.POPULATE_INNER);

                ChunkCopy_Impl(ref copysource, copytarget);
            }
        }
        
        private static void Populate_RepeatCopy<T>(T[][][] array, ref T[] copysource, int x, int y) // 'x' and 'y' should be the indexes of the first inner target
        {
            AZAssert.NotNullInternal(array, nameof(array));

            // we know 'x' is valid, because that is the inner array where 'copysource' was found.
            // (this is not the case for 'y' which could be equal to the length of that inner array)
            do
            {
                T[][] inner = array[x];

                if (inner == null)
                    throw new ArgumentException(paramName: nameof(array), message: ERR.POPULATE_INNER);

                Populate_RepeatCopy(inner, ref copysource, y);
                y = 0; // <-- set to zero after the above call so the original y-argument is used exactly once.
            }
            while (++x < array.Length);
        }
    }
}
