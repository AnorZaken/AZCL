using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AZCL.Collections;

namespace AZCL
{
    public static partial class ArrayHelper
    {
        /* Static methods from System.Array - expanded to cover multi-dimensional and readonly arrays! */
        /* ******************************************************************************************* */

        /// <summary>
        /// Creates a ReadOnlyArray wrapper for an array.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="array"/> is null.
        /// </exception>
        public static ReadOnlyArray<T> AsReadOnly<T>(this T[] array)
        {
            return new ReadOnlyArray<T>(array);
        }

        /// <summary>
        /// Creates a ReadOnlyArray wrapper for an array.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="array"/> is null.
        /// </exception>
        public static ReadOnlyArrayR2<T> AsReadOnly<T>(this T[,] array)
        {
            return new ReadOnlyArrayR2<T>(array);
        }

        /// <summary>
        /// Creates a ReadOnlyArray wrapper for an array.
        /// </summary>
        public static ReadOnlyArrayR2<T> AsReadOnly<T>(this ArrayR2<T> array)
        {
            return new ReadOnlyArrayR2<T>(array);
        }

        /* wip
        /// <summary>
        /// Creates a ReadOnlyArray wrapper for an array.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="array"/> is null.
        /// </exception>
        public static ReadOnlyArrayR3<T> AsReadOnly<T>(this T[,,] array)
        {
            return new ReadOnlyArrayR3<T>(array);
        }
        
        /// <summary>
        /// Creates a ReadOnlyArray wrapper for an array.
        /// </summary>
        public static ReadOnlyArrayR3<T> AsReadOnly<T>(this ArrayR3<T> array)
        {
            return new ReadOnlyArrayR3<T>(array);
        }
        */

        /// <summary>
        /// Creates a System.Collections.ObjectModel.ReadOnlyCollection&lt;T&gt; wrapper for an array.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="array"/> is null.
        /// </exception>
        public static ReadOnlyCollection<T> AsReadOnlyColletion<T>(this T[] array)
        {
            return System.Array.AsReadOnly(array);
        }

        /// <summary>
        /// Creates a System.Collections.ObjectModel.ReadOnlyCollection&lt;T&gt; wrapper for an array.
        /// </summary><returns>
        /// A ReadOnlyCollection&lt;T&gt; wrapping the backing array of the <paramref name="array"/> argument,
        /// or an empty ReadOnlyCollection&lt;T&gt; if the backing array is absent.
        /// </returns>
        public static ReadOnlyCollection<T> AsReadOnlyColletion<T>(this ReadOnlyArray<T> array)
        {
            return array.Array == null ? Empty<T>.ReadOnlyCollection : new ReadOnlyCollection<T>(array.Array);
        }

        /// <summary>
        /// Searches a sorted array for a specific element, using the IComparable&lt;T&gt; interface implemented by each element and by the specified object.
        /// </summary><returns>
        /// The index of the sought value in the array, if the value is found; otherwise, a negative number.
        /// If the sought value is (comparably) greater than all elements in the array, then the negative number
        /// returned will be the bitwise complement of array.Length.
        /// If the sought value is not found the negative number returned will be the bitwise complement of the
        /// index of the first element that is (comparably) larger.
        /// If this method is called with a non-sorted array, the return value can be incorrect and a negative
        /// number could be returned, even if value is present in the array.
        /// </returns>
        /// <param name="array">A sorted array to search.</param>
        /// <param name="value">The value to search for.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array is null.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown if <typeparamref name="T"/> does not implement the IComparable&lt;T&gt; generic interface.
        /// </exception>
        public static int BinarySearch<T>(T[] array, T value)
        {
            return Array.BinarySearch(array, value);
        }

        /// <summary>
        /// Searches a sorted array for a specific element, using the IComparable&lt;T&gt; interface implemented by each element and by the specified object.
        /// </summary>
        /// <inheritdoc cref="BinarySearch{T}(T[], T)" select="returns"/>
        /// <param name="array">A sorted array to search.</param>
        /// <param name="value">The value to search for.</param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if <typeparamref name="T"/> does not implement the IComparable&lt;T&gt; generic interface.
        /// </exception>
        public static int BinarySearch<T>(this ReadOnlyArray<T> array, T value)
        {
            return array.Array == null ? -1 : Array.BinarySearch<T>(array.Array, value);
        }

        /// <summary>
        /// Searches a sorted array for a value using the specified IComparer&lt;T&gt;.
        /// </summary>
        /// <inheritdoc cref="BinarySearch{T}(T[], T)" select="returns"/>
        /// <param name="array">A sorted array to search.</param>
        /// <param name="value">The value to search for.</param>
        /// <param name="comparer">The IComparer&lt;T&gt; to use when comparing elements, or null to use the IComparable&lt;T&gt; implementation of each element.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="comparer"/> is null, and <paramref name="value"/> is of a type that is not compatible with the elements of array.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown if <paramref name="comparer"/> is null, and <typeparamref name="T"/> does not implement the IComparable&lt;T&gt; generic interface.
        /// </exception>
        public static int BinarySearch<T>(T[] array, T value, IComparer<T> comparer)
        {
            return Array.BinarySearch(array, value, comparer);
        }
        
        /// <summary>
        /// Searches a sorted array for a value using the specified IComparer&lt;T&gt;.
        /// </summary>
        /// <inheritdoc cref="BinarySearch{T}(T[], T)" select="returns"/>
        /// <param name="array">A sorted array to search.</param>
        /// <param name="value">The value to search for.</param>
        /// <param name="comparer">The IComparer&lt;T&gt; to use when comparing elements, or null to use the IComparable&lt;T&gt; implementation of each element.</param>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="comparer"/> is null, and <paramref name="value"/> is of a type that is not compatible with the elements of array.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown if <paramref name="comparer"/> is null, and <typeparamref name="T"/> does not implement the IComparable&lt;T&gt; generic interface.
        /// </exception>
        public static int BinarySearch<T>(this ReadOnlyArray<T> array, T value, IComparer<T> comparer)
        {
            return array.Array == null ? -1 : Array.BinarySearch<T>(array.Array, value, comparer);
        }

        /// <summary>
        /// Searches a range of elements in a sorted array for a value, using the IComparable&lt;T&gt; interface implemented by each element and by the specified value.
        /// </summary>
        /// <inheritdoc cref="BinarySearch{T}(T[], T)" select="returns"/>
        /// <param name="array">A sorted array to search.</param>
        /// <param name="index">The starting index of the range to search.</param>
        /// <param name="length">The length of the range to search.</param>
        /// <param name="value">The value to search for.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="index"/> or <paramref name="length"/> is less than zero.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="index"/> and <paramref name="length"/> do not specify a valid range in the <paramref name="array"/>.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown if <typeparamref name="T"/> does not implement the IComparable&lt;T&gt; generic interface.
        /// </exception>
        public static int BinarySearch<T>(T[] array, int index, int length, T value)
        {
            return Array.BinarySearch(array, index, length, value);
        }

        /// <summary>
        /// Searches a range of elements in a sorted array for a value, using the IComparable&lt;T&gt; interface implemented by each element and by the specified value.
        /// </summary>
        /// <inheritdoc cref="BinarySearch{T}(T[], T)" select="returns"/>
        /// <param name="array">A sorted array to search.</param>
        /// <param name="index">The starting index of the range to search.</param>
        /// <param name="length">The length of the range to search.</param>
        /// <param name="value">The value to search for.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="index"/> or <paramref name="length"/> is less than zero.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="index"/> and <paramref name="length"/> do not specify a valid range in the <paramref name="array"/>.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown if <typeparamref name="T"/> does not implement the IComparable&lt;T&gt; generic interface.
        /// </exception>
        public static int BinarySearch<T>(this ReadOnlyArray<T> array, int index, int length, T value)
        {
            return array.Array == null ? -1 : Array.BinarySearch<T>(array.Array, index, length, value);
        }

        /// <summary>
        /// Searches a range of elements in a sorted array for a value using the specified IComparer&lt;T&gt;.
        /// </summary>
        /// <inheritdoc cref="BinarySearch{T}(T[], T)" select="returns"/>
        /// <param name="array">A sorted array to search.</param>
        /// <param name="index">The starting index of the range to search.</param>
        /// <param name="length">The length of the range to search.</param>
        /// <param name="value">The value to search for.</param>
        /// <param name="comparer">The IComparer&lt;T&gt; to use when comparing elements, or null to use the IComparable&lt;T&gt; implementation of each element.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="index"/> or <paramref name="length"/> is less than zero.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="index"/> and <paramref name="length"/> do not specify a valid range in the <paramref name="array"/>.
        /// Also thrown if <paramref name="comparer"/> is null, and <paramref name="value"/> is of a type that is not compatible with the elements of array.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown if <paramref name="comparer"/> is null, and <typeparamref name="T"/> does not implement the IComparable&lt;T&gt; generic interface.
        /// </exception>
        public static int BinarySearch<T>(T[] array, int index, int length, T value, IComparer<T> comparer)
        {
            return Array.BinarySearch(array, index, length, value, comparer);
        }

        /// <summary>
        /// Searches a range of elements in a sorted array for a value using the specified IComparer&lt;T&gt;.
        /// </summary>
        /// <inheritdoc cref="BinarySearch{T}(T[], T)" select="returns"/>
        /// <param name="array">A sorted array to search.</param>
        /// <param name="index">The starting index of the range to search.</param>
        /// <param name="length">The length of the range to search.</param>
        /// <param name="value">The value to search for.</param>
        /// <param name="comparer">The IComparer&lt;T&gt; to use when comparing elements, or null to use the IComparable&lt;T&gt; implementation of each element.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="index"/> or <paramref name="length"/> is less than zero.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="index"/> and <paramref name="length"/> do not specify a valid range in the <paramref name="array"/>.
        /// Also thrown if <paramref name="comparer"/> is null, and <paramref name="value"/> is of a type that is not compatible with the elements of array.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown if <paramref name="comparer"/> is null, and <typeparamref name="T"/> does not implement the IComparable&lt;T&gt; generic interface.
        /// </exception>
        public static int BinarySearch<T>(this ReadOnlyArray<T> array, int index, int length, T value, IComparer<T> comparer)
        {
            return array.Array == null ? -1 : Array.BinarySearch<T>(array.Array, index, length, value, comparer);
        }

        /// <summary>
        /// Clears an array by setting all values to default(T).
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array is null.
        /// </exception>
        /// <seealso cref="Populate{T}(T[])"/>
        public static void Clear<T>(this T[] array)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            Array.Clear(array, 0, array.Length);
        }

        /// <summary>
        /// Clears an array by setting all values to default(T).
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array is null.
        /// </exception>
        public static void Clear<T>(this T[,] array)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            Array.Clear(array, 0, array.Length);
        }

        /// <summary>
        /// Clears an array by setting all values to default(T).
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array is null.
        /// </exception>
        public static void Clear<T>(this T[,,] array)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            Array.Clear(array, 0, array.Length);
        }

        /// <summary>
        /// Clears an array by setting all values to default(T).
        /// </summary><remarks>
        /// Does nothing if backing array is absent.
        /// </remarks>
        public static void Clear<T>(this ArrayR2<T> array)
        {
            var arr = array.Array;
            if (arr != null)
                Array.Clear(arr, 0, arr.Length);
        }

        /* wip
        /// <summary>
        /// Clears an array by setting all values to default(T).
        /// </summary><remarks>
        /// Does nothing if backing array is absent.
        /// </remarks>
        public static void Clear<T>(this ArrayR3<T> array)
        {
            var arr = array.Array;
            if (arr != null)
                Array.Clear(arr, 0, arr.Length);
        }
        */

        /// <summary>
        /// Clears all elements of all inner arrays by setting them to default(T).
        /// </summary><remarks>
        /// This method does not remove / overwrite any of the inner arrays themselves, only the <typeparamref name="T"/>-elements inside them.
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if any inner array is null.
        /// </exception>
        public static void ClearInner<T>(this T[][] array)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            foreach (T[] arr in array)
            {
                if (arr == null)
                    throw new ArgumentException(paramName: nameof(array), message: ERR.CLEAR_INNER);
                else
                    Clear(arr);
            }
        }

        /// <summary>
        /// Clears all elements of all inner arrays by setting them to default(T).
        /// </summary><remarks>
        /// This method does not remove / overwrite any of the inner arrays themselves, only the <typeparamref name="T"/>-elements inside them.
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if any inner array is null.
        /// </exception>
        public static void ClearInner<T>(this T[][][] array)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            foreach (T[][] arr in array)
            {
                if (arr == null)
                    throw new ArgumentException(paramName: nameof(array), message: ERR.CLEAR_INNER);
                else
                    ClearInner(arr);
            }
        }

        /// <summary>
        /// Converts all elements of an array of one type to an array of another type.
        /// </summary><remarks>
        /// This is simply a redirected call to System.Array.ConvertAll. It exist here for completeness and convenience.
        /// </remarks><returns>
        /// An array of type <typeparamref name="TOutput"/> of the same size and rank as the input <paramref name="array"/>.
        /// </returns>
        /// <typeparam name="TInput">The type of the elements of the input <paramref name="array"/>.</typeparam>
        /// <typeparam name="TOutput">The type of the elements of the output array.</typeparam>
        /// <param name="array">Input array containing elements to convert.</param>
        /// <param name="converter">A System.Converter&lt;TInput, TOutput&gt; used to converts elements from one type to another type.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if any of the arguments are null.
        /// </exception>
        public static TOutput[] ConvertAll<TInput, TOutput>(TInput[] array, Converter<TInput, TOutput> converter)
        {
            return Array.ConvertAll(array, converter);
        }

        /// <summary>
        /// Converts all elements of an array of one type to an array of another type.
        /// </summary><returns>
        /// An array of type <typeparamref name="TOutput"/> of the same size and rank as the <paramref name="input"/> array.
        /// </returns>
        /// <typeparam name="TInput">The type of the elements of the <paramref name="input"/> array.</typeparam>
        /// <typeparam name="TOutput">The type of the elements of the output array.</typeparam>
        /// <param name="input">Input array containing elements to convert.</param>
        /// <param name="converter">A System.Converter&lt;TInput, TOutput&gt; used to converts elements from one type to another type.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if any of the arguments are null.
        /// </exception>
        public static TOutput[,] ConvertAll<TInput, TOutput>(TInput[,] input, Converter<TInput, TOutput> converter)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));
            if (converter == null)
                throw new ArgumentNullException(nameof(converter));

            int lenx = input.GetLength(0);
            int leny = input.GetLength(1);
            var output = new TOutput[lenx, leny];
            for (int x = 0; x < lenx; ++x)
                for (int y = 0; y < leny; ++y)
                    output[x, y] = converter(input[x, y]);

            return output;
        }

        /// <summary>
        /// Converts all elements of an array of one type to an array of another type.
        /// </summary><returns>
        /// An array of type <typeparamref name="TOutput"/> of the same size and rank as the <paramref name="input"/> array.
        /// </returns>
        /// <typeparam name="TInput">The type of the elements of the <paramref name="input"/> array.</typeparam>
        /// <typeparam name="TOutput">The type of the elements of the output array.</typeparam>
        /// <param name="input">Input array containing elements to convert.</param>
        /// <param name="converter">A System.Converter&lt;TInput, TOutput&gt; used to converts elements from one type to another type.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if any of the arguments are null.
        /// </exception>
        public static TOutput[,,] ConvertAll<TInput, TOutput>(TInput[,,] input, Converter<TInput, TOutput> converter)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));
            if (converter == null)
                throw new ArgumentNullException(nameof(converter));

            int lenx = input.GetLength(0);
            int leny = input.GetLength(1);
            int lenz = input.GetLength(2);
            var output = new TOutput[lenx, leny, lenz];
            for (int x = 0; x < lenx; ++x)
                for (int y = 0; y < leny; ++y)
                    for (int z = 0; z < lenz; ++z)
                        output[x, y, z] = converter(input[x, y, z]);

            return output;
        }

        /// <summary>
        /// Converts all elements of an array of one type to an array of another type.
        /// </summary><returns>
        /// An array of type <typeparamref name="TOutput"/> of the same size and rank as the wrapped <paramref name="input"/> array,
        /// or an empty <typeparamref name="TOutput"/>[] array if the wrapper's backing array is absent.
        /// </returns>
        /// <typeparam name="TInput">The type of the elements of the <paramref name="input"/> array.</typeparam>
        /// <typeparam name="TOutput">The type of the elements of the output array.</typeparam>
        /// <param name="input">Input array containing elements to convert.</param>
        /// <param name="converter">A System.Converter&lt;TInput, TOutput&gt; used to converts elements from one type to another type.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="converter"/> argument is null.
        /// </exception>
        public static TOutput[] ConvertAll<TInput, TOutput>(ReadOnlyArray<TInput> input, Converter<TInput, TOutput> converter)
        {
            if (input.Array == null)
            {
                if (converter == null)
                    throw new ArgumentNullException(nameof(converter));

                return Empty<TOutput>.Array;
            }
            return Array.ConvertAll(input.Array, converter);
        }

        /// <summary>
        /// Converts all elements of an array of one type to an array of another type.
        /// </summary><remarks>
        /// If backing array is absent for the <paramref name="input"/> then it will also be absent for the output.
        /// </remarks><returns>
        /// An <see cref="ArrayR2{T}"/> wrapper of type <typeparamref name="TOutput"/> matching the size and rank of the <paramref name="input"/> array.
        /// </returns>
        /// <typeparam name="TInput">The type of the elements of the <paramref name="input"/> array.</typeparam>
        /// <typeparam name="TOutput">The type of the elements of the output array.</typeparam>
        /// <param name="input">Input array containing elements to convert.</param>
        /// <param name="converter">A System.Converter&lt;TInput, TOutput&gt; used to converts elements from one type to another type.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="converter"/> argument is null.
        /// </exception>
        public static ArrayR2<TOutput> ConvertAll<TInput, TOutput>(ArrayR2<TInput> input, Converter<TInput, TOutput> converter)
        {
            if (input.Array == null)
            {
                if (converter == null)
                    throw new ArgumentNullException(nameof(converter));

                return new ArrayR2<TOutput>();
            }
            return ConvertAll(input.Array, converter);
        }

        /// <summary>
        /// Converts all elements of an array of one type to an array of another type.
        /// </summary>
        /// <inheritdoc cref="ConvertAll{TInput, TOutput}(ArrayR2{TInput}, Converter{TInput, TOutput})"/>
        /// <param name="input">Input array containing elements to convert.</param>
        /// <param name="converter">A System.Converter&lt;TInput, TOutput&gt; used to converts elements from one type to another type.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="converter"/> argument is null.
        /// </exception>
        public static ArrayR2<TOutput> ConvertAll<TInput, TOutput>(ReadOnlyArrayR2<TInput> input, Converter<TInput, TOutput> converter)
        {
            if (input.Array == null)
            {
                if (converter == null)
                    throw new ArgumentNullException(nameof(converter));

                return new ArrayR2<TOutput>();
            }
            return ConvertAll(input.Array, converter);
        }

        /* wip
        /// <summary>
        /// Converts all elements of an array of one type to an array of another type.
        /// </summary><remarks>
        /// If backing array is absent for the <paramref name="input"/> then it will also be absent for the output.
        /// </remarks><returns>
        /// An <see cref="ArrayR3{T}"/> wrapper of type <typeparamref name="TOutput"/> matching the size and rank of the <paramref name="input"/> array.
        /// </returns>
        /// <typeparam name="TInput">The type of the elements of the <paramref name="input"/> array.</typeparam>
        /// <typeparam name="TOutput">The type of the elements of the output array.</typeparam>
        /// <param name="input">Input array containing elements to convert.</param>
        /// <param name="converter">A System.Converter&lt;TInput, TOutput&gt; used to converts elements from one type to another type.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="converter"/> argument is null.
        /// </exception>
        public static ArrayR3<TOutput> ConvertAll<TInput, TOutput>(ArrayR3<TInput> input, Converter<TInput, TOutput> converter)
        {
            if (input.Array == null)
            {
                if (converter == null)
                    throw new ArgumentNullException(nameof(converter));

                return new ArrayR3<TOutput>();
            }
            return ConvertAll(input.Array, converter);
        }

        /// <summary>
        /// Converts all elements of an array of one type to an array of another type.
        /// </summary>
        /// <inheritdoc cref="ConvertAll{TInput, TOutput}(ArrayR3{TInput}, Converter{TInput, TOutput})"/>
        /// <typeparam name="TInput">The type of the elements of the <paramref name="input"/> array.</typeparam>
        /// <typeparam name="TOutput">The type of the elements of the output array.</typeparam>
        /// <param name="input">Input array containing elements to convert.</param>
        /// <param name="converter">A System.Converter&lt;TInput, TOutput&gt; used to converts elements from one type to another type.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="converter"/> argument is null.
        /// </exception>
        public static ArrayR3<TOutput> ConvertAll<TInput, TOutput>(ReadOnlyArrayR3<TInput> input, Converter<TInput, TOutput> converter)
        {
            if (input.Array == null)
            {
                if (converter == null)
                    throw new ArgumentNullException(nameof(converter));

                return new ArrayR3<TOutput>();
            }
            return ConvertAll(input.Array, converter);
        }
        */

        /// <summary>
        /// Converts all elements of a jagged array of one type to an identically shaped jagged array of another type.
        /// </summary><returns>
        /// A jagged array of type <typeparamref name="TOutput"/> of the same shape as the jagged <paramref name="input"/> array.
        /// </returns>
        /// <typeparam name="TInput">The type of the elements of the jagged <paramref name="input"/> array.</typeparam>
        /// <typeparam name="TOutput">The type of the elements of the jagged output array.</typeparam>
        /// <param name="input">Input array containing elements to convert.</param>
        /// <param name="converter">A System.Converter&lt;TInput, TOutput&gt; used to converts elements from one type to another type.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if any of the arguments are null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if any of the inner arrays are null.
        /// </exception>
        public static TOutput[][] ConvertAll<TInput, TOutput>(TInput[][] input, Converter<TInput, TOutput> converter)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));
            if (converter == null)
                throw new ArgumentNullException(nameof(converter));

            var output = new TOutput[input.Length][];

            for (int i = 0; i < input.Length; ++i)
            {
                var inner = input[i];

                if (inner == null)
                    throw new ArgumentException(paramName: nameof(input), message: ERR.CONVERT_INNER);

                output[i] = Array.ConvertAll(inner, converter);
            }

            return output;
        }

        /// <summary>
        /// Converts all elements of a jagged array of one type to an identically shaped jagged array of another type.
        /// </summary><returns>
        /// A jagged array of type <typeparamref name="TOutput"/> of the same shape as the jagged <paramref name="input"/> array.
        /// </returns>
        /// <typeparam name="TInput">The type of the elements of the jagged <paramref name="input"/> array.</typeparam>
        /// <typeparam name="TOutput">The type of the elements of the jagged output array.</typeparam>
        /// <param name="input">Input array containing elements to convert.</param>
        /// <param name="converter">A System.Converter&lt;TInput, TOutput&gt; used to converts elements from one type to another type.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if any of the arguments are null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if any of the inner arrays are null.
        /// </exception>
        public static TOutput[][][] ConvertAll<TInput, TOutput>(TInput[][][] input, Converter<TInput, TOutput> converter)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));
            if (converter == null)
                throw new ArgumentNullException(nameof(converter));

            var output = new TOutput[input.Length][][];

            for (int i = 0; i < input.Length; ++i)
            {
                var inner = input[i];

                if (inner == null)
                    throw new ArgumentException(paramName: nameof(input), message: ERR.CONVERT_INNER);

                output[i] = ConvertAll(inner, converter);
            }

            return output;
        }

        /// <summary>
        /// Determines whether the array contains elements that match the conditions of the predicate.
        /// </summary><returns>
        /// True if the array contains one or more elements that match the conditions defined by the predicate; otherwise, false.
        /// </returns>
        /// <param name="array">The array to search.</param>
        /// <param name="match">The Predicate&lt;T&gt; that defines the conditions of the elements to search for.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array or the predicate is null.
        /// </exception>
        public static bool Exists<T>(T[] array, Predicate<T> match)
        {
            return Array.Exists<T>(array, match);
        }
        
        /// <summary>
        /// Determines whether the array contains elements that match the conditions of the predicate.
        /// </summary><returns>
        /// True if the array contains one or more elements that match the conditions defined by the predicate; otherwise, false.
        /// </returns>
        /// <param name="array">The array to search.</param>
        /// <param name="match">The Predicate&lt;T&gt; that defines the conditions of the elements to search for.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array or the predicate is null.
        /// </exception>
        public static bool Exists<T>(T[,] array, Predicate<T> match)
        {
            return Exists(new ReadOnlyArrayR2<T>(array), match); // <-- ctor does null check.
        }

        /* wip
        /// <summary>
        /// Determines whether the array contains elements that match the conditions of the predicate.
        /// </summary><returns>
        /// True if the array contains one or more elements that match the conditions defined by the predicate; otherwise, false.
        /// </returns>
        /// <param name="array">The array to search.</param>
        /// <param name="match">The Predicate&lt;T&gt; that defines the conditions of the elements to search for.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array or the predicate is null.
        /// </exception>
        public static bool Exists<T>(T[,,] array, Predicate<T> match)
        {
            return Exists(new ReadOnlyArrayR3<T>(array), match); // <-- ctor does null check.
        }
        */

        /// <summary>
        /// Determines whether the array contains elements that match the conditions of the predicate.
        /// </summary><returns>
        /// True if the array contains one or more elements that match the conditions defined by the predicate; otherwise, false.
        /// </returns>
        /// <param name="array">The array to search.</param>
        /// <param name="match">The Predicate&lt;T&gt; that defines the conditions of the elements to search for.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the predicate is null.
        /// </exception>
        public static bool Exists<T>(ReadOnlyArray<T> array, Predicate<T> match) // Linq: Any(Func<T, bool>)
        {
            if (array.Array == null)
            {
                if (match == null)
                    throw new ArgumentNullException(nameof(match));

                return false;
            }
            return Array.Exists<T>(array.Array, match);
        }

        /// <summary>
        /// Obsolete: Use Linq Any(Func&lt;T, bool&gt;) extension instead.
        /// </summary>
        /// <seealso cref="Exists{T}(T[,], Predicate{T})"/>
        [Obsolete("Use Linq Any(Func<T, bool>) extension instead.")]
        public static bool Exists<T>(ArrayR2<T> array, Predicate<T> match)
        {
            if (match == null)
                throw new ArgumentNullException(nameof(match));

            return array.Any(new Func<T, bool>(match));
        }

        /// <summary>
        /// Obsolete: Use Linq Any(Func&lt;T, bool&gt;) extension instead.
        /// </summary>
        /// <seealso cref="Exists{T}(T[,], Predicate{T})"/>
        [Obsolete("Use Linq Any(Func<T, bool>) extension instead.")]
        public static bool Exists<T>(ReadOnlyArrayR2<T> array, Predicate<T> match)
        {
            if (match == null)
                throw new ArgumentNullException(nameof(match));

            return array.Any(new Func<T, bool>(match));
        }

        /* wip
        /// <summary>
        /// Obsolete: Use Linq Any(Func&lt;T, bool&gt;) extension instead.
        /// </summary>
        /// <seealso cref="Exists{T}(T[,,], Predicate{T})"/>
        [Obsolete("Use Linq Any(Func<T, bool>) extension instead.")]
        public static bool Exists<T>(ArrayR3<T> array, Predicate<T> match)
        {
            if (match == null)
                throw new ArgumentNullException(nameof(match));

            return array.Any(new Func<T, bool>(match));
        }
        
        /// <summary>
        /// Obsolete: Use Linq Any(Func&lt;T, bool&gt;) extension instead.
        /// </summary>
        /// <seealso cref="Exists{T}(T[,,], Predicate{T})"/>
        [Obsolete("Use Linq Any(Func<T, bool>) extension instead.")]
        public static bool Exists<T>(ReadOnlyArrayR3<T> array, Predicate<T> match)
        {
            if (match == null)
                throw new ArgumentNullException(nameof(match));

            return array.Any(new Func<T, bool>(match));
        }
        */

        /// <summary>
        /// Determines whether every element in the array matches the conditions defined by the predicate.
        /// </summary><returns>
        /// True if the array is empty or every element matches the conditions defined by the predicate; otherwise, false.
        /// </returns>
        /// <param name="array">The array to search.</param>
        /// <param name="match">The Predicate&lt;T&gt; that defines the conditions to check against all elements.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array or predicate is null.
        /// </exception>
        public static bool TrueForAll<T>(T[] array, Predicate<T> match)
        {
            return Array.TrueForAll<T>(array, match);
        }

        /// <summary>
        /// Determines whether every element in the array matches the conditions defined by the predicate.
        /// </summary><returns>
        /// True if the array is empty or every element matches the conditions defined by the predicate; otherwise, false.
        /// </returns>
        /// <param name="array">The array to search.</param>
        /// <param name="match">The Predicate&lt;T&gt; that defines the conditions to check against all elements.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array or predicate is null.
        /// </exception>
        public static bool TrueForAll<T>(T[,] array, Predicate<T> match)
        {
            return TrueForAll(new ReadOnlyArrayR2<T>(array), match); // <-- ctor does null check.
        }

        /* wip
        /// <summary>
        /// Determines whether every element in the array matches the conditions defined by the predicate.
        /// </summary><returns>
        /// True if the array is empty or every element matches the conditions defined by the predicate; otherwise, false.
        /// </returns>
        /// <param name="array">The array to search.</param>
        /// <param name="match">The Predicate&lt;T&gt; that defines the conditions to check against all elements.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array or predicate is null.
        /// </exception>
        public static bool TrueForAll<T>(T[,,] array, Predicate<T> match)
        {
            return TrueForAll(new ReadOnlyArrayR3<T>(array), match); // <-- ctor does null check.
        }
        */

        /// <summary>
        /// Determines whether every element in the array matches the conditions defined by the predicate.
        /// </summary><returns>
        /// True if the array is empty, absent, or every element matches the conditions defined by the predicate; otherwise, false.
        /// </returns>
        /// <param name="array">The array to search.</param>
        /// <param name="match">The Predicate&lt;T&gt; that defines the conditions to check against all elements.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the predicate is null.
        /// </exception>
        public static bool TrueForAll<T>(ReadOnlyArray<T> array, Predicate<T> match)
        {
            if (array.Array == null)
            {
                if (match == null)
                    throw new ArgumentNullException(nameof(match));

                return false;
            }
            return Array.TrueForAll<T>(array.Array, match);
        }

        /// <summary>
        /// Obsolete: Use Linq All(Func&lt;T, bool&gt;) extension instead.
        /// </summary>
        /// <seealso cref="TrueForAll{T}(T[,], Predicate{T})"/>
        [Obsolete("Use Linq All(Func<T, bool>) extension instead.")]
        public static bool TrueForAll<T>(ArrayR2<T> array, Predicate<T> match)
        {
            if (match == null)
                throw new ArgumentNullException(nameof(match));

            return array.All(new Func<T, bool>(match));
        }

        /// <summary>
        /// Obsolete: Use Linq All(Func&lt;T, bool&gt;) extension instead.
        /// </summary>
        /// <seealso cref="TrueForAll{T}(T[,], Predicate{T})"/>
        [Obsolete("Use Linq All(Func<T, bool>) extension instead.")]
        public static bool TrueForAll<T>(ReadOnlyArrayR2<T> array, Predicate<T> match)
        {
            if (match == null)
                throw new ArgumentNullException(nameof(match));

            return array.All(new Func<T, bool>(match));
        }

        /* wip
        /// <summary>
        /// Obsolete: Use Linq All(Func&lt;T, bool&gt;) extension instead.
        /// </summary>
        /// <seealso cref="TrueForAll{T}(T[,,], Predicate{T})"/>
        [Obsolete("Use Linq All(Func<T, bool>) extension instead.")]
        public static bool TrueForAll<T>(ArrayR3<T> array, Predicate<T> match)
        {
            if (match == null)
                throw new ArgumentNullException(nameof(match));

            return array.All(new Func<T, bool>(match));
        }
        
        /// <summary>
        /// Obsolete: Use Linq All(Func&lt;T, bool&gt;) extension instead.
        /// </summary>
        /// <seealso cref="TrueForAll{T}(T[,,], Predicate{T})"/>
        [Obsolete("Use Linq All(Func<T, bool>) extension instead.")]
        public static bool TrueForAll<T>(ReadOnlyArrayR3<T> array, Predicate<T> match)
        {
            if (match == null)
                throw new ArgumentNullException(nameof(match));

            return array.All(new Func<T, bool>(match));
        }
        */

        /// <summary>
        /// Searches the array for an element that matches the conditions defined by the predicate, returning the first occurrence.
        /// </summary><returns>
        /// The first element that matches the conditions defined by the specified predicate, if found; otherwise, the default value for type T.
        /// </returns>
        /// <param name="array">The array to search.</param>
        /// <param name="match">The Predicate&lt;T&gt; that defines the conditions of the element to search for.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array or predicate is null.
        /// </exception>
        public static T Find<T>(T[] array, Predicate<T> match)
        {
            return Array.Find(array, match);
        }

        /// <summary>
        /// Searches the array for an element that matches the conditions defined by the predicate, returning the first occurrence.
        /// </summary><returns>
        /// The first element that matches the conditions defined by the specified predicate, if found; otherwise, the default value for type T.
        /// </returns>
        /// <param name="array">The array to search.</param>
        /// <param name="match">The Predicate&lt;T&gt; that defines the conditions of the element to search for.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array or predicate is null.
        /// </exception>
        public static T Find<T>(T[,] array, Predicate<T> match)
        {
            return Find(new ArrayR2<T>(array), match); // <-- ctor does null check.
        }


        /* wip
        /// <summary>
        /// Searches the array for an element that matches the conditions defined by the predicate, returning the first occurrence.
        /// </summary><returns>
        /// The first element that matches the conditions defined by the specified predicate, if found; otherwise, the default value for type T.
        /// </returns>
        /// <param name="array">The array to search.</param>
        /// <param name="match">The Predicate&lt;T&gt; that defines the conditions of the element to search for.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array or predicate is null.
        /// </exception>
        public static T Find<T>(T[,,] array, Predicate<T> match)
        {
            return Find(new ArrayR3<T>(array), match); // <-- ctor does null check.
        }
        */

        /// <summary>
        /// Searches the array for an element that matches the conditions defined by the predicate, returning the first occurrence.
        /// </summary><returns>
        /// The first element that matches the conditions defined by the specified predicate, if found; otherwise, the default value for type T.
        /// </returns>
        /// <param name="array">The array to search.</param>
        /// <param name="match">The Predicate&lt;T&gt; that defines the conditions of the element to search for.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the predicate is null.
        /// </exception>
        public static T Find<T>(ReadOnlyArray<T> array, Predicate<T> match)
        {
            if (array.Array == null)
            {
                if (match == null)
                    throw new ArgumentNullException(nameof(match));

                return default(T);
            }
            return Array.Find<T>(array.Array, match);
        }

        /// <summary>
        /// Obsolete: Use Linq FirstOrDefault(Func&lt;T, bool&gt;) extension instead.
        /// </summary>
        /// <seealso cref="Find{T}(T[,], Predicate{T})"/>
        [Obsolete("Use Linq FirstOrDefault(Func<T, bool>) extension instead.")]
        public static T Find<T>(ArrayR2<T> array, Predicate<T> match)
        {
            if (match == null)
                throw new ArgumentNullException(nameof(match));

            return array.FirstOrDefault(new Func<T, bool>(match));
        }

        /// <summary>
        /// Obsolete: Use Linq FirstOrDefault(Func&lt;T, bool&gt;) extension instead.
        /// </summary>
        /// <seealso cref="Find{T}(T[,], Predicate{T})"/>
        [Obsolete("Use Linq FirstOrDefault(Func<T, bool>) extension instead.")]
        public static T Find<T>(ReadOnlyArrayR2<T> array, Predicate<T> match)
        {
            if (match == null)
                throw new ArgumentNullException(nameof(match));

            return array.FirstOrDefault(new Func<T, bool>(match));
        }

        /* wip
        /// <summary>
        /// Obsolete: Use Linq FirstOrDefault(Func&lt;T, bool&gt;) extension instead.
        /// </summary>
        /// <seealso cref="Find{T}(T[,,], Predicate{T})"/>
        [Obsolete("Use Linq FirstOrDefault(Func<T, bool>) extension instead.")]
        public static T Find<T>(ArrayR3<T> array, Predicate<T> match)
        {
            if (match == null)
                throw new ArgumentNullException(nameof(match));

            return array.FirstOrDefault(new Func<T, bool>(match));
        }
        
        /// <summary>
        /// Obsolete: Use Linq FirstOrDefault(Func&lt;T, bool&gt;) extension instead.
        /// </summary>
        /// <seealso cref="Find{T}(T[,,], Predicate{T})"/>
        [Obsolete("Use Linq FirstOrDefault(Func<T, bool>) extension instead.")]
        public static T Find<T>(ReadOnlyArrayR3<T> array, Predicate<T> match)
        {
            if (match == null)
                throw new ArgumentNullException(nameof(match));

            return array.FirstOrDefault(new Func<T, bool>(match));
        }
        */

        /// <summary>
        /// Retrieves all the elements that match the conditions defined by the specified predicate.
        /// </summary><returns>
        /// A T[] array containing all the elements that match the conditions defined by the specified predicate; or an empty array if no matching elements were found.
        /// </returns>
        /// <param name="array">The array to search.</param>
        /// <param name="match">The Predicate&lt;T&gt; that defines the conditions of the elements to search for.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array or predicate is null.
        /// </exception>
        public static T[] FindAll<T>(T[] array, Predicate<T> match)
        {
            return Array.FindAll<T>(array, match);
        }

        /// <summary>
        /// Retrieves all the elements that match the conditions defined by the specified predicate.
        /// </summary><returns>
        /// A T[] array containing all the elements that match the conditions defined by the specified predicate; or an empty array if no matching elements were found.
        /// </returns>
        /// <param name="array">The array to search.</param>
        /// <param name="match">The Predicate&lt;T&gt; that defines the conditions of the elements to search for.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array or predicate is null.
        /// </exception>
        public static T[] FindAll<T>(T[,] array, Predicate<T> match)
        {
            return FindAll(new ArrayR2<T>(array), match); // <-- ctor does null check.
        }

        /* wip
        /// <summary>
        /// Retrieves all the elements that match the conditions defined by the specified predicate.
        /// </summary><returns>
        /// A T[] array containing all the elements that match the conditions defined by the specified predicate; or an empty array if no matching elements were found.
        /// </returns>
        /// <param name="array">The array to search.</param>
        /// <param name="match">The Predicate&lt;T&gt; that defines the conditions of the elements to search for.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array or predicate is null.
        /// </exception>
        public static T[] FindAll<T>(T[,,] array, Predicate<T> match)
        {
            return FindAll(new ArrayR3<T>(array), match); // <-- ctor does null check.
        }
        */

        /// <summary>
        /// Retrieves all the elements that match the conditions defined by the specified predicate.
        /// </summary><returns>
        /// A T[] array containing all the elements that match the conditions defined by the specified predicate; or an empty array if no matching elements were found.
        /// </returns>
        /// <param name="array">The array to search.</param>
        /// <param name="match">The Predicate&lt;T&gt; that defines the conditions of the elements to search for.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the predicate is null.
        /// </exception>
        public static T[] FindAll<T>(ReadOnlyArray<T> array, Predicate<T> match)
        {
            if (array.Array == null)
            {
                if (match == null)
                    throw new ArgumentNullException(nameof(match));

                return Empty<T>.Array;
            }
            return Array.FindAll<T>(array.Array, match);
        }

        /// <summary>
        /// Obsolete: Use Linq Where(Func&lt;T, bool&gt;) extension instead.
        /// </summary>
        /// <seealso cref="FindAll{T}(T[,], Predicate{T})"/>
        [Obsolete("Use Linq Where(Func<T, bool>) extension instead.")]
        public static T[] FindAll<T>(ArrayR2<T> array, Predicate<T> match)
        {
            if (match == null)
                throw new ArgumentNullException(nameof(match));

            return array.IsAbsent ? Empty<T>.Array : array.Where(new Func<T, bool>(match)).ToArray();
        }

        /// <summary>
        /// Obsolete: Use Linq Where(Func&lt;T, bool&gt;) extension instead.
        /// </summary>
        /// <seealso cref="FindAll{T}(T[,], Predicate{T})"/>
        [Obsolete("Use Linq Where(Func<T, bool>) extension instead.")]
        public static T[] FindAll<T>(ReadOnlyArrayR2<T> array, Predicate<T> match)
        {
            if (match == null)
                throw new ArgumentNullException(nameof(match));

            return array.IsAbsent ? Empty<T>.Array : array.Where(new Func<T, bool>(match)).ToArray();
        }

        /* wip
        /// <summary>
        /// Obsolete: Use Linq Where(Func&lt;T, bool&gt;) extension instead.
        /// </summary>
        /// <seealso cref="FindAll{T}(T[,,], Predicate{T})"/>
        [Obsolete("Use Linq Where(Func<T, bool>) extension instead.")]
        public static T[] FindAll<T>(ArrayR3<T> array, Predicate<T> match)
        {
            if (match == null)
                throw new ArgumentNullException(nameof(match));

            return array.IsAbsent ? Empty<T>.Array : array.Where(new Func<T, bool>(match)).ToArray();
        }
        
        /// <summary>
        /// Obsolete: Use Linq Where(Func&lt;T, bool&gt;) extension instead.
        /// </summary>
        /// <seealso cref="FindAll{T}(T[,,], Predicate{T})"/>
        [Obsolete("Use Linq Where(Func<T, bool>) extension instead.")]
        public static T[] FindAll<T>(ReadOnlyArrayR3<T> array, Predicate<T> match)
        {
            if (match == null)
                throw new ArgumentNullException(nameof(match));

            return array.IsAbsent ? Empty<T>.Array : array.Where(new Func<T, bool>(match)).ToArray();
        }
        */

        /// <summary>
        /// Searches the array for an element that matches the conditions defined by the predicate, returning the last occurrence.
        /// </summary><returns>
        /// The last element in the array that matches the conditions defined by the predicate, if a matching element exists; otherwise, the default value for type T.
        /// </returns>
        /// <param name="array">The array to search.</param>
        /// <param name="match">The Predicate&lt;T&gt; that defines the conditions of the element to search for.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array or predicate is null.
        /// </exception>
        public static T FindLast<T>(T[] array, Predicate<T> match)
        {
            return Array.FindLast(array, match);
        }

        /// <summary>
        /// Searches the array for an element that matches the conditions defined by the predicate, returning the last occurrence.
        /// </summary><returns>
        /// The last element in the array that matches the conditions defined by the predicate, if a matching element exists; otherwise, the default value for type T.
        /// </returns>
        /// <param name="array">The array to search.</param>
        /// <param name="match">The Predicate&lt;T&gt; that defines the conditions of the element to search for.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array or predicate is null.
        /// </exception>
        public static T FindLast<T>(T[,] array, Predicate<T> match)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            if (match == null)
                throw new ArgumentNullException(nameof(match));

            int uby = array.LengthY() - 1;
            for (int x = array.LengthX() - 1; x >= 0; --x)
            {
                for (int y = uby; y >= 0; --y)
                {
                    T val = array[x, y];
                    if (match(val))
                        return val;
                }
            }
            return default(T);
        }

        /// <summary>
        /// Searches the array for an element that matches the conditions defined by the predicate, returning the last occurrence.
        /// </summary><returns>
        /// The last element in the array that matches the conditions defined by the predicate, if a matching element exists; otherwise, the default value for type T.
        /// </returns>
        /// <param name="array">The array to search.</param>
        /// <param name="match">The Predicate&lt;T&gt; that defines the conditions of the element to search for.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array or predicate is null.
        /// </exception>
        public static T FindLast<T>(T[,,] array, Predicate<T> match)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            if (match == null)
                throw new ArgumentNullException(nameof(match));

            int uby = array.LengthY() - 1;
            int ubz = array.LengthZ() - 1;
            for (int x = array.LengthX() - 1; x >= 0; --x)
            {
                for (int y = uby; y >= 0; --y)
                {
                    for (int z = ubz; z >= 0; --z)
                    {
                        T val = array[x, y, z];
                        if (match(val))
                            return val;
                    }
                }
            }
            return default(T);
        }

        /// <summary>
        /// Searches the array for an element that matches the conditions defined by the predicate, returning the last occurrence.
        /// </summary><returns>
        /// The last element in the array that matches the conditions defined by the predicate, if a matching element exists; otherwise, the default value for type T.
        /// </returns>
        /// <param name="array">The array to search.</param>
        /// <param name="match">The Predicate&lt;T&gt; that defines the conditions of the element to search for.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the predicate is null.
        /// </exception>
        public static T FindLast<T>(ReadOnlyArray<T> array, Predicate<T> match) // Linq has LastOrDefault - but this is usually faster!
        {
            if (array.Array == null)
            {
                if (match == null)
                    throw new ArgumentNullException(nameof(match));

                return default(T);
            }
            return Array.FindLast<T>(array.Array, match);
        }

        /// <summary>
        /// Searches the array for an element that matches the conditions defined by the predicate, returning the last occurrence.
        /// </summary><returns>
        /// The last element in the array that matches the conditions defined by the predicate, if a matching element exists; otherwise, the default value for type T.
        /// </returns>
        /// <param name="array">The array to search.</param>
        /// <param name="match">The Predicate&lt;T&gt; that defines the conditions of the element to search for.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the predicate is null.
        /// </exception>
        public static T FindLast<T>(ArrayR2<T> array, Predicate<T> match)
        {
            if (array.Array == null)
            {
                if (match == null)
                    throw new ArgumentNullException(nameof(match));

                return default(T);
            }
            return FindLast<T>(array.Array, match);
        }

        /// <summary>
        /// Searches the array for an element that matches the conditions defined by the predicate, returning the last occurrence.
        /// </summary><returns>
        /// The last element in the array that matches the conditions defined by the predicate, if a matching element exists; otherwise, the default value for type T.
        /// </returns>
        /// <param name="array">The array to search.</param>
        /// <param name="match">The Predicate&lt;T&gt; that defines the conditions of the element to search for.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the predicate is null.
        /// </exception>
        public static T FindLast<T>(ReadOnlyArrayR2<T> array, Predicate<T> match)
        {
            if (array.Array == null)
            {
                if (match == null)
                    throw new ArgumentNullException(nameof(match));

                return default(T);
            }
            return FindLast<T>(array.Array, match);
        }

        /* wip
        /// <summary>
        /// Searches the array for an element that matches the conditions defined by the predicate, returning the last occurrence.
        /// </summary><returns>
        /// The last element in the array that matches the conditions defined by the predicate, if a matching element exists; otherwise, the default value for type T.
        /// </returns>
        /// <param name="array">The array to search.</param>
        /// <param name="match">The Predicate&lt;T&gt; that defines the conditions of the element to search for.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the predicate is null.
        /// </exception>
        public static T FindLast<T>(ArrayR3<T> array, Predicate<T> match)
        {
            if (array.Array == null)
            {
                if (match == null)
                    throw new ArgumentNullException(nameof(match));

                return default(T);
            }
            return FindLast<T>(array.Array, match);
        }
        
        /// <summary>
        /// Searches the array for an element that matches the conditions defined by the predicate, returning the last occurrence.
        /// </summary><returns>
        /// The last element in the array that matches the conditions defined by the predicate, if a matching element exists; otherwise, the default value for type T.
        /// </returns>
        /// <param name="array">The array to search.</param>
        /// <param name="match">The Predicate&lt;T&gt; that defines the conditions of the element to search for.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the predicate is null.
        /// </exception>
        public static T FindLast<T>(ReadOnlyArrayR3<T> array, Predicate<T> match)
        {
            if (array.Array == null)
            {
                if (match == null)
                    throw new ArgumentNullException(nameof(match));

                return default(T);
            }
            return FindLast<T>(array.Array, match);
        }
        */

        /// <summary>
        /// Searches the array for an element that matches the conditions defined by the predicate, returning the index of the first occurrence found.
        /// </summary><returns>
        /// The zero-based index of the first element in the array that matches the conditions defined by the predicate, if found; otherwise, -1.
        /// </returns>
        /// <param name="array">The array to search.</param>
        /// <param name="match">The Predicate&lt;T&gt; that defines the conditions of the element to search for.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array or predicate is null.
        /// </exception>
        public static int FindIndex<T>(this T[] array, Predicate<T> match)
        {
            return Array.FindIndex(array, match);
        }

        /// <summary>
        /// Searches the array for an element that matches the conditions defined by the predicate, returning the index of the first occurrence found.
        /// </summary><returns>
        /// The zero-based index of the first element in the array that matches the conditions defined by the predicate, if found; otherwise, -1.
        /// </returns>
        /// <param name="array">The array to search.</param>
        /// <param name="match">The Predicate&lt;T&gt; that defines the conditions of the element to search for.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array or predicate is null.
        /// </exception>
        public static int FindIndex<T>(this T[,] array, Predicate<T> match)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            if (match == null)
                throw new ArgumentNullException(nameof(match));

            int lenx = array.LengthX();
            int leny = array.LengthY();
            for (int x = 0; x < lenx; ++x)
                for (int y = 0; y < leny; ++y)
                    if (match(array[x, y]))
                        return x * leny + y;

            return -1;
        }

        /// <summary>
        /// Searches the array for an element that matches the conditions defined by the predicate, returning the index of the first occurrence found.
        /// </summary><returns>
        /// The zero-based index of the first element in the array that matches the conditions defined by the predicate, if found; otherwise, -1.
        /// </returns>
        /// <param name="array">The array to search.</param>
        /// <param name="match">The Predicate&lt;T&gt; that defines the conditions of the element to search for.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array or predicate is null.
        /// </exception>
        public static int FindIndex<T>(this T[,,] array, Predicate<T> match)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            if (match == null)
                throw new ArgumentNullException(nameof(match));

            int lenx = array.LengthX();
            int leny = array.LengthY();
            int lenz = array.LengthZ();
            for (int x = 0; x < lenx; ++x)
                for (int y = 0; y < leny; ++y)
                    for (int z = 0; z < lenz; ++z)
                        if (match(array[x, y, z]))
                            return (x * leny + y) * lenz + z;

            return -1;
        }

        /// <summary>
        /// Searches the array for an element that matches the conditions defined by the predicate, returning the index of the first occurrence found.
        /// </summary><returns>
        /// The zero-based index of the first element in the array that matches the conditions defined by the predicate, if found; otherwise, -1.
        /// </returns>
        /// <param name="array">The array to search.</param>
        /// <param name="match">The Predicate&lt;T&gt; that defines the conditions of the element to search for.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the predicate is null.
        /// </exception>
        public static int FindIndex<T>(this ReadOnlyArray<T> array, Predicate<T> match)
        {
            if (array.Array == null)
            {
                if (match == null)
                    throw new ArgumentNullException(nameof(match));

                return -1;
            }
            return Array.FindIndex<T>(array.Array, match);
        }

        /// <summary>
        /// Searches the array for an element that matches the conditions defined by the predicate, returning the index of the first occurrence found.
        /// </summary><returns>
        /// The zero-based index of the first element in the array that matches the conditions defined by the predicate, if found; otherwise, -1.
        /// </returns>
        /// <param name="array">The array to search.</param>
        /// <param name="match">The Predicate&lt;T&gt; that defines the conditions of the element to search for.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the predicate is null.
        /// </exception>
        public static int FindIndex<T>(this ArrayR2<T> array, Predicate<T> match)
        {
            if (array.Array == null)
            {
                if (match == null)
                    throw new ArgumentNullException(nameof(match));

                return -1;
            }
            return FindIndex<T>(array.Array, match);
        }

        /// <summary>
        /// Searches the array for an element that matches the conditions defined by the predicate, returning the index of the first occurrence found.
        /// </summary><returns>
        /// The zero-based index of the first element in the array that matches the conditions defined by the predicate, if found; otherwise, -1.
        /// </returns>
        /// <param name="array">The array to search.</param>
        /// <param name="match">The Predicate&lt;T&gt; that defines the conditions of the element to search for.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the predicate is null.
        /// </exception>
        public static int FindIndex<T>(this ReadOnlyArrayR2<T> array, Predicate<T> match)
        {
            if (array.Array == null)
            {
                if (match == null)
                    throw new ArgumentNullException(nameof(match));

                return -1;
            }
            return FindIndex<T>(array.Array, match);
        }

        /* wip
        /// <summary>
        /// Searches the array for an element that matches the conditions defined by the predicate, returning the index of the first occurrence found.
        /// </summary><returns>
        /// The zero-based index of the first element in the array that matches the conditions defined by the predicate, if found; otherwise, -1.
        /// </returns>
        /// <param name="array">The array to search.</param>
        /// <param name="match">The Predicate&lt;T&gt; that defines the conditions of the element to search for.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the predicate is null.
        /// </exception>
        public static int FindIndex<T>(this ArrayR3<T> array, Predicate<T> match)
        {
            if (array.Array == null)
            {
                if (match == null)
                    throw new ArgumentNullException(nameof(match));

                return -1;
            }
            return FindIndex<T>(array.Array, match);
        }
        
        /// <summary>
        /// Searches the array for an element that matches the conditions defined by the predicate, returning the index of the first occurrence found.
        /// </summary><returns>
        /// The zero-based index of the first element in the array that matches the conditions defined by the predicate, if found; otherwise, -1.
        /// </returns>
        /// <param name="array">The array to search.</param>
        /// <param name="match">The Predicate&lt;T&gt; that defines the conditions of the element to search for.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the predicate is null.
        /// </exception>
        public static int FindIndex<T>(this ReadOnlyArrayR3<T> array, Predicate<T> match)
        {
            if (array.Array == null)
            {
                if (match == null)
                    throw new ArgumentNullException(nameof(match));

                return -1;
            }
            return FindIndex<T>(array.Array, match);
        }
        */

        /// <summary>
        /// Searches the array, starting at the specified index, for an element that matches the conditions defined by the predicate, returning the index of the first occurrence found within that range.
        /// </summary><returns>
        /// The zero-based index of the first element in the searched range that matches the conditions defined by the predicate, if found; otherwise, -1.
        /// </returns>
        /// <param name="array">The array to search.</param>
        /// <param name="startIndex">The zero-based starting index of the search.</param>
        /// <param name="match">The Predicate&lt;T&gt; that defines the conditions of the element to search for.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array or predicate is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="startIndex"/> is less than zero or greater than the length of the array.
        /// </exception>
        public static int FindIndex<T>(this T[] array, int startIndex, Predicate<T> match)
        {
            return Array.FindIndex(array, startIndex, match);
        }

        /// <summary>
        /// Searches the array, starting at the specified index, for an element that matches the conditions defined by the predicate, returning the index of the first occurrence found within that range.
        /// </summary><returns>
        /// The zero-based index of the first element in the searched range that matches the conditions defined by the predicate, if found; otherwise, -1.
        /// </returns>
        /// <param name="array">The array to search.</param>
        /// <param name="startIndex">The zero-based starting index of the search.</param>
        /// <param name="match">The Predicate&lt;T&gt; that defines the conditions of the element to search for.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array or predicate is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="startIndex"/> is less than zero or greater than the length of the array.
        /// </exception>
        public static int FindIndex<T>(this T[,] array, int startIndex, Predicate<T> match)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            if (unchecked((uint)startIndex > (uint)array.Length))
                throw new ArgumentOutOfRangeException(nameof(startIndex));
            if (match == null)
                throw new ArgumentNullException(nameof(match));

            if (startIndex == array.Length)
                return -1;

            int leny = array.LengthY();
            int x = startIndex / leny; // Fast DivRem (div-part)
            int lenx = array.LengthX();
            int y = startIndex - x * leny; // Fast DivRem (mod-part)

            do
            {
                do
                {
                    if (match(array[x, y]))
                        return x * leny + y;
                }
                while (++y < leny);
                y = 0; // <-- set to zero after the above loop so the original y-argument is used exactly once.
            }
            while (++x < lenx);

            return -1;
        }

        /// <summary>
        /// Searches the array, starting at the specified index, for an element that matches the conditions defined by the predicate, returning the index of the first occurrence found within that range.
        /// </summary><returns>
        /// The zero-based index of the first element in the searched range that matches the conditions defined by the predicate, if found; otherwise, -1.
        /// </returns>
        /// <param name="array">The array to search.</param>
        /// <param name="startIndex">The zero-based starting index of the search.</param>
        /// <param name="match">The Predicate&lt;T&gt; that defines the conditions of the element to search for.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array or predicate is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="startIndex"/> is less than zero or greater than the length of the array.
        /// </exception>
        public static int FindIndex<T>(this T[,,] array, int startIndex, Predicate<T> match)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            if (unchecked((uint)startIndex > (uint)array.Length))
                throw new ArgumentOutOfRangeException(nameof(startIndex));
            if (match == null)
                throw new ArgumentNullException(nameof(match));

            if (startIndex == array.Length)
                return -1;
                
            int lenx = array.LengthX();
            int leny = array.LengthY();
            int lenz = array.LengthZ();
            int lenm = leny * lenz;
            int x = startIndex / lenm; // Fast DivRem (div-part 1)
            int m = startIndex - x * lenm; // Fast DivRem (mod-part 1)
            int y = m / lenz; // Fast DivRem (div-part 2)
            int z = m - y * lenz;  // Fast DivRem (mod-part 2)

            do
            {
                do
                {
                    do
                    {
                        if (match(array[x, y, z]))
                            return x * lenm + y * leny + z;
                    }
                    while (++z < lenz);
                    z = 0; // <-- set to zero after the above loop so the original z-argument is used exactly once.
                }
                while (++y < leny);
                y = 0; // <-- set to zero after the above loop so the original y-argument is used exactly once.
            }
            while (++x < lenx);

            return -1;
        }

        /// <summary>
        /// Searches the array, starting at the specified index, for an element that matches the conditions defined by the predicate, returning the index of the first occurrence found within that range.
        /// </summary><returns>
        /// The zero-based index of the first element in the searched range that matches the conditions defined by the predicate, if found; otherwise, -1.
        /// </returns>
        /// <param name="array">The array to search.</param>
        /// <param name="startIndex">The zero-based starting index of the search.</param>
        /// <param name="match">The Predicate&lt;T&gt; that defines the conditions of the element to search for.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the predicate is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="startIndex"/> is less than zero or greater than the length of the array.
        /// </exception>
        public static int FindIndex<T>(this ReadOnlyArray<T> array, int startIndex, Predicate<T> match)
        {
            return Array.FindIndex<T>(array.Array ?? Empty<T>.Array, startIndex, match);
        }

        /// <summary>
        /// Searches the array, starting at the specified index, for an element that matches the conditions defined by the predicate, returning the index of the first occurrence found within that range.
        /// </summary><returns>
        /// The zero-based index of the first element in the searched range that matches the conditions defined by the predicate, if found; otherwise, -1.
        /// </returns>
        /// <param name="array">The array to search.</param>
        /// <param name="startIndex">The zero-based starting index of the search.</param>
        /// <param name="match">The Predicate&lt;T&gt; that defines the conditions of the element to search for.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the predicate is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="startIndex"/> is less than zero or greater than the length of the array.
        /// </exception>
        public static int FindIndex<T>(this ArrayR2<T> array, int startIndex, Predicate<T> match)
        {
            return FindIndex<T>(array.Array ?? Empty<T>.ArrayR2, startIndex, match);
        }

        /// <summary>
        /// Searches the array, starting at the specified index, for an element that matches the conditions defined by the predicate, returning the index of the first occurrence found within that range.
        /// </summary><returns>
        /// The zero-based index of the first element in the searched range that matches the conditions defined by the predicate, if found; otherwise, -1.
        /// </returns>
        /// <param name="array">The array to search.</param>
        /// <param name="startIndex">The zero-based starting index of the search.</param>
        /// <param name="match">The Predicate&lt;T&gt; that defines the conditions of the element to search for.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the predicate is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="startIndex"/> is less than zero or greater than the length of the array.
        /// </exception>
        public static int FindIndex<T>(this ReadOnlyArrayR2<T> array, int startIndex, Predicate<T> match)
        {
            return FindIndex<T>(array.Array ?? Empty<T>.ArrayR2, startIndex, match);
        }

        /* wip
        /// <summary>
        /// Searches the array, starting at the specified index, for an element that matches the conditions defined by the predicate, returning the index of the first occurrence found within that range.
        /// </summary><returns>
        /// The zero-based index of the first element in the searched range that matches the conditions defined by the predicate, if found; otherwise, -1.
        /// </returns>
        /// <param name="array">The array to search.</param>
        /// <param name="startIndex">The zero-based starting index of the search.</param>
        /// <param name="match">The Predicate&lt;T&gt; that defines the conditions of the element to search for.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the predicate is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="startIndex"/> is less than zero or greater than the length of the array.
        /// </exception>
        public static int FindIndex<T>(this ArrayR3<T> array, int startIndex, Predicate<T> match)
        {
            return FindIndex<T>(array.Array ?? Empty<T>.ArrayR3, startIndex, match);
        }
        
        /// <summary>
        /// Searches the array, starting at the specified index, for an element that matches the conditions defined by the predicate, returning the index of the first occurrence found within that range.
        /// </summary><returns>
        /// The zero-based index of the first element in the searched range that matches the conditions defined by the predicate, if found; otherwise, -1.
        /// </returns>
        /// <param name="array">The array to search.</param>
        /// <param name="startIndex">The zero-based starting index of the search.</param>
        /// <param name="match">The Predicate&lt;T&gt; that defines the conditions of the element to search for.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the predicate is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="startIndex"/> is less than zero or greater than the length of the array.
        /// </exception>
        public static int FindIndex<T>(this ReadOnlyArrayR3<T> array, int startIndex, Predicate<T> match)
        {
            return FindIndex<T>(array.Array ?? Empty<T>.ArrayR3, startIndex, match);
        }
        */

        /// <summary>
        /// Searches the specified range of the array for an element that matches the conditions defined by the predicate, returning the index of the first occurrence found.
        /// </summary><returns>
        /// The zero-based index of the first element in the searched range that matches the conditions defined by the predicate, if found; otherwise, -1.
        /// </returns>
        /// <param name="array">The array to search.</param>
        /// <param name="startIndex">The zero-based starting index of the search.</param>
        /// <param name="count">The number of elements covered by the search range.</param>
        /// <param name="match">The Predicate&lt;T&gt; that defines the conditions of the element to search for.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array or predicate is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="startIndex"/> or <paramref name="count"/> is less than zero;
        /// or if <paramref name="startIndex"/> + <paramref name="count"/> is greater than the length of the array (or overflows).
        /// </exception>
        public static int FindIndex<T>(this T[] array, int startIndex, int count, Predicate<T> match)
        {
            return Array.FindIndex(array, startIndex, count, match);
        }

        /// <summary>
        /// Searches the specified range of the array for an element that matches the conditions defined by the predicate, returning the index of the first occurrence found.
        /// </summary><returns>
        /// The zero-based index of the first element in the searched range that matches the conditions defined by the predicate, if found; otherwise, -1.
        /// </returns>
        /// <param name="array">The array to search.</param>
        /// <param name="startIndex">The zero-based starting index of the search.</param>
        /// <param name="count">The number of elements covered by the search range.</param>
        /// <param name="match">The Predicate&lt;T&gt; that defines the conditions of the element to search for.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array or predicate is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="startIndex"/> or <paramref name="count"/> is less than zero;
        /// or if <paramref name="startIndex"/> + <paramref name="count"/> is greater than the length of the array (or overflows).
        /// </exception>
        public static int FindIndex<T>(this T[,] array, int startIndex, int count, Predicate<T> match)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            if (unchecked((uint)startIndex > (uint)array.Length))
                throw new ArgumentOutOfRangeException(nameof(startIndex));
            if (unchecked((uint)count > (uint)(array.Length - startIndex)))
                throw new ArgumentOutOfRangeException(nameof(count));
            if (match == null)
                throw new ArgumentNullException(nameof(match));

            if (count == 0)
                return -1;

            int lenx = array.LengthX();
            int leny = array.LengthY();
            int x = startIndex / leny; // Fast DivRem (div-part)
            int y = startIndex - x * leny; // Fast DivRem (mod-part)

            do
            {
                do
                {
                    if (match(array[x, y]))
                        return x * leny + y;
                    if (--count == 0)
                        return -1;
                }
                while (++y < leny);
                y = 0; // <-- set to zero after the above loop so the original y-argument is used exactly once.
            }
            while (++x < lenx);

            return -1;
        }

        /// <summary>
        /// Searches the specified range of the array for an element that matches the conditions defined by the predicate, returning the index of the first occurrence found.
        /// </summary><returns>
        /// The zero-based index of the first element in the searched range that matches the conditions defined by the predicate, if found; otherwise, -1.
        /// </returns>
        /// <param name="array">The array to search.</param>
        /// <param name="startIndex">The zero-based starting index of the search.</param>
        /// <param name="count">The number of elements covered by the search range.</param>
        /// <param name="match">The Predicate&lt;T&gt; that defines the conditions of the element to search for.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array or predicate is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="startIndex"/> or <paramref name="count"/> is less than zero;
        /// or if <paramref name="startIndex"/> + <paramref name="count"/> is greater than the length of the array (or overflows).
        /// </exception>
        public static int FindIndex<T>(this T[,,] array, int startIndex, int count, Predicate<T> match)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            if (unchecked((uint)startIndex > (uint)array.Length))
                throw new ArgumentOutOfRangeException(nameof(startIndex));
            if (unchecked((uint)count > (uint)(array.Length - startIndex)))
                throw new ArgumentOutOfRangeException(nameof(count));
            if (match == null)
                throw new ArgumentNullException(nameof(match));

            if (count == 0)
                return -1;
                
            int lenx = array.LengthX();
            int leny = array.LengthY();
            int lenz = array.LengthZ();
            int lenm = leny * lenz;
            int x = startIndex / lenm; // Fast DivRem (div-part 1)
            int m = startIndex - x * lenm; // Fast DivRem (mod-part 1)
            int y = m / lenz; // Fast DivRem (div-part 2)
            int z = m - y * lenz;  // Fast DivRem (mod-part 2)

            do
            {
                do
                {
                    do
                    {
                        if (match(array[x, y, z]))
                            return x * lenm + y * leny + z;
                        if (--count == 0)
                            return -1;
                    }
                    while (++z < lenz);
                    z = 0; // <-- set to zero after the above loop so the original z-argument is used exactly once.
                }
                while (++y < leny);
                y = 0; // <-- set to zero after the above loop so the original y-argument is used exactly once.
            }
            while (++x < lenx);

            return -1;
        }

        /// <summary>
        /// Searches the specified range of the array for an element that matches the conditions defined by the predicate, returning the index of the first occurrence found.
        /// </summary><returns>
        /// The zero-based index of the first element in the searched range that matches the conditions defined by the predicate, if found; otherwise, -1.
        /// </returns>
        /// <param name="array">The array to search.</param>
        /// <param name="startIndex">The zero-based starting index of the search.</param>
        /// <param name="count">The number of elements covered by the search range.</param>
        /// <param name="match">The Predicate&lt;T&gt; that defines the conditions of the element to search for.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the predicate is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="startIndex"/> or <paramref name="count"/> is less than zero;
        /// or if <paramref name="startIndex"/> + <paramref name="count"/> is greater than the length of the array (or overflows).
        /// </exception>
        public static int FindIndex<T>(this ReadOnlyArray<T> array, int startIndex, int count, Predicate<T> match)
        {
            return Array.FindIndex<T>(array.Array ?? Empty<T>.Array, startIndex, count, match);
        }

        /// <summary>
        /// Searches the specified range of the array for an element that matches the conditions defined by the predicate, returning the index of the first occurrence found.
        /// </summary><returns>
        /// The zero-based index of the first element in the searched range that matches the conditions defined by the predicate, if found; otherwise, -1.
        /// </returns>
        /// <param name="array">The array to search.</param>
        /// <param name="startIndex">The zero-based starting index of the search.</param>
        /// <param name="count">The number of elements covered by the search range.</param>
        /// <param name="match">The Predicate&lt;T&gt; that defines the conditions of the element to search for.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the predicate is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="startIndex"/> or <paramref name="count"/> is less than zero;
        /// or if <paramref name="startIndex"/> + <paramref name="count"/> is greater than the length of the array (or overflows).
        /// </exception>
        public static int FindIndex<T>(this ArrayR2<T> array, int startIndex, int count, Predicate<T> match)
        {
            return FindIndex(array.Array ?? Empty<T>.ArrayR2, startIndex, count, match);
        }

        /// <summary>
        /// Searches the specified range of the array for an element that matches the conditions defined by the predicate, returning the index of the first occurrence found.
        /// </summary><returns>
        /// The zero-based index of the first element in the searched range that matches the conditions defined by the predicate, if found; otherwise, -1.
        /// </returns>
        /// <param name="array">The array to search.</param>
        /// <param name="startIndex">The zero-based starting index of the search.</param>
        /// <param name="count">The number of elements covered by the search range.</param>
        /// <param name="match">The Predicate&lt;T&gt; that defines the conditions of the element to search for.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the predicate is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="startIndex"/> or <paramref name="count"/> is less than zero;
        /// or if <paramref name="startIndex"/> + <paramref name="count"/> is greater than the length of the array (or overflows).
        /// </exception>
        public static int FindIndex<T>(this ReadOnlyArrayR2<T> array, int startIndex, int count, Predicate<T> match)
        {
            return FindIndex(array.Array ?? Empty<T>.ArrayR2, startIndex, count, match);
        }

        /* wip
        /// <summary>
        /// Searches the specified range of the array for an element that matches the conditions defined by the predicate, returning the index of the first occurrence found.
        /// </summary><returns>
        /// The zero-based index of the first element in the searched range that matches the conditions defined by the predicate, if found; otherwise, -1.
        /// </returns>
        /// <param name="array">The array to search.</param>
        /// <param name="startIndex">The zero-based starting index of the search.</param>
        /// <param name="count">The number of elements covered by the search range.</param>
        /// <param name="match">The Predicate&lt;T&gt; that defines the conditions of the element to search for.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the predicate is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="startIndex"/> or <paramref name="count"/> is less than zero;
        /// or if <paramref name="startIndex"/> + <paramref name="count"/> is greater than the length of the array (or overflows).
        /// </exception>
        public static int FindIndex<T>(this ArrayR3<T> array, int startIndex, int count, Predicate<T> match)
        {
            return FindIndex(array.Array ?? Empty<T>.ArrayR3, startIndex, count, match);
        }
        
        /// <summary>
        /// Searches the specified range of the array for an element that matches the conditions defined by the predicate, returning the index of the first occurrence found.
        /// </summary><returns>
        /// The zero-based index of the first element in the searched range that matches the conditions defined by the predicate, if found; otherwise, -1.
        /// </returns>
        /// <param name="array">The array to search.</param>
        /// <param name="startIndex">The zero-based starting index of the search.</param>
        /// <param name="count">The number of elements covered by the search range.</param>
        /// <param name="match">The Predicate&lt;T&gt; that defines the conditions of the element to search for.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the predicate is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="startIndex"/> or <paramref name="count"/> is less than zero;
        /// or if <paramref name="startIndex"/> + <paramref name="count"/> is greater than the length of the array (or overflows).
        /// </exception>
        public static int FindIndex<T>(this ReadOnlyArrayR3<T> array, int startIndex, int count, Predicate<T> match)
        {
            return FindIndex(array.Array ?? Empty<T>.ArrayR3, startIndex, count, match);
        }
        */

        /// <summary>
        /// Searches the array backwards for an element that matches the conditions defined by the predicate, returning the index of the last occurrence in the array.
        /// </summary><returns>
        /// The zero-based index of the last element in the array that matches the conditions defined by the predicate, if found; otherwise, -1.
        /// </returns>
        /// <param name="array">The array to search.</param>
        /// <param name="match">The Predicate&lt;T&gt; that defines the conditions of the element to search for.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array or predicate is null.
        /// </exception>
        public static int FindLastIndex<T>(T[] array, Predicate<T> match)
        {
            return Array.FindLastIndex<T>(array, match);
        }

        /// <summary>
        /// Searches the array backwards for an element that matches the conditions defined by the predicate, returning the index of the last occurrence in the array.
        /// </summary><returns>
        /// The zero-based index of the last element in the array that matches the conditions defined by the predicate, if found; otherwise, -1.
        /// </returns>
        /// <param name="array">The array to search.</param>
        /// <param name="match">The Predicate&lt;T&gt; that defines the conditions of the element to search for.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array or predicate is null.
        /// </exception>
        public static int FindLastIndex<T>(T[,] array, Predicate<T> match)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            if (match == null)
                throw new ArgumentNullException(nameof(match));

            int leny = array.LengthY();
            for (int x = array.LengthX() - 1; x >= 0; --x)
                for (int y = leny - 1; y >= 0; --y)
                    if (match(array[x, y]))
                        return x * leny + y;

            return -1;
        }

        /// <summary>
        /// Searches the array backwards for an element that matches the conditions defined by the predicate, returning the index of the last occurrence in the array.
        /// </summary><returns>
        /// The zero-based index of the last element in the array that matches the conditions defined by the predicate, if found; otherwise, -1.
        /// </returns>
        /// <param name="array">The array to search.</param>
        /// <param name="match">The Predicate&lt;T&gt; that defines the conditions of the element to search for.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array or predicate is null.
        /// </exception>
        public static int FindLastIndex<T>(T[,,] array, Predicate<T> match)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            if (match == null)
                throw new ArgumentNullException(nameof(match));

            int x = array.LengthX() - 1;
            if (x >= 0) // minor optimization (a convoluted way of doing 3 nested for-loops...)
            {
                int leny = array.LengthY(); // (...to avoid these two calls if LengthX is zero)
                int lenz = array.LengthZ();

                do
                {
                    for (int y = leny - 1; y >= 0; --y)
                        for (int z = lenz - 1; z >= 0; --z)
                            if (match(array[x, y, z]))
                                return (x * leny + y) * lenz + z;
                }
                while (--x >= 0);
            }

            return -1;
        }

        /// <summary>
        /// Searches the array backwards for an element that matches the conditions defined by the predicate, returning the index of the last occurrence in the array.
        /// </summary><returns>
        /// The zero-based index of the last element in the array that matches the conditions defined by the predicate, if found; otherwise, -1.
        /// </returns>
        /// <param name="array">The array to search.</param>
        /// <param name="match">The Predicate&lt;T&gt; that defines the conditions of the element to search for.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array or predicate is null.
        /// </exception>
        public static int FindLastIndex<T>(ReadOnlyArray<T> array, Predicate<T> match)
        {
            if (array.Array == null)
            {
                if (match == null)
                    throw new ArgumentNullException(nameof(match));

                return -1;
            }
            return Array.FindLastIndex(array.Array, match);
        }

        /// <summary>
        /// Searches the array backwards for an element that matches the conditions defined by the predicate, returning the index of the last occurrence in the array.
        /// </summary><returns>
        /// The zero-based index of the last element in the array that matches the conditions defined by the predicate, if found; otherwise, -1.
        /// </returns>
        /// <param name="array">The array to search.</param>
        /// <param name="match">The Predicate&lt;T&gt; that defines the conditions of the element to search for.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array or predicate is null.
        /// </exception>
        public static int FindLastIndex<T>(ArrayR2<T> array, Predicate<T> match)
        {
            if (array.Array == null)
            {
                if (match == null)
                    throw new ArgumentNullException(nameof(match));

                return -1;
            }
            return FindLastIndex(array.Array, match);
        }

        /// <summary>
        /// Searches the array backwards for an element that matches the conditions defined by the predicate, returning the index of the last occurrence in the array.
        /// </summary><returns>
        /// The zero-based index of the last element in the array that matches the conditions defined by the predicate, if found; otherwise, -1.
        /// </returns>
        /// <param name="array">The array to search.</param>
        /// <param name="match">The Predicate&lt;T&gt; that defines the conditions of the element to search for.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array or predicate is null.
        /// </exception>
        public static int FindLastIndex<T>(ReadOnlyArrayR2<T> array, Predicate<T> match)
        {
            if (array.Array == null)
            {
                if (match == null)
                    throw new ArgumentNullException(nameof(match));

                return -1;
            }
            return FindLastIndex(array.Array, match);
        }

        /* wip
        /// <summary>
        /// Searches the array backwards for an element that matches the conditions defined by the predicate, returning the index of the last occurrence in the array.
        /// </summary><returns>
        /// The zero-based index of the last element in the array that matches the conditions defined by the predicate, if found; otherwise, -1.
        /// </returns>
        /// <param name="array">The array to search.</param>
        /// <param name="match">The Predicate&lt;T&gt; that defines the conditions of the element to search for.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array or predicate is null.
        /// </exception>
        public static int FindLastIndex<T>(ArrayR3<T> array, Predicate<T> match)
        {
            if (array.Array == null)
            {
                if (match == null)
                    throw new ArgumentNullException(nameof(match));

                return -1;
            }
            return FindLastIndex(array.Array, match);
        }
        
        /// <summary>
        /// Searches the array backwards for an element that matches the conditions defined by the predicate, returning the index of the last occurrence in the array.
        /// </summary><returns>
        /// The zero-based index of the last element in the array that matches the conditions defined by the predicate, if found; otherwise, -1.
        /// </returns>
        /// <param name="array">The array to search.</param>
        /// <param name="match">The Predicate&lt;T&gt; that defines the conditions of the element to search for.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array or predicate is null.
        /// </exception>
        public static int FindLastIndex<T>(ReadOnlyArrayR3<T> array, Predicate<T> match)
        {
            if (array.Array == null)
            {
                if (match == null)
                    throw new ArgumentNullException(nameof(match));

                return -1;
            }
            return FindLastIndex(array.Array, match);
        }
        */

        /// <summary>
        /// Searches the array backwards, starting at the specified index, for an element that matches the conditions defined by the predicate, returning the index of the last occurrence in that range.
        /// </summary><remarks>
        /// <b>This method differs slightly from its System.Array counterpart:</b><br/>
        /// Whereas this method allows <paramref name="startIndex"/> to be -1 regardless of array length, the System.Array version
        /// allows <paramref name="startIndex"/> to be -1 if and only if the length of the <paramref name="array"/> is zero.<br/>
        /// This seemed oddly inconsistent with forward searching methods where going one step out of bounds (i.e. startIndex = array.Length)
        /// is always permitted (regardless of array length). Since the cost of this oddity included a convoluted contract, a convoluted
        /// documentation (not really since this oddity was in fact not documented!), and suboptimal usability, it was deemed costlier to
        /// mimic this behavior than to allow what is arguably a small and rather obscure inconsistency with its System.Array counterpart.
        /// </remarks><returns>
        /// The zero-based index of the last element in the searched range that matches the conditions defined by the predicate, if found; otherwise, -1.
        /// </returns>
        /// <param name="array">The array to search.</param>
        /// <param name="startIndex">The zero-based starting index of the backwards search (i.e. the highest index in the search range).</param>
        /// <param name="match">The Predicate&lt;T&gt; that defines the conditions of the element to search for.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array or predicate is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="startIndex"/> is less than -1 or greater than or equal to the length of the array.
        /// </exception>
        public static int FindLastIndex<T>(T[] array, int startIndex, Predicate<T> match)
        {
            if (match == null)
                throw new ArgumentNullException(nameof(match));
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            if (unchecked((uint)(startIndex + 1) > (uint)array.Length))
                throw new ArgumentOutOfRangeException(nameof(startIndex));

            for (int i = startIndex; i >= 0; --i)
                if (match(array[i]))
                    return i;

            return -1;
        }

        /// <summary>
        /// Searches the array backwards, starting at the specified index, for an element that matches the conditions defined by the predicate, returning the index of the last occurrence in that range.
        /// </summary><returns>
        /// The zero-based index of the last element in the searched range that matches the conditions defined by the predicate, if found; otherwise, -1.
        /// </returns>
        /// <inheritdoc cref="FindLastIndex{T}(T[], int, Predicate{T})" select="remarks"/>
        /// <param name="array">The array to search.</param>
        /// <param name="startIndex">The zero-based starting index of the backwards search (i.e. the highest index in the search range).</param>
        /// <param name="match">The Predicate&lt;T&gt; that defines the conditions of the element to search for.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array or predicate is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="startIndex"/> is less than -1 or greater than or equal to the length of the array.
        /// </exception>
        public static int FindLastIndex<T>(T[,] array, int startIndex, Predicate<T> match)
        {
            if (match == null)
                throw new ArgumentNullException(nameof(match));
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            if (unchecked((uint)(startIndex + 1) > (uint)array.Length))
                throw new ArgumentOutOfRangeException(nameof(startIndex));

            if (startIndex == -1)
                return -1;

            int leny = array.LengthY();
            int x = startIndex / leny; // Fast DivRem (div-part)
            int y = startIndex - x * leny; // Fast DivRem (mod-part)

            do
            {
                do
                {
                    if (match(array[x, y]))
                        return x * leny + y;
                }
                while (--y >= 0);
                y += leny; // <-- reset after the above loop so the original y-argument is used exactly once.
            }
            while (--x >= 0);

            return -1;
        }

        /// <summary>
        /// Searches the array backwards, starting at the specified index, for an element that matches the conditions defined by the predicate, returning the index of the last occurrence in that range.
        /// </summary><returns>
        /// The zero-based index of the last element in the searched range that matches the conditions defined by the predicate, if found; otherwise, -1.
        /// </returns>
        /// <inheritdoc cref="FindLastIndex{T}(T[], int, Predicate{T})" select="remarks"/>
        /// <param name="array">The array to search.</param>
        /// <param name="startIndex">The zero-based starting index of the backwards search (i.e. the highest index in the search range).</param>
        /// <param name="match">The Predicate&lt;T&gt; that defines the conditions of the element to search for.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array or predicate is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="startIndex"/> is less than -1 or greater than or equal to the length of the array.
        /// </exception>
        public static int FindLastIndex<T>(T[,,] array, int startIndex, Predicate<T> match)
        {
            if (match == null)
                throw new ArgumentNullException(nameof(match));
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            if (unchecked((uint)(startIndex + 1) > (uint)array.Length))
                throw new ArgumentOutOfRangeException(nameof(startIndex));

            if (startIndex == -1)
                return -1;

            int leny = array.LengthY();
            int lenz = array.LengthZ();
            int lenm = leny * lenz;
            int x = startIndex / lenm; // Fast DivRem (div-part 1)
            int m = startIndex - x * lenm; // Fast DivRem (mod-part 1)
            int y = m / lenz; // Fast DivRem (div-part 2)
            int z = m - y * lenz;  // Fast DivRem (mod-part 2)

            do
            {
                do
                {
                    do
                    {
                        if (match(array[x, y, z]))
                            return x * lenm + y * leny + z;
                    }
                    while (--z >= 0);
                    z += lenz; // <-- reset after the above loop so the original z-argument is used exactly once.
                }
                while (--y >= 0);
                y += leny; // <-- reset after the above loop so the original y-argument is used exactly once.
            }
            while (--x >= 0);

            return -1;
        }

        /// <summary>
        /// Searches the array backwards, starting at the specified index, for an element that matches the conditions defined by the predicate, returning the index of the last occurrence in that range.
        /// </summary><returns>
        /// The zero-based index of the last element in the searched range that matches the conditions defined by the predicate, if found; otherwise, -1.
        /// </returns>
        /// <inheritdoc cref="FindLastIndex{T}(T[], int, Predicate{T})" select="remarks"/>
        /// <param name="array">The array to search.</param>
        /// <param name="startIndex">The zero-based starting index of the backwards search (i.e. the highest index in the search range).</param>
        /// <param name="match">The Predicate&lt;T&gt; that defines the conditions of the element to search for.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the predicate is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="startIndex"/> is less than -1 or greater than or equal to the length of the array.
        /// </exception>
        public static int FindLastIndex<T>(ReadOnlyArray<T> array, int startIndex, Predicate<T> match)
        {
            return FindLastIndex(array.Array ?? Empty<T>.Array, startIndex, match);
        }

        /// <summary>
        /// Searches the array backwards, starting at the specified index, for an element that matches the conditions defined by the predicate, returning the index of the last occurrence in that range.
        /// </summary><returns>
        /// The zero-based index of the last element in the searched range that matches the conditions defined by the predicate, if found; otherwise, -1.
        /// </returns>
        /// <inheritdoc cref="FindLastIndex{T}(T[], int, Predicate{T})" select="remarks"/>
        /// <param name="array">The array to search.</param>
        /// <param name="startIndex">The zero-based starting index of the backwards search (i.e. the highest index in the search range).</param>
        /// <param name="match">The Predicate&lt;T&gt; that defines the conditions of the element to search for.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the predicate is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="startIndex"/> is less than -1 or greater than or equal to the length of the array.
        /// </exception>
        public static int FindLastIndex<T>(ArrayR2<T> array, int startIndex, Predicate<T> match)
        {
            return FindLastIndex(array.Array ?? Empty<T>.ArrayR2, startIndex, match);
        }

        /// <summary>
        /// Searches the array backwards, starting at the specified index, for an element that matches the conditions defined by the predicate, returning the index of the last occurrence in that range.
        /// </summary><returns>
        /// The zero-based index of the last element in the searched range that matches the conditions defined by the predicate, if found; otherwise, -1.
        /// </returns>
        /// <inheritdoc cref="FindLastIndex{T}(T[], int, Predicate{T})" select="remarks"/>
        /// <param name="array">The array to search.</param>
        /// <param name="startIndex">The zero-based starting index of the backwards search (i.e. the highest index in the search range).</param>
        /// <param name="match">The Predicate&lt;T&gt; that defines the conditions of the element to search for.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the predicate is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="startIndex"/> is less than -1 or greater than or equal to the length of the array.
        /// </exception>
        public static int FindLastIndex<T>(ReadOnlyArrayR2<T> array, int startIndex, Predicate<T> match)
        {
            return FindLastIndex(array.Array ?? Empty<T>.ArrayR2, startIndex, match);
        }

        /* wip
        /// <summary>
        /// Searches the array backwards, starting at the specified index, for an element that matches the conditions defined by the predicate, returning the index of the last occurrence in that range.
        /// </summary><returns>
        /// The zero-based index of the last element in the searched range that matches the conditions defined by the predicate, if found; otherwise, -1.
        /// </returns>
        /// <inheritdoc cref="FindLastIndex{T}(T[], int, Predicate{T})" select="remarks"/>
        /// <param name="array">The array to search.</param>
        /// <param name="startIndex">The zero-based starting index of the backwards search (i.e. the highest index in the search range).</param>
        /// <param name="match">The Predicate&lt;T&gt; that defines the conditions of the element to search for.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the predicate is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="startIndex"/> is less than -1 or greater than or equal to the length of the array.
        /// </exception>
        public static int FindLastIndex<T>(ArrayR3<T> array, int startIndex, Predicate<T> match)
        {
            return FindLastIndex(array.Array ?? Empty<T>.ArrayR3, startIndex, match);
        }
        
        /// <summary>
        /// Searches the array backwards, starting at the specified index, for an element that matches the conditions defined by the predicate, returning the index of the last occurrence in that range.
        /// </summary><returns>
        /// The zero-based index of the last element in the searched range that matches the conditions defined by the predicate, if found; otherwise, -1.
        /// </returns>
        /// <inheritdoc cref="FindLastIndex{T}(T[], int, Predicate{T})" select="remarks"/>
        /// <param name="array">The array to search.</param>
        /// <param name="startIndex">The zero-based starting index of the backwards search (i.e. the highest index in the search range).</param>
        /// <param name="match">The Predicate&lt;T&gt; that defines the conditions of the element to search for.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the predicate is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="startIndex"/> is less than -1 or greater than or equal to the length of the array.
        /// </exception>
        public static int FindLastIndex<T>(ReadOnlyArrayR3<T> array, int startIndex, Predicate<T> match)
        {
            return FindLastIndex(array.Array ?? Empty<T>.ArrayR3, startIndex, match);
        }
        */

        /// <summary>
        /// Searches the array, starting at the specified index and proceeding backwards over a range of <paramref name="count"/> elements,
        /// for an element that matches the conditions defined by the predicate, returning the index of the last occurrence in that range.
        /// </summary><returns>
        /// The zero-based index of the last element in the searched range that matches the conditions defined by the predicate, if found; otherwise, -1.
        /// </returns>
        /// <inheritdoc cref="FindLastIndex{T}(T[], int, Predicate{T})" select="remarks"/>
        /// <param name="array">The array to search.</param>
        /// <param name="startIndex">The zero-based starting index of the backwards search (i.e. the highest index in the search range).</param>
        /// <param name="count">The number of elements covered by the search range.</param>
        /// <param name="match">The Predicate&lt;T&gt; that defines the conditions of the element to search for.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array or predicate is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="startIndex"/> is less than -1 or greater than or equal to the length of the array.
        /// Also thrown if either <paramref name="count"/> or [<paramref name="startIndex"/> - <paramref name="count"/> + 1] is less than zero.
        /// </exception>
        public static int FindLastIndex<T>(T[] array, int startIndex, int count, Predicate<T> match)
        {
            if (match == null)
                throw new ArgumentNullException(nameof(match));
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            if (unchecked((uint)(startIndex + 1) > (uint)array.Length))
                throw new ArgumentOutOfRangeException(nameof(startIndex));

            int lastIndex = unchecked(startIndex + 1 - count);

            if ((count | lastIndex) < 0)
                throw new ArgumentOutOfRangeException(nameof(count));
            
            for (int i = startIndex; i >= lastIndex; --i)
                if (match(array[i]))
                    return i;

            return -1;
        }

        /// <summary>
        /// Searches the array, starting at the specified index and proceeding backwards over a range of <paramref name="count"/> elements,
        /// for an element that matches the conditions defined by the predicate, returning the index of the last occurrence in that range.
        /// </summary><returns>
        /// The zero-based index of the last element in the searched range that matches the conditions defined by the predicate, if found; otherwise, -1.
        /// </returns>
        /// <inheritdoc cref="FindLastIndex{T}(T[], int, Predicate{T})" select="remarks"/>
        /// <param name="array">The array to search.</param>
        /// <param name="startIndex">The zero-based starting index of the backwards search (i.e. the highest index in the search range).</param>
        /// <param name="count">The number of elements covered by the search range.</param>
        /// <param name="match">The Predicate&lt;T&gt; that defines the conditions of the element to search for.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array or predicate is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="startIndex"/> is less than -1 or greater than or equal to the length of the array.
        /// Also thrown if either <paramref name="count"/> or [<paramref name="startIndex"/> - <paramref name="count"/> + 1] is less than zero.
        /// </exception>
        public static int FindLastIndex<T>(T[,] array, int startIndex, int count, Predicate<T> match)
        {
            if (match == null)
                throw new ArgumentNullException(nameof(match));
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            if (unchecked((uint)(startIndex + 1) > (uint)array.Length))
                throw new ArgumentOutOfRangeException(nameof(startIndex));
            if (unchecked(startIndex + 1 - count | count) < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            if (count == 0)
                return -1;

            int leny = array.LengthY();
            int x = startIndex / leny; // Fast DivRem (div-part)
            int y = startIndex - x * leny; // Fast DivRem (mod-part)

            do
            {
                do
                {
                    if (match(array[x, y]))
                        return x * leny + y;
                    if (--count == 0)
                        return -1;
                }
                while (--y >= 0);
                y += leny; // <-- reset after the above loop so the original y-argument is used exactly once.
            }
            while (--x >= 0);

            return -1;
        }

        /// <summary>
        /// Searches the array, starting at the specified index and proceeding backwards over a range of <paramref name="count"/> elements,
        /// for an element that matches the conditions defined by the predicate, returning the index of the last occurrence in that range.
        /// </summary><returns>
        /// The zero-based index of the last element in the searched range that matches the conditions defined by the predicate, if found; otherwise, -1.
        /// </returns>
        /// <inheritdoc cref="FindLastIndex{T}(T[], int, Predicate{T})" select="remarks"/>
        /// <param name="array">The array to search.</param>
        /// <param name="startIndex">The zero-based starting index of the backwards search (i.e. the highest index in the search range).</param>
        /// <param name="count">The number of elements covered by the search range.</param>
        /// <param name="match">The Predicate&lt;T&gt; that defines the conditions of the element to search for.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array or predicate is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="startIndex"/> is less than -1 or greater than or equal to the length of the array.
        /// Also thrown if either <paramref name="count"/> or [<paramref name="startIndex"/> - <paramref name="count"/> + 1] is less than zero.
        /// </exception>
        public static int FindLastIndex<T>(T[,,] array, int startIndex, int count, Predicate<T> match)
        {
            if (match == null)
                throw new ArgumentNullException(nameof(match));
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            if (unchecked((uint)(startIndex + 1) > (uint)array.Length))
                throw new ArgumentOutOfRangeException(nameof(startIndex));
            if (unchecked(startIndex + 1 - count | count) < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            if (count == 0)
                return -1;

            int leny = array.LengthY();
            int lenz = array.LengthZ();
            int lenm = leny * lenz;
            int x = startIndex / lenm; // Fast DivRem (div-part 1)
            int m = startIndex - x * lenm; // Fast DivRem (mod-part 1)
            int y = m / lenz; // Fast DivRem (div-part 2)
            int z = m - y * lenz;  // Fast DivRem (mod-part 2)

            do
            {
                do
                {
                    do
                    {
                        if (match(array[x, y, z]))
                            return x * lenm + y * leny + z;
                        if (--count == 0)
                            return -1;
                    }
                    while (--z >= 0);
                    z += lenz; // <-- reset after the above loop so the original z-argument is used exactly once.
                }
                while (--y >= 0);
                y += leny; // <-- reset after the above loop so the original y-argument is used exactly once.
            }
            while (--x >= 0);

            return -1;
        }

        /// <summary>
        /// Searches the array, starting at the specified index and proceeding backwards over a range of <paramref name="count"/> elements,
        /// for an element that matches the conditions defined by the predicate, returning the index of the last occurrence in that range.
        /// </summary><returns>
        /// The zero-based index of the last element in the searched range that matches the conditions defined by the predicate, if found; otherwise, -1.
        /// </returns>
        /// <inheritdoc cref="FindLastIndex{T}(T[], int, Predicate{T})" select="remarks"/>
        /// <param name="array">The array to search.</param>
        /// <param name="startIndex">The zero-based starting index of the backwards search (i.e. the highest index in the search range).</param>
        /// <param name="count">The number of elements covered by the search range.</param>
        /// <param name="match">The Predicate&lt;T&gt; that defines the conditions of the element to search for.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the predicate is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="startIndex"/> is less than -1 or greater than or equal to the length of the array.
        /// Also thrown if either <paramref name="count"/> or [<paramref name="startIndex"/> - <paramref name="count"/> + 1] is less than zero.
        /// </exception>
        public static int FindLastIndex<T>(ReadOnlyArray<T> array, int startIndex, int count, Predicate<T> match)
        {
            return FindLastIndex<T>(array.Array ?? Empty<T>.Array, startIndex, count, match);
        }

        /// <summary>
        /// Searches the array, starting at the specified index and proceeding backwards over a range of <paramref name="count"/> elements,
        /// for an element that matches the conditions defined by the predicate, returning the index of the last occurrence in that range.
        /// </summary><returns>
        /// The zero-based index of the last element in the searched range that matches the conditions defined by the predicate, if found; otherwise, -1.
        /// </returns>
        /// <inheritdoc cref="FindLastIndex{T}(T[], int, Predicate{T})" select="remarks"/>
        /// <param name="array">The array to search.</param>
        /// <param name="startIndex">The zero-based starting index of the backwards search (i.e. the highest index in the search range).</param>
        /// <param name="count">The number of elements covered by the search range.</param>
        /// <param name="match">The Predicate&lt;T&gt; that defines the conditions of the element to search for.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the predicate is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="startIndex"/> is less than -1 or greater than or equal to the length of the array.
        /// Also thrown if either <paramref name="count"/> or [<paramref name="startIndex"/> - <paramref name="count"/> + 1] is less than zero.
        /// </exception>
        public static int FindLastIndex<T>(ArrayR2<T> array, int startIndex, int count, Predicate<T> match)
        {
            return FindLastIndex(array.Array ?? Empty<T>.ArrayR2, startIndex, count, match);
        }

        /// <summary>
        /// Searches the array, starting at the specified index and proceeding backwards over a range of <paramref name="count"/> elements,
        /// for an element that matches the conditions defined by the predicate, returning the index of the last occurrence in that range.
        /// </summary><returns>
        /// The zero-based index of the last element in the searched range that matches the conditions defined by the predicate, if found; otherwise, -1.
        /// </returns>
        /// <inheritdoc cref="FindLastIndex{T}(T[], int, Predicate{T})" select="remarks"/>
        /// <param name="array">The array to search.</param>
        /// <param name="startIndex">The zero-based starting index of the backwards search (i.e. the highest index in the search range).</param>
        /// <param name="count">The number of elements covered by the search range.</param>
        /// <param name="match">The Predicate&lt;T&gt; that defines the conditions of the element to search for.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the predicate is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="startIndex"/> is less than -1 or greater than or equal to the length of the array.
        /// Also thrown if either <paramref name="count"/> or [<paramref name="startIndex"/> - <paramref name="count"/> + 1] is less than zero.
        /// </exception>
        public static int FindLastIndex<T>(ReadOnlyArrayR2<T> array, int startIndex, int count, Predicate<T> match)
        {
            return FindLastIndex(array.Array ?? Empty<T>.ArrayR2, startIndex, count, match);
        }

        /* wip
        /// <summary>
        /// Searches the array, starting at the specified index and proceeding backwards over a range of <paramref name="count"/> elements,
        /// for an element that matches the conditions defined by the predicate, returning the index of the last occurrence in that range.
        /// </summary><returns>
        /// The zero-based index of the last element in the searched range that matches the conditions defined by the predicate, if found; otherwise, -1.
        /// </returns>
        /// <inheritdoc cref="FindLastIndex{T}(T[], int, Predicate{T})" select="remarks"/>
        /// <param name="array">The array to search.</param>
        /// <param name="startIndex">The zero-based starting index of the backwards search (i.e. the highest index in the search range).</param>
        /// <param name="count">The number of elements covered by the search range.</param>
        /// <param name="match">The Predicate&lt;T&gt; that defines the conditions of the element to search for.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the predicate is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="startIndex"/> is less than -1 or greater than or equal to the length of the array.
        /// Also thrown if either <paramref name="count"/> or [<paramref name="startIndex"/> - <paramref name="count"/> + 1] is less than zero.
        /// </exception>
        public static int FindLastIndex<T>(ArrayR3<T> array, int startIndex, int count, Predicate<T> match)
        {
            return FindLastIndex(array.Array ?? Empty<T>.ArrayR3, startIndex, count, match);
        }
        
        /// <summary>
        /// Searches the array, starting at the specified index and proceeding backwards over a range of <paramref name="count"/> elements,
        /// for an element that matches the conditions defined by the predicate, returning the index of the last occurrence in that range.
        /// </summary><returns>
        /// The zero-based index of the last element in the searched range that matches the conditions defined by the predicate, if found; otherwise, -1.
        /// </returns>
        /// <inheritdoc cref="FindLastIndex{T}(T[], int, Predicate{T})" select="remarks"/>
        /// <param name="array">The array to search.</param>
        /// <param name="startIndex">The zero-based starting index of the backwards search (i.e. the highest index in the search range).</param>
        /// <param name="count">The number of elements covered by the search range.</param>
        /// <param name="match">The Predicate&lt;T&gt; that defines the conditions of the element to search for.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the predicate is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="startIndex"/> is less than -1 or greater than or equal to the length of the array.
        /// Also thrown if either <paramref name="count"/> or [<paramref name="startIndex"/> - <paramref name="count"/> + 1] is less than zero.
        /// </exception>
        public static int FindLastIndex<T>(ReadOnlyArrayR3<T> array, int startIndex, int count, Predicate<T> match)
        {
            return FindLastIndex(array.Array ?? Empty<T>.ArrayR3, startIndex, count, match);
        }
        */

        /// <summary>
        /// Searches the array for the specified value and returns the index of its first occurrence.
        /// </summary><remarks>
        /// This method performs an equality comparison by calling the <b>T.Equals</b> method on every element.
        /// This means that if <typeparamref name="T"/> overrides the Equals method, that override is called.
        /// </remarks><returns>
        /// The zero-based index of the first occurrence of <paramref name="value"/> in the array; or –1 if no equal value was found.
        /// </returns>
        /// <param name="array">The array to search.</param>
        /// <param name="value">The value to locate in the array.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array is null.
        /// </exception>
        public static int IndexOf<T>(this T[] array, T value)
        {
            return Array.IndexOf<T>(array, value);
        }

        /// <summary>
        /// Searches the array for the specified value and returns the index of its first occurrence.
        /// </summary><returns>
        /// The zero-based index of the first occurrence of <paramref name="value"/> in the array; or –1 if no equal value was found.
        /// </returns>
        /// <inheritdoc cref="IndexOf{T}(T[], T)" select="remarks"/>
        /// <param name="array">The array to search.</param>
        /// <param name="value">The value to locate in the array.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array is null.
        /// </exception>
        public static int IndexOf<T>(this T[,] array, T value)
        {
            return IndexFinder<T>.instance.IndexOf(array, ref value);
        }

        /// <summary>
        /// Searches the array for the specified value and returns the index of its first occurrence.
        /// </summary><returns>
        /// The zero-based index of the first occurrence of <paramref name="value"/> in the array; or –1 if no equal value was found.
        /// </returns>
        /// <inheritdoc cref="IndexOf{T}(T[], T)" select="remarks"/>
        /// <param name="array">The array to search.</param>
        /// <param name="value">The value to locate in the array.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array is null.
        /// </exception>
        public static int IndexOf<T>(this T[,,] array, T value)
        {
            return IndexFinder<T>.instance.IndexOf(array, ref value);
        }

        /// <summary>
        /// Searches the array for the specified value and returns the index of its first occurrence.
        /// </summary><returns>
        /// The zero-based index of the first occurrence of <paramref name="value"/> in the array; or –1 if no equal value was found.
        /// </returns>
        /// <inheritdoc cref="IndexOf{T}(T[], T)" select="remarks"/>
        /// <param name="array">The array to search.</param>
        /// <param name="value">The value to locate in the array.</param>
        public static int IndexOf<T>(this ReadOnlyArray<T> array, T value)
        {
            return array.Array == null ? -1 : Array.IndexOf<T>(array.Array, value);
        }

        /// <summary>
        /// Searches the array for the specified value and returns the index of its first occurrence.
        /// </summary><returns>
        /// The zero-based index of the first occurrence of <paramref name="value"/> in the array; or –1 if no equal value was found.
        /// </returns>
        /// <inheritdoc cref="IndexOf{T}(T[], T)" select="remarks"/>
        /// <param name="array">The array to search.</param>
        /// <param name="value">The value to locate in the array.</param>
        public static int IndexOf<T>(this ArrayR2<T> array, T value)
        {
            return array.Array == null ? -1 : IndexFinder<T>.instance.IndexOf(array.Array, ref value);
        }

        /// <summary>
        /// Searches the array for the specified value and returns the index of its first occurrence.
        /// </summary><returns>
        /// The zero-based index of the first occurrence of <paramref name="value"/> in the array; or –1 if no equal value was found.
        /// </returns>
        /// <inheritdoc cref="IndexOf{T}(T[], T)" select="remarks"/>
        /// <param name="array">The array to search.</param>
        /// <param name="value">The value to locate in the array.</param>
        public static int IndexOf<T>(this ReadOnlyArrayR2<T> array, T value)
        {
            return array.Array == null ? -1 : IndexFinder<T>.instance.IndexOf(array.Array, ref value);
        }

        /* wip
        /// <summary>
        /// Searches the array for the specified value and returns the index of its first occurrence.
        /// </summary><returns>
        /// The zero-based index of the first occurrence of <paramref name="value"/> in the array; or –1 if no equal value was found.
        /// </returns>
        /// <inheritdoc cref="IndexOf{T}(T[], T)" select="remarks"/>
        /// <param name="array">The array to search.</param>
        /// <param name="value">The value to locate in the array.</param>
        public static int IndexOf<T>(this ArrayR3<T> array, T value)
        {
            return array.Array == null ? -1 : IndexFinder<T>.instance.IndexOf(array.Array, ref value);
        }
        
        /// <summary>
        /// Searches the array for the specified value and returns the index of its first occurrence.
        /// </summary><returns>
        /// The zero-based index of the first occurrence of <paramref name="value"/> in the array; or –1 if no equal value was found.
        /// </returns>
        /// <inheritdoc cref="IndexOf{T}(T[], T)" select="remarks"/>
        /// <param name="array">The array to search.</param>
        /// <param name="value">The value to locate in the array.</param>
        public static int IndexOf<T>(this ReadOnlyArrayR3<T> array, T value)
        {
            return array.Array == null ? -1 : IndexFinder<T>.instance.IndexOf(array.Array, ref value);
        }
        */

        /// <summary>
        /// Searches the array for the specified value, starting at the specified index, returning the index of the first occurrence found within that range.
        /// </summary><returns>
        /// The zero-based index of the first occurrence of <paramref name="value"/> in the searched range; or –1 if no equal value was found.
        /// </returns>
        /// <inheritdoc cref="IndexOf{T}(T[], T)" select="remarks"/>
        /// <param name="array">The array to search.</param>
        /// <param name="value">The value to locate in the array.</param>
        /// <param name="startIndex">The zero-based starting index of the search.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="startIndex"/> is less than zero or greater than the length of the array.
        /// </exception>
        public static int IndexOf<T>(this T[] array, T value, int startIndex)
        {
            return Array.IndexOf<T>(array, value, startIndex);
        }

        /// <summary>
        /// Searches the array for the specified value, starting at the specified index, returning the index of the first occurrence found within that range.
        /// </summary><returns>
        /// The zero-based index of the first occurrence of <paramref name="value"/> in the searched range; or –1 if no equal value was found.
        /// </returns>
        /// <inheritdoc cref="IndexOf{T}(T[], T)" select="remarks"/>
        /// <param name="array">The array to search.</param>
        /// <param name="value">The value to locate in the array.</param>
        /// <param name="startIndex">The zero-based starting index of the search.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="startIndex"/> is less than zero or greater than the length of the array.
        /// </exception>
        public static int IndexOf<T>(this T[,] array, T value, int startIndex)
        {
            return IndexFinder<T>.instance.IndexOf(array, ref value, startIndex);
        }

        /// <summary>
        /// Searches the array for the specified value, starting at the specified index, returning the index of the first occurrence found within that range.
        /// </summary><returns>
        /// The zero-based index of the first occurrence of <paramref name="value"/> in the searched range; or –1 if no equal value was found.
        /// </returns>
        /// <inheritdoc cref="IndexOf{T}(T[], T)" select="remarks"/>
        /// <param name="array">The array to search.</param>
        /// <param name="value">The value to locate in the array.</param>
        /// <param name="startIndex">The zero-based starting index of the search.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="startIndex"/> is less than zero or greater than the length of the array.
        /// </exception>
        public static int IndexOf<T>(this T[,,] array, T value, int startIndex)
        {
            return IndexFinder<T>.instance.IndexOf(array, ref value, startIndex);
        }

        /// <summary>
        /// Searches the array for the specified value, starting at the specified index, returning the index of the first occurrence found within that range.
        /// </summary><returns>
        /// The zero-based index of the first occurrence of <paramref name="value"/> in the searched range; or –1 if no equal value was found.
        /// </returns>
        /// <inheritdoc cref="IndexOf{T}(T[], T)" select="remarks"/>
        /// <param name="array">The array to search.</param>
        /// <param name="value">The value to locate in the array.</param>
        /// <param name="startIndex">The zero-based starting index of the search.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="startIndex"/> is less than zero or greater than the length of the array.
        /// </exception>
        public static int IndexOf<T>(this ReadOnlyArray<T> array, T value, int startIndex)
        {
            return Array.IndexOf<T>(array.Array ?? Empty<T>.Array, value, startIndex);
        }

        /// <summary>
        /// Searches the array for the specified value, starting at the specified index, returning the index of the first occurrence found within that range.
        /// </summary><returns>
        /// The zero-based index of the first occurrence of <paramref name="value"/> in the searched range; or –1 if no equal value was found.
        /// </returns>
        /// <inheritdoc cref="IndexOf{T}(T[], T)" select="remarks"/>
        /// <param name="array">The array to search.</param>
        /// <param name="value">The value to locate in the array.</param>
        /// <param name="startIndex">The zero-based starting index of the search.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="startIndex"/> is less than zero or greater than the length of the array.
        /// </exception>
        public static int IndexOf<T>(this ArrayR2<T> array, T value, int startIndex)
        {
            return IndexFinder<T>.instance.IndexOf(array.Array ?? Empty<T>.ArrayR2, ref value, startIndex);
        }

        /// <summary>
        /// Searches the array for the specified value, starting at the specified index, returning the index of the first occurrence found within that range.
        /// </summary><returns>
        /// The zero-based index of the first occurrence of <paramref name="value"/> in the searched range; or –1 if no equal value was found.
        /// </returns>
        /// <inheritdoc cref="IndexOf{T}(T[], T)" select="remarks"/>
        /// <param name="array">The array to search.</param>
        /// <param name="value">The value to locate in the array.</param>
        /// <param name="startIndex">The zero-based starting index of the search.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="startIndex"/> is less than zero or greater than the length of the array.
        /// </exception>
        public static int IndexOf<T>(this ReadOnlyArrayR2<T> array, T value, int startIndex)
        {
            return IndexFinder<T>.instance.IndexOf(array.Array ?? Empty<T>.ArrayR2, ref value, startIndex);
        }

        /* wip
        /// <summary>
        /// Searches the array for the specified value, starting at the specified index, returning the index of the first occurrence found within that range.
        /// </summary><returns>
        /// The zero-based index of the first occurrence of <paramref name="value"/> in the searched range; or –1 if no equal value was found.
        /// </returns>
        /// <inheritdoc cref="IndexOf{T}(T[], T)" select="remarks"/>
        /// <param name="array">The array to search.</param>
        /// <param name="value">The value to locate in the array.</param>
        /// <param name="startIndex">The zero-based starting index of the search.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="startIndex"/> is less than zero or greater than the length of the array.
        /// </exception>
        public static int IndexOf<T>(this ArrayR3<T> array, T value, int startIndex)
        {
            return IndexFinder<T>.instance.IndexOf(array.Array ?? Empty<T>.ArrayR3, ref value, startIndex);
        }
        
        /// <summary>
        /// Searches the array for the specified value, starting at the specified index, returning the index of the first occurrence found within that range.
        /// </summary><returns>
        /// The zero-based index of the first occurrence of <paramref name="value"/> in the searched range; or –1 if no equal value was found.
        /// </returns>
        /// <inheritdoc cref="IndexOf{T}(T[], T)" select="remarks"/>
        /// <param name="array">The array to search.</param>
        /// <param name="value">The value to locate in the array.</param>
        /// <param name="startIndex">The zero-based starting index of the search.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="startIndex"/> is less than zero or greater than the length of the array.
        /// </exception>
        public static int IndexOf<T>(this ReadOnlyArrayR3<T> array, T value, int startIndex)
        {
            return IndexFinder<T>.instance.IndexOf(array.Array ?? Empty<T>.ArrayR3, ref value, startIndex);
        }
        */

        /// <summary>
        /// Searches the specified range of the array for the specified value, returning the index of the first occurrence found.
        /// </summary><returns>
        /// The zero-based index of the first occurrence of <paramref name="value"/> in the searched range; or –1 if no equal value was found.
        /// </returns>
        /// <inheritdoc cref="IndexOf{T}(T[], T)" select="remarks"/>
        /// <param name="array">The array to search.</param>
        /// <param name="value">The value to locate in the array.</param>
        /// <param name="startIndex">The zero-based starting index of the search.</param>
        /// <param name="count">The number of elements covered by the search range.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="startIndex"/> or <paramref name="count"/> is less than zero;
        /// or if <paramref name="startIndex"/> + <paramref name="count"/> is greater than the length of the array (or overflows).
        /// </exception>
        public static int IndexOf<T>(this T[] array, T value, int startIndex, int count)
        {
            return Array.IndexOf<T>(array, value, startIndex, count);
        }

        /// <summary>
        /// Searches the specified range of the array for the specified value, returning the index of the first occurrence found.
        /// </summary><returns>
        /// The zero-based index of the first occurrence of <paramref name="value"/> in the searched range; or –1 if no equal value was found.
        /// </returns>
        /// <inheritdoc cref="IndexOf{T}(T[], T)" select="remarks"/>
        /// <param name="array">The array to search.</param>
        /// <param name="value">The value to locate in the array.</param>
        /// <param name="startIndex">The zero-based starting index of the search.</param>
        /// <param name="count">The number of elements covered by the search range.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="startIndex"/> or <paramref name="count"/> is less than zero;
        /// or if <paramref name="startIndex"/> + <paramref name="count"/> is greater than the length of the array (or overflows).
        /// </exception>
        public static int IndexOf<T>(this T[,] array, T value, int startIndex, int count)
        {
            return IndexFinder<T>.instance.IndexOf(array, ref value, startIndex, count);
        }

        /// <summary>
        /// Searches the specified range of the array for the specified value, returning the index of the first occurrence found.
        /// </summary><returns>
        /// The zero-based index of the first occurrence of <paramref name="value"/> in the searched range; or –1 if no equal value was found.
        /// </returns>
        /// <inheritdoc cref="IndexOf{T}(T[], T)" select="remarks"/>
        /// <param name="array">The array to search.</param>
        /// <param name="value">The value to locate in the array.</param>
        /// <param name="startIndex">The zero-based starting index of the search.</param>
        /// <param name="count">The number of elements covered by the search range.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="startIndex"/> or <paramref name="count"/> is less than zero;
        /// or if <paramref name="startIndex"/> + <paramref name="count"/> is greater than the length of the array (or overflows).
        /// </exception>
        public static int IndexOf<T>(this T[,,] array, T value, int startIndex, int count)
        {
            return IndexFinder<T>.instance.IndexOf(array, ref value, startIndex, count);
        }

        /// <summary>
        /// Searches the specified range of the array for the specified value, returning the index of the first occurrence found.
        /// </summary><returns>
        /// The zero-based index of the first occurrence of <paramref name="value"/> in the searched range; or –1 if no equal value was found.
        /// </returns>
        /// <inheritdoc cref="IndexOf{T}(T[], T)" select="remarks"/>
        /// <param name="array">The array to search.</param>
        /// <param name="value">The value to locate in the array.</param>
        /// <param name="startIndex">The zero-based starting index of the search.</param>
        /// <param name="count">The number of elements covered by the search range.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="startIndex"/> or <paramref name="count"/> is less than zero;
        /// or if <paramref name="startIndex"/> + <paramref name="count"/> is greater than the length of the array (or overflows).
        /// </exception>
        public static int IndexOf<T>(this ReadOnlyArray<T> array, T value, int startIndex, int count)
        {
            return Array.IndexOf<T>(array.Array ?? Empty<T>.Array, value, startIndex, count);
        }

        /// <summary>
        /// Searches the specified range of the array for the specified value, returning the index of the first occurrence found.
        /// </summary><returns>
        /// The zero-based index of the first occurrence of <paramref name="value"/> in the searched range; or –1 if no equal value was found.
        /// </returns>
        /// <inheritdoc cref="IndexOf{T}(T[], T)" select="remarks"/>
        /// <param name="array">The array to search.</param>
        /// <param name="value">The value to locate in the array.</param>
        /// <param name="startIndex">The zero-based starting index of the search.</param>
        /// <param name="count">The number of elements covered by the search range.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="startIndex"/> or <paramref name="count"/> is less than zero;
        /// or if <paramref name="startIndex"/> + <paramref name="count"/> is greater than the length of the array (or overflows).
        /// </exception>
        public static int IndexOf<T>(this ArrayR2<T> array, T value, int startIndex, int count)
        {
            return IndexFinder<T>.instance.IndexOf(array.Array ?? Empty<T>.ArrayR2, ref value, startIndex, count);
        }

        /// <summary>
        /// Searches the specified range of the array for the specified value, returning the index of the first occurrence found.
        /// </summary><returns>
        /// The zero-based index of the first occurrence of <paramref name="value"/> in the searched range; or –1 if no equal value was found.
        /// </returns>
        /// <inheritdoc cref="IndexOf{T}(T[], T)" select="remarks"/>
        /// <param name="array">The array to search.</param>
        /// <param name="value">The value to locate in the array.</param>
        /// <param name="startIndex">The zero-based starting index of the search.</param>
        /// <param name="count">The number of elements covered by the search range.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="startIndex"/> or <paramref name="count"/> is less than zero;
        /// or if <paramref name="startIndex"/> + <paramref name="count"/> is greater than the length of the array (or overflows).
        /// </exception>
        public static int IndexOf<T>(this ReadOnlyArrayR2<T> array, T value, int startIndex, int count)
        {
            return IndexFinder<T>.instance.IndexOf(array.Array ?? Empty<T>.ArrayR2, ref value, startIndex, count);
        }

        /* wip
        /// <summary>
        /// Searches the specified range of the array for the specified value, returning the index of the first occurrence found.
        /// </summary><returns>
        /// The zero-based index of the first occurrence of <paramref name="value"/> in the searched range; or –1 if no equal value was found.
        /// </returns>
        /// <inheritdoc cref="IndexOf{T}(T[], T)" select="remarks"/>
        /// <param name="array">The array to search.</param>
        /// <param name="value">The value to locate in the array.</param>
        /// <param name="startIndex">The zero-based starting index of the search.</param>
        /// <param name="count">The number of elements covered by the search range.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="startIndex"/> or <paramref name="count"/> is less than zero;
        /// or if <paramref name="startIndex"/> + <paramref name="count"/> is greater than the length of the array (or overflows).
        /// </exception>
        public static int IndexOf<T>(this ArrayR3<T> array, T value, int startIndex, int count)
        {
            return IndexFinder<T>.instance.IndexOf(array.Array ?? Empty<T>.ArrayR3, ref value, startIndex, count);
        }
        
        /// <summary>
        /// Searches the specified range of the array for the specified value, returning the index of the first occurrence found.
        /// </summary><returns>
        /// The zero-based index of the first occurrence of <paramref name="value"/> in the searched range; or –1 if no equal value was found.
        /// </returns>
        /// <inheritdoc cref="IndexOf{T}(T[], T)" select="remarks"/>
        /// <param name="array">The array to search.</param>
        /// <param name="value">The value to locate in the array.</param>
        /// <param name="startIndex">The zero-based starting index of the search.</param>
        /// <param name="count">The number of elements covered by the search range.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="startIndex"/> or <paramref name="count"/> is less than zero;
        /// or if <paramref name="startIndex"/> + <paramref name="count"/> is greater than the length of the array (or overflows).
        /// </exception>
        public static int IndexOf<T>(this ReadOnlyArrayR3<T> array, T value, int startIndex, int count)
        {
            return IndexFinder<T>.instance.IndexOf(array.Array ?? Empty<T>.ArrayR3, ref value, startIndex, count);
        }
        */

        /// <summary>
        /// Searches the array backwards for the specified value, returning the index of the last occurrence in the array.
        /// </summary><returns>
        /// The zero-based index of the last occurrence of <paramref name="value"/> in the array; or -1 if no equal value was found.
        /// </returns>
        /// <inheritdoc cref="IndexOf{T}(T[], T)" select="remarks"/>
        /// <param name="array">The array to search.</param>
        /// <param name="value">The value to locate in the array.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array is null.
        /// </exception>
        public static int LastIndexOf<T>(this T[] array, T value)
        {
            return Array.LastIndexOf<T>(array, value);
        }

        /// <summary>
        /// Searches the array backwards for the specified value, returning the index of the last occurrence in the array.
        /// </summary><returns>
        /// The zero-based index of the last occurrence of <paramref name="value"/> in the array; or -1 if no equal value was found.
        /// </returns>
        /// <inheritdoc cref="IndexOf{T}(T[], T)" select="remarks"/>
        /// <param name="array">The array to search.</param>
        /// <param name="value">The value to locate in the array.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array is null.
        /// </exception>
        public static int LastIndexOf<T>(this T[,] array, T value)
        {
            return IndexFinder<T>.instance.LastIndexOf(array, ref value);
        }

        /// <summary>
        /// Searches the array backwards for the specified value, returning the index of the last occurrence in the array.
        /// </summary><returns>
        /// The zero-based index of the last occurrence of <paramref name="value"/> in the array; or -1 if no equal value was found.
        /// </returns>
        /// <inheritdoc cref="IndexOf{T}(T[], T)" select="remarks"/>
        /// <param name="array">The array to search.</param>
        /// <param name="value">The value to locate in the array.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array is null.
        /// </exception>
        public static int LastIndexOf<T>(this T[,,] array, T value)
        {
            return IndexFinder<T>.instance.LastIndexOf(array, ref value);
        }

        /// <summary>
        /// Searches the array backwards for the specified value, returning the index of the last occurrence in the array.
        /// </summary><returns>
        /// The zero-based index of the last occurrence of <paramref name="value"/> in the array; or -1 if no equal value was found.
        /// </returns>
        /// <inheritdoc cref="IndexOf{T}(T[], T)" select="remarks"/>
        /// <param name="array">The array to search.</param>
        /// <param name="value">The value to locate in the array.</param>
        public static int LastIndexOf<T>(this ReadOnlyArray<T> array, T value)
        {
            return array.Array == null ? -1 : Array.LastIndexOf<T>(array.Array, value);
        }

        /// <summary>
        /// Searches the array backwards for the specified value, returning the index of the last occurrence in the array.
        /// </summary><returns>
        /// The zero-based index of the last occurrence of <paramref name="value"/> in the array; or -1 if no equal value was found.
        /// </returns>
        /// <inheritdoc cref="IndexOf{T}(T[], T)" select="remarks"/>
        /// <param name="array">The array to search.</param>
        /// <param name="value">The value to locate in the array.</param>
        public static int LastIndexOf<T>(this ArrayR2<T> array, T value)
        {
            return array.Array == null ? -1 : IndexFinder<T>.instance.LastIndexOf(array.Array, ref value);
        }

        /// <summary>
        /// Searches the array backwards for the specified value, returning the index of the last occurrence in the array.
        /// </summary><returns>
        /// The zero-based index of the last occurrence of <paramref name="value"/> in the array; or -1 if no equal value was found.
        /// </returns>
        /// <inheritdoc cref="IndexOf{T}(T[], T)" select="remarks"/>
        /// <param name="array">The array to search.</param>
        /// <param name="value">The value to locate in the array.</param>
        public static int LastIndexOf<T>(this ReadOnlyArrayR2<T> array, T value)
        {
            return array.Array == null ? -1 : IndexFinder<T>.instance.LastIndexOf(array.Array, ref value);
        }

        /* wip
        /// <summary>
        /// Searches the array backwards for the specified value, returning the index of the last occurrence in the array.
        /// </summary><returns>
        /// The zero-based index of the last occurrence of <paramref name="value"/> in the array; or -1 if no equal value was found.
        /// </returns>
        /// <inheritdoc cref="IndexOf{T}(T[], T)" select="remarks"/>
        /// <param name="array">The array to search.</param>
        /// <param name="value">The value to locate in the array.</param>
        public static int LastIndexOf<T>(this ArrayR3<T> array, T value)
        {
            return array.Array == null ? -1 : IndexFinder<T>.instance.LastIndexOf(array.Array, ref value);
        }
        
        /// <summary>
        /// Searches the array backwards for the specified value, returning the index of the last occurrence in the array.
        /// </summary><returns>
        /// The zero-based index of the last occurrence of <paramref name="value"/> in the array; or -1 if no equal value was found.
        /// </returns>
        /// <inheritdoc cref="IndexOf{T}(T[], T)" select="remarks"/>
        /// <param name="array">The array to search.</param>
        /// <param name="value">The value to locate in the array.</param>
        public static int LastIndexOf<T>(this ReadOnlyArrayR3<T> array, T value)
        {
            return array.Array == null ? -1 : IndexFinder<T>.instance.LastIndexOf(array.Array, ref value);
        }
        */

        /// <summary>
        /// Searches the array backwards, starting at the specified index, for the specified value, returning the index of the last occurrence in that range.
        /// </summary><remarks>
        /// This method performs an equality comparison by calling the <b>T.Equals</b> method on every element.
        /// This means that if <typeparamref name="T"/> overrides the Equals method, that override is called.
        /// <para/>
        /// <b>This method differs slightly from its System.Array counterpart:</b><br/>
        /// Whereas this method allows <paramref name="startIndex"/> to be -1 regardless of array length, the System.Array version
        /// allows <paramref name="startIndex"/> to be -1 if and only if the length of the <paramref name="array"/> is zero.<br/>
        /// This seemed oddly inconsistent with forward searching methods where going one step out of bounds (i.e. startIndex = array.Length)
        /// is always permitted (regardless of array length). Since the cost of this oddity included a convoluted contract, a convoluted
        /// documentation (not really since this oddity was in fact not documented!), and suboptimal usability, it was deemed costlier to
        /// mimic this behavior than to allow what is arguably a small and rather obscure inconsistency with its System.Array counterpart.
        /// <br/>
        /// Also note that this method, unlike its counterpart, does not accept a startIndex of zero for zero-length arrays. This was another
        /// oddity as not even the similar System.Array.FindLastIndex accepts anything other than a startIndex of -1 for zero-length arrays.
        /// </remarks><returns>
        /// The zero-based index of the last occurrence of <paramref name="value"/> in the searched range; or -1 if no equal value was found.
        /// </returns>
        /// <param name="array">The array to search.</param>
        /// <param name="value">The value to locate in the array.</param>
        /// <param name="startIndex">The zero-based starting index of the backwards search (i.e. the highest index in the search range).</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="startIndex"/> is less than -1 or greater than or equal to the length of the array.
        /// </exception>
        public static int LastIndexOf<T>(this T[] array, T value, int startIndex)
        {
            return IndexFinder<T>.instance.LastIndexOf(array, ref value, startIndex);
        }

        /// <summary>
        /// Searches the array backwards, starting at the specified index, for the specified value, returning the index of the last occurrence in that range.
        /// </summary><returns>
        /// The zero-based index of the last occurrence of <paramref name="value"/> in the searched range; or -1 if no equal value was found.
        /// </returns>
        /// <inheritdoc cref="LastIndexOf{T}(T[], T, int)" select="remarks"/>
        /// <param name="array">The array to search.</param>
        /// <param name="value">The value to locate in the array.</param>
        /// <param name="startIndex">The zero-based starting index of the backwards search (i.e. the highest index in the search range).</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="startIndex"/> is less than -1 or greater than or equal to the length of the array.
        /// </exception>
        public static int LastIndexOf<T>(this T[,] array, T value, int startIndex)
        {
            return IndexFinder<T>.instance.LastIndexOf(array, ref value, startIndex);
        }

        /// <summary>
        /// Searches the array backwards, starting at the specified index, for the specified value, returning the index of the last occurrence in that range.
        /// </summary><returns>
        /// The zero-based index of the last occurrence of <paramref name="value"/> in the searched range; or -1 if no equal value was found.
        /// </returns>
        /// <inheritdoc cref="LastIndexOf{T}(T[], T, int)" select="remarks"/>
        /// <param name="array">The array to search.</param>
        /// <param name="value">The value to locate in the array.</param>
        /// <param name="startIndex">The zero-based starting index of the backwards search (i.e. the highest index in the search range).</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="startIndex"/> is less than -1 or greater than or equal to the length of the array.
        /// </exception>
        public static int LastIndexOf<T>(this T[,,] array, T value, int startIndex)
        {
            return IndexFinder<T>.instance.LastIndexOf(array, ref value, startIndex);
        }

        /// <summary>
        /// Searches the array backwards, starting at the specified index, for the specified value, returning the index of the last occurrence in that range.
        /// </summary><returns>
        /// The zero-based index of the last occurrence of <paramref name="value"/> in the searched range; or -1 if no equal value was found.
        /// </returns>
        /// <inheritdoc cref="LastIndexOf{T}(T[], T, int)" select="remarks"/>
        /// <param name="array">The array to search.</param>
        /// <param name="value">The value to locate in the array.</param>
        /// <param name="startIndex">The zero-based starting index of the backwards search (i.e. the highest index in the search range).</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="startIndex"/> is less than -1 or greater than or equal to the length of the array.
        /// </exception>
        public static int LastIndexOf<T>(this ReadOnlyArray<T> array, T value, int startIndex)
        {
            return IndexFinder<T>.instance.LastIndexOf(array.Array ?? Empty<T>.Array, ref value, startIndex);
        }

        /// <summary>
        /// Searches the array backwards, starting at the specified index, for the specified value, returning the index of the last occurrence in that range.
        /// </summary><returns>
        /// The zero-based index of the last occurrence of <paramref name="value"/> in the searched range; or -1 if no equal value was found.
        /// </returns>
        /// <inheritdoc cref="LastIndexOf{T}(T[], T, int)" select="remarks"/>
        /// <param name="array">The array to search.</param>
        /// <param name="value">The value to locate in the array.</param>
        /// <param name="startIndex">The zero-based starting index of the backwards search (i.e. the highest index in the search range).</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="startIndex"/> is less than -1 or greater than or equal to the length of the array.
        /// </exception>
        public static int LastIndexOf<T>(this ArrayR2<T> array, T value, int startIndex)
        {
            return IndexFinder<T>.instance.LastIndexOf(array.Array ?? Empty<T>.ArrayR2, ref value, startIndex);
        }

        /// <summary>
        /// Searches the array backwards, starting at the specified index, for the specified value, returning the index of the last occurrence in that range.
        /// </summary><returns>
        /// The zero-based index of the last occurrence of <paramref name="value"/> in the searched range; or -1 if no equal value was found.
        /// </returns>
        /// <inheritdoc cref="LastIndexOf{T}(T[], T, int)" select="remarks"/>
        /// <param name="array">The array to search.</param>
        /// <param name="value">The value to locate in the array.</param>
        /// <param name="startIndex">The zero-based starting index of the backwards search (i.e. the highest index in the search range).</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="startIndex"/> is less than -1 or greater than or equal to the length of the array.
        /// </exception>
        public static int LastIndexOf<T>(this ReadOnlyArrayR2<T> array, T value, int startIndex)
        {
            return IndexFinder<T>.instance.LastIndexOf(array.Array ?? Empty<T>.ArrayR2, ref value, startIndex);
        }

        /* wip
        /// <summary>
        /// Searches the array backwards, starting at the specified index, for the specified value, returning the index of the last occurrence in that range.
        /// </summary><returns>
        /// The zero-based index of the last occurrence of <paramref name="value"/> in the searched range; or -1 if no equal value was found.
        /// </returns>
        /// <inheritdoc cref="LastIndexOf{T}(T[], T, int)" select="remarks"/>
        /// <param name="array">The array to search.</param>
        /// <param name="value">The value to locate in the array.</param>
        /// <param name="startIndex">The zero-based starting index of the backwards search (i.e. the highest index in the search range).</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="startIndex"/> is less than -1 or greater than or equal to the length of the array.
        /// </exception>
        public static int LastIndexOf<T>(this ArrayR3<T> array, T value, int startIndex)
        {
            return IndexFinder<T>.instance.LastIndexOf(array.Array ?? Empty<T>.ArrayR3, ref value, startIndex);
        }
        
        /// <summary>
        /// Searches the array backwards, starting at the specified index, for the specified value, returning the index of the last occurrence in that range.
        /// </summary><returns>
        /// The zero-based index of the last occurrence of <paramref name="value"/> in the searched range; or -1 if no equal value was found.
        /// </returns>
        /// <inheritdoc cref="LastIndexOf{T}(T[], T, int)" select="remarks"/>
        /// <param name="array">The array to search.</param>
        /// <param name="value">The value to locate in the array.</param>
        /// <param name="startIndex">The zero-based starting index of the backwards search (i.e. the highest index in the search range).</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="startIndex"/> is less than -1 or greater than or equal to the length of the array.
        /// </exception>
        public static int LastIndexOf<T>(this ReadOnlyArrayR3<T> array, T value, int startIndex)
        {
            return IndexFinder<T>.instance.LastIndexOf(array.Array ?? Empty<T>.ArrayR3, ref value, startIndex);
        }
        */

        /// <summary>
        /// Searches the array, starting at the specified index and proceeding backwards over a range of <paramref name="count"/> elements,
        /// for the specified value, returning the index of the last occurrence in that range.
        /// </summary><returns>
        /// The zero-based index of the last occurrence of <paramref name="value"/> in the searched range; or -1 if no equal value was found.
        /// </returns>
        /// <inheritdoc cref="LastIndexOf{T}(T[], T, int)" select="remarks"/>
        /// <param name="array">The array to search.</param>
        /// <param name="value">The value to locate in the array.</param>
        /// <param name="startIndex">The zero-based starting index of the backwards search (i.e. the highest index in the search range).</param>
        /// <param name="count">The number of elements covered by the search range.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="startIndex"/> is less than -1 or greater than or equal to the length of the array.
        /// Also thrown if either <paramref name="count"/> or [<paramref name="startIndex"/> - <paramref name="count"/> + 1] is less than zero.
        /// </exception>
        public static int LastIndexOf<T>(this T[] array, T value, int startIndex, int count)
        {
            return IndexFinder<T>.instance.LastIndexOf(array, ref value, startIndex, count);
        }

        /// <summary>
        /// Searches the array, starting at the specified index and proceeding backwards over a range of <paramref name="count"/> elements,
        /// for the specified value, returning the index of the last occurrence in that range.
        /// </summary><returns>
        /// The zero-based index of the last occurrence of <paramref name="value"/> in the searched range; or -1 if no equal value was found.
        /// </returns>
        /// <inheritdoc cref="LastIndexOf{T}(T[], T, int)" select="remarks"/>
        /// <param name="array">The array to search.</param>
        /// <param name="value">The value to locate in the array.</param>
        /// <param name="startIndex">The zero-based starting index of the backwards search (i.e. the highest index in the search range).</param>
        /// <param name="count">The number of elements covered by the search range.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="startIndex"/> is less than -1 or greater than or equal to the length of the array.
        /// Also thrown if either <paramref name="count"/> or [<paramref name="startIndex"/> - <paramref name="count"/> + 1] is less than zero.
        /// </exception>
        public static int LastIndexOf<T>(this T[,] array, T value, int startIndex, int count)
        {
            return IndexFinder<T>.instance.LastIndexOf(array, ref value, startIndex, count);
        }

        /// <summary>
        /// Searches the array, starting at the specified index and proceeding backwards over a range of <paramref name="count"/> elements,
        /// for the specified value, returning the index of the last occurrence in that range.
        /// </summary><returns>
        /// The zero-based index of the last occurrence of <paramref name="value"/> in the searched range; or -1 if no equal value was found.
        /// </returns>
        /// <inheritdoc cref="LastIndexOf{T}(T[], T, int)" select="remarks"/>
        /// <param name="array">The array to search.</param>
        /// <param name="value">The value to locate in the array.</param>
        /// <param name="startIndex">The zero-based starting index of the backwards search (i.e. the highest index in the search range).</param>
        /// <param name="count">The number of elements covered by the search range.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="startIndex"/> is less than -1 or greater than or equal to the length of the array.
        /// Also thrown if either <paramref name="count"/> or [<paramref name="startIndex"/> - <paramref name="count"/> + 1] is less than zero.
        /// </exception>
        public static int LastIndexOf<T>(this T[,,] array, T value, int startIndex, int count)
        {
            return IndexFinder<T>.instance.LastIndexOf(array, ref value, startIndex, count);
        }

        /// <summary>
        /// Searches the array, starting at the specified index and proceeding backwards over a range of <paramref name="count"/> elements,
        /// for the specified value, returning the index of the last occurrence in that range.
        /// </summary><returns>
        /// The zero-based index of the last occurrence of <paramref name="value"/> in the searched range; or -1 if no equal value was found.
        /// </returns>
        /// <inheritdoc cref="LastIndexOf{T}(T[], T, int)" select="remarks"/>
        /// <param name="array">The array to search.</param>
        /// <param name="value">The value to locate in the array.</param>
        /// <param name="startIndex">The zero-based starting index of the backwards search (i.e. the highest index in the search range).</param>
        /// <param name="count">The number of elements covered by the search range.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="startIndex"/> is less than -1 or greater than or equal to the length of the array.
        /// Also thrown if either <paramref name="count"/> or [<paramref name="startIndex"/> - <paramref name="count"/> + 1] is less than zero.
        /// </exception>
        public static int LastIndexOf<T>(this ReadOnlyArray<T> array, T value, int startIndex, int count)
        {
            return IndexFinder<T>.instance.LastIndexOf(array.Array ?? Empty<T>.Array, ref value, startIndex, count);
        }

        /// <summary>
        /// Searches the array, starting at the specified index and proceeding backwards over a range of <paramref name="count"/> elements,
        /// for the specified value, returning the index of the last occurrence in that range.
        /// </summary><returns>
        /// The zero-based index of the last occurrence of <paramref name="value"/> in the searched range; or -1 if no equal value was found.
        /// </returns>
        /// <inheritdoc cref="LastIndexOf{T}(T[], T, int)" select="remarks"/>
        /// <param name="array">The array to search.</param>
        /// <param name="value">The value to locate in the array.</param>
        /// <param name="startIndex">The zero-based starting index of the backwards search (i.e. the highest index in the search range).</param>
        /// <param name="count">The number of elements covered by the search range.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="startIndex"/> is less than -1 or greater than or equal to the length of the array.
        /// Also thrown if either <paramref name="count"/> or [<paramref name="startIndex"/> - <paramref name="count"/> + 1] is less than zero.
        /// </exception>
        public static int LastIndexOf<T>(this ArrayR2<T> array, T value, int startIndex, int count)
        {
            return IndexFinder<T>.instance.LastIndexOf(array.Array ?? Empty<T>.ArrayR2, ref value, startIndex, count);
        }

        /// <summary>
        /// Searches the array, starting at the specified index and proceeding backwards over a range of <paramref name="count"/> elements,
        /// for the specified value, returning the index of the last occurrence in that range.
        /// </summary><returns>
        /// The zero-based index of the last occurrence of <paramref name="value"/> in the searched range; or -1 if no equal value was found.
        /// </returns>
        /// <inheritdoc cref="LastIndexOf{T}(T[], T, int)" select="remarks"/>
        /// <param name="array">The array to search.</param>
        /// <param name="value">The value to locate in the array.</param>
        /// <param name="startIndex">The zero-based starting index of the backwards search (i.e. the highest index in the search range).</param>
        /// <param name="count">The number of elements covered by the search range.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="startIndex"/> is less than -1 or greater than or equal to the length of the array.
        /// Also thrown if either <paramref name="count"/> or [<paramref name="startIndex"/> - <paramref name="count"/> + 1] is less than zero.
        /// </exception>
        public static int LastIndexOf<T>(this ReadOnlyArrayR2<T> array, T value, int startIndex, int count)
        {
            return IndexFinder<T>.instance.LastIndexOf(array.Array ?? Empty<T>.ArrayR2, ref value, startIndex, count);
        }

        /* wip
        /// <summary>
        /// Searches the array, starting at the specified index and proceeding backwards over a range of <paramref name="count"/> elements,
        /// for the specified value, returning the index of the last occurrence in that range.
        /// </summary><returns>
        /// The zero-based index of the last occurrence of <paramref name="value"/> in the searched range; or -1 if no equal value was found.
        /// </returns>
        /// <inheritdoc cref="LastIndexOf{T}(T[], T, int)" select="remarks"/>
        /// <param name="array">The array to search.</param>
        /// <param name="value">The value to locate in the array.</param>
        /// <param name="startIndex">The zero-based starting index of the backwards search (i.e. the highest index in the search range).</param>
        /// <param name="count">The number of elements covered by the search range.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="startIndex"/> is less than -1 or greater than or equal to the length of the array.
        /// Also thrown if either <paramref name="count"/> or [<paramref name="startIndex"/> - <paramref name="count"/> + 1] is less than zero.
        /// </exception>
        public static int LastIndexOf<T>(this ArrayR3<T> array, T value, int startIndex, int count)
        {
            return IndexFinder<T>.instance.LastIndexOf(array.Array ?? Empty<T>.ArrayR3, ref value, startIndex, count);
        }
        
        /// <summary>
        /// Searches the array, starting at the specified index and proceeding backwards over a range of <paramref name="count"/> elements,
        /// for the specified value, returning the index of the last occurrence in that range.
        /// </summary><returns>
        /// The zero-based index of the last occurrence of <paramref name="value"/> in the searched range; or -1 if no equal value was found.
        /// </returns>
        /// <inheritdoc cref="LastIndexOf{T}(T[], T, int)" select="remarks"/>
        /// <param name="array">The array to search.</param>
        /// <param name="value">The value to locate in the array.</param>
        /// <param name="startIndex">The zero-based starting index of the backwards search (i.e. the highest index in the search range).</param>
        /// <param name="count">The number of elements covered by the search range.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="startIndex"/> is less than -1 or greater than or equal to the length of the array.
        /// Also thrown if either <paramref name="count"/> or [<paramref name="startIndex"/> - <paramref name="count"/> + 1] is less than zero.
        /// </exception>
        public static int LastIndexOf<T>(this ReadOnlyArrayR3<T> array, T value, int startIndex, int count)
        {
            return IndexFinder<T>.instance.LastIndexOf(array.Array ?? Empty<T>.ArrayR3, ref value, startIndex, count);
        }
        */
    }
}
