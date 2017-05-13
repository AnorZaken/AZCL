using System;
using System.Collections.Generic;
using System.Linq;

namespace AZCL
{
    /// <summary>
    /// Helper class with various array related methods and extensions.
    /// </summary>
    public static class ArrayHelper
    {
        private const string
            ERR_POPULATE_INNER = "Inner array can't be populated because it's null.",
            ERR_CREATE_INNER = "One or more inner array already exist.";

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
            array = array == null ? new T[] { value } : ArrayCopyExtensions.CopyAndAppend(array, value);
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
                    throw new ArgumentException(paramName: nameof(array), message: ERR_CREATE_INNER);

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
                    throw new ArgumentException(paramName: nameof(array), message: ERR_CREATE_INNER);

                CreateJagged(out array[i], secondLength, innermostLength);
            }
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
                    throw new ArgumentException(paramName: nameof(array), message: ERR_POPULATE_INNER);
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
                    throw new ArgumentException(paramName: nameof(array), message: ERR_POPULATE_INNER);
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

        private static bool Populate_FindNonEmptyInner<T>(T[][] array, out T[] inner, out int i) // 'i' will be the index of 'inner'
        {
            for (i = 0; i < array.Length; ++i)
            {
                inner = array[i];

                if (inner == null)
                    throw new ArgumentException(paramName: nameof(array), message: ERR_POPULATE_INNER);

                if (inner.Length != 0)
                    return true;
            }

            inner = null;
            return false;
        }

        private static void Populate_RepeatCopy<T>(T[][] array, ref T[] copysource, int i) // 'i' should be the index of the first inner target
        {
            // use Array.Copy and 'copysource' to populate all remaining inner arrays
            for (; i < array.Length; ++i)
            {
                T[] copytarget = array[i];

                if (copytarget == null)
                    throw new ArgumentException(paramName: nameof(array), message: ERR_POPULATE_INNER);

                ChunkCopy_Impl(ref copysource, copytarget);
            }
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
            
            foreach (T[][] arr in array)
                if (arr == null)
                    throw new ArgumentException(paramName: nameof(array), message: ERR_POPULATE_INNER);
                else
                    Populate(arr, value);
        }

        /// <summary>
        /// Fills the array using a System.Func&lt;T&gt;.
        /// </summary><remarks>
        /// The System.Func&lt;int, T&gt; will be called once per element in the array, starting at the first element and progressing in order.
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
        /// Fills the inner arrays using a System.Func&lt;T&gt;.
        /// </summary><remarks>
        /// The System.Func&lt;int, T&gt; will be called once per element of each inner array,
        /// filling one inner array at a time, in index order, e.g. [0][0], [0][1], [0][2], ... , [1][0], [1][1], etc.
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
                    throw new ArgumentException(paramName: nameof(array), message: ERR_POPULATE_INNER);

                Populate(arr, factoryFunc);
            }
        }

        /// <summary>
        /// Fills the innermost arrays using a System.Func&lt;T&gt;.
        /// </summary><remarks>
        /// The System.Func&lt;int, T&gt; will be called once per element of each innermost array,
        /// filling one inner array at a time, in index order, e.g. [0][0][0], [0][0][1], [0][0][2], ... , [0][1][0], [0][1][1], etc.
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
                    throw new ArgumentException(paramName: nameof(array), message: ERR_POPULATE_INNER);

                Populate(arr, factoryFunc);
            }
        }

        /// <summary>
        /// Fills the array using a System.Func&lt;int, T&gt; that takes the array index as argument.
        /// </summary><remarks>
        /// The System.Func&lt;int, T&gt; will be called once per element in the array, with the index of the element to populate as argument.
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
        /// Fills the inner arrays using a System.Func&lt;int, int, T&gt; that takes the array indexes as arguments.
        /// </summary><remarks>
        /// The System.Func&lt;int, int, T&gt; will be called once per element of each inner array,
        /// filling one inner array at a time, in index order, e.g. [0][0], [0][1], [0][2], ... , [1][0], [1][1], etc.
        /// The index arguments will be supplied in the same order, i.e <paramref name="array"/>[x][y] = <paramref name="factoryFunc"/>(x, y).
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
                    throw new ArgumentException(paramName: nameof(array), message: ERR_POPULATE_INNER);

                for (int y = 0; y < arr1.Length; ++y)
                    arr1[y] = factoryFunc(x, y);
            }
        }

        /// <summary>
        /// Fills the innermost arrays using a System.Func&lt;int, int, int, T&gt; that takes the array indexes as arguments.
        /// </summary><remarks>
        /// The System.Func&lt;int, T&gt; will be called once per element of each innermost array,
        /// filling one inner array at a time, in index order, e.g. [0][0][0], [0][0][1], [0][0][2], ... , [0][1][0], [0][1][1], etc.
        /// The index arguments will be supplied in the same order, i.e <paramref name="array"/>[x][y][z] = <paramref name="factoryFunc"/>(x, y, z).
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
                    throw new ArgumentException(paramName: nameof(array), message: ERR_POPULATE_INNER);

                for (int y = 0; y < arr2.Length; ++y)
                {
                    T[] arr1 = arr2[y];
                    if (arr1 == null)
                        throw new ArgumentException(paramName: nameof(array), message: ERR_POPULATE_INNER);

                    for (int z = 0; z < arr1.Length; ++z)
                        arr1[z] = factoryFunc(x, y, z);
                }
            }
        }

        // -----

        // assumes valid arguments!
        private static void ChunkCopy_Impl<T>(ref T[] source, T[] target)
        {
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

        // assumes valid arguments!
        private static void RepeatRange_Impl(Array array, int has) // (array 'has' available)
        {
            int rem = array.Length - has; // (elements remaining)
            while (rem > has)
            {
                System.Array.Copy(array, 0, array, has, has); // copy as much as the array has
                rem -= has;
                has <<= 1; // 'has' doubles each pass
            }
            System.Array.Copy(array, 0, array, has, rem); // copy final elements (less than 'has')
        }

        // assumes valid arguments!
        private static void SetRange_ForLoop<T>(T[,] array, ref T value, int count, int x = 0, int y = 0)
        {
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

        // assumes valid arguments!
        private static void SetRange_ForLoop<T>(T[,,] array, ref T value, int count, int x = 0, int y = 0, int z = 0)
        {
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

        // todo: populate for rank 2 & 3 arrays
    }
}
