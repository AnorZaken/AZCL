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
            ERR_TYPECODE = "TypeCode does not correspond to a numeric simple type.",
            ERR_MAX_OVERFLOW = "This MaxValue can not be represented in that type.",
            ERR_MIN_OVERFLOW = "This MinValue can not be represented in that type.",
            ERR_NOT_ISNUMERIC = "This operation cannot be performed on a NumericInfo instance on which " + nameof(NumericInfo.IsNumeric) + " is false.";


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

        // tries to convert an integral value of type TIn to an integral value of type TOut ...
        // ... but only if the numerical value of the result would be identical to the numerical value of the input - including sign.
        internal static bool TryConvertInteger<TIn, TOut>(TIn input, out TOut result) // TODO: doc and make public?
            where TIn : IConvertible
            where TOut : IConvertible
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            result = default(TOut);

            if (result == null)
                return false;
            
            var niOut = new NumericInfo(result.GetTypeCode());
            
            if (niOut.IsInteger && niOut.FitsIntValue(input)) // (FitsIntValue: if 'input' (TIn) isn't an Integral type it simply returns false, but if TOut isn't numeric... it throws!)
            {
                try
                {
                    switch(niOut.TypeCode) // TODO: I dislike this switch and hate these double casts, but fixing it requires a lot of extra work, so maybe in the future :/
                    {
                        case TypeCode.SByte:
                            result = (TOut)(object)input.ToSByte(null);
                            return true;
                        case TypeCode.Byte:
                            result = (TOut)(object)input.ToByte(null);
                            return true;
                        case TypeCode.Int16:
                            result = (TOut)(object)input.ToInt16(null);
                            return true;
                        case TypeCode.UInt16:
                            result = (TOut)(object)input.ToUInt16(null);
                            return true;
                        case TypeCode.Int32:
                            result = (TOut)(object)input.ToInt32(null);
                            return true;
                        case TypeCode.UInt32:
                            result = (TOut)(object)input.ToUInt32(null);
                            return true;
                        case TypeCode.Int64:
                            result = (TOut)(object)input.ToInt64(null);
                            return true;
                        case TypeCode.UInt64:
                            result = (TOut)(object)input.ToUInt64(null);
                            return true;
                    }
                }
                catch
                {
#if DEBUG
                    System.Diagnostics.Debug.Assert(false, "Unexpected cast failure"); // this should never happen!
#endif
                }
            }

            return false;
        }
    }
}
