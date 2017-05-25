using System;

namespace AZCL.Meta
{
    /// <summary>
    /// Provides info about numeric simple types.
    /// </summary><remarks>
    /// See <a href="https://docs.microsoft.com/en-us/dotnet/articles/csharp/language-reference/keywords/built-in-types-table">
    /// https://docs.microsoft.com/en-us/dotnet/articles/csharp/language-reference/keywords/built-in-types-table </a>
    /// to read more about simple types.
    /// </remarks>
    public static class Numeric
    {
        internal const string
            ERR_TYPECODE = "TypeCode does not correspond to a numeric simple type.";

        /// <summary>
        /// Returns whether the TypeCode corresponds to one of the numeric simple types.
        /// </summary><remarks>
        /// The numeric simple types are: sbyte, byte, short, ushort, int, uint, long, ulong, float, double, and decimal.
        /// </remarks>
        public static bool IsNumeric(TypeCode typeCode)
        {
            return unchecked((uint)typeCode - 5u) <= 10u;
        }

        /// <summary>
        /// Returns whether the Type corresponds to one of the numeric simple types.
        /// </summary><remarks>
        /// The numeric simple types are: sbyte, byte, short, ushort, int, uint, long, ulong, float, double, and decimal.
        /// </remarks>
        public static bool IsNumeric(Type type)
        {
            return IsNumeric(Type.GetTypeCode(type));
        }

        /// <summary>
        /// Returns whether the TypeCode corresponds to an integral type.
        /// </summary><remarks>
        /// The integral types are: sbyte, byte, short, ushort, int, uint, long, and ulong.
        /// </remarks>
        public static bool IsInteger(TypeCode typeCode)
        {
            return unchecked((uint)typeCode - 5u) < 8u;
        }

        /// <summary>
        /// Returns whether the TypeCode corresponds to an unsigned integral type.
        /// </summary><remarks>
        /// The unsigned integral types are: byte, ushort, uint, and ulong.
        /// </remarks>
        public static bool IsUnsigned(TypeCode typeCode)
        {
            return unchecked((int)typeCode - 5 & 9) == 1;
        }

        /// <summary>
        /// Returns whether the TypeCode corresponds to a floating-point type.
        /// </summary><remarks>
        /// The floating-point types are: float and double.
        /// </remarks>
        public static bool IsFloat(TypeCode typeCode)
        {
            return unchecked((int)typeCode - 5 & -2) == 8;
        }

        /// <summary>
        /// Returns a NumericInfo instance for the type <typeparamref name="T"/>.
        /// </summary>
        public static NumericInfo Info<T>() where T : struct, IConvertible
        {
            return Numeric<T>.info;
        }

        /// <summary>
        /// Returns a NumericInfo instance for the type <typeparamref name="T"/>.
        /// </summary>
        public static NumericInfo Info<T>(T instance) where T : struct, IConvertible // (type inference for caller)
        {
            return Numeric<T>.info;
        }

        /// <summary>
        /// Returns a NumericInfo instance for the specified type.
        /// </summary>
        public static NumericInfo Info(Type type)
        {
            return new NumericInfo(Type.GetTypeCode(type));
        }

        /// <summary>
        /// Returns a NumericInfo instance for an IConvertible type.
        /// </summary>
        public static NumericInfo Info(IConvertible instance)
        {
            if (instance == null)
                throw new ArgumentNullException(nameof(instance));
            return new NumericInfo(instance.GetTypeCode());
        }

        /// <summary>
        /// Returns a NumericInfo instance for the type with the specified TypeCode.
        /// </summary>
        public static NumericInfo Info(TypeCode tc)
        {
            return new NumericInfo(tc);
        }

        /// <summary>
        /// Returns a <see cref="MaxValue"/> instance corresponding to type <typeparamref name="T"/>.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// Thrown if the specified type doesn't correspond to a numeric simple type.
        /// </exception>
        public static MaxValue Max<T>(T value) where T : struct, IConvertible
        {
            return new MaxValue(value.GetTypeCode());
        }

        /// <summary>
        /// Returns a <see cref="MaxValue"/> instance corresponding to type <typeparamref name="T"/>.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// Thrown if the specified type doesn't correspond to a numeric simple type.
        /// </exception>
        public static MaxValue Max<T>() where T : struct, IConvertible
        {
            return new MaxValue(default(T).GetTypeCode());
        }

        /// <summary>
        /// Returns a <see cref="MaxValue"/> instance corresponding to the specified type.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="typeCode"/> doesn't correspond to a numeric simple type.
        /// </exception>
        public static MaxValue Max(TypeCode typeCode)
        {
            return new MaxValue(typeCode);
        }

        /// <summary>
        /// Returns a <see cref="MinValue"/> instance corresponding to type <typeparamref name="T"/>.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// Thrown if the specified type doesn't correspond to a numeric simple type.
        /// </exception>
        public static MinValue Min<T>() where T : struct, IConvertible
        {
            return new MinValue(default(T).GetTypeCode());
        }

        /// <summary>
        /// Returns a <see cref="MinValue"/> instance corresponding to type <typeparamref name="T"/>.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// Thrown if the specified type doesn't correspond to a numeric simple type.
        /// </exception>
        public static MinValue Min<T>(T value) where T : struct, IConvertible
        {
            return new MinValue(value.GetTypeCode());
        }

        /// <summary>
        /// Returns a <see cref="MinValue"/> instance corresponding to the specified type.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="typeCode"/> doesn't correspond to a numeric simple type.
        /// </exception>
        public static MinValue Min(TypeCode typeCode)
        {
            return new MinValue(typeCode);
        }
    }
}
