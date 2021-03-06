﻿
namespace AZCL
{
    // XML summary is in ArrayExtensions_Copy.cs
    public static partial class ArrayExtensions
    {
        /// <summary>
        /// Get array length in the first (left-most) dimension of a rank 2 array.
        /// </summary><returns>
        /// array.GetLength(0);
        /// </returns>
        public static int LengthX<T>(this T[,] array)
        {
            return array.GetLength(0);
        }

        /// <summary>
        /// Get array length in the first (left-most) dimension of a rank 3 array.
        /// </summary><returns>
        /// array.GetLength(0);
        /// </returns>
        public static int LengthX<T>(this T[,,] array)
        {
            return array.GetLength(0);
        }

        /// <summary>
        /// Get array length in the second (right-most) dimension of a rank 2 array.
        /// </summary><returns>
        /// array.GetLength(1);
        /// </returns>
        public static int LengthY<T>(this T[,] array)
        {
            return array.GetLength(1);
        }

        /// <summary>
        /// Get array length in the second (middle) dimension of a rank 3 array.
        /// </summary><returns>
        /// array.GetLength(1);
        /// </returns>
        public static int LengthY<T>(this T[,,] array)
        {
            return array.GetLength(1);
        }

        /// <summary>
        /// Get array length in the third (right-most) dimension of a rank 3 array.
        /// </summary><returns>
        /// array.GetLength(2);
        /// </returns>
        public static int LengthZ<T>(this T[,,] array)
        {
            return array.GetLength(2);
        }
    }
}
