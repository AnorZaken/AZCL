
namespace AZCL
{
    // XML summary is in ArrayExtensions.cs
    public static partial class ArrayExtensions
    {
        /// <summary>
        /// Get array length, or zero if the array is null.
        /// </summary>
        public static int LengthOrZero(this System.Array array)
            => array == null ? 0 : array.Length;

        /// <summary>
        /// Get array length in the first (left-most) dimension of a rank 4 array.
        /// </summary><returns>
        /// array.GetLength(0);
        /// </returns>
        public static int LengthW<T>(this T[,,,] array)
            => array.GetLength(0);

        /// <summary>
        /// Get array length in the first (left-most) dimension of a rank 2 array.
        /// </summary><returns>
        /// array.GetLength(0);
        /// </returns>
        public static int LengthX<T>(this T[,] array)
            => array.GetLength(0);

        /// <summary>
        /// Get array length in the first (left-most) dimension of a rank 3 array.
        /// </summary><returns>
        /// array.GetLength(0);
        /// </returns>
        public static int LengthX<T>(this T[,,] array)
            => array.GetLength(0);

        /// <summary>
        /// Get array length in the second (from the left) dimension of a rank 4 array.
        /// </summary><returns>
        /// array.GetLength(1);
        /// </returns>
        public static int LengthX<T>(this T[,,,] array)
            => array.GetLength(1);

        /// <summary>
        /// Get array length in the second (right-most) dimension of a rank 2 array.
        /// </summary><returns>
        /// array.GetLength(1);
        /// </returns>
        public static int LengthY<T>(this T[,] array)
            => array.GetLength(1);

        /// <summary>
        /// Get array length in the second (middle) dimension of a rank 3 array.
        /// </summary><returns>
        /// array.GetLength(1);
        /// </returns>
        public static int LengthY<T>(this T[,,] array)
            => array.GetLength(1);

        /// <summary>
        /// Get array length in the third (from the left) dimension of a rank 4 array.
        /// </summary><returns>
        /// array.GetLength(2);
        /// </returns>
        public static int LengthY<T>(this T[,,,] array)
            => array.GetLength(2);

        /// <summary>
        /// Get array length in the third (right-most) dimension of a rank 3 array.
        /// </summary><returns>
        /// array.GetLength(2);
        /// </returns>
        public static int LengthZ<T>(this T[,,] array)
            => array.GetLength(2);

        /// <summary>
        /// Get array length in the fourth (right-most) dimension of a rank 4 array.
        /// </summary><returns>
        /// array.GetLength(3);
        /// </returns>
        public static int LengthZ<T>(this T[,,,] array)
            => array.GetLength(3);
    }
}
