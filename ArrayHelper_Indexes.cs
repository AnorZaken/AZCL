using System;
using AZCL.Collections;

namespace AZCL
{
    // XML summary is in ArrayHelper.cs
    public static partial class ArrayHelper
    {
        /// <summary>
        /// Given a one-dimensional enumeration index, calculates the corresponding x and y element indexes.
        /// </summary><remarks>
        /// <inheritdoc cref="AZCL.Collections.ArrayR2{T}.Enumerator" select="para[@id='enumerationOrder']"/>
        /// </remarks>
        /// <param name="array">The array whose dimensions to calculate indexes for.</param>
        /// <param name="index">An enumeration index to transform into regular indexes.</param>
        /// <param name="x">Resulting x index.</param>
        /// <param name="y">Resulting y index.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="array"/> is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="index"/> is less than zero or greater than or equal to the length of the specified array.
        /// </exception>
        /// <seealso cref="TryCalculateIndexes{T}(T[,], int, out int, out int)"/>
        /// <seealso cref="AZCL.Collections.ArrayR2{T}.GetValue1D(int)"/>
        /// <seealso cref="AZCL.Collections.LinqForMultiRankArrays.ElementAt{TSource}(TSource[,], int)"/>
        public static void CalculateIndexes<T>(T[,] array, int index, out int x, out int y)
        {
            if (!TryCalculateIndexes(array, index, out x, out y))
                throw new ArgumentOutOfRangeException(paramName: nameof(index));
        }

        /// <summary>
        /// Given a one-dimensional enumeration index, calculates the corresponding x, y, and z element indexes.
        /// </summary>
        /// <inheritdoc cref="CalculateIndexes{T}(T[,], int, out int, out int)" select="remarks"/>
        /// <param name="array">The array whose dimensions to calculate indexes for.</param>
        /// <param name="index">An enumeration index to transform into regular indexes.</param>
        /// <param name="x">Resulting x index.</param>
        /// <param name="y">Resulting y index.</param>
        /// <param name="z">Resulting z index.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="array"/> is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="index"/> is less than zero or greater than or equal to the length of the specified array.
        /// </exception>
        /// <seealso cref="TryCalculateIndexes{T}(T[,,], int, out int, out int, out int)"/>
        /// <seealso cref="AZCL.Collections.ArrayR3{T}.GetValue1D(int)"/>
        /// <seealso cref="AZCL.Collections.LinqForMultiRankArrays.ElementAt{TSource}(TSource[,,], int)"/>
        public static void CalculateIndexes<T>(T[,,] array, int index, out int x, out int y, out int z)
        {
            if (!TryCalculateIndexes(array, index, out x, out y, out z))
                throw new ArgumentOutOfRangeException(paramName: nameof(index));
        }

        /// <summary>
        /// Given a one-dimensional enumeration index, calculates the corresponding x, y, and z element indexes.
        /// </summary>
        /// <inheritdoc cref="CalculateIndexes{T}(T[,], int, out int, out int)" select="remarks"/>
        /// <returns>
        /// An <see cref="Tuples.Int3"/> tuple with the resulting x, y, and z index.
        /// </returns>
        /// <param name="array">The array whose dimensions to calculate indexes for.</param>
        /// <param name="index">An enumeration index to transform into regular indexes.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="array"/> is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="index"/> is less than zero or greater than or equal to the length of the specified array.
        /// </exception>
        /// <seealso cref="TryCalculateIndexes{T}(T[,,], int, out Tuples.Int3)"/>
        /// <seealso cref="AZCL.Collections.ArrayR3{T}.GetValue1D(int)"/>
        /// <seealso cref="AZCL.Collections.LinqForMultiRankArrays.ElementAt{TSource}(TSource[,,], int)"/>
        public static Tuples.Int3 CalculateIndexes<T>(T[,,] array, int index)
        {
            Tuples.Int3 r;
            if (TryCalculateIndexes(array, index, out r))
                return r;
            throw new ArgumentOutOfRangeException(paramName: nameof(index));
        }

        /// <summary>
        /// Given a one-dimensional enumeration index, calculates the corresponding w, x, y, and z element indexes.
        /// </summary>
        /// <inheritdoc cref="CalculateIndexes{T}(T[,], int, out int, out int)" select="remarks"/>
        /// <param name="array">The array whose dimensions to calculate indexes for.</param>
        /// <param name="index">An enumeration index to transform into regular indexes.</param>
        /// <param name="w">Resulting w index.</param>
        /// <param name="x">Resulting x index.</param>
        /// <param name="y">Resulting y index.</param>
        /// <param name="z">Resulting z index.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="array"/> is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="index"/> is less than zero or greater than or equal to the length of the specified array.
        /// </exception>
        /// <seealso cref="TryCalculateIndexes{T}(T[,,,], int, out int, out int, out int, out int)"/>
        /// <seealso cref="AZCL.Collections.LinqForMultiRankArrays.ElementAt{TSource}(TSource[,,,], int)"/>
        public static void CalculateIndexes<T>(T[,,,] array, int index, out int w, out int x, out int y, out int z)
        {
            if (!TryCalculateIndexes(array, index, out w, out x, out y, out z))
                throw new ArgumentOutOfRangeException(paramName: nameof(index));
        }

        /// <summary>
        /// Given a one-dimensional enumeration index, calculates the corresponding w, x, y, and z element indexes.
        /// </summary>
        /// <inheritdoc cref="CalculateIndexes{T}(T[,], int, out int, out int)" select="remarks"/>
        /// <returns>
        /// An <see cref="Tuples.Int4"/> tuple with the resulting w, x, y, and z index.
        /// </returns>
        /// <param name="array">The array whose dimensions to calculate indexes for.</param>
        /// <param name="index">An enumeration index to transform into regular indexes.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="array"/> is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="index"/> is less than zero or greater than or equal to the length of the specified array.
        /// </exception>
        /// <seealso cref="TryCalculateIndexes{T}(T[,,,], int, out Tuples.Int4)"/>
        /// <seealso cref="AZCL.Collections.LinqForMultiRankArrays.ElementAt{TSource}(TSource[,,,], int)"/>
        public static Tuples.Int4 CalculateIndexes<T>(T[,,,] array, int index)
        {
            Tuples.Int4 r;
            if (TryCalculateIndexes(array, index, out r))
                return r;
            throw new ArgumentOutOfRangeException(paramName: nameof(index));
        }

        /// <summary>
        /// Given a one-dimensional enumeration index, calculates the corresponding v, w, x, y, and z element indexes.
        /// </summary>
        /// <inheritdoc cref="CalculateIndexes{T}(T[,], int, out int, out int)" select="remarks"/>
        /// <returns>
        /// An <see cref="Tuples.Int5"/> tuple with the resulting v, w, x, y, and z index.
        /// </returns>
        /// <param name="array">The array whose dimensions to calculate indexes for.</param>
        /// <param name="index">An enumeration index to transform into regular indexes.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="array"/> is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="index"/> is less than zero or greater than or equal to the length of the specified array.
        /// </exception>
        /// <seealso cref="TryCalculateIndexes{T}(T[,,,,], int, out Tuples.Int5)"/>
        /// <seealso cref="AZCL.Collections.LinqForMultiRankArrays.ElementAt{TSource}(TSource[,,,,], int)"/>
        public static Tuples.Int5 CalculateIndexes<T>(T[,,,,] array, int index)
        {
            Tuples.Int5 r;
            if (TryCalculateIndexes(array, index, out r))
                return r;
            throw new ArgumentOutOfRangeException(paramName: nameof(index));
        }

        /// <summary>
        /// Given a one-dimensional enumeration index, calculates the corresponding six element indexes.
        /// </summary>
        /// <inheritdoc cref="CalculateIndexes{T}(T[,], int, out int, out int)" select="remarks"/>
        /// <returns>
        /// An <see cref="Tuples.Int6"/> tuple with the resulting six element indexes.
        /// </returns>
        /// <param name="array">The array whose dimensions to calculate indexes for.</param>
        /// <param name="index">An enumeration index to transform into regular indexes.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="array"/> is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="index"/> is less than zero or greater than or equal to the length of the specified array.
        /// </exception>
        /// <seealso cref="TryCalculateIndexes{T}(T[,,,,,], int, out Tuples.Int6)"/>
        /// <seealso cref="AZCL.Collections.LinqForMultiRankArrays.ElementAt{TSource}(TSource[,,,,,], int)"/>
        public static Tuples.Int6 CalculateIndexes<T>(T[,,,,,] array, int index)
        {
            Tuples.Int6 r;
            if (TryCalculateIndexes(array, index, out r))
                return r;
            throw new ArgumentOutOfRangeException(paramName: nameof(index));
        }

        /// <summary>
        /// Given a one-dimensional enumeration index, calculates the corresponding element indexes for retrieving that element.
        /// </summary>
        /// <inheritdoc cref="CalculateIndexes{T}(T[,], int, out int, out int)" select="remarks"/>
        /// <returns>
        /// Returns an <c>int[]</c> array containing the resulting element indexes.
        /// (Length of the returned array is equal to the Rank of the <paramref name="array"/> parameter.)
        /// </returns>
        /// <example><code>
        /// var arr = new [,] {{ 'A', 'B' }, { 'C', 'D' }};
        /// Console.Write(arr.GetValue(ArrayHelper.CalculateIndexes(arr, 2))); // prints "C"
        /// </code></example>
        /// <param name="array">The array whose dimensions to calculate indexes for.</param>
        /// <param name="index">An enumeration index to transform into regular indexes.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="array"/> is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="index"/> is less than zero or greater than or equal to the length of the specified array.
        /// </exception>
        /// <seealso cref="TryCalculateIndexes(Array, int)"/>
        /// <seealso cref="O:AZCL.Collections.LinqForMultiRankArrays.ElementAt{TSource}"/>
        public static int[] CalculateIndexes(Array array, int index)
        {
            var r = TryCalculateIndexes(array, index);
            if (r == null)
                throw new ArgumentOutOfRangeException(paramName: nameof(index));
            return r;
        }

        //---

        /* 
         * From MSDN:
         * https://docs.microsoft.com/en-us/dotnet/framework/configure-apps/file-schema/runtime/gcallowverylargeobjects-element
         * "
         * The maximum number of elements in an array is System.UInt32.MaxValue.
         * The maximum index in any single dimension is 2,147,483,591 (0x7FFFFFC7) for byte arrays and arrays of single-byte structures,
         *  and 2,146,435,071 (0X7FEFFFFF) for other types.
         * "
        */

        /// <summary>
        /// Calculates the enumeration index for the specified array and element indexes.
        /// </summary>
        /// <param name="array">The array to calculate enumeration index for.</param>
        /// <param name="x">X element index.</param>
        /// <param name="y">Y element index.</param>
        /// <remarks>
        /// This is the same as calling <see cref="O:AZCL.ArrayHelper.CalculateIndexUC{T}"/> except that it throws an OverflowException
        /// if the resulting enumeration index is greater than Int32.MaxValue, instead of simply returning a negative value.
        /// <para/>
        /// For information on maximum array sizes see MSDN:<br/>
        /// <a href="https://docs.microsoft.com/en-us/dotnet/framework/configure-apps/file-schema/runtime/gcallowverylargeobjects-element">
        /// https://docs.microsoft.com/en-us/dotnet/framework/configure-apps/file-schema/runtime/gcallowverylargeobjects-element </a>
        /// (specifically the Remarks section).
        /// </remarks>
        /// <inheritdoc cref="CalculateIndex{T}(T[,], int, int)" select="returns"/>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if any of the element indexes are out of bounds.
        /// </exception>
        /// <exception cref="OverflowException">
        /// Thrown if the resulting enumeration index is greater than Int32.MaxValue.
        /// </exception>
        /// <seealso cref="O:AZCL.ArrayHelper.CalculateIndexUC{T}"/>
        public static int CalculateIndex<T>(T[,] array, int x, int y)
        {
            int i = CalculateIndexUC(array, x, y);
            if (i < 0) throw new OverflowException();
            return i;
        }

        /// <summary>
        /// Calculates the enumeration index for the specified array and element indexes.
        /// </summary>
        /// <param name="array">The array to calculate enumeration index for.</param>
        /// <param name="x">X element index.</param>
        /// <param name="y">Y element index.</param>
        /// <param name="z">Z element index.</param>
        /// <inheritdoc cref="CalculateIndex{T}(T[,], int, int)" select="remarks|returns|seealso"/>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if any of the element indexes are out of bounds.
        /// </exception>
        /// <exception cref="OverflowException">
        /// Thrown if the resulting enumeration index is greater than Int32.MaxValue.
        /// </exception>
        public static int CalculateIndex<T>(T[,,] array, int x, int y, int z)
        {
            int i = CalculateIndexUC(array, x, y, z);
            if (i < 0) throw new OverflowException();
            return i;
        }

        /// <summary>
        /// Calculates the enumeration index for the specified array and element indexes.
        /// </summary>
        /// <param name="array">The array to calculate enumeration index for.</param>
        /// <param name="w">W element index.</param>
        /// <param name="x">X element index.</param>
        /// <param name="y">Y element index.</param>
        /// <param name="z">Z element index.</param>
        /// <inheritdoc cref="CalculateIndex{T}(T[,], int, int)" select="remarks|returns|seealso"/>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if any of the element indexes are out of bounds.
        /// </exception>
        /// <exception cref="OverflowException">
        /// Thrown if the resulting enumeration index is greater than Int32.MaxValue.
        /// </exception>
        public static int CalculateIndex<T>(T[,,,] array, int w, int x, int y, int z)
        {
            int i = CalculateIndexUC(array, w, x, y, z);
            if (i < 0) throw new OverflowException();
            return i;
        }

        /// <summary>
        /// Calculates the enumeration index for the specified array and element indexes.
        /// </summary>
        /// <param name="array">The array to calculate enumeration index for.</param>
        /// <param name="xyz">The element indexes.</param>
        /// <inheritdoc cref="CalculateIndex{T}(T[,], int, int)" select="remarks|returns|seealso"/>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if any of the element indexes are out of bounds.
        /// </exception>
        /// <exception cref="OverflowException">
        /// Thrown if the resulting enumeration index is greater than Int32.MaxValue.
        /// </exception>
        public static int CalculateIndex<T>(T[,,] array, Tuples.Int3 xyz)
        {
            int i = CalculateIndexUC(array, xyz);
            if (i < 0) throw new OverflowException();
            return i;
        }

        /// <summary>
        /// Calculates the enumeration index for the specified array and element indexes.
        /// </summary>
        /// <param name="array">The array to calculate enumeration index for.</param>
        /// <param name="wxyz">The element indexes.</param>
        /// <inheritdoc cref="CalculateIndex{T}(T[,], int, int)" select="remarks|returns|seealso"/>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if any of the element indexes are out of bounds.
        /// </exception>
        /// <exception cref="OverflowException">
        /// Thrown if the resulting enumeration index is greater than Int32.MaxValue.
        /// </exception>
        public static int CalculateIndex<T>(T[,,,] array, Tuples.Int4 wxyz)
        {
            int i = CalculateIndexUC(array, wxyz);
            if (i < 0) throw new OverflowException();
            return i;
        }

        /// <summary>
        /// Calculates the enumeration index for the specified array and element indexes.
        /// </summary>
        /// <param name="array">The array to calculate enumeration index for.</param>
        /// <param name="vwxyz">The element indexes.</param>
        /// <inheritdoc cref="CalculateIndex{T}(T[,], int, int)" select="remarks|returns|seealso"/>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if any of the element indexes are out of bounds.
        /// </exception>
        /// <exception cref="OverflowException">
        /// Thrown if the resulting enumeration index is greater than Int32.MaxValue.
        /// </exception>
        public static int CalculateIndex<T>(T[,,,,] array, Tuples.Int5 vwxyz)
        {
            int i = CalculateIndexUC(array, vwxyz);
            if (i < 0) throw new OverflowException();
            return i;
        }

        /// <summary>
        /// Calculates the enumeration index for the specified array and element indexes.
        /// </summary>
        /// <param name="array">The array to calculate enumeration index for.</param>
        /// <param name="indexes">The element indexes.</param>
        /// <inheritdoc cref="CalculateIndex{T}(T[,], int, int)" select="remarks|returns|seealso"/>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if any of the element indexes are out of bounds.
        /// </exception>
        /// <exception cref="OverflowException">
        /// Thrown if the resulting enumeration index is greater than Int32.MaxValue.
        /// </exception>
        public static int CalculateIndex<T>(T[,,,,,] array, Tuples.Int6 indexes)
        {
            int i = CalculateIndexUC(array, indexes);
            if (i < 0) throw new OverflowException();
            return i;
        }

        /// <summary>
        /// Calculates the enumeration index for the specified array and element indexes.
        /// </summary>
        /// <param name="array">The array to calculate enumeration index for.</param>
        /// <param name="indexes">An array of element indexes.</param>
        /// <inheritdoc cref="CalculateIndex{T}(T[,], int, int)" select="remarks|returns|seealso"/>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if any of the element indexes are out of bounds.
        /// </exception>
        /// <exception cref="RankException">
        /// Thrown if the rank of the array differs from the number of indexes provided, or if the rank of the array is zero.
        /// </exception>
        /// <exception cref="OverflowException">
        /// Thrown if the resulting enumeration index is greater than Int32.MaxValue.
        /// </exception>
        public static int CalculateIndex(Array array, int[] indexes)
        {
            int i = CalculateIndexUC(array, indexes);
            if (i < 0) throw new OverflowException();
            return i;
        }

        // ---

        /// <summary>
        /// Calculates the enumeration index for the specified array and element indexes. (UC = Unchecked)
        /// </summary>
        /// <param name="array">The array to calculate enumeration index for.</param>
        /// <param name="x">X element index.</param>
        /// <param name="y">Y element index.</param>
        /// <remarks>
        /// <note type="important">
        /// Since the theoretical limit for number of elements a multi-rank array can hold is UInt32.MaxValue* the returned
        /// enumeration index is not guaranteed to be positive! The caller should check that the value returned is positive
        /// unless the specified array is known by the caller to be sufficiently small.
        /// <br/>
        /// <i>*Slightly less for rank 2 arrays, and in practice usually much less because of other constraints such as memory.</i>
        /// </note>
        /// <para/>
        /// For information on maximum array sizes see MSDN:<br/>
        /// <a href="https://docs.microsoft.com/en-us/dotnet/framework/configure-apps/file-schema/runtime/gcallowverylargeobjects-element">
        /// https://docs.microsoft.com/en-us/dotnet/framework/configure-apps/file-schema/runtime/gcallowverylargeobjects-element </a>
        /// (specifically the Remarks section).
        /// </remarks>
        /// <returns>
        /// The enumeration index that corresponds to the specified element indexes for the given array.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if any of the element indexes are out of bounds.
        /// </exception>
        /// <seealso cref="O:AZCL.ArrayHelper.CalculateIndex{T}"/>
        public static int CalculateIndexUC<T>(T[,] array, int x, int y)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            int xlen = array.LengthX();
            int ylen = array.LengthY();

            unchecked
            {
                if ((uint)x >= (uint)xlen)
                    throw new ArgumentOutOfRangeException(nameof(x));
                if ((uint)y >= (uint)ylen)
                    throw new ArgumentOutOfRangeException(nameof(y));

                return x * ylen + y;
            }
        }

        /// <summary>
        /// Calculates the enumeration index for the specified array and element indexes. (UC = Unchecked)
        /// </summary>
        /// <param name="array">The array to calculate enumeration index for.</param>
        /// <param name="x">X element index.</param>
        /// <param name="y">Y element index.</param>
        /// <param name="z">Z element index.</param>
        /// <inheritdoc cref="CalculateIndexUC{T}(T[,], int, int)" select="remarks|returns|seealso"/>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if any of the element indexes are out of bounds.
        /// </exception>
        public static int CalculateIndexUC<T>(T[,,] array, int x, int y, int z)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            int xlen = array.LengthX();
            int ylen = array.LengthY();
            int zlen = array.LengthZ();

            unchecked
            {
                if ((uint)x >= (uint)xlen)
                    throw new ArgumentOutOfRangeException(nameof(x));
                if ((uint)y >= (uint)ylen)
                    throw new ArgumentOutOfRangeException(nameof(y));
                if ((uint)z >= (uint)zlen)
                    throw new ArgumentOutOfRangeException(nameof(z));

                return z + (y + x * ylen) * zlen;
            }
        }

        /// <summary>
        /// Calculates the enumeration index for the specified array and element indexes. (UC = Unchecked)
        /// </summary>
        /// <param name="array">The array to calculate enumeration index for.</param>
        /// <param name="w">W element index.</param>
        /// <param name="x">X element index.</param>
        /// <param name="y">Y element index.</param>
        /// <param name="z">Z element index.</param>
        /// <inheritdoc cref="CalculateIndexUC{T}(T[,], int, int)" select="remarks|returns|seealso"/>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if any of the element indexes are out of bounds.
        /// </exception>
        public static int CalculateIndexUC<T>(T[,,,] array, int w, int x, int y, int z)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            int wlen = array.LengthW();
            int xlen = array.LengthX();
            int ylen = array.LengthY();
            int zlen = array.LengthZ();

            unchecked
            {
                if ((uint)w >= (uint)wlen)
                    throw new ArgumentOutOfRangeException(nameof(w));
                if ((uint)x >= (uint)xlen)
                    throw new ArgumentOutOfRangeException(nameof(x));
                if ((uint)y >= (uint)ylen)
                    throw new ArgumentOutOfRangeException(nameof(y));
                if ((uint)z >= (uint)zlen)
                    throw new ArgumentOutOfRangeException(nameof(z));

                return z + (y + (x + w * xlen) * ylen) * zlen;
            }
        }

        /// <summary>
        /// Calculates the enumeration index for the specified array and element indexes. (UC = Unchecked)
        /// </summary>
        /// <param name="array">The array to calculate enumeration index for.</param>
        /// <param name="xyz">The element indexes.</param>
        /// <inheritdoc cref="CalculateIndexUC{T}(T[,], int, int)" select="remarks|returns|seealso"/>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if any of the element indexes are out of bounds.
        /// </exception>
        public static int CalculateIndexUC<T>(T[,,] array, Tuples.Int3 xyz)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            int xlen = array.LengthX();
            int ylen = array.LengthY();
            int zlen = array.LengthZ();

            unchecked
            {
                if ((uint)xyz.x >= (uint)xlen | // do not branch these - only makes the code faster if it throws, which is pointless.
                    (uint)xyz.y >= (uint)ylen |
                    (uint)xyz.z >= (uint)zlen)
                    throw new ArgumentOutOfRangeException(nameof(xyz));

                return xyz.z + (xyz.y + xyz.x * ylen) * zlen;
            }
        }

        /// <summary>
        /// Calculates the enumeration index for the specified array and element indexes. (UC = Unchecked)
        /// </summary>
        /// <param name="array">The array to calculate enumeration index for.</param>
        /// <param name="wxyz">The element indexes.</param>
        /// <inheritdoc cref="CalculateIndexUC{T}(T[,], int, int)" select="remarks|returns|seealso"/>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if any of the element indexes are out of bounds.
        /// </exception>
        public static int CalculateIndexUC<T>(T[,,,] array, Tuples.Int4 wxyz)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            int wlen = array.GetLength(0);
            int xlen = array.GetLength(1);
            int ylen = array.GetLength(2);
            int zlen = array.GetLength(3);

            unchecked
            {
                if ((uint)wxyz.w >= (uint)wlen | // do not branch these - only makes the code faster if it throws, which is pointless.
                    (uint)wxyz.x >= (uint)xlen |
                    (uint)wxyz.y >= (uint)ylen |
                    (uint)wxyz.z >= (uint)zlen)
                    throw new ArgumentOutOfRangeException(nameof(wxyz));

                return wxyz.z + (wxyz.y + (wxyz.x + wxyz.w * xlen) * ylen) * zlen;
            }
        }

        /// <summary>
        /// Calculates the enumeration index for the specified array and element indexes. (UC = Unchecked)
        /// </summary>
        /// <param name="array">The array to calculate enumeration index for.</param>
        /// <param name="vwxyz">The element indexes.</param>
        /// <inheritdoc cref="CalculateIndexUC{T}(T[,], int, int)" select="remarks|returns|seealso"/>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if any of the element indexes are out of bounds.
        /// </exception>
        public static int CalculateIndexUC<T>(T[,,,,] array, Tuples.Int5 vwxyz)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            int vlen = array.GetLength(0);
            int wlen = array.GetLength(1);
            int xlen = array.GetLength(2);
            int ylen = array.GetLength(3);
            int zlen = array.GetLength(4);

            unchecked
            {
                if ((uint)vwxyz.v >= (uint)vlen | // do not branch these - only makes the code faster if it throws, which is pointless.
                    (uint)vwxyz.w >= (uint)wlen |
                    (uint)vwxyz.x >= (uint)xlen |
                    (uint)vwxyz.y >= (uint)ylen |
                    (uint)vwxyz.z >= (uint)zlen)
                    throw new ArgumentOutOfRangeException(nameof(vwxyz));

                return vwxyz.z + (vwxyz.y + (vwxyz.x + (vwxyz.w + vwxyz.v * wlen) * xlen) * ylen) * zlen;
            }
        }

        /// <summary>
        /// Calculates the enumeration index for the specified array and element indexes. (UC = Unchecked)
        /// </summary>
        /// <param name="array">The array to calculate enumeration index for.</param>
        /// <param name="indexes">The element indexes.</param>
        /// <inheritdoc cref="CalculateIndexUC{T}(T[,], int, int)" select="remarks|returns|seealso"/>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if any of the element indexes are out of bounds.
        /// </exception>
        public static int CalculateIndexUC<T>(T[,,,,,] array, Tuples.Int6 indexes)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            int alen = array.GetLength(0);
            int blen = array.GetLength(1);
            int clen = array.GetLength(2);
            int dlen = array.GetLength(3);
            int elen = array.GetLength(4);
            int flen = array.GetLength(5);

            unchecked
            {
                if ((uint)indexes.a >= (uint)alen | // do not branch these - only makes the code faster if it throws, which is pointless.
                    (uint)indexes.b >= (uint)blen |
                    (uint)indexes.c >= (uint)clen |
                    (uint)indexes.d >= (uint)dlen |
                    (uint)indexes.e >= (uint)elen |
                    (uint)indexes.f >= (uint)flen)
                    throw new ArgumentOutOfRangeException(nameof(indexes));

                return indexes.f + (indexes.e + (indexes.d + (indexes.c + (indexes.b + indexes.a * blen) * clen) * dlen) * elen) * flen;
            }
        }

        /// <summary>
        /// Calculates the enumeration index for the specified array and element indexes. (UC = Unchecked)
        /// </summary>
        /// <param name="array">The array to calculate enumeration index for.</param>
        /// <param name="indexes">An array of element indexes.</param>
        /// <inheritdoc cref="CalculateIndexUC{T}(T[,], int, int)" select="remarks|returns|seealso"/>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="array"/> or <paramref name="indexes"/> is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if any of the element indexes are out of bounds.
        /// </exception>
        /// <exception cref="RankException">
        /// Thrown if the rank of the array differs from the number of indexes provided, or if the rank of the array is zero.
        /// </exception>
        public static int CalculateIndexUC(Array array, int[] indexes)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            if (indexes == null)
                throw new ArgumentNullException(nameof(indexes));
            if (array.Rank != indexes.Length)
                throw new RankException("array.Rank != indexes.Length");
            if (indexes.Length == 0)
                throw new RankException("array.Rank == 0");
            
            unchecked
            {
                int sum = 0;
                for (int r = 0; r < indexes.Length; ++r)
                {
                    int len = array.GetLength(r);
                    int idx = indexes[r];
                    if ((uint)idx >= (uint)len)
                        throw new ArgumentOutOfRangeException(nameof(indexes));

                    sum = sum * len + idx;
                }
                return sum;
            }
        }

        // ---

        /// <summary>
        /// Calculates the enumeration index for the specified array and element indexes.
        /// </summary>
        /// <param name="array">The array to calculate enumeration index for.</param>
        /// <param name="x">X element index.</param>
        /// <param name="y">Y element index.</param>
        /// <inheritdoc cref="CalculateIndex{T}(T[,], int, int)" select="remarks|returns|seealso"/>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if any of the element indexes are out of bounds.
        /// </exception>
        /// <exception cref="OverflowException">
        /// Thrown if the resulting enumeration index is greater than Int32.MaxValue.
        /// </exception>
        public static int CalculateIndex<T>(ArrayR2<T> array, int x, int y)
        {
            int i = CalculateIndexUC(array.Array, x, y);
            if (i < 0) throw new OverflowException();
            return i;
        }

        /// <summary>
        /// Calculates the enumeration index for the specified array and element indexes.
        /// </summary>
        /// <param name="array">The array to calculate enumeration index for.</param>
        /// <param name="x">X element index.</param>
        /// <param name="y">Y element index.</param>
        /// <param name="z">Z element index.</param>
        /// <inheritdoc cref="CalculateIndex{T}(T[,], int, int)" select="remarks|returns|seealso"/>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if any of the element indexes are out of bounds.
        /// </exception>
        /// <exception cref="OverflowException">
        /// Thrown if the resulting enumeration index is greater than Int32.MaxValue.
        /// </exception>
        public static int CalculateIndex<T>(ArrayR3<T> array, int x, int y, int z)
        {
            int i = CalculateIndexUC(array.Array, x, y, z);
            if (i < 0) throw new OverflowException();
            return i;
        }

        /// <summary>
        /// Calculates the enumeration index for the specified array and element indexes.
        /// </summary>
        /// <param name="array">The array to calculate enumeration index for.</param>
        /// <param name="xyz">The element indexes.</param>
        /// <inheritdoc cref="CalculateIndex{T}(T[,], int, int)" select="remarks|returns|seealso"/>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if any of the element indexes are out of bounds.
        /// </exception>
        /// <exception cref="OverflowException">
        /// Thrown if the resulting enumeration index is greater than Int32.MaxValue.
        /// </exception>
        public static int CalculateIndex<T>(ArrayR3<T> array, Tuples.Int3 xyz)
        {
            int i = CalculateIndexUC(array.Array, xyz);
            if (i < 0) throw new OverflowException();
            return i;
        }

        /// <summary>
        /// Calculates the enumeration index for the specified array and element indexes.
        /// </summary>
        /// <param name="array">The array to calculate enumeration index for.</param>
        /// <param name="x">X element index.</param>
        /// <param name="y">Y element index.</param>
        /// <inheritdoc cref="CalculateIndex{T}(T[,], int, int)" select="remarks|returns|seealso"/>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if any of the element indexes are out of bounds.
        /// </exception>
        /// <exception cref="OverflowException">
        /// Thrown if the resulting enumeration index is greater than Int32.MaxValue.
        /// </exception>
        public static int CalculateIndex<T>(ReadOnlyArrayR2<T> array, int x, int y)
        {
            int i = CalculateIndexUC(array.Array, x, y);
            if (i < 0) throw new OverflowException();
            return i;
        }

        /// <summary>
        /// Calculates the enumeration index for the specified array and element indexes.
        /// </summary>
        /// <param name="array">The array to calculate enumeration index for.</param>
        /// <param name="x">X element index.</param>
        /// <param name="y">Y element index.</param>
        /// <param name="z">Z element index.</param>
        /// <inheritdoc cref="CalculateIndex{T}(T[,], int, int)" select="remarks|returns|seealso"/>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if any of the element indexes are out of bounds.
        /// </exception>
        /// <exception cref="OverflowException">
        /// Thrown if the resulting enumeration index is greater than Int32.MaxValue.
        /// </exception>
        public static int CalculateIndex<T>(ReadOnlyArrayR3<T> array, int x, int y, int z)
        {
            int i = CalculateIndexUC(array.Array, x, y, z);
            if (i < 0) throw new OverflowException();
            return i;
        }

        /// <summary>
        /// Calculates the enumeration index for the specified array and element indexes.
        /// </summary>
        /// <param name="array">The array to calculate enumeration index for.</param>
        /// <param name="xyz">The element indexes.</param>
        /// <inheritdoc cref="CalculateIndex{T}(T[,], int, int)" select="remarks|returns|seealso"/>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if any of the element indexes are out of bounds.
        /// </exception>
        /// <exception cref="OverflowException">
        /// Thrown if the resulting enumeration index is greater than Int32.MaxValue.
        /// </exception>
        public static int CalculateIndex<T>(ReadOnlyArrayR3<T> array, Tuples.Int3 xyz)
        {
            int i = CalculateIndexUC(array.Array, xyz);
            if (i < 0) throw new OverflowException();
            return i;
        }

        // ---

        /// <summary>
        /// Calculates the enumeration index for the specified array and element indexes. (UC = Unchecked)
        /// </summary>
        /// <param name="array">The array to calculate enumeration index for.</param>
        /// <param name="x">X element index.</param>
        /// <param name="y">Y element index.</param>
        /// <inheritdoc cref="CalculateIndexUC{T}(T[,], int, int)" select="remarks|returns|seealso"/>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if any of the element indexes are out of bounds.
        /// </exception>
        public static int CalculateIndexUC<T>(ArrayR2<T> array, int x, int y)
            => CalculateIndexUC(array.Array, x, y);

        /// <summary>
        /// Calculates the enumeration index for the specified array and element indexes. (UC = Unchecked)
        /// </summary>
        /// <param name="array">The array to calculate enumeration index for.</param>
        /// <param name="x">X element index.</param>
        /// <param name="y">Y element index.</param>
        /// <param name="z">Z element index.</param>
        /// <inheritdoc cref="CalculateIndexUC{T}(T[,], int, int)" select="remarks|returns|seealso"/>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if any of the element indexes are out of bounds.
        /// </exception>
        public static int CalculateIndexUC<T>(ArrayR3<T> array, int x, int y, int z)
            => CalculateIndexUC(array.Array, x, y, z);

        /// <summary>
        /// Calculates the enumeration index for the specified array and element indexes. (UC = Unchecked)
        /// </summary>
        /// <param name="array">The array to calculate enumeration index for.</param>
        /// <param name="xyz">The element indexes.</param>
        /// <inheritdoc cref="CalculateIndexUC{T}(T[,], int, int)" select="remarks|returns|seealso"/>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if any of the element indexes are out of bounds.
        /// </exception>
        public static int CalculateIndexUC<T>(ArrayR3<T> array, Tuples.Int3 xyz)
            => CalculateIndexUC(array.Array, xyz);

        /// <summary>
        /// Calculates the enumeration index for the specified array and element indexes. (UC = Unchecked)
        /// </summary>
        /// <param name="array">The array to calculate enumeration index for.</param>
        /// <param name="x">X element index.</param>
        /// <param name="y">Y element index.</param>
        /// <inheritdoc cref="CalculateIndexUC{T}(T[,], int, int)" select="remarks|returns|seealso"/>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if any of the element indexes are out of bounds.
        /// </exception>
        public static int CalculateIndexUC<T>(ReadOnlyArrayR2<T> array, int x, int y)
            => CalculateIndexUC(array.Array, x, y);

        /// <summary>
        /// Calculates the enumeration index for the specified array and element indexes. (UC = Unchecked)
        /// </summary>
        /// <param name="array">The array to calculate enumeration index for.</param>
        /// <param name="x">X element index.</param>
        /// <param name="y">Y element index.</param>
        /// <param name="z">Z element index.</param>
        /// <inheritdoc cref="CalculateIndexUC{T}(T[,], int, int)" select="remarks|returns|seealso"/>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if any of the element indexes are out of bounds.
        /// </exception>
        public static int CalculateIndexUC<T>(ReadOnlyArrayR3<T> array, int x, int y, int z)
            => CalculateIndexUC(array.Array, x, y, z);

        /// <summary>
        /// Calculates the enumeration index for the specified array and element indexes. (UC = Unchecked)
        /// </summary>
        /// <param name="array">The array to calculate enumeration index for.</param>
        /// <param name="xyz">The element indexes.</param>
        /// <inheritdoc cref="CalculateIndexUC{T}(T[,], int, int)" select="remarks|returns|seealso"/>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if any of the element indexes are out of bounds.
        /// </exception>
        public static int CalculateIndexUC<T>(ReadOnlyArrayR3<T> array, Tuples.Int3 xyz)
            => CalculateIndexUC(array.Array, xyz);

        // ---

        /* Feature creep..?
         * 
        /// <summary>
        /// Calculates the enumeration index for the specified array and element indexes. (US = Unsigned)
        /// </summary>
        /// <param name="array">The array to calculate enumeration index for.</param>
        /// <param name="x">X element index.</param>
        /// <param name="y">Y element index.</param>
        /// <remarks>
        /// For information on maximum array sizes see MSDN:<br/>
        /// <a href="https://docs.microsoft.com/en-us/dotnet/framework/configure-apps/file-schema/runtime/gcallowverylargeobjects-element">
        /// https://docs.microsoft.com/en-us/dotnet/framework/configure-apps/file-schema/runtime/gcallowverylargeobjects-element </a>
        /// (specifically the Remarks section).
        /// </remarks>
        /// <returns>
        /// The unsigned enumeration index that corresponds to the specified element indexes for the given array.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if any of the element indexes are out of bounds.
        /// </exception>
        public static uint CalculateIndexUS<T>(T[,] array, int x, int y)
            => unchecked((uint)CalculateIndexUC(array, x, y));

        /// <summary>
        /// Calculates the enumeration index for the specified array and element indexes. (US = Unsigned)
        /// </summary>
        /// <param name="array">The array to calculate enumeration index for.</param>
        /// <param name="x">X element index.</param>
        /// <param name="y">Y element index.</param>
        /// <param name="z">Z element index.</param>
        /// <inheritdoc cref="CalculateIndexUS{T}(T[,], int, int)" select="remarks|returns"/>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if any of the element indexes are out of bounds.
        /// </exception>
        public static uint CalculateIndexUS<T>(T[,,] array, int x, int y, int z)
            => unchecked((uint)CalculateIndexUC(array, x, y, z));

        /// <summary>
        /// Calculates the enumeration index for the specified array and element indexes. (US = Unsigned)
        /// </summary>
        /// <param name="array">The array to calculate enumeration index for.</param>
        /// <param name="xyz">The element indexes.</param>
        /// <inheritdoc cref="CalculateIndexUS{T}(T[,], int, int)" select="remarks|returns"/>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if any of the element indexes are out of bounds.
        /// </exception>
        public static uint CalculateIndexUS<T>(T[,,] array, Tuples.Int3 xyz)
            => unchecked((uint)CalculateIndexUC(array, xyz));

        /// <summary>
        /// Calculates the enumeration index for the specified array and element indexes. (US = Unsigned)
        /// </summary>
        /// <param name="array">The array to calculate enumeration index for.</param>
        /// <param name="w">W element index.</param>
        /// <param name="x">X element index.</param>
        /// <param name="y">Y element index.</param>
        /// <param name="z">Z element index.</param>
        /// <inheritdoc cref="CalculateIndexUS{T}(T[,], int, int)" select="remarks|returns"/>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if any of the element indexes are out of bounds.
        /// </exception>
        public static uint CalculateIndexUS<T>(T[,,,] array, int w, int x, int y, int z)
            => unchecked((uint)CalculateIndexUC(array, w, x, y, z));

        /// <summary>
        /// Calculates the enumeration index for the specified array and element indexes. (US = Unsigned)
        /// </summary>
        /// <param name="array">The array to calculate enumeration index for.</param>
        /// <param name="wxyz">The element indexes.</param>
        /// <inheritdoc cref="CalculateIndexUS{T}(T[,], int, int)" select="remarks|returns"/>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if any of the element indexes are out of bounds.
        /// </exception>
        public static uint CalculateIndexUS<T>(T[,,,] array, Tuples.Int4 wxyz)
            => unchecked((uint)CalculateIndexUC(array, wxyz));

        /// <summary>
        /// Calculates the enumeration index for the specified array and element indexes. (US = Unsigned)
        /// </summary>
        /// <param name="array">The array to calculate enumeration index for.</param>
        /// <param name="vwxyz">The element indexes.</param>
        /// <inheritdoc cref="CalculateIndexUS{T}(T[,], int, int)" select="remarks|returns"/>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if any of the element indexes are out of bounds.
        /// </exception>
        public static uint CalculateIndexUS<T>(T[,,,,] array, Tuples.Int5 vwxyz)
            => unchecked((uint)CalculateIndexUC(array, vwxyz));

        /// <summary>
        /// Calculates the enumeration index for the specified array and element indexes. (US = Unsigned)
        /// </summary>
        /// <param name="array">The array to calculate enumeration index for.</param>
        /// <param name="indexes">The element indexes.</param>
        /// <inheritdoc cref="CalculateIndexUS{T}(T[,], int, int)" select="remarks|returns"/>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if any of the element indexes are out of bounds.
        /// </exception>
        public static uint CalculateIndexUS<T>(T[,,,,,] array, Tuples.Int6 indexes)
            => unchecked((uint)CalculateIndexUC(array, indexes));
        */

        // ---

        /// <summary>
        /// Finds the index of a <paramref name="value"/> using reference-equality.
        /// </summary>
        /// <param name="array">The array to search.</param>
        /// <param name="value">The value to locate in the array.</param>
        /// <returns>
        /// The zero-based index of the first occurrence of <paramref name="value"/> in the array; or –1 if no equal value was found.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array is null.
        /// </exception>
        public static int IndexOfRef<TArray, TObject>(TArray[] array, TObject value)
            where TArray : class
            where TObject : class // <-- 'value' could be of type Object and thus non-generic, but using constraints gives compile time detection of accidental boxing !
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            for (int i = 0; i < array.Length; ++i)
                if (ReferenceEquals(array[i], value))
                    return i;

            return -1;
        }

        /// <summary>
        /// Finds the zero-based x and y indexes of a <paramref name="value"/> using reference-equality.
        /// </summary><remarks>
        /// <inheritdoc cref="AZCL.Collections.ArrayR2{T}.Enumerator" select="para[@id='enumerationOrder']"/>
        /// </remarks>
        /// <param name="array">The array to search.</param>
        /// <param name="value">The value to locate in the array.</param>
        /// <param name="x">The x index of the first occurrence of the specified value, if found; otherwise -1.</param>
        /// <param name="y">The y index of the first occurrence of the specified value, if found; otherwise -1.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array is null.
        /// </exception>
        public static void IndexOfRef<TArray, TObject>(TArray[,] array, TObject value, out int x, out int y)
            where TArray : class
            where TObject : class // <-- 'value' could be of type Object and thus non-generic, but using constraints gives compile time detection of accidental boxing !
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            int xlen = array.LengthX();
            int ylen = array.LengthY();
            for (x = 0; x < xlen; ++x)
                for (y = 0; y < ylen; ++y)
                    if (ReferenceEquals(array[x, y], value))
                        return;

            x = y = -1;
        }

        /// <summary>
        /// Finds the zero-based x, y, and z indexes of a <paramref name="value"/> using reference-equality.
        /// </summary><remarks>
        /// <inheritdoc cref="AZCL.Collections.ArrayR2{T}.Enumerator" select="para[@id='enumerationOrder']"/>
        /// </remarks>
        /// <param name="array">The array to search.</param>
        /// <param name="value">The value to locate in the array.</param>
        /// <param name="x">The x index of the first occurrence of the specified value, if found; otherwise -1.</param>
        /// <param name="y">The y index of the first occurrence of the specified value, if found; otherwise -1.</param>
        /// <param name="z">The z index of the first occurrence of the specified value, if found; otherwise -1.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array is null.
        /// </exception>
        public static void IndexOfRef<TArray, TObject>(TArray[,,] array, TObject value, out int x, out int y, out int z)
            where TArray : class
            where TObject : class // <-- 'value' could be of type Object and thus non-generic, but using constraints gives compile time detection of accidental boxing !
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            int xlen = array.LengthX();
            int ylen = array.LengthY();
            int zlen = array.LengthZ();
            for (x = 0; x < xlen; ++x)
                for (y = 0; y < ylen; ++y)
                    for (z = 0; z < zlen; ++z)
                        if (ReferenceEquals(array[x, y, z], value))
                            return;

            x = y = z = -1;
        }

        // ---

        /// <summary>
        /// Finds the index of a <paramref name="value"/> using reference-equality, starting at the specified index.
        /// </summary>
        /// <param name="array">The array to search.</param>
        /// <param name="value">The value to locate in the array.</param>
        /// <param name="startIndex">The zero-based starting index of the search.</param>
        /// <returns>
        /// The zero-based index of the first reference to <paramref name="value"/> in the searched range; or –1 if no reference to value occurs within that range.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="startIndex"/> is less than zero or greater than the length of the array.
        /// </exception>
        public static int IndexOfRef<TArray, TObject>(TArray[] array, TObject value, int startIndex)
            where TArray : class
            where TObject : class // <-- 'value' could be of type Object and thus non-generic, but using constraints gives compile time detection of accidental boxing !
            => IndexOfRef(array, value, startIndex, array.LengthOrZero() - startIndex);

        /// <summary>
        /// Finds the index of a <paramref name="value"/> using reference-equality, starting at the specified index.
        /// </summary>
        /// <param name="array">The array to search.</param>
        /// <param name="value">The value to locate in the array.</param>
        /// <param name="startIndex">The zero-based starting index of the search.</param>
        /// <returns>
        /// The zero-based index of the first reference to <paramref name="value"/> in the searched range; or –1 if no reference to value occurs within that range.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="startIndex"/> is less than zero or greater than the length of the array.
        /// </exception>
        public static int IndexOfRef<TArray, TObject>(ReadOnlyArray<TArray> array, TObject value, int startIndex)
            where TArray : class
            where TObject : class // <-- 'value' could be of type Object and thus non-generic, but using constraints gives compile time detection of accidental boxing !
            => IndexOfRef(array.Array, value, startIndex, array.Length - startIndex);

        // ---

        /// <summary>
        /// Finds the index of a <paramref name="value"/>, in the specified range of the array, using reference-equality.
        /// </summary><returns>
        /// The zero-based index of the first reference to <paramref name="value"/> in the searched range; or –1 if no reference to value occurs within that range.
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
        public static int IndexOfRef<TArray, TObject>(TArray[] array, TObject value, int startIndex, int count)
            where TArray : class
            where TObject : class // <-- 'value' could be of type Object and thus non-generic, but using constraints gives compile time detection of accidental boxing !
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            if (unchecked((uint)startIndex >= (uint)array.Length))
                throw new ArgumentOutOfRangeException(nameof(startIndex));
            if (unchecked((uint)count >= (uint)(array.Length - startIndex)))
                throw new ArgumentOutOfRangeException(nameof(count));

            count += startIndex;
            for (int i = startIndex; i < count; ++i)
                if (ReferenceEquals(array[i], value))
                    return i;

            return -1;
        }

        /// <summary>
        /// Finds the index of a <paramref name="value"/>, in the specified range of the array, using reference-equality.
        /// </summary><returns>
        /// The zero-based index of the first reference to <paramref name="value"/> in the searched range; or –1 if no reference to value occurs within that range.
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
        public static int IndexOfRef<TArray, TObject>(ReadOnlyArray<TArray> array, TObject value, int startIndex, int count)
            where TArray : class
            where TObject : class // <-- 'value' could be of type Object and thus non-generic, but using constraints gives compile time detection of accidental boxing !
            => IndexOfRef(array.Array, value, startIndex, count);

        // ---

        /// <summary>
        /// Finds the index of a <paramref name="value"/> using reference-equality.
        /// </summary>
        /// <param name="array">The array to search.</param>
        /// <param name="value">The value to locate in the array.</param>
        /// <returns>
        /// The zero-based index of the first occurrence of <paramref name="value"/> in the array; or –1 if no equal value was found.
        /// </returns>
        public static int IndexOfRef<TArray, TObject>(ReadOnlyArray<TArray> array, TObject value)
            where TArray : class
            where TObject : class // <-- 'value' could be of type Object and thus non-generic, but using constraints gives compile time detection of accidental boxing !
            => IndexOfRef(array.Array, value);

        /// <summary>
        /// Finds the zero-based x and y indexes of a <paramref name="value"/> using reference-equality.
        /// </summary><remarks>
        /// <inheritdoc cref="AZCL.Collections.ArrayR2{T}.Enumerator" select="para[@id='enumerationOrder']"/>
        /// </remarks>
        /// <param name="array">The array to search.</param>
        /// <param name="value">The value to locate in the array.</param>
        /// <param name="x">The x index of the first occurrence of the specified value, if found; otherwise -1.</param>
        /// <param name="y">The y index of the first occurrence of the specified value, if found; otherwise -1.</param>
        public static void IndexOfRef<TArray, TObject>(ArrayR2<TArray> array, TObject value, out int x, out int y)
            where TArray : class
            where TObject : class // <-- 'value' could be of type Object and thus non-generic, but using constraints gives compile time detection of accidental boxing !
        {
            IndexOfRef(array.Array, value, out x, out y);
        }

        /// <summary>
        /// Finds the zero-based x and y indexes of a <paramref name="value"/> using reference-equality.
        /// </summary><remarks>
        /// <inheritdoc cref="AZCL.Collections.ArrayR2{T}.Enumerator" select="para[@id='enumerationOrder']"/>
        /// </remarks>
        /// <param name="array">The array to search.</param>
        /// <param name="value">The value to locate in the array.</param>
        /// <param name="x">The x index of the first occurrence of the specified value, if found; otherwise -1.</param>
        /// <param name="y">The y index of the first occurrence of the specified value, if found; otherwise -1.</param>
        public static void IndexOfRef<TArray, TObject>(ReadOnlyArrayR2<TArray> array, TObject value, out int x, out int y)
            where TArray : class
            where TObject : class // <-- 'value' could be of type Object and thus non-generic, but using constraints gives compile time detection of accidental boxing !
        {
            IndexOfRef(array.Array, value, out x, out y);
        }

        /// <summary>
        /// Finds the zero-based x, y, and z indexes of a <paramref name="value"/> using reference-equality.
        /// </summary><remarks>
        /// <inheritdoc cref="AZCL.Collections.ArrayR2{T}.Enumerator" select="para[@id='enumerationOrder']"/>
        /// </remarks>
        /// <param name="array">The array to search.</param>
        /// <param name="value">The value to locate in the array.</param>
        /// <param name="x">The x index of the first occurrence of the specified value, if found; otherwise -1.</param>
        /// <param name="y">The y index of the first occurrence of the specified value, if found; otherwise -1.</param>
        /// <param name="z">The z index of the first occurrence of the specified value, if found; otherwise -1.</param>
        public static void IndexOfRef<TArray, TObject>(ArrayR3<TArray> array, TObject value, out int x, out int y, out int z)
            where TArray : class
            where TObject : class // <-- 'value' could be of type Object and thus non-generic, but using constraints gives compile time detection of accidental boxing !
        {
            IndexOfRef(array.Array, value, out x, out y, out z);
        }

        /// <summary>
        /// Finds the zero-based x, y, and z indexes of a <paramref name="value"/> using reference-equality.
        /// </summary><remarks>
        /// <inheritdoc cref="AZCL.Collections.ArrayR2{T}.Enumerator" select="para[@id='enumerationOrder']"/>
        /// </remarks>
        /// <param name="array">The array to search.</param>
        /// <param name="value">The value to locate in the array.</param>
        /// <param name="x">The x index of the first occurrence of the specified value, if found; otherwise -1.</param>
        /// <param name="y">The y index of the first occurrence of the specified value, if found; otherwise -1.</param>
        /// <param name="z">The z index of the first occurrence of the specified value, if found; otherwise -1.</param>
        public static void IndexOfRef<TArray, TObject>(ReadOnlyArrayR3<TArray> array, TObject value, out int x, out int y, out int z)
            where TArray : class
            where TObject : class // <-- 'value' could be of type Object and thus non-generic, but using constraints gives compile time detection of accidental boxing !
        {
            IndexOfRef(array.Array, value, out x, out y, out z);
        }

        /// <summary>
        /// Finds the zero-based x, y, and z indexes of a <paramref name="value"/> using reference-equality.
        /// </summary><remarks>
        /// <inheritdoc cref="AZCL.Collections.ArrayR2{T}.Enumerator" select="para[@id='enumerationOrder']"/>
        /// </remarks>
        /// <param name="array">The array to search.</param>
        /// <param name="value">The value to locate in the array.</param>
        /// <returns>
        /// The zero-based indexes of the first occurrence of <paramref name="value"/> in the array; or a tuple with all indexes set to –1 if no equal value was found.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array is null.
        /// </exception>
        public static Tuples.Int3 IndexOfRef<TArray, TObject>(TArray[,,] array, TObject value)
            where TArray : class
            where TObject : class // <-- 'value' could be of type Object and thus non-generic, but using constraints gives compile time detection of accidental boxing !
        {
            int x, y, z;
            IndexOfRef(array, value, out x, out y, out z);
            return new Tuples.Int3(x, y, z);
        }

        /// <summary>
        /// Finds the zero-based x, y, and z indexes of a <paramref name="value"/> using reference-equality.
        /// </summary><remarks>
        /// <inheritdoc cref="AZCL.Collections.ArrayR2{T}.Enumerator" select="para[@id='enumerationOrder']"/>
        /// </remarks>
        /// <param name="array">The array to search.</param>
        /// <param name="value">The value to locate in the array.</param>
        /// <returns>
        /// The zero-based indexes of the first occurrence of <paramref name="value"/> in the array; or a tuple with all indexes set to –1 if no equal value was found.
        /// </returns>
        public static Tuples.Int3 IndexOfRef<TArray, TObject>(ArrayR3<TArray> array, TObject value)
            where TArray : class
            where TObject : class // <-- 'value' could be of type Object and thus non-generic, but using constraints gives compile time detection of accidental boxing !
            => IndexOfRef(array.Array, value);

        /// <summary>
        /// Finds the zero-based x, y, and z indexes of a <paramref name="value"/> using reference-equality.
        /// </summary><remarks>
        /// <inheritdoc cref="AZCL.Collections.ArrayR2{T}.Enumerator" select="para[@id='enumerationOrder']"/>
        /// </remarks>
        /// <param name="array">The array to search.</param>
        /// <param name="value">The value to locate in the array.</param>
        /// <returns>
        /// The zero-based indexes of the first occurrence of <paramref name="value"/> in the array; or a tuple with all indexes set to –1 if no equal value was found.
        /// </returns>
        public static Tuples.Int3 IndexOfRef<TArray, TObject>(ReadOnlyArrayR3<TArray> array, TObject value)
            where TArray : class
            where TObject : class // <-- 'value' could be of type Object and thus non-generic, but using constraints gives compile time detection of accidental boxing !
            => IndexOfRef(array.Array, value);

        // ---

        /// <summary>
        /// Given a one-dimensional enumeration index, calculates the corresponding x and y element indexes.
        /// </summary><remarks>
        /// <inheritdoc cref="AZCL.Collections.ArrayR2{T}.Enumerator" select="para[@id='enumerationOrder']"/>
        /// </remarks><returns>
        /// False if the resulting indexes are out of bounds; otherwise true.
        /// </returns>
        /// <param name="array">The array whose dimensions to calculate indexes for.</param>
        /// <param name="index">An enumeration index to transform into regular indexes.</param>
        /// <param name="x">Resulting x index.</param>
        /// <param name="y">Resulting y index.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="array"/> is null.
        /// </exception>
        /// <seealso cref="AZCL.Collections.ArrayR2{T}.GetValue1D(int)"/>
        /// <seealso cref="AZCL.Collections.LinqForMultiRankArrays.ElementAt{TSource}(TSource[,], int)"/>
        public static bool TryCalculateIndexes<T>(T[,] array, int index, out int x, out int y)
        {
            int leny = NullCheck(array).LengthY();
            if (leny == 0)
            {
                x = y = -1;
                return false;
            }

            // Fast DivRem
            x = index / leny;     // (unbound)
            y = index - x * leny; // (bound y)

            return unchecked((uint)x < (uint)array.LengthX()); // (x bound?)

            // IL doesn't have a DivRem instruction because IL doesn't support instructions with two return values.
            // Thus the above is the fastest way to DivRem in .Net (and it's the way .Net Core does it) because as of
            // yet the Jitter doesn't optimize when it sees % and / used together. (There is a petition for it though.)
        }

        /// <summary>
        /// Given a one-dimensional enumeration index, calculates the corresponding x, y, and z element indexes.
        /// </summary>
        /// <inheritdoc cref="TryCalculateIndexes{T}(T[,], int, out int, out int)" select="remarks|returns"/>
        /// <param name="array">The array whose dimensions to calculate indexes for.</param>
        /// <param name="index">An enumeration index to transform into regular indexes.</param>
        /// <param name="x">Resulting x index.</param>
        /// <param name="y">Resulting y index.</param>
        /// <param name="z">Resulting z index.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="array"/> is null.
        /// </exception>
        /// <seealso cref="AZCL.Collections.ArrayR3{T}.GetValue1D(int)"/>
        /// <seealso cref="AZCL.Collections.LinqForMultiRankArrays.ElementAt{TSource}(TSource[,,], int)"/>
        public static bool TryCalculateIndexes<T>(T[,,] array, int index, out int x, out int y, out int z)
        {
            NullCheck(array);
            int leny = array.GetLength(1);
            int lenz = array.GetLength(2);
            if (leny == 0 | lenz == 0)
            {
                x = y = z = -1;
                return false;
            }

            y = index / lenz;     // (unbound)
            z = index - y * lenz; // (bound z)
            x = y / leny;         // (unbound)
            y = y - x * leny;     // (bound y)

            return unchecked((uint)x < (uint)array.GetLength(0)); // (x bound?)

            // IL doesn't have a DivRem instruction because IL doesn't support instructions with two return values.
            // Thus the above is the fastest way to DivRem in .Net (and it's the way .Net Core does it) because as of
            // yet the Jitter doesn't optimize when it sees % and / used together. (There is a petition for it though.)
        }


        /// <summary>
        /// Given a one-dimensional enumeration index, calculates the corresponding x, y, and z element indexes.
        /// </summary>
        /// <inheritdoc cref="TryCalculateIndexes{T}(T[,], int, out int, out int)" select="remarks|returns"/>
        /// <param name="array">The array whose dimensions to calculate indexes for.</param>
        /// <param name="index">An enumeration index to transform into regular indexes.</param>
        /// <param name="xyz">Int3 tuple with the resulting x, y, and z index.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="array"/> is null.
        /// </exception>
        /// <seealso cref="AZCL.Collections.ArrayR3{T}.GetValue1D(int)"/>
        /// <seealso cref="AZCL.Collections.LinqForMultiRankArrays.ElementAt{TSource}(TSource[,,], int)"/>
        public static bool TryCalculateIndexes<T>(T[,,] array, int index, out Tuples.Int3 xyz)
        {
            NullCheck(array);
            int leny = array.GetLength(1);
            int lenz = array.GetLength(2);
            if (leny == 0 | lenz == 0)
            {
                xyz = default(Tuples.Int3);
                return false;
            }

            int x, y, z;
            y = index / lenz;     // (unbound)
            z = index - y * lenz; // (bound z)
            x = y / leny;         // (unbound)
            y = y - x * leny;     // (bound y)

            xyz = new Tuples.Int3(x, y, z);
            return unchecked((uint)x < (uint)array.GetLength(0)); // (x bound?)
        }

        /// <summary>
        /// Given a one-dimensional enumeration index, calculates the corresponding w, x, y, and z element indexes.
        /// </summary>
        /// <inheritdoc cref="TryCalculateIndexes{T}(T[,], int, out int, out int)" select="remarks|returns"/>
        /// <param name="array">The array whose dimensions to calculate indexes for.</param>
        /// <param name="index">An enumeration index to transform into regular indexes.</param>
        /// <param name="w">Resulting w index.</param>
        /// <param name="x">Resulting x index.</param>
        /// <param name="y">Resulting y index.</param>
        /// <param name="z">Resulting z index.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="array"/> is null.
        /// </exception>
        /// <seealso cref="AZCL.Collections.LinqForMultiRankArrays.ElementAt{TSource}(TSource[,,,], int)"/>
        public static bool TryCalculateIndexes<T>(T[,,,] array, int index, out int w, out int x, out int y, out int z)
        {
            NullCheck(array);
            int lenx = array.GetLength(1);
            int leny = array.GetLength(2);
            int lenz = array.GetLength(3);
            if (lenx == 0 | leny == 0 | lenz == 0)
            {
                w = x = y = z = -1;
                return false;
            }

            y = index / lenz;     // (unbound)
            z = index - y * lenz; // (bound z)
            x = y / leny;         // (unbound)
            y = y - x * leny;     // (bound y)
            w = x / lenx;         // (unbound)
            x = x - w * lenx;     // (bound x)

            return unchecked((uint)w < (uint)array.GetLength(0)); // (w bound?)

            // IL doesn't have a DivRem instruction because IL doesn't support instructions with two return values.
            // Thus the above is the fastest way to DivRem in .Net (and it's the way .Net Core does it) because as of
            // yet the Jitter doesn't optimize when it sees % and / used together. (There is a petition for it though.)
        }

        /// <summary>
        /// Given a one-dimensional enumeration index, calculates the corresponding w, x, y, and z element indexes.
        /// </summary>
        /// <inheritdoc cref="TryCalculateIndexes{T}(T[,], int, out int, out int)" select="remarks|returns"/>
        /// <param name="array">The array whose dimensions to calculate indexes for.</param>
        /// <param name="index">An enumeration index to transform into regular indexes.</param>
        /// <param name="wxyz">Int4 tuple with the resulting w, x, y, and z index.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="array"/> is null.
        /// </exception>
        /// <seealso cref="AZCL.Collections.LinqForMultiRankArrays.ElementAt{TSource}(TSource[,,,], int)"/>
        public static bool TryCalculateIndexes<T>(T[,,,] array, int index, out Tuples.Int4 wxyz)
        {
            NullCheck(array);
            int lenx = array.GetLength(1);
            int leny = array.GetLength(2);
            int lenz = array.GetLength(3);
            if (lenx == 0 | leny == 0 | lenz == 0)
            {
                wxyz = default(Tuples.Int4);
                return false;
            }

            int w, x, y, z;
            y = index / lenz;     // (unbound)
            z = index - y * lenz; // (bound z)
            x = y / leny;         // (unbound)
            y = y - x * leny;     // (bound y)
            w = x / lenx;         // (unbound)
            x = x - w * lenx;     // (bound x)

            wxyz = new Tuples.Int4(w, x, y, z);
            return unchecked((uint)w < (uint)array.GetLength(0)); // (w bound?)
        }

        /// <summary>
        /// Given a one-dimensional enumeration index, calculates the corresponding v, w, x, y, and z element indexes.
        /// </summary>
        /// <inheritdoc cref="TryCalculateIndexes{T}(T[,], int, out int, out int)" select="remarks|returns"/>
        /// <param name="array">The array whose dimensions to calculate indexes for.</param>
        /// <param name="index">An enumeration index to transform into regular indexes.</param>
        /// <param name="vwxyz">Int5 tuple with the resulting v, w, x, y, and z index.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="array"/> is null.
        /// </exception>
        /// <seealso cref="AZCL.Collections.LinqForMultiRankArrays.ElementAt{TSource}(TSource[,,,,], int)"/>
        public static bool TryCalculateIndexes<T>(T[,,,,] array, int index, out Tuples.Int5 vwxyz)
        {
            NullCheck(array);
            int lenw = array.GetLength(1);
            int lenx = array.GetLength(2);
            int leny = array.GetLength(3);
            int lenz = array.GetLength(4);
            if (lenw == 0 | lenx == 0 | leny == 0 | lenz == 0)
            {
                vwxyz = default(Tuples.Int5);
                return false;
            }

            int v, w, x, y, z;
            y = index / lenz;     // (unbound)
            z = index - y * lenz; // (bound z)
            x = y / leny;         // (unbound)
            y = y - x * leny;     // (bound y)
            w = x / lenx;         // (unbound)
            x = x - w * lenx;     // (bound x)
            v = w / lenw;         // (unbound)
            w = w - v * lenw;     // (bound w)

            vwxyz = new Tuples.Int5(v, w, x, y, z);
            return unchecked((uint)v < (uint)array.GetLength(0)); // (v bound?)
        }

        /// <summary>
        /// Given a one-dimensional enumeration index, calculates the corresponding six element indexes.
        /// </summary>
        /// <inheritdoc cref="TryCalculateIndexes{T}(T[,], int, out int, out int)" select="remarks|returns"/>
        /// <param name="array">The array whose dimensions to calculate indexes for.</param>
        /// <param name="index">An enumeration index to transform into regular indexes.</param>
        /// <param name="indexes">Int6 tuple with the resulting six element indexes.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="array"/> is null.
        /// </exception>
        /// <seealso cref="AZCL.Collections.LinqForMultiRankArrays.ElementAt{TSource}(TSource[,,,,,], int)"/>
        public static bool TryCalculateIndexes<T>(T[,,,,,] array, int index, out Tuples.Int6 indexes)
        {
            NullCheck(array);
            int lenv = array.GetLength(1);
            int lenw = array.GetLength(2);
            int lenx = array.GetLength(3);
            int leny = array.GetLength(4);
            int lenz = array.GetLength(5);
            if (lenv == 0| lenw == 0 | lenx == 0 | leny == 0 | lenz == 0)
            {
                indexes = default(Tuples.Int6);
                return false;
            }

            int u, v, w, x, y, z;
            y = index / lenz;     // (unbound)
            z = index - y * lenz; // (bound z)
            x = y / leny;         // (unbound)
            y = y - x * leny;     // (bound y)
            w = x / lenx;         // (unbound)
            x = x - w * lenx;     // (bound x)
            v = w / lenw;         // (unbound)
            w = w - v * lenw;     // (bound w)
            u = v / lenv;         // (unbound)
            v = v - u * lenv;     // (bound v)

            indexes = new Tuples.Int6(u, v, w, x, y, z);
            return unchecked((uint)u < (uint)array.GetLength(0)); // (u bound?)
        }

        /// <summary>
        /// Given a one-dimensional enumeration index, calculates the corresponding element indexes for retrieving that element.
        /// </summary>
        /// <inheritdoc cref="TryCalculateIndexes{T}(T[,], int, out int, out int)" select="remarks"/>
        /// <returns>
        /// Returns an <c>int[]</c> array containing the resulting element indexes,
        /// if the resulting indexes are within bounds; otherwise null.
        /// </returns>
        /// <example><code>
        /// var arr = new [,] {{ 'A', 'B' }, { 'C', 'D' }};
        /// Console.Write(arr.GetValue(ArrayHelper.TryCalculateIndexes(arr, 2))); // prints "C"
        /// </code></example>
        /// <param name="array">The array whose dimensions to calculate indexes for.</param>
        /// <param name="index">An enumeration index to transform into regular indexes.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="array"/> is null.
        /// </exception>
        /// <seealso cref="O:AZCL.Collections.LinqForMultiRankArrays.ElementAt{TSource}"/>
        public static int[] TryCalculateIndexes(Array array, int index)
        {
            int rank = NullCheck(array).Rank;
            if (unchecked((uint)index >= (uint)array.Length))
                return null; // index is negative OR too large (the latter covers the case here at least one dimension has a length of zero).

            //if (rank == 1) // <-- screw this optimization: you're not supposed to call this method for single-dimensional arrays anyway!
            //    return new[] { index };

            var indexes = new int[rank];
            int dim = indexes.Length - 1;
            while (index > 0)
            {
                AZAssert.GEQZeroInternal(dim, nameof(dim));

                int len = array.GetLength(dim);   // (guaranteed non-zero)
                int tmp = index / len;            // (unbound)
                indexes[dim] = index - tmp * len; // (bound @ dim)
                index = tmp; // ('tmp' holds the "carry over" to the next index position)
                --dim;
            }

            return indexes;
        }

        // -----

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
    }
}
