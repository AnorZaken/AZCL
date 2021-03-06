﻿using System;
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
        /// Wraps a multi-rank array in an <see cref="ArrayR2{T}"/> wrapper which implements IEnumerable&lt;<typeparamref name="T"/>&gt; for use with Linq and foreach loops.
        /// </summary><remarks>
        /// Unfortunately multi-rank arrays in C# only implements IEnumerable and not IEnumerable&lt;<typeparamref name="T"/>&gt;.
        /// To solve this AZCL implements its own enumerators for multi-rank arrays (up to rank 3) and provides associated wrappers for use in foreach loops.
        /// <br/>
        /// Example: given "T[,] arr;" for some type T it can be used as "foreach(T elem in arr.AsLinqable())".
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="array"/> is null.
        /// </exception>
        public static ArrayR2<T> AsLinqable<T>(this T[,] array)
        {
            return new ArrayR2<T>(array);
        }

        /* wip
        /// <summary>
        /// Wraps a multi-rank array in an <see cref="ArrayR2{T}"/> wrapper which implements IEnumerable&lt;<typeparamref name="T"/>&gt; for use with Linq and foreach loops.
        /// </summary>
        /// <inheritdoc cref="AsLinqable{T}(T[,])" select="remarks"/>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="array"/> is null.
        /// </exception>
        public static ArrayR3<T> AsLinqable<T>(this T[,,] array)
        {
            return new ArrayR3<T>(array);
        }
        */

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
            if (array == null)
                throw new ArgumentNullException(nameof(array));

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
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            for (int i = 0; i < array.Length; ++i)
            {
                if (array[i] != null)
                    throw new ArgumentException(paramName: nameof(array), message: ERR.CREATE_INNER);

                CreateJagged(out array[i], secondLength, innermostLength);
            }
        }

        /// <summary>
        /// Tests if an array is null or of zero length.
        /// </summary><returns>
        /// True if the array is null or empty, otherwise false.
        /// </returns>
        public static bool IsNullOrEmpty(Array array)
        {
            return array == null || array.Length == 0;
        }

        /// <summary>
        /// Gets a 32-bit integer that represents the total number of elements in all
        /// the dimensions of the array, or 0 if the array is null.
        /// </summary>
        public static int LengthOrZero<T>(Array array)
        {
            return array == null ? 0 : array.Length;
        }

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
            if (array == null)
                throw new ArgumentNullException(nameof(array));

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
            if (array == null)
                throw new ArgumentNullException(nameof(array));

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
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            int lenx = array.GetLength(0);
            int leny = array.GetLength(1);
            int lenz = array.GetLength(2);
            for (int x = 0; x < lenx; ++x)
                for (int y = 0; y < leny; ++y)
                    for (int z = 0; z < lenz; ++z)
                        array[x, y, z] = new T();
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
            if (array == null)
                throw new ArgumentNullException(nameof(array));

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
            if (array == null)
                throw new ArgumentNullException(nameof(array));

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

            if (array == null)
                throw new ArgumentNullException(nameof(array));

            int len = array.Length;
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

            if (array == null)
                throw new ArgumentNullException(nameof(array));
            
            if (array.Length < THRESHOLD) // few elements: for-loops are faster
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

            if (array == null)
                throw new ArgumentNullException(nameof(array));
            
            if (array.Length < THRESHOLD) // few elements: for-loops are faster
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
            if (array == null)
                throw new ArgumentNullException(nameof(array));

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
            if (array == null)
                throw new ArgumentNullException(nameof(array));

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


        //public static void Slice<T>(T[,] input, out T[][] output); // <-- TODO !

        // -----

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

        // all inputs "valid" but likewise output is not guaranteed to be "sane"!
        internal static void CalculateIndexesUnbound<T>(T[,] array, int index, out int x, out int y)
        {
            int leny;
            if (array == null || (leny = array.GetLength(1)) == 0)
            {
                x = index;
                y = 0;
            }
            else
            {
                // Fast DivRem
                x = index / leny;
                y = index - x * leny;
                // IL doesn't have a DivRem instruction because IL doesn't support instructions with two return values.
                // Thus the above is the fastest way to DivRem in .Net (and it's the way .Net Core does it) because as of
                // yet the Jitter doesn't optimize when it sees % and / used together. (There is a petition for it though.)
            }
        }

        // all inputs "valid" but likewise output is not guaranteed to be "sane"!
        internal static void CalculateIndexesUnbound<T>(T[,,] array, int index, out int x, out int y, out int z)
        {
            int leny, lenz;
            if (array == null || (leny = array.GetLength(1)) == 0 || (lenz = array.GetLength(2)) == 0)
            {
                x = index;
                y = 0;
                z = 0;
            }
            else
            {
                int lenm = leny * lenz;
                int m;

                x = index / lenm;     // Fast DivRem (div-part 1)
                m = index - x * lenm; // Fast DivRem (mod-part 1)
                y = m / lenz;     // Fast DivRem (div-part 2)
                z = m - y * lenz; // Fast DivRem (mod-part 2)
            }
        }

        // output is not guaranteed to be within bounds!
        internal static int CalculateCountUnbound<T>(T[,] array, int x, int y, bool inclusive)
        {
            AZAssert.NotNullInternal(array, nameof(array));
            AZAssert.GEQZeroInternal(x, nameof(x));
            AZAssert.GEQZeroInternal(y, nameof(y));

            int count = y + array.LengthY() * x;
            return inclusive ? count : count - 1;
        }

        // output is not guaranteed to be within bounds!
        internal static int CalculateCountUnbound<T>(T[,,] array, int x, int y, int z, bool inclusive)
        {
            AZAssert.NotNullInternal(array, nameof(array));
            AZAssert.GEQZeroInternal(x, nameof(x));
            AZAssert.GEQZeroInternal(y, nameof(y));

            int temp = array.LengthZ();
            temp = z + y * temp + x * temp * array.LengthY();
            return inclusive ? temp : temp - 1;
        }

        internal static bool ExistsNull<T>(T[] array) where T : class
        {
            AZAssert.NotNullInternal(array, nameof(array));

            for (int i = 0; i < array.Length; ++i)
                if (array[i] == null)
                    return true;
            return false;
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
                    if (++y == leny)
                    {
                        y = 0;
                        ++x;
                    }
                }
                array[x, y, z] = value;
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
