using System;
using System.Runtime.InteropServices;

namespace AZCL.Meta
{
    /// <summary>
    /// Represents meta information about a numeric simple type. (Struct size: 1 byte)
    /// </summary>
    [System.Runtime.InteropServices.StructLayout(LayoutKind.Sequential)]
    public struct NumericInfo
    {
        private readonly byte tc;

        /// <summary>
        /// Initializes a NumericInfo instance from a TypeCode.
        /// </summary>
        public NumericInfo(TypeCode tc)
        {
            this.tc = (byte)tc;
        }

        /// <summary>
        /// System.TypeCode of the represented type.
        /// </summary>
        public TypeCode TypeCode => (TypeCode)tc;

        /// <summary>
        /// Indicates whether the represented type is a numeric simple type.
        /// </summary><remarks>
        /// The numeric simple types are: sbyte, byte, short, ushort, int, uint, long, ulong, float, double, and decimal.
        /// </remarks>
        public bool IsNumeric
            => unchecked(tc - 5u) <= 10u;

        /// <summary>
        /// Indicates whether the represented type is an integral type.
        /// </summary><remarks>
        /// The integral types are: sbyte, byte, short, ushort, int, uint, long, and ulong.
        /// </remarks>
        public bool IsInteger
            => unchecked(tc - 5u) < 8u;

        /// <summary>
        /// Indicates whether the represented type is a signed integral type.
        /// </summary><remarks>
        /// The signed integral types are: sbyte, short, int, and long.
        /// </remarks>
        public bool IsIntSigned
            => (tc - 5 & 9) == 0;

        /// <summary>
        /// Indicates whether the represented type is an unsigned integral type.
        /// </summary><remarks>
        /// The unsigned integral types are: byte, ushort, uint, and ulong.
        /// </remarks>
        public bool IsIntUnsigned
            => (tc - 5 & 9) == 1;

        /// <summary>
        /// Indicates whether the represented type is a floating-point type.
        /// </summary><remarks>
        /// The floating-point types are: float and double.
        /// </remarks>
        public bool IsFloat
            => (tc - 5 & -2) == 8;

        /// <summary>
        /// Size of the represented type in bytes, or -1 if the represented type is not a numeric simple type.
        /// </summary>
        private int SizeOf
        {
            get
            {
                if (IsInteger)
                    return 1 << (tc - 5 >> 1);
                if (IsNumeric)
                    return 1 << tc - 11;

                return -1;
            }
        }

        /// <summary>
        /// Indicates whether the represented type is bound by a 32-bit signed integer.
        /// </summary><remarks>
        /// This is true if the type is sbyte, byte, short, ushort, or int.
        /// </remarks>
        public bool FitsInt32
            => unchecked(tc - 5u) <= 4u;

        /// <summary>
        /// Gets a <see cref="MaxValue"/> instance of the represented type.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// Thrown if the represented type isn't a numeric simple type.
        /// </exception>
        public MaxValue Max
            => new MaxValue(this.TypeCode);

        /// <summary>
        /// Gets a <see cref="MinValue"/> instance of the represented type.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// Thrown if the represented type isn't a numeric simple type.
        /// </exception>
        public MinValue Min
            => new MinValue(this.TypeCode);

        internal bool FitsValue(sbyte value) // TODO: doc and make public
        {
            if (!IsNumeric)
                throw new InvalidOperationException(Numeric.ERR_NOT_ISNUMERIC);

            return value > 0 || !IsIntUnsigned;
        }

        internal bool FitsValue(byte value) // TODO: doc and make public
        {
            if (!IsNumeric)
                throw new InvalidOperationException(Numeric.ERR_NOT_ISNUMERIC);

            return tc != (int)TypeCode.SByte | value < sbyte.MaxValue;
        }

        internal bool FitsValue(short value) // TODO: doc and make public
        {
            if (!IsNumeric)
                throw new InvalidOperationException(Numeric.ERR_NOT_ISNUMERIC);

            return Max.CompareTo(value) >= 0 && Min.CompareTo(value) <= 0;
        }

        internal bool FitsValue(ushort value) // TODO: doc and make public
        {
            if (!IsNumeric)
                throw new InvalidOperationException(Numeric.ERR_NOT_ISNUMERIC);

            return Max.CompareTo(value) >= 0 && Min.CompareTo(value) <= 0;
        }

        internal bool FitsValue(int value) // TODO: doc and make public
        {
            if (!IsNumeric)
                throw new InvalidOperationException(Numeric.ERR_NOT_ISNUMERIC);

            return Max.CompareTo(value) >= 0 && Min.CompareTo(value) <= 0;
        }

        internal bool FitsValue(uint value) // TODO: doc and make public
        {
            if (!IsNumeric)
                throw new InvalidOperationException(Numeric.ERR_NOT_ISNUMERIC);

            return Max.CompareTo(value) >= 0 && Min.CompareTo(value) <= 0;
        }

        internal bool FitsValue(long value) // TODO: doc and make public
        {
            if (!IsNumeric)
                throw new InvalidOperationException(Numeric.ERR_NOT_ISNUMERIC);

            return Max.CompareTo(value) >= 0 && Min.CompareTo(value) <= 0;
        }

        internal bool FitsValue(ulong value) // TODO: doc and make public
        {
            if (!IsNumeric)
                throw new InvalidOperationException(Numeric.ERR_NOT_ISNUMERIC);

            return Max.CompareTo(value) >= 0 && Min.CompareTo(value) <= 0;
        }

        internal bool FitsIntValue<TIntegral>(TIntegral value) // TODO: doc and make public?
        {
            if (!IsNumeric)
                throw new InvalidOperationException(Numeric.ERR_NOT_ISNUMERIC);
            
            var conv = value as IConvertible;
            if (conv == null)
                return false;

            var tcVal = conv.GetTypeCode();
            if (!Numeric.IsInteger(tcVal))
                return false;

            // (check if TypeCodes are identical first before proceeding to more expensive operations)
            if ((int)tcVal == tc)
                return true;
            
            if (Numeric.IsUnsigned(tcVal))
            {
                ulong v64 = conv.ToUInt64(null);
                return Max.CompareTo(v64) >= 0 && Min.CompareTo(v64) <= 0;
            }
            else
            {
                long v64 = conv.ToInt64(null);
                return Max.CompareTo(v64) >= 0 && Min.CompareTo(v64) <= 0;
            }
        }
    }
}
