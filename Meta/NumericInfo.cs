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
        public TypeCode TypeCode
        {
            get { return (TypeCode)tc; }
        }

        /// <summary>
        /// Indicates whether the represented type is a numeric simple type.
        /// </summary><remarks>
        /// The numeric simple types are: sbyte, byte, short, ushort, int, uint, long, ulong, float, double, and decimal.
        /// </remarks>
        public bool IsNumeric
        {
            get { return unchecked(tc - 5u) <= 10u; }
        }

        /// <summary>
        /// Indicates whether the represented type is an integral type.
        /// </summary><remarks>
        /// The integral types are: sbyte, byte, short, ushort, int, uint, long, and ulong.
        /// </remarks>
        public bool IsInteger
        {
            get { return unchecked(tc - 5u) < 8u; }
        }

        /// <summary>
        /// Indicates whether the represented type is a signed integral type.
        /// </summary><remarks>
        /// The signed integral types are: sbyte, short, int, and long.
        /// </remarks>
        public bool IsIntSigned
        {
            get { return (tc - 5 & 9) == 0; }
        }

        /// <summary>
        /// Indicates whether the represented type is an unsigned integral type.
        /// </summary><remarks>
        /// The unsigned integral types are: byte, ushort, uint, and ulong.
        /// </remarks>
        public bool IsIntUnsigned
        {
            get { return (tc - 5 & 9) == 1; }
        }

        /// <summary>
        /// Indicates whether the represented type is a floating-point type.
        /// </summary><remarks>
        /// The floating-point types are: float and double.
        /// </remarks>
        public bool IsFloat
        {
            get { return (tc - 5 & -2) == 8; }
        }

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
        {
            get { return unchecked(tc - 5u) <= 4u; }
        }

        /// <summary>
        /// Gets a <see cref="MaxValue"/> instance of the represented type.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// Thrown if the represented type isn't a numeric simple type.
        /// </exception>
        public MaxValue Max
        {
            get { return new MaxValue(this.TypeCode); }
        }

        /// <summary>
        /// Gets a <see cref="MinValue"/> instance of the represented type.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// Thrown if the represented type isn't a numeric simple type.
        /// </exception>
        public MinValue Min
        {
            get { return new MinValue(this.TypeCode); }
        }
    }
}
