using System;

namespace AZCL.Meta
{
    /// <summary>
    /// Get info about primitive numeric types.
    /// </summary>
    public static class Numeric
    {
        internal const string _ERR_TC_NOT_NUMERIC = "TypeCode does not correspond to a primitive numeric type.";

        /// <summary>
        /// Checks if a TypeCode corresponds to one of the primitive numeric types.
        /// </summary><remarks>
        /// The primitive numeric types are: sbyte, byte, short, ushort, int, uint, long, ulong, float, double, decimal.
        /// </remarks>
        public static bool IsNumeric(TypeCode typeCode)
        {
            return unchecked((uint)typeCode - 5u) <= 10u;
        }

        /// <summary>
        /// Checks if a Type corresponds to one of the primitive numeric types.
        /// </summary><remarks>
        /// The primitive numeric types are: sbyte, byte, short, ushort, int, uint, long, ulong, float, double, decimal.
        /// </remarks>
        public static bool IsNumeric(Type type)
        {
            return IsNumeric(Type.GetTypeCode(type));
        }

        /// <summary>
        /// Checks if a TypeCode corresponds to an unsigned primitive numeric type.
        /// </summary><remarks>
        /// The unsigned primitive numeric types are: byte, ushort, uint, ulong.
        /// </remarks>
        public static bool IsUnsigned(TypeCode typeCode)
        {
            return unchecked((int)typeCode - 5 & 9) == 1;
        }

        /// <summary>
        /// Checks if a TypeCode corresponds to the float or double type.
        /// </summary>
        public static bool IsFloat(TypeCode typeCode)
        {
            return unchecked((int)typeCode - 5 & -2) == 8;
        }

        /// <summary>
        /// Numeric info about type <typeparamref name="T"/>.
        /// </summary>
        public static NumericInfo Info<T>() where T : struct, IConvertible
        {
            return Numeric<T>.info;
        }

        /// <summary>
        /// Numeric info about type <typeparamref name="T"/>.
        /// </summary>
        public static NumericInfo Info<T>(T instance) where T : struct, IConvertible // (type inference for caller)
        {
            return Numeric<T>.info;
        }

        /// <summary>
        /// Numeric info about a type.
        /// </summary>
        public static NumericInfo Info(Type type)
        {
            return new NumericInfo(Type.GetTypeCode(type));
        }

        /// <summary>
        /// Numeric info about an IConvertible type.
        /// </summary>
        public static NumericInfo Info(IConvertible instance)
        {
            if (instance == null)
                throw new ArgumentNullException(nameof(instance));
            return new NumericInfo(instance.GetTypeCode());
        }

        /// <summary>
        /// Numeric info about type with specified TypeCode.
        /// </summary>
        public static NumericInfo Info(TypeCode tc)
        {
            return new NumericInfo(tc);
        }

        /// <summary>
        /// Get a <see cref="MaxValue"/> instance corresponding to type <typeparamref name="T"/>.
        /// </summary>
        /// <exception cref="ArgumentException">Thrown if the corresponding type isn't primitive numeric.</exception>
        public static MaxValue Max<T>(T value) where T : struct, IConvertible
        {
            return new MaxValue(value.GetTypeCode());
        }

        /// <summary>
        /// Get a <see cref="MaxValue"/> instance corresponding to type <typeparamref name="T"/>.
        /// </summary>
        /// <exception cref="ArgumentException">Thrown if the corresponding type isn't primitive numeric.</exception>
        public static MaxValue Max<T>() where T : struct, IConvertible
        {
            return new MaxValue(default(T).GetTypeCode());
        }

        /// <summary>
        /// Get a <see cref="MaxValue"/> instance corresponding to the specified type.
        /// </summary>
        /// <exception cref="ArgumentException">Thrown if the corresponding type isn't primitive numeric.</exception>
        public static MaxValue Max(TypeCode typeCode)
        {
            return new MaxValue(typeCode);
        }

        /// <summary>
        /// Get a <see cref="MinValue"/> instance corresponding to type <typeparamref name="T"/>.
        /// </summary>
        /// <exception cref="ArgumentException">Thrown if the corresponding type isn't primitive numeric.</exception>
        public static MinValue Min<T>() where T : struct, IConvertible
        {
            return new MinValue(default(T).GetTypeCode());
        }

        /// <summary>
        /// Get a <see cref="MinValue"/> instance corresponding to type <typeparamref name="T"/>.
        /// </summary>
        /// <exception cref="ArgumentException">Thrown if the corresponding type isn't primitive numeric.</exception>
        public static MinValue Min<T>(T value) where T : struct, IConvertible
        {
            return new MinValue(value.GetTypeCode());
        }

        /// <summary>
        /// Get a <see cref="MinValue"/> instance corresponding to the specified type.
        /// </summary>
        /// <exception cref="ArgumentException">Thrown if the corresponding type isn't primitive numeric.</exception>
        public static MinValue Min(TypeCode typeCode)
        {
            return new MinValue(typeCode);
        }
    }
}
