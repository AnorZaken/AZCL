using System;
using System.Runtime.InteropServices;

namespace AZCL.Meta
{
    /// <summary>
    /// A struct that holds info about a primitive numeric type. (Struct size: 1 byte)
    /// </summary>
    [System.Runtime.InteropServices.StructLayout(LayoutKind.Sequential)]
    public struct NumericInfo
    {
        private readonly byte tc;

        /// <summary>
        /// Create a NumericInfo instance from a TypeCode.
        /// </summary>
        public NumericInfo(TypeCode tc)
        {
            this.tc = (byte)tc;
        }

        /// <summary>
        /// The System.TypeCode of the type.
        /// </summary>
        public TypeCode TypeCode
        {
            get { return (TypeCode)tc; }
        }

        /// <summary>
        /// True if the type is one of the primitive numeric types.
        /// </summary><remarks>
        /// The primitive numeric types are: sbyte, byte, short, ushort, int, uint, long, ulong, float, double, decimal.
        /// </remarks>
        public bool IsNumeric
        {
            get { return unchecked(tc - 5u) <= 10u; }
        }

        /// <summary>
        /// True if the type is an integer type. (NOT DECIMAL!)
        /// </summary><remarks>
        /// True if this NumericInfo instance corresponds to sbyte, byte, short, ushort, int, uint, long or ulong.
        /// </remarks>
        public bool IsInteger
        {
            get { return unchecked(tc - 5u) < 8u; }
        }

        /// <summary>
        /// True if the type is a signed integer type. (NOT DECIMAL!)
        /// </summary><remarks>
        /// True if this NumericInfo instance corresponds to sbyte, short, int or long.
        /// </remarks>
        public bool IsIntSigned
        {
            get { return (tc - 5 & 9) == 0; }
        }

        /// <summary>
        /// True if the type is an unsigned integer type.
        /// </summary><remarks>
        /// True if this NumericInfo instance corresponds to byte, ushort, uint or ulong.
        /// </remarks>
        public bool IsIntUnsigned
        {
            get { return (tc - 5 & 9) == 1; }
        }

        /// <summary>
        /// True if the type is float or double.
        /// </summary>
        public bool IsFloat
        {
            get { return (tc - 5 & -2) == 8; }
        }

        /// <summary>
        /// Size of the type in bytes, or -1 if the type is not one of the primitive numeric types!
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
        /// True if the type is bound by a 32-bit signed integer.
        /// </summary><remarks>
        /// This is true if the type is sbyte, byte, short, ushort or int.
        /// </remarks>
        public bool FitsInt32
        {
            get { return unchecked(tc - 5u) <= 4u; }
        }

        /// <summary>
        /// Get a <see cref="MaxValue"/> instance of the represented type.
        /// </summary>
        /// <exception cref="ArgumentException">Thrown if the represented type isn't primitive numeric.</exception>
        public MaxValue Max
        {
            get { return new MaxValue(this.TypeCode); }
        }

        /// <summary>
        /// Get a <see cref="MinValue"/> instance of the represented type.
        /// </summary>
        /// <exception cref="ArgumentException">Thrown if the represented type isn't primitive numeric.</exception>
        public MinValue Min
        {
            get { return new MinValue(this.TypeCode); }
        }
    }
}
