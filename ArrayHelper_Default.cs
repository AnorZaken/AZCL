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


        public static int BinarySearch<T>(this ReadOnlyArray<T> array, T value)
        {
            return array.Array == null ? -1 : Array.BinarySearch<T>(array.Array, value);
        }


        public static int BinarySearch<T>(this ReadOnlyArray<T> array, T value, IComparer<T> comparer)
        {
            return array.Array == null ? -1 : Array.BinarySearch<T>(array.Array, value, comparer);
        }


        public static int BinarySearch<T>(this ReadOnlyArray<T> array, int index, int length, T value)
        {
            return array.Array == null ? -1 : Array.BinarySearch<T>(array.Array, index, length, value);
        }


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

            System.Array.Clear(array, 0, array.Length);
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

            System.Array.Clear(array, 0, array.Length);
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

            System.Array.Clear(array, 0, array.Length);
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
                System.Array.Clear(arr, 0, arr.Length);
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
                System.Array.Clear(arr, 0, arr.Length);
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
                if (arr == null)
                    throw new ArgumentException(paramName: nameof(array), message: ERR_POPULATE_INNER);
                else
                    Clear(arr);
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
                if (arr == null)
                    throw new ArgumentException(paramName: nameof(array), message: ERR_POPULATE_INNER);
                else
                    ClearInner(arr);
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
            return System.Array.ConvertAll(array, converter);
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
            if (input.IsAbsent)
            {
                if (converter == null)
                    throw new ArgumentNullException(nameof(converter));

                return Empty<TOutput>.Array;
            }

            return System.Array.ConvertAll(input.Array, converter);
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
            if (converter == null)
                throw new ArgumentNullException(nameof(converter));

            if (input.IsAbsent)
                return new ArrayR2<TOutput>();

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
            if (converter == null)
                throw new ArgumentNullException(nameof(converter));

            if (input.IsAbsent)
                return new ArrayR2<TOutput>();

            return ConvertAll(input.Array, converter);
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
            if (converter == null)
                throw new ArgumentNullException(nameof(converter));

            if (input.IsAbsent)
                return new ArrayR3<TOutput>();

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
            if (converter == null)
                throw new ArgumentNullException(nameof(converter));

            if (input.IsAbsent)
                return new ArrayR3<TOutput>();

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
                    throw new ArgumentException(paramName: nameof(input), message: ERR_CONVERT_INNER);

                output[i] = System.Array.ConvertAll(inner, converter);
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
                    throw new ArgumentException(paramName: nameof(input), message: ERR_CONVERT_INNER);

                output[i] = ConvertAll(inner, converter);
            }

            return output;
        }


        public static bool Exists<T>(T[] array, Predicate<T> match)
        {
            return Array.Exists<T>(array, match);
        }


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


        public static bool Exists<T>(T[,] array, Predicate<T> match)
        {
            return Exists(new ArrayR2<T>(array), match); // <-- ctor does null check.
        }


        [Obsolete("Use Linq Any(Func<T, bool>) extension instead.")]
        public static bool Exists<T>(ArrayR2<T> array, Predicate<T> match)
        {
            if (match == null)
                throw new ArgumentNullException(nameof(match));

            return array.Any(new Func<T, bool>(match));
        }


        [Obsolete("Use Linq Any(Func<T, bool>) extension instead.")]
        public static bool Exists<T>(ReadOnlyArrayR2<T> array, Predicate<T> match)
        {
            if (match == null)
                throw new ArgumentNullException(nameof(match));

            return array.Any(new Func<T, bool>(match));
        }


        /* wip
        public static bool Exists<T>(T[,,] array, Predicate<T> match)
        {
            return Exists(new ArrayR3<T>(array), match); // <-- ctor does null check.
        }


        [Obsolete("Use Linq Any(Func<T, bool>) extension instead.")]
        public static bool Exists<T>(ArrayR3<T> array, Predicate<T> match)
        {
            if (match == null)
                throw new ArgumentNullException(nameof(match));

            return array.Any(new Func<T, bool>(match));
        }


        [Obsolete("Use Linq Any(Func<T, bool>) extension instead.")]
        public static bool Exists<T>(ReadOnlyArrayR3<T> array, Predicate<T> match)
        {
            if (match == null)
                throw new ArgumentNullException(nameof(match));

            return array.Any(new Func<T, bool>(match));
        }
        */


        public static bool TrueForAll<T>(T[] array, Predicate<T> match)
        {
            return Array.TrueForAll<T>(array, match);
        }


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


        public static bool TrueForAll<T>(T[,] array, Predicate<T> match)
        {
            return TrueForAll(new ArrayR2<T>(array), match); // <-- ctor does null check.
        }


        [Obsolete("Use Linq All(Func<T, bool>) extension instead.")]
        public static bool TrueForAll<T>(ArrayR2<T> array, Predicate<T> match)
        {
            if (match == null)
                throw new ArgumentNullException(nameof(match));

            return array.All(new Func<T, bool>(match));
        }


        [Obsolete("Use Linq All(Func<T, bool>) extension instead.")]
        public static bool TrueForAll<T>(ReadOnlyArrayR2<T> array, Predicate<T> match)
        {
            if (match == null)
                throw new ArgumentNullException(nameof(match));

            return array.All(new Func<T, bool>(match));
        }


        /* wip
        public static bool TrueForAll<T>(T[,,] array, Predicate<T> match)
        {
            return TrueForAll(new ArrayR3<T>(array), match); // <-- ctor does null check.
        }


        [Obsolete("Use Linq All(Func<T, bool>) extension instead.")]
        public static bool TrueForAll<T>(ArrayR3<T> array, Predicate<T> match)
        {
            if (match == null)
                throw new ArgumentNullException(nameof(match));

            return array.All(new Func<T, bool>(match));
        }


        [Obsolete("Use Linq All(Func<T, bool>) extension instead.")]
        public static bool TrueForAll<T>(ReadOnlyArrayR3<T> array, Predicate<T> match)
        {
            if (match == null)
                throw new ArgumentNullException(nameof(match));

            return array.All(new Func<T, bool>(match));
        }
        */


        public static T Find<T>(T[] array, Predicate<T> match)
        {
            return Array.Find(array, match);
        }


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


        public static T Find<T>(T[,] array, Predicate<T> match)
        {
            return Find(new ArrayR2<T>(array), match); // <-- ctor does null check.
        }


        [Obsolete("Use Linq FirstOrDefault(Func<T, bool>) extension instead.")]
        public static T Find<T>(ArrayR2<T> array, Predicate<T> match)
        {
            if (match == null)
                throw new ArgumentNullException(nameof(match));

            return array.FirstOrDefault(new Func<T, bool>(match));
        }


        [Obsolete("Use Linq FirstOrDefault(Func<T, bool>) extension instead.")]
        public static T Find<T>(ReadOnlyArrayR2<T> array, Predicate<T> match)
        {
            if (match == null)
                throw new ArgumentNullException(nameof(match));

            return array.FirstOrDefault(new Func<T, bool>(match));
        }


        /* wip
        public static T Find<T>(T[,,] array, Predicate<T> match)
        {
            return Find(new ArrayR3<T>(array), match); // <-- ctor does null check.
        }


        [Obsolete("Use Linq FirstOrDefault(Func<T, bool>) extension instead.")]
        public static T Find<T>(ArrayR3<T> array, Predicate<T> match)
        {
            if (match == null)
                throw new ArgumentNullException(nameof(match));

            return array.FirstOrDefault(new Func<T, bool>(match));
        }


        [Obsolete("Use Linq FirstOrDefault(Func<T, bool>) extension instead.")]
        public static T Find<T>(ReadOnlyArrayR3<T> array, Predicate<T> match)
        {
            if (match == null)
                throw new ArgumentNullException(nameof(match));

            return array.FirstOrDefault(new Func<T, bool>(match));
        }
        */


        public static T[] FindAll<T>(T[] array, Predicate<T> match)
        {
            return Array.FindAll<T>(array, match);
        }


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


        public static T[] FindAll<T>(T[,] array, Predicate<T> match)
        {
            return FindAll(new ArrayR2<T>(array), match); // <-- ctor does null check.
        }


        [Obsolete("Use Linq Where(Func<T, bool>) extension instead.")]
        public static T[] FindAll<T>(ArrayR2<T> array, Predicate<T> match)
        {
            if (match == null)
                throw new ArgumentNullException(nameof(match));

            return array.IsAbsent ? Empty<T>.Array : array.Where(new Func<T, bool>(match)).ToArray();
        }


        [Obsolete("Use Linq Where(Func<T, bool>) extension instead.")]
        public static T[] FindAll<T>(ReadOnlyArrayR2<T> array, Predicate<T> match)
        {
            if (match == null)
                throw new ArgumentNullException(nameof(match));

            return array.IsAbsent ? Empty<T>.Array : array.Where(new Func<T, bool>(match)).ToArray();
        }


        /* wip
        public static T[] FindAll<T>(T[,,] array, Predicate<T> match)
        {
            return FindAll(new ArrayR3<T>(array), match); // <-- ctor does null check.
        }


        [Obsolete("Use Linq Where(Func<T, bool>) extension instead.")]
        public static T[] FindAll<T>(ArrayR3<T> array, Predicate<T> match)
        {
            if (match == null)
                throw new ArgumentNullException(nameof(match));

            return array.IsAbsent ? Empty<T>.Array : array.Where(new Func<T, bool>(match)).ToArray();
        }


        [Obsolete("Use Linq Where(Func<T, bool>) extension instead.")]
        public static T[] FindAll<T>(ReadOnlyArrayR3<T> array, Predicate<T> match)
        {
            if (match == null)
                throw new ArgumentNullException(nameof(match));

            return array.IsAbsent ? Empty<T>.Array : array.Where(new Func<T, bool>(match)).ToArray();
        }
        */


        public static T FindLast<T>(T[] array, Predicate<T> match)
        {
            return Array.FindLast(array, match);
        }


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


        public static T FindLast<T>(this ReadOnlyArray<T> array, Predicate<T> match) // Linq: LastOrDefault - but this is faster!
        {
            if (array.Array == null)
            {
                if (match == null)
                    throw new ArgumentNullException(nameof(match));
                return default(T);
            }
            return Array.FindLast<T>(array.Array, match);
        }


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


        public static T FindLast<T>(ReadOnlyArrayR2<T> array, Predicate<T> match) // <-- Not obsolete because it's faster than Linq LastOrDefault.
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


        public static T FindLast<T>(ReadOnlyArrayR3<T> array, Predicate<T> match) // <-- Not obsolete because it's faster than Linq LastOrDefault.
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


        public static int FindIndex<T>(this T[] array, Predicate<T> match)
        {
            return Array.FindIndex(array, match);
        }


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


        public static int FindIndex<T>(this T[] array, int startIndex, Predicate<T> match)
        {
            return System.Array.FindIndex(array, startIndex, match);
        }


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


        public static int FindIndex<T>(this ReadOnlyArray<T> array, int startIndex, Predicate<T> match)
        {
            if (array.Array == null)
            {
                if (startIndex != 0)
                    throw new ArgumentOutOfRangeException(nameof(startIndex));
                if (match == null)
                    throw new ArgumentNullException(nameof(match));
                return -1;
            }
            return Array.FindIndex<T>(array.Array, startIndex, match);
        }


        public static int FindIndex<T>(this ArrayR2<T> array, int startIndex, Predicate<T> match)
        {
            if (array.Array == null)
            {
                if (startIndex != 0)
                    throw new ArgumentOutOfRangeException(nameof(startIndex));
                if (match == null)
                    throw new ArgumentNullException(nameof(match));
                return -1;
            }
            return FindIndex<T>(array.Array, startIndex, match);
        }


        public static int FindIndex<T>(this ReadOnlyArrayR2<T> array, int startIndex, Predicate<T> match)
        {
            if (array.Array == null)
            {
                if (startIndex != 0)
                    throw new ArgumentOutOfRangeException(nameof(startIndex));
                if (match == null)
                    throw new ArgumentNullException(nameof(match));
                return -1;
            }
            return FindIndex<T>(array.Array, startIndex, match);
        }

        /* wip
        public static int FindIndex<T>(this ArrayR3<T> array, int startIndex, Predicate<T> match)
        {
            if (array.Array == null)
            {
                if (startIndex != 0)
                    throw new ArgumentOutOfRangeException(nameof(startIndex));
                if (match == null)
                    throw new ArgumentNullException(nameof(match));
                return -1;
            }
            return FindIndex<T>(array.Array, startIndex, match);
        }


        public static int FindIndex<T>(this ReadOnlyArrayR3<T> array, int startIndex, Predicate<T> match)
        {
            if (array.Array == null)
            {
                if (startIndex != 0)
                    throw new ArgumentOutOfRangeException(nameof(startIndex));
                if (match == null)
                    throw new ArgumentNullException(nameof(match));
                return -1;
            }
            return FindIndex<T>(array.Array, startIndex, match);
        }
        */


        public static int FindIndex<T>(this T[] array, int startIndex, int count, Predicate<T> match)
        {
            return Array.FindIndex(array, startIndex, count, match);
        }


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


        public static int FindIndex<T>(this ReadOnlyArray<T> array, int startIndex, int count, Predicate<T> match)
        {
            if (array.Array == null)
            {
                if (startIndex != 0)
                    throw new ArgumentOutOfRangeException(nameof(startIndex));
                if (count != 0)
                    throw new ArgumentOutOfRangeException(nameof(count));
                if (match == null)
                    throw new ArgumentNullException(nameof(match));
                return -1;
            }
            return Array.FindIndex<T>(array.Array, startIndex, count, match);
        }


        public static int FindIndex<T>(this ArrayR2<T> array, int startIndex, int count, Predicate<T> match)
        {
            if (array.Array == null)
            {
                if (startIndex != 0)
                    throw new ArgumentOutOfRangeException(nameof(startIndex));
                if (count != 0)
                    throw new ArgumentOutOfRangeException(nameof(count));
                if (match == null)
                    throw new ArgumentNullException(nameof(match));
                return -1;
            }
            return FindIndex(array.Array, startIndex, count, match);
        }


        public static int FindIndex<T>(this ReadOnlyArrayR2<T> array, int startIndex, int count, Predicate<T> match)
        {
            if (array.Array == null)
            {
                if (startIndex != 0)
                    throw new ArgumentOutOfRangeException(nameof(startIndex));
                if (count != 0)
                    throw new ArgumentOutOfRangeException(nameof(count));
                if (match == null)
                    throw new ArgumentNullException(nameof(match));
                return -1;
            }
            return FindIndex(array.Array, startIndex, count, match);
        }

        /* wip
        public static int FindIndex<T>(this ArrayR3<T> array, int startIndex, int count, Predicate<T> match)
        {
            if (array.Array == null)
            {
                if (startIndex != 0)
                    throw new ArgumentOutOfRangeException(nameof(startIndex));
                if (count != 0)
                    throw new ArgumentOutOfRangeException(nameof(count));
                if (match == null)
                    throw new ArgumentNullException(nameof(match));
                return -1;
            }
            return FindIndex(array.Array, startIndex, count, match);
        }


        public static int FindIndex<T>(this ReadOnlyArrayR3<T> array, int startIndex, int count, Predicate<T> match)
        {
            if (array.Array == null)
            {
                if (startIndex != 0)
                    throw new ArgumentOutOfRangeException(nameof(startIndex));
                if (count != 0)
                    throw new ArgumentOutOfRangeException(nameof(count));
                if (match == null)
                    throw new ArgumentNullException(nameof(match));
                return -1;
            }
            return FindIndex(array.Array, startIndex, count, match);
        }
        */


        public static int FindLastIndex<T>(T[] array, Predicate<T> match)
        {
            return Array.FindLastIndex<T>(array, match);
        }


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


        public static int FindLastIndex<T>(T[,,] array, Predicate<T> match)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            if (match == null)
                throw new ArgumentNullException(nameof(match));

            int leny = array.LengthY();
            int lenz = array.LengthZ();
            for (int x = array.LengthX() - 1; x >= 0; --x)
                for (int y = leny - 1; y >= 0; --y)
                    for (int z = lenz - 1; z >= 0; --z)
                        if (match(array[x, y, z]))
                            return (x * leny + y) * lenz + z;

            return -1;
        }


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


        public static int FindLastIndex<T>(T[] array, int startIndex, Predicate<T> match)
        {
            return Array.FindLastIndex<T>(array, startIndex, match);
        }


        public static int FindLastIndex<T>(T[,] array, int startIndex, Predicate<T> match)
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

        
        public static int FindLastIndex<T>(T[,,] array, int startIndex, Predicate<T> match)
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


        public static int FindLastIndex<T>(ReadOnlyArray<T> array, int startIndex, Predicate<T> match)
        {
            if (array.Array == null)
            {
                if (startIndex != 0)
                    throw new ArgumentOutOfRangeException(nameof(startIndex));
                if (match == null)
                    throw new ArgumentNullException(nameof(match));
                return -1;
            }
            return Array.FindLastIndex(array.Array, startIndex, match);
        }


        public static int FindLastIndex<T>(ArrayR2<T> array, int startIndex, Predicate<T> match)
        {
            if (array.Array == null)
            {
                if (startIndex != 0)
                    throw new ArgumentOutOfRangeException(nameof(startIndex));
                if (match == null)
                    throw new ArgumentNullException(nameof(match));
                return -1;
            }
            return FindLastIndex(array.Array, startIndex, match);
        }


        public static int FindLastIndex<T>(ReadOnlyArrayR2<T> array, int startIndex, Predicate<T> match)
        {
            if (array.Array == null)
            {
                if (startIndex != 0)
                    throw new ArgumentOutOfRangeException(nameof(startIndex));
                if (match == null)
                    throw new ArgumentNullException(nameof(match));
                return -1;
            }
            return FindLastIndex(array.Array, startIndex, match);
        }

        /* wip
        public static int FindLastIndex<T>(ArrayR3<T> array, int startIndex, Predicate<T> match)
        {
            if (array.Array == null)
            {
                if (startIndex != 0)
                    throw new ArgumentOutOfRangeException(nameof(startIndex));
                if (match == null)
                    throw new ArgumentNullException(nameof(match));
                return -1;
            }
            return FindLastIndex(array.Array, startIndex, match);
        }


        public static int FindLastIndex<T>(ReadOnlyArrayR3<T> array, int startIndex, Predicate<T> match)
        {
            if (array.Array == null)
            {
                if (startIndex != 0)
                    throw new ArgumentOutOfRangeException(nameof(startIndex));
                if (match == null)
                    throw new ArgumentNullException(nameof(match));
                return -1;
            }
            return FindLastIndex(array.Array, startIndex, match);
        }
        */


        public static int FindLastIndex<T>(T[] array, int startIndex, int count, Predicate<T> match)
        {
            return Array.FindLastIndex<T>(array, startIndex, count, match);
        }


        public static int FindLastIndex<T>(T[,] array, int startIndex, int count, Predicate<T> match)
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


        public static int FindLastIndex<T>(T[,,] array, int startIndex, int count, Predicate<T> match)
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


        public static int FindLastIndex<T>(ReadOnlyArray<T> array, int startIndex, int count, Predicate<T> match)
        {
            if (array.Array == null)
            {
                if (startIndex != 0)
                    throw new ArgumentOutOfRangeException(nameof(startIndex));
                if (count != 0)
                    throw new ArgumentOutOfRangeException(nameof(count));
                if (match == null)
                    throw new ArgumentNullException(nameof(match));
                return -1;
            }
            return Array.FindLastIndex<T>(array.Array, startIndex, count, match);
        }


        public static int FindLastIndex<T>(ArrayR2<T> array, int startIndex, int count, Predicate<T> match)
        {
            if (array.Array == null)
            {
                if (startIndex != 0)
                    throw new ArgumentOutOfRangeException(nameof(startIndex));
                if (count != 0)
                    throw new ArgumentOutOfRangeException(nameof(count));
                if (match == null)
                    throw new ArgumentNullException(nameof(match));
                return -1;
            }
            return FindLastIndex(array.Array, startIndex, count, match);
        }


        public static int FindLastIndex<T>(ReadOnlyArrayR2<T> array, int startIndex, int count, Predicate<T> match)
        {
            if (array.Array == null)
            {
                if (startIndex != 0)
                    throw new ArgumentOutOfRangeException(nameof(startIndex));
                if (count != 0)
                    throw new ArgumentOutOfRangeException(nameof(count));
                if (match == null)
                    throw new ArgumentNullException(nameof(match));
                return -1;
            }
            return FindLastIndex(array.Array, startIndex, count, match);
        }

        /* wip
        public static int FindLastIndex<T>(ArrayR3<T> array, int startIndex, int count, Predicate<T> match)
        {
            if (array.Array == null)
            {
                if (startIndex != 0)
                    throw new ArgumentOutOfRangeException(nameof(startIndex));
                if (count != 0)
                    throw new ArgumentOutOfRangeException(nameof(count));
                if (match == null)
                    throw new ArgumentNullException(nameof(match));
                return -1;
            }
            return FindLastIndex(array.Array, startIndex, count, match);
        }


        public static int FindLastIndex<T>(ReadOnlyArrayR3<T> array, int startIndex, int count, Predicate<T> match)
        {
            if (array.Array == null)
            {
                if (startIndex != 0)
                    throw new ArgumentOutOfRangeException(nameof(startIndex));
                if (count != 0)
                    throw new ArgumentOutOfRangeException(nameof(count));
                if (match == null)
                    throw new ArgumentNullException(nameof(match));
                return -1;
            }
            return FindLastIndex(array.Array, startIndex, count, match);
        }
        */


        public static int IndexOf<T>(this T[] array, T value)
        {
            return Array.IndexOf<T>(array, value);
        }


        public static int IndexOf<T>(this ReadOnlyArray<T> array, T value)
        {
            return array.Array == null ? -1 : Array.IndexOf<T>(array.Array, value);
        }


        public static int IndexOf<T>(this T[,] array, T value)
        {
            return IndexFinder<T>.instance.IndexOf(array, ref value);
        }


        public static int IndexOf<T>(this ArrayR2<T> array, T value)
        {
            return array.Array == null ? -1 : IndexFinder<T>.instance.IndexOf(array.Array, ref value);
        }


        public static int IndexOf<T>(this ReadOnlyArrayR2<T> array, T value)
        {
            return array.Array == null ? -1 : IndexFinder<T>.instance.IndexOf(array.Array, ref value);
        }

        
        public static int IndexOf<T>(this T[,,] array, T value)
        {
            return IndexFinder<T>.instance.IndexOf(array, ref value);
        }

        /* wip
        public static int IndexOf<T>(this ArrayR3<T> array, T value)
        {
            return array.Array == null ? -1 : IndexFinder<T>.instance.IndexOf(array.Array, ref value);
        }


        public static int IndexOf<T>(this ReadOnlyArrayR3<T> array, T value)
        {
            return array.Array == null ? -1 : IndexFinder<T>.instance.IndexOf(array.Array, ref value);
        }
        */


        public static int IndexOf<T>(this T[] array, T value, int startIndex)
        {
            return Array.IndexOf<T>(array, value, startIndex);
        }


        public static int IndexOf<T>(this T[,] array, T value, int startIndex)
        {
            return IndexFinder<T>.instance.IndexOf(array, ref value, startIndex);
        }


        public static int IndexOf<T>(this T[,,] array, T value, int startIndex)
        {
            return IndexFinder<T>.instance.IndexOf(array, ref value, startIndex);
        }


        public static int IndexOf<T>(this ReadOnlyArray<T> array, T value, int startIndex)
        {
            if (array.Array == null)
            {
                if (startIndex != 0)
                    throw new ArgumentOutOfRangeException(nameof(startIndex));
                return -1;
            }
            return Array.IndexOf<T>(array.Array, value, startIndex);
        }


        public static int IndexOf<T>(this ArrayR2<T> array, T value, int startIndex)
        {
            if (array.Array == null)
            {
                if (startIndex != 0)
                    throw new ArgumentOutOfRangeException(nameof(startIndex));
                return -1;
            }
            return IndexFinder<T>.instance.IndexOf(array.Array, ref value, startIndex);
        }


        public static int IndexOf<T>(this ReadOnlyArrayR2<T> array, T value, int startIndex)
        {
            if (array.Array == null)
            {
                if (startIndex != 0)
                    throw new ArgumentOutOfRangeException(nameof(startIndex));
                return -1;
            }
            return IndexFinder<T>.instance.IndexOf(array.Array, ref value, startIndex);
        }

        /* wip
        public static int IndexOf<T>(this ArrayR3<T> array, T value, int startIndex)
        {
            if (array.Array == null)
            {
                if (startIndex != 0)
                    throw new ArgumentOutOfRangeException(nameof(startIndex));
                return -1;
            }
            return IndexFinder<T>.instance.IndexOf(array.Array, ref value, startIndex);
        }


        public static int IndexOf<T>(this ReadOnlyArrayR3<T> array, T value, int startIndex)
        {
            if (array.Array == null)
            {
                if (startIndex != 0)
                    throw new ArgumentOutOfRangeException(nameof(startIndex));
                return -1;
            }
            return IndexFinder<T>.instance.IndexOf(array.Array, ref value, startIndex);
        }
        */


        public static int IndexOf<T>(this T[] array, T value, int startIndex, int count)
        {
            return Array.IndexOf<T>(array, value, startIndex, count);
        }


        public static int IndexOf<T>(this T[,] array, T value, int startIndex, int count)
        {
            return IndexFinder<T>.instance.IndexOf(array, ref value, startIndex, count);
        }


        public static int IndexOf<T>(this T[,,] array, T value, int startIndex, int count)
        {
            return IndexFinder<T>.instance.IndexOf(array, ref value, startIndex, count);
        }


        public static int IndexOf<T>(this ReadOnlyArray<T> array, T value, int startIndex, int count)
        {
            if (array.Array == null)
            {
                if (startIndex != 0)
                    throw new ArgumentOutOfRangeException(nameof(startIndex));
                if (count != 0)
                    throw new ArgumentOutOfRangeException(nameof(count));

                return -1;
            }
            return Array.IndexOf<T>(array.Array, value, startIndex, count);
        }


        public static int IndexOf<T>(this ArrayR2<T> array, T value, int startIndex, int count)
        {
            if (array.Array == null)
            {
                if (startIndex != 0)
                    throw new ArgumentOutOfRangeException(nameof(startIndex));
                if (count != 0)
                    throw new ArgumentOutOfRangeException(nameof(count));

                return -1;
            }
            return IndexFinder<T>.instance.IndexOf(array.Array, ref value, startIndex, count);
        }


        public static int IndexOf<T>(this ReadOnlyArrayR2<T> array, T value, int startIndex, int count)
        {
            if (array.Array == null)
            {
                if (startIndex != 0)
                    throw new ArgumentOutOfRangeException(nameof(startIndex));
                if (count != 0)
                    throw new ArgumentOutOfRangeException(nameof(count));

                return -1;
            }
            return IndexFinder<T>.instance.IndexOf(array.Array, ref value, startIndex, count);
        }

        /* wip
        public static int IndexOf<T>(this ArrayR3<T> array, T value, int startIndex, int count)
        {
            if (array.Array == null)
            {
                if (startIndex != 0)
                    throw new ArgumentOutOfRangeException(nameof(startIndex));
                if (count != 0)
                    throw new ArgumentOutOfRangeException(nameof(count));

                return -1;
            }
            return IndexFinder<T>.instance.IndexOf(array.Array, ref value, startIndex, count);
        }


        public static int IndexOf<T>(this ReadOnlyArrayR3<T> array, T value, int startIndex, int count)
        {
            if (array.Array == null)
            {
                if (startIndex != 0)
                    throw new ArgumentOutOfRangeException(nameof(startIndex));
                if (count != 0)
                    throw new ArgumentOutOfRangeException(nameof(count));

                return -1;
            }
            return IndexFinder<T>.instance.IndexOf(array.Array, ref value, startIndex, count);
        }
        */


        public static int LastIndexOf<T>(this T[] array, T value)
        {
            return Array.LastIndexOf<T>(array, value);
        }


        public static int LastIndexOf<T>(this T[,] array, T value)
        {
            return IndexFinder<T>.instance.LastIndexOf(array, ref value);
        }


        public static int LastIndexOf<T>(this T[,,] array, T value)
        {
            return IndexFinder<T>.instance.LastIndexOf(array, ref value);
        }


        public static int LastIndexOf<T>(this ReadOnlyArray<T> array, T value)
        {
            return array.Array == null ? -1 : Array.LastIndexOf<T>(array.Array, value);
        }


        public static int LastIndexOf<T>(this ArrayR2<T> array, T value)
        {
            return array.Array == null ? -1 : IndexFinder<T>.instance.LastIndexOf(array.Array, ref value);
        }


        public static int LastIndexOf<T>(this ReadOnlyArrayR2<T> array, T value)
        {
            return array.Array == null ? -1 : IndexFinder<T>.instance.LastIndexOf(array.Array, ref value);
        }

        /* wip
        public static int LastIndexOf<T>(this ArrayR3<T> array, T value)
        {
            return array.Array == null ? -1 : IndexFinder<T>.instance.LastIndexOf(array.Array, ref value);
        }


        public static int LastIndexOf<T>(this ReadOnlyArrayR3<T> array, T value)
        {
            return array.Array == null ? -1 : IndexFinder<T>.instance.LastIndexOf(array.Array, ref value);
        }
        */


        public static int LastIndexOf<T>(this T[] array, T value, int startIndex)
        {
            return Array.LastIndexOf<T>(array, value, startIndex);
        }


        public static int LastIndexOf<T>(this T[,] array, T value, int startIndex)
        {
            return IndexFinder<T>.instance.LastIndexOf(array, ref value, startIndex);
        }


        public static int LastIndexOf<T>(this T[,,] array, T value, int startIndex)
        {
            return IndexFinder<T>.instance.LastIndexOf(array, ref value, startIndex);
        }


        public static int LastIndexOf<T>(this ReadOnlyArray<T> array, T value, int startIndex)
        {
            if (array.Array == null)
            {
                if (startIndex != 0)
                    throw new ArgumentOutOfRangeException(nameof(startIndex));

                return -1;
            }
            return Array.LastIndexOf<T>(array.Array, value, startIndex);
        }


        public static int LastIndexOf<T>(this ArrayR2<T> array, T value, int startIndex)
        {
            if (array.Array == null)
            {
                if (startIndex != 0)
                    throw new ArgumentOutOfRangeException(nameof(startIndex));

                return -1;
            }
            return IndexFinder<T>.instance.LastIndexOf(array.Array, ref value, startIndex);
        }


        public static int LastIndexOf<T>(this ReadOnlyArrayR2<T> array, T value, int startIndex)
        {
            if (array.Array == null)
            {
                if (startIndex != 0)
                    throw new ArgumentOutOfRangeException(nameof(startIndex));

                return -1;
            }
            return IndexFinder<T>.instance.LastIndexOf(array.Array, ref value, startIndex);
        }

        /* wip
        public static int LastIndexOf<T>(this ArrayR3<T> array, T value, int startIndex)
        {
            if (array.Array == null)
            {
                if (startIndex != 0)
                    throw new ArgumentOutOfRangeException(nameof(startIndex));

                return -1;
            }
            return IndexFinder<T>.instance.LastIndexOf(array.Array, ref value, startIndex);
        }


        public static int LastIndexOf<T>(this ReadOnlyArrayR3<T> array, T value, int startIndex)
        {
            if (array.Array == null)
            {
                if (startIndex != 0)
                    throw new ArgumentOutOfRangeException(nameof(startIndex));

                return -1;
            }
            return IndexFinder<T>.instance.LastIndexOf(array.Array, ref value, startIndex);
        }
        */


        public static int LastIndexOf<T>(this T[] array, T value, int startIndex, int count)
        {
            return Array.LastIndexOf<T>(array, value, startIndex, count);
        }


        public static int LastIndexOf<T>(this T[,] array, T value, int startIndex, int count)
        {
            return IndexFinder<T>.instance.LastIndexOf(array, ref value, startIndex, count);
        }


        public static int LastIndexOf<T>(this T[,,] array, T value, int startIndex, int count)
        {
            return IndexFinder<T>.instance.LastIndexOf(array, ref value, startIndex, count);
        }


        public static int LastIndexOf<T>(this ReadOnlyArray<T> array, T value, int startIndex, int count)
        {
            if (array.Array == null)
            {
                if (startIndex != 0)
                    throw new ArgumentOutOfRangeException(nameof(startIndex));
                if (count != 0)
                    throw new ArgumentOutOfRangeException(nameof(count));

                return -1;
            }
            return Array.LastIndexOf<T>(array.Array, value, startIndex, count);
        }


        public static int LastIndexOf<T>(this ArrayR2<T> array, T value, int startIndex, int count)
        {
            if (array.Array == null)
            {
                if (startIndex != 0)
                    throw new ArgumentOutOfRangeException(nameof(startIndex));
                if (count != 0)
                    throw new ArgumentOutOfRangeException(nameof(count));

                return -1;
            }
            return IndexFinder<T>.instance.LastIndexOf(array.Array, ref value, startIndex, count);
        }


        public static int LastIndexOf<T>(this ReadOnlyArrayR2<T> array, T value, int startIndex, int count)
        {
            if (array.Array == null)
            {
                if (startIndex != 0)
                    throw new ArgumentOutOfRangeException(nameof(startIndex));
                if (count != 0)
                    throw new ArgumentOutOfRangeException(nameof(count));

                return -1;
            }
            return IndexFinder<T>.instance.LastIndexOf(array.Array, ref value, startIndex, count);
        }

        /* wip
        public static int LastIndexOf<T>(this ArrayR3<T> array, T value, int startIndex, int count)
        {
            if (array.Array == null)
            {
                if (startIndex != 0)
                    throw new ArgumentOutOfRangeException(nameof(startIndex));
                if (count != 0)
                    throw new ArgumentOutOfRangeException(nameof(count));

                return -1;
            }
            return IndexFinder<T>.instance.LastIndexOf(array.Array, ref value, startIndex, count);
        }


        public static int LastIndexOf<T>(this ReadOnlyArrayR3<T> array, T value, int startIndex, int count)
        {
            if (array.Array == null)
            {
                if (startIndex != 0)
                    throw new ArgumentOutOfRangeException(nameof(startIndex));
                if (count != 0)
                    throw new ArgumentOutOfRangeException(nameof(count));

                return -1;
            }
            return IndexFinder<T>.instance.LastIndexOf(array.Array, ref value, startIndex, count);
        }
        */
    }
}
