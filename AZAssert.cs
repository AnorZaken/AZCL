using System;
using System.Diagnostics;

namespace AZCL
{
    internal sealed class AZAssert
    {
        [Conditional("AZCL_DEBUG")]
        internal static void NotNullInternal(object obj, string param_name)
        {
            Debug.Assert(obj != null, param_name + " == null");
        }

        [Conditional("AZCL_DEBUG")]
        internal static void NotEmptyInternal(string s, string param_name)
        {
            Debug.Assert(s != null && s.Length != 0, param_name + " is null or empty");
        }

        [Conditional("AZCL_DEBUG")]
        internal static void NotEmptyInternal(Array arr, string param_name)
        {
            Debug.Assert(arr != null, param_name + " == null");
            Debug.Assert(arr.Length != 0, param_name + " is empty");
        }

        [Conditional("AZCL_DEBUG")]
        internal static void BoundsInternal(int i, int length, string iname, bool allowEqual)
        {
            Debug.Assert(unchecked(allowEqual ? (uint)i <= (uint)length : (uint)i < (uint)length), iname + " out of bounds");
        }

        [Conditional("AZCL_DEBUG")]
        internal static void BoundsInternal(Array arr, int i, string iname, bool allowEqual)
        {
            Debug.Assert(arr != null);
            Debug.Assert(unchecked(allowEqual ? (uint)i <= (uint)arr.Length : (uint)i < (uint)arr.Length), iname + " out of bounds");
        }

        [Conditional("AZCL_DEBUG")]
        internal static void BoundsInternal<T>(T[,] arr, int x, int y)
        {
            Debug.Assert(arr != null);
            Debug.Assert(unchecked((uint)x < (uint)arr.LengthX()), "x out of bounds");
            Debug.Assert(unchecked((uint)y < (uint)arr.LengthY()), "y out of bounds");
        }

        [Conditional("AZCL_DEBUG")]
        internal static void BoundsInternal<T>(T[,,] arr, int x, int y, int z)
        {
            Debug.Assert(arr != null);
            Debug.Assert(unchecked((uint)x < (uint)arr.LengthX()), "x out of bounds");
            Debug.Assert(unchecked((uint)y < (uint)arr.LengthY()), "y out of bounds");
            Debug.Assert(unchecked((uint)z < (uint)arr.LengthZ()), "z out of bounds");
        }

        [Conditional("AZCL_DEBUG")]
        internal static void BoundsInternal<T>(T[,,,] arr, int w, int x, int y, int z)
        {
            Debug.Assert(arr != null);
            Debug.Assert(unchecked((uint)w < (uint)arr.LengthW()), "w out of bounds");
            Debug.Assert(unchecked((uint)x < (uint)arr.LengthX()), "x out of bounds");
            Debug.Assert(unchecked((uint)y < (uint)arr.LengthY()), "y out of bounds");
            Debug.Assert(unchecked((uint)z < (uint)arr.LengthZ()), "z out of bounds");
        }

        [Conditional("AZCL_DEBUG")]
        internal static void BoundsInternal<T>(T[,] arr, int startX, int startY, int count)
        {
            BoundsInternal(arr, startX, startY);
            GEQZeroInternal(count, nameof(count));
            Debug.Assert(ArrayKit.CalculateCountUnbound(arr, startX, startY, false) + count <= arr.Length, "count out of bounds");
        }

        [Conditional("AZCL_DEBUG")]
        internal static void BoundsInternal<T>(T[,,] arr, int startX, int startY, int startZ, int count)
        {
            BoundsInternal(arr, startX, startY, startY);
            GEQZeroInternal(count, nameof(count));
            Debug.Assert(ArrayKit.CalculateCountUnbound(arr, startX, startY, startZ, false) + count <= arr.Length, "count out of bounds");
        }

        [Conditional("AZCL_DEBUG")]
        internal static void BoundsInternal<T>(T[,,,] arr, int startW, int startX, int startY, int startZ, int count)
        {
            BoundsInternal(arr, startW, startX, startY, startY);
            GEQZeroInternal(count, nameof(count));
            Debug.Assert(ArrayKit.CalculateCountUnbound(arr, startW, startX, startY, startZ, false) + count <= arr.Length, "count out of bounds");
        }

        /*
        [Conditional("AZCL_DEBUG")]
        internal static void BoundsInternal(int count, int index, string iname, bool allowEqual)
        {
            Debug.Assert(unchecked(allowEqual ? (uint)index <= (uint)count : (uint)index < (uint)count), iname + " out of bounds");
        }
        */

        [Conditional("AZCL_DEBUG")]
        internal static void GEQZeroInternal(int i, string param_name)
        {
            Debug.Assert(i >= 0, param_name + " < 0");
        }

        [Conditional("AZCL_DEBUG")]
        internal static void Internal(string message)
        {
            Debug.Assert(false, message);
        }

        [Conditional("AZCL_DEBUG")]
        internal static void Internal(bool condition, string message)
        {
            Debug.Assert(condition, message);
        }

        [Conditional("AZCL_DEBUG")]
        internal static void Internal(bool condition, string message, string detailedMessage)
        {
            Debug.Assert(condition, message, detailedMessage);
        }
    }
}
