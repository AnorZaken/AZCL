using System;

namespace AZCL
{
    // XML summary is in ArrayHelper.cs
    public static partial class ArrayHelper
    {
        /// <summary>
        /// Functions related to array index calculations.
        /// </summary>
        public static class Indexes
        {
            /// <summary>
            /// Given a one-dimensional enumeration index, calculates the corresponding x and y element indexes.
            /// </summary><remarks>
            /// When enumerating over multi-dimensional arrays elements are traversed such that the indices of the
            /// rightmost dimension are increased first, then the next left dimension, and so on to the left.
            /// <br/><i>(See C# Language Specification - Version 4.0 paragraph 8.8.4)</i>
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
            public static bool TryCalculate<T>(T[,] array, int index, out int x, out int y)
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
            /// <inheritdoc cref="TryCalculate{T}(T[,], int, out int, out int)" select="remarks|returns"/>
            /// <param name="array">The array whose dimensions to calculate indexes for.</param>
            /// <param name="index">An enumeration index to transform into regular indexes.</param>
            /// <param name="x">Resulting x index.</param>
            /// <param name="y">Resulting y index.</param>
            /// <param name="z">Resulting z index.</param>
            /// <exception cref="ArgumentNullException">
            /// Thrown if the <paramref name="array"/> is null.
            /// </exception>
            /// <seealso cref="AZCL.Collections.ArrayR3{T}.GetValue1D(int)"/>
            public static bool TryCalculate<T>(T[,,] array, int index, out int x, out int y, out int z)
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
            /// Given a one-dimensional enumeration index, calculates the corresponding x, y, z, and w element indexes.
            /// </summary>
            /// <inheritdoc cref="TryCalculate{T}(T[,], int, out int, out int)" select="remarks|returns"/>
            /// <param name="array">The array whose dimensions to calculate indexes for.</param>
            /// <param name="index">An enumeration index to transform into regular indexes.</param>
            /// <param name="x">Resulting x index.</param>
            /// <param name="y">Resulting y index.</param>
            /// <param name="z">Resulting z index.</param>
            /// <param name="w">Resulting w index.</param>
            /// <exception cref="ArgumentNullException">
            /// Thrown if the <paramref name="array"/> is null.
            /// </exception>
            /// <seealso cref="AZCL.Collections.LinqForMultiRankArrays.ElementAt{TSource}(TSource[,,,], int)"/>
            public static bool TryCalculate<T>(T[,,,] array, int index, out int x, out int y, out int z, out int w)
            {
                NullCheck(array);
                int leny = array.GetLength(1);
                int lenz = array.GetLength(2);
                int lenw = array.GetLength(3);
                if (leny == 0 | lenz == 0 | lenw == 0)
                {
                    x = y = z = w = -1;
                    return false;
                }

                z = index / lenw;     // (unbound)
                w = index - z * lenw; // (bound w)
                y = z / lenz;         // (unbound)
                z = z - y * lenz;     // (bound z)
                x = y / leny;         // (unbound)
                y = y - x * leny;     // (bound y)

                return unchecked((uint)x < (uint)array.GetLength(0)); // (x bound?)

                // IL doesn't have a DivRem instruction because IL doesn't support instructions with two return values.
                // Thus the above is the fastest way to DivRem in .Net (and it's the way .Net Core does it) because as of
                // yet the Jitter doesn't optimize when it sees % and / used together. (There is a petition for it though.)
            }

            /// <summary>
            /// Given a one-dimensional enumeration index, calculates the corresponding element indexes for retrieving that element.
            /// </summary>
            /// <inheritdoc cref="TryCalculate{T}(T[,], int, out int, out int)" select="remarks"/>
            /// <returns>
            /// Returns an <c>int[]</c> array containing the resulting element indexes,
            /// if the resulting indexes are within bounds; otherwise null.
            /// </returns>
            /// <example><code>
            /// var arr = new [,] {{ 'A', 'B' }, { 'C', 'D' }};
            /// Console.Write(arr.GetValue(ArrayHelper.Indexes.TryCalculate(arr, 2))); // prints "C"
            /// </code></example>
            /// <param name="array">The array whose dimensions to calculate indexes for.</param>
            /// <param name="index">An enumeration index to transform into regular indexes.</param>
            /// <exception cref="ArgumentNullException">
            /// Thrown if the <paramref name="array"/> is null.
            /// </exception>
            /// <seealso cref="O:AZCL.Collections.LinqForMultiRankArrays.ElementAt{TSource}"/>
            public static int[] TryCalculate(Array array, int index)
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
        }
    }
}
