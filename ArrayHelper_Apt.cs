using System;
using AZCL.Collections;

/* Members that throw exceptions are not getting inlined. Moving the throw statement inside an exception builder method might allow the member to be inlined. */

namespace AZCL
{
    // XML summary is in ArrayHelper.cs
    public static partial class ArrayHelper
    {
        /// <summary>
        /// Appends a value to an array which currently has <paramref name="count"/> elements occupied,
        /// incrementing <paramref name="count"/> and resizing the array if necessary.
        /// </summary>
        /// <param name="array">The array into which the specified value should be appended (at the first unoccupied element slot).</param>
        /// <param name="count">The current element count, i.e. the number of valid elements in the array.
        /// <para/>(Not to be confused with 'capacity', i.e. <c>array.Length</c>.)</param>
        /// <param name="value">The value to append (at the first unoccupied element slot).</param>
        /// <remarks>
        /// This method has the same outcome as calling <see cref="AptInsert{T}(ref T[], ref int, int, T)"/> with <c>index == count</c> (but is ever so slightly faster).
        /// <para/>
        /// This overload uses <see cref="ResizeBehavior.Balanced"/>.
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="count"/> is less than zero or greater than the length of the array.
        /// </exception>
        public static void AptAdd<T>(ref T[] array, ref int count, T value)
        {
            if (NullCheck(array).Length == count) //resize needed
            {
                T[] copy = new T[GrowBalanced(count)];
                System.Array.Copy(array, copy, count);
                copy[count] = value;
                array = copy;
                ++count;
            }
            else //resize not needed
            {
                CountCheck(count, array.Length);
                array[count] = value;
                ++count;
            }
        }

        /// <summary>
        /// Appends a value to an array which currently has <paramref name="count"/> elements occupied,
        /// incrementing <paramref name="count"/> and resizing the array if necessary.
        /// </summary>
        /// <param name="array">The array into which the specified value should be appended (at the first unoccupied element slot).</param>
        /// <param name="count">The current element count, i.e. the number of valid elements in the array.
        /// <para/>(Not to be confused with 'capacity', i.e. <c>array.Length</c>.)</param>
        /// <param name="value">The value to append (at the first unoccupied element slot).</param>
        /// <param name="behavior">The resize behavior to use if the array needs to grow.</param>
        /// <remarks>
        /// Has the same effect as calling <see cref="AptInsert{T}(ref T[], ref int, int, T, ResizeBehavior)"/> with <c>index == count</c> (but is ever so slightly faster).
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="count"/> is less than zero or greater than the length of the array.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if the array needs to grow, but an invalid <see cref="ResizeBehavior"/> was specified.
        /// </exception>
        public static void AptAdd<T>(ref T[] array, ref int count, T value, ResizeBehavior behavior)
        {
            if (NullCheck(array).Length == count) //resize needed
            {
                T[] copy = new T[GrowHelper(count, behavior)];
                System.Array.Copy(array, copy, count);
                copy[count] = value;
                array = copy;
                ++count;
            }
            else //resize not needed
            {
                CountCheck(count, array.Length);
                array[count] = value;
                ++count;
            }
        }

        // ---

        /// <summary>
        /// Removes the element at the specified <paramref name="index"/> (by overwriting it with the last element in the array),
        /// and decrements <paramref name="count"/>.
        /// <para/>
        /// <b>This overload does NOT resize the array!</b>
        /// </summary>
        /// <param name="array">The array to delete from.</param>
        /// <param name="count">The current element count, i.e. the number of valid elements in the array.
        /// <para/>(Not to be confused with 'capacity', i.e. <c>array.Length</c>.)</param>
        /// <param name="index">The index of the element to remove.</param>
        /// <remarks>
        /// This is faster than <see cref="AptRemoveAt{T}(T[], ref int, int)"/> but does <i>not</i> maintain the order of elements.
        /// Use the stable-delete method if element order is important.
        /// <para/>
        /// The vacated element slot will be overwritten with <c>default(T)</c> to avoid any memory-leakage.
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="index"/> is less than zero or greater than or equal to <paramref name="count"/>.
        /// Thrown if <paramref name="count"/> is less than or equal to zero or greater than the length of the array.
        /// </exception>
        public static void AptRemoveFast<T>(T[] array, ref int count, int index)
        {
            IndexCheck(index, count);
            CountCheckDelete(count, NullCheck(array).Length);

            array[index] = array[--count];
            array[count] = default(T); //memory cleanup
        }

        /// <summary>
        /// Removes the element at the specified <paramref name="index"/>, and decrements <paramref name="count"/>.
        /// <para/>
        /// <b>This overload does NOT resize the array!</b>
        /// </summary>
        /// <param name="array">The array to delete from.</param>
        /// <param name="count">The current element count, i.e. the number of valid elements in the array.
        /// <para/>(Not to be confused with 'capacity', i.e. <c>array.Length</c>.)</param>
        /// <param name="index">The index of the element to remove.</param>
        /// <remarks>
        /// This is slower than <see cref="AptRemoveFast{T}(T[], ref int, int)"/> but is stable, i.e. maintains the order of elements.
        /// Use the fast remove method if element order is irrelevant.
        /// <para/>
        /// The vacated element slot will be overwritten with <c>default(T)</c> to avoid any memory-leakage.
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="index"/> is less than zero or greater than or equal to <paramref name="count"/>.
        /// Thrown if <paramref name="count"/> is less than or equal to zero or greater than the length of the array.
        /// </exception>
        public static void AptRemoveAt<T>(T[] array, ref int count, int index)
        {
            IndexCheck(index, count);
            CountCheckDelete(count, NullCheck(array).Length);

            if (--count != index)
                Array.Copy(array, index + 1, array, index, count - index);
            array[count] = default(T); //memory cleanup
        }

        /// <summary>
        /// Removes the element at the specified <paramref name="index"/>, decrements <paramref name="count"/>,
        /// and shrinks the array if the spill becomes too large.
        /// </summary>
        /// <param name="array">The array to delete from.</param>
        /// <param name="count">The current element count, i.e. the number of valid elements in the array.
        /// <para/>(Not to be confused with 'capacity', i.e. <c>array.Length</c>.)</param>
        /// <param name="index">The index of the element to remove.</param>
        /// <remarks>
        /// This is slower than <see cref="AptRemoveFast{T}(T[], ref int, int)"/> but is stable, i.e. maintains the order of elements.
        /// Use the fast remove method if element order is irrelevant.
        /// <para/>
        /// The vacated element slot will be overwritten with <c>default(T)</c> to avoid any memory-leakage.
        /// <para/>
        /// Uses <see cref="ResizeBehavior.Balanced"/>.
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="index"/> is less than zero or greater than or equal to <paramref name="count"/>.
        /// Thrown if <paramref name="count"/> is less than or equal to zero or greater than the length of the array.
        /// </exception>
        public static void AptRemoveAt<T>(ref T[] array, ref int count, int index)
        {
            IndexCheck(index, count);
            CountCheckDelete(count, NullCheck(array).Length);

            T[] arr = ShrinkBalanced<T>(--count, array.Length);
            if (arr == null) //no resize needed
            {
                if (index != count)
                    Array.Copy(array, index + 1, array, index, count - index);
                array[count] = default(T); //memory cleanup
            }
            else
            {
                if (index != 0)
                    System.Array.Copy(array, arr, index);
                if (index != count)
                    System.Array.Copy(array, index + 1, arr, index, count - index);
                array = arr;
            }
        }

        /// <summary>
        /// Removes the element at the specified <paramref name="index"/>, decrements <paramref name="count"/>,
        /// and shrinks the array if the spill becomes too large.
        /// </summary>
        /// <param name="array">The array to delete from.</param>
        /// <param name="count">The current element count, i.e. the number of valid elements in the array.
        /// <para/>(Not to be confused with 'capacity', i.e. <c>array.Length</c>.)</param>
        /// <param name="index">The index of the element to remove.</param>
        /// <param name="behavior">The resize behavior to use if the array needs to shrink.</param>
        /// <remarks>
        /// This is slower than <see cref="AptRemoveFast{T}(T[], ref int, int)"/> but is stable, i.e. maintains the order of elements.
        /// Use the fast remove method if element order is irrelevant.
        /// <para/>
        /// The vacated element slot will be overwritten with <c>default(T)</c> to avoid any memory-leakage.
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="index"/> is less than zero or greater than or equal to <paramref name="count"/>.
        /// Thrown if <paramref name="count"/> is less than or equal to zero or greater than the length of the array.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if an invalid <see cref="ResizeBehavior"/> was specified.
        /// </exception>
        public static void AptRemoveAt<T>(ref T[] array, ref int count, int index, ResizeBehavior behavior)
        {
            IndexCheck(index, count);
            CountCheckDelete(count, NullCheck(array).Length);

            T[] arr = ShrinkHelper<T>(--count, array.Length, behavior);
            if (arr == null) //no resize needed
            {
                if (index != count)
                    Array.Copy(array, index + 1, array, index, count - index);
                array[count] = default(T); //memory cleanup
            }
            else
            {
                if (index != 0)
                    System.Array.Copy(array, arr, index);
                if (index != count)
                    System.Array.Copy(array, index + 1, arr, index, count - index);
                array = arr;
            }
        }

        // ---

        /// <summary>
        /// Inserts a <paramref name="value"/> at the specified <paramref name="index"/> of an <paramref name="array"/>
        /// which currently has <paramref name="count"/> elements occupied, incrementing <paramref name="count"/> and resizing the array if necessary.
        /// </summary>
        /// <param name="array">The array into which the specified value should be inserted.</param>
        /// <param name="count">The current element count, i.e. the number of valid elements in the array.
        /// <para/>(Not to be confused with 'capacity', i.e. <c>array.Length</c>.)</param>
        /// <param name="index">The element index to insert the new value at.</param>
        /// <param name="value">The value to insert.</param>
        /// <remarks>
        /// Uses <see cref="ResizeBehavior.Balanced"/>.
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="index"/> is less than zero or greater than <paramref name="count"/>.
        /// Thrown if <paramref name="count"/> is less than zero or greater than the length of the array.
        /// </exception>
        public static void AptInsert<T>(ref T[] array, ref int count, int index, T value)
        {
            if (NullCheck(array).Length == count) //resize needed
            {
                T[] copy = new T[GrowBalanced(count)];
                if (index != 0)
                    System.Array.Copy(array, copy, index);
                copy[index] = value;
                if (index != count)
                    System.Array.Copy(array, index, copy, index + 1, count - index);
                array = copy;
                ++count;
            }
            else //resize not needed
            {
                CountCheck(count, array.Length);
                if (index != count) // (Array.Copy will throw if 'index' is OOR)
                    System.Array.Copy(array, index, array, index + 1, count - index); // shifts range [index, count-1] one step
                array[index] = value;
                ++count;
            }
        }

        /// <summary>
        /// Inserts a <paramref name="value"/> at the specified <paramref name="index"/> of an <paramref name="array"/>
        /// which currently has <paramref name="count"/> elements occupied, incrementing <paramref name="count"/> and resizing the array if necessary.
        /// </summary>
        /// <param name="array">The array into which the specified value should be inserted.</param>
        /// <param name="count">The current element count, i.e. the number of valid elements in the array.
        /// <para/>(Not to be confused with 'capacity', i.e. <c>array.Length</c>.)</param>
        /// <param name="index">The element index to insert the new value at.</param>
        /// <param name="value">The value to insert.</param>
        /// <param name="behavior">The resize behavior to use if the array needs to grow.</param>
        /// <remarks>
        /// Uses <see cref="ResizeBehavior.Balanced"/>.
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="index"/> is less than zero or greater than <paramref name="count"/>.
        /// Thrown if <paramref name="count"/> is less than zero or greater than the length of the array.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if the array needs to resize, but an invalid <see cref="ResizeBehavior"/> was specified.
        /// </exception>
        public static void AptInsert<T>(ref T[] array, ref int count, int index, T value, ResizeBehavior behavior)
        {
            if (NullCheck(array).Length == count) //resize needed
            {
                T[] copy = new T[GrowHelper(count, behavior)];
                if (index != 0)
                    System.Array.Copy(array, copy, index);
                copy[index] = value;
                if (index != count)
                    System.Array.Copy(array, index, copy, index + 1, count - index);
                array = copy;
                ++count;
            }
            else //resize not needed
            {
                CountCheck(count, array.Length);
                if (index != count) // (Array.Copy will throw if 'index' is OOR)
                    System.Array.Copy(array, index, array, index + 1, count - index); // shifts range [index, count-1] one step
                array[index] = value;
                ++count;
            }
        }

        // ---

        /// <summary>
        /// Shrinks the array if its element utilization (i.e. ratio of used to unused elements) is too low.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <param name="count">The current element count, i.e. the number of valid elements in the array.
        /// <para/>(Not to be confused with 'capacity', i.e. <c>array.Length</c>.)</param>
        /// <param name="behavior">The <see cref="ResizeBehavior"/> used in determining if a resize is needed.</param>
        /// <remarks>
        /// To trim <b>all</b> unused element slots use <see cref="Array.Resize{T}(ref T[], int)"/>.
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the array is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="count"/> is less than zero or greater than the length of the array.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if an invalid <see cref="ResizeBehavior"/> was specified.
        /// </exception>
        public static void AptShrink<T>(ref T[] array, int count, ResizeBehavior behavior = ResizeBehavior.Balanced)
        {
            int len = NullCheck(array).Length;
            CountCheck(count, len);

            var copy = ShrinkHelper<T>(count, len, behavior);
            if (copy != null)
            {
                Array.Copy(array, copy, count);
                array = copy;
            }
        }

        // -----

        private static int GrowHelper(int oldCount, ResizeBehavior behavior)
        {
            switch (behavior)
            {
                case ResizeBehavior.Balanced:
                    return GrowBalanced(oldCount);
                case ResizeBehavior.Spacious:
                    return GrowSpacious(oldCount);
                case ResizeBehavior.Trimmed:
                    return GrowTight(oldCount);
                default:
                    return BadResizeBehavior<int>(behavior); // why this method call? because methods that throw doesn't get inlined.
            }
        }

        private static TReturn BadResizeBehavior<TReturn>(ResizeBehavior invalid_behavior)
        {
            throw new System.ArgumentException(message: "Unsupported " + nameof(ResizeBehavior) + ": " + invalid_behavior, paramName: "behavior");
        }

        private const int GROW_BALANCED = 8;
        private const int GROW_SPACIOUS = 32;

        private static int GrowBalanced(int oldCount)
            => oldCount == 0 ? GROW_BALANCED : ((oldCount << 1) - (oldCount >> 1));

        private static int GrowSpacious(int oldCount)
            => oldCount == 0 ? GROW_SPACIOUS : (oldCount << 1);

        private static int GrowTight(int oldCount)
            => oldCount + 1;

        // ---

        private static T[] ShrinkHelper<T>(int newCount, int length, ResizeBehavior behavior) //returns null if no resize needed
        {
            switch (behavior)
            {
                case ResizeBehavior.Balanced:
                    return ShrinkBalanced<T>(newCount, length);
                case ResizeBehavior.Spacious:
                    return ShrinkSpacious<T>(newCount, length);
                case ResizeBehavior.Trimmed:
                    return ShrinkTight<T>(newCount, length);
                default:
                    return BadResizeBehavior<T[]>(behavior);
            }
        }

        private static T[] ShrinkBalanced<T>(int newCount, int length) //returns null if no resize needed
        {
            if (newCount == 0)
                return new T[0];
            if (length < 13)
                return null;
            int resize = (length >> 1) + (length >> 2); // ~75%
            if (newCount <= resize - ((length >> 3) | 0x3)) // ~75% - 10~23%
            {
                /* (see above)
                 * At length == 13, the minimum length eligible for shrinking,
                 * in which case a shrink will happen if newCount <= 9 - (3 | 0x3)
                 * (or simply put newCount <= 6), the right percentage is 23%
                 * The percentage shrinks linearly up to length == 31, where it is 10%
                 * It then jumps up to 22% for 32, after which it shrinks linearly again
                 * up to length == 63, where it is 11%
                 * And then it jumps to 17% at length == 64  ...This pattern continues,
                 * with smaller and smaller peeks/jumps at every multiple of 32.
                 * (because 32 / 8 == 4 == 0x3 + 1) */
                return new T[Math.Max(8, resize)]; //minimum non-empty size: 8
            }
            return null; //no resize needed
        }

        private static T[] ShrinkSpacious<T>(int newCount, int length) //returns null if no resize needed
        {
            if (newCount == 0)
                return new T[0];
            if (length < 40)
                return null;
            int resize = (length >> 1); // ~50%
            if (newCount <= resize - ((length >> 3) | 0xf)) // ~50% - 12~37%
            {
                /* (see above)
                 * 37% is for length == 40, the minimum length eligible for shrinking,
                 * in which case a shrink will happen if newCount <= 20 - (5 | 0xf)
                 * (or simply put newCount <= 5)
                 * The percentage shrinks linearly up to length == 127, where it is 12%
                 * It then jumps up to 24% for 128, after which it shrinks linearly again
                 * up to length == 255, where it again is 12%
                 * And then it jumps to 18% at length == 256  ...This pattern continues,
                 * with smaller and smaller peeks/jumps at every multiple of 128.
                 * (because 128 / 8 == 16 == 0xf + 1) */
                return new T[Math.Max(32, resize)]; //minimum non-empty size: 32
            }
            return null; //no resize needed
        }

        private static T[] ShrinkTight<T>(int newCount, int length) //returns null if no resize needed
            => newCount == length ? null : new T[newCount];

        // -----

        /// <summary>
        /// 0 &lt;= index &lt; count; otherwise throw ArgumentOutOfRangeException("index").
        /// <para/>
        /// Does NOT check that 'count' is non-negative!
        /// </summary>
        private static void IndexCheck(int index, int count)
        {
            if (unchecked((uint)index >= (uint)count))
                throw new ArgumentOutOfRangeException("index");
        }

        /// <summary>
        /// 0 &lt;= count &lt;= length; otherwise throw ArgumentOutOfRangeException("count").
        /// <para/>
        /// Does NOT check that 'length' is non-negative!
        /// </summary>
        private static void CountCheck(int count, int length)
        {
            if (unchecked((uint)count > (uint)length))
                throw new ArgumentOutOfRangeException("count");
        }

        /// <summary>
        /// 0 &lt; count &lt;= length; otherwise throw ArgumentOutOfRangeException("count").
        /// <para/>
        /// Does NOT check that 'length' is non-negative! (Always throws if 'length' is zero.)
        /// </summary>
        private static void CountCheckDelete(int count, int length)
        {
            if (unchecked((uint)(count - 1) >= (uint)length))
                throw new ArgumentOutOfRangeException("count");
        }
    }
}
