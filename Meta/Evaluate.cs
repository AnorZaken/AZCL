using System;

namespace AZCL.Meta
{
    internal static class Evaluate // TODO: doc and public
    {
        /// <summary>
        /// Returns whether <typeparamref name="TTarget"/> is assignable from <typeparamref name="TFrom"/>
        /// </summary>
        public static bool IsAssignableFrom<TTarget, TFrom>()
        {
            return typeof(TTarget).IsAssignableFrom(typeof(TFrom));
        }

        /// <summary>
        /// Returns whether <typeparamref name="T"/> : IComparable&lt;<typeparamref name="T"/>&gt;.
        /// </summary>
        public static bool IsComparable<T>()
        {
            return IsAssignableFrom<IComparable<T>, T>();
        }

        /// <summary>
        /// Returns whether <typeparamref name="T"/> : IEquatable&lt;<typeparamref name="T"/>&gt;.
        /// </summary>
        public static bool IsEquatable<T>()
        {
            return IsAssignableFrom<IEquatable<T>, T>();
        }

        /// <summary>
        /// Returns whether <typeparamref name="T"/> is an integral simple type.
        /// </summary><remarks>
        /// The integral types are: sbyte, byte, short, ushort, int, uint, long, and ulong.
        /// </remarks>
        public static bool IsInteger<T>()
        {
            var tc = Type.GetTypeCode(typeof(T));
            return unchecked((uint)tc - 5u) < 8u;
        }

        /// <summary>
        /// Returns whether <typeparamref name="T"/> is a numeric simple type.
        /// </summary><remarks>
        /// The numeric simple types are: sbyte, byte, short, ushort, int, uint, long, ulong, float, double, and decimal.
        /// </remarks>
        public static bool IsNumeric<T>()
        {
            var tc = Type.GetTypeCode(typeof(T));
            return unchecked((uint)tc - 5u) <= 10u;
        }

        /// <summary>
        /// Returns whether <typeparamref name="T"/> is a Nullable&lt;?&gt;.
        /// </summary>
        public static bool IsNullable<T>()
        {
            return typeof(T).IsNullable(); // <-- AZCL extension.
        }
    }
}
